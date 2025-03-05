using Backend.Controllers;
using Backend.Data;
using Backend.Models;
using Backend.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Text;
using Xunit;

namespace Backend.Tests.Controllers
{
    /// <summary>
    /// Testovi za AutorizacijaController.
    /// Ova klasa sadrži testove koji provjeravaju ispravnost generiranja JWT tokena za autorizaciju.
    /// </summary>
    public class AutorizacijaControllerTests
    {
        private readonly DbContextOptions<BackendContext> _options;
        private readonly BackendContext _context;
        private readonly AutorizacijaController _controller;

        /// <summary>
        /// Inicijalizira novu instancu klase AutorizacijaControllerTests.
        /// Konfigurira kontekst baze vrsta podataka i inicijalizira kontroler.
        /// </summary>
        public AutorizacijaControllerTests()
        {
            _options = new DbContextOptionsBuilder<BackendContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_" + Guid.NewGuid().ToString())
                .Options;

            _context = new BackendContext(_options);
            _controller = new AutorizacijaController(_context);
        }

        /// <summary>
        /// Testira generiranje tokena za valjane vjerodajnice.
        /// Očekuje se povrat OkObjectResult s ne-praznim tokenom.
        /// </summary>
        [Fact]
        public void GenerirajToken_ValidCredentials_ReturnsOkWithToken()
        {
            // Arrange
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword("testPassword");
            var operater = new Operater
            {
                Sifra = 1,
                Email = "test@example.com",
                Lozinka = hashedPassword
            };

            _context.Operateri.Add(operater);
            _context.SaveChanges();

            var operaterDTO = new OperaterDTO("test@example.com", "testPassword");

            // Act
            var result = _controller.GenerirajToken(operaterDTO);

            // Assert
            var okResult = Xunit.Assert.IsType<OkObjectResult>(result);
            var token = Xunit.Assert.IsType<string>(okResult.Value);
            Xunit.Assert.NotEmpty(token);
        }

        /// <summary>
        /// Testira slučaj kada uneseni email nije pronađen u bazi.
        /// Očekuje se povrat ObjectResult sa statusom Forbidden i odgovarajućom porukom.
        /// </summary>
        [Fact]
        public void GenerirajToken_InvalidEmail_ReturnsForbidden()
        {
            // Arrange
            var operaterDTO = new OperaterDTO("nonexistent@example.com", "testPassword");

            // Act
            var result = _controller.GenerirajToken(operaterDTO);

            // Assert
            var statusCodeResult = Xunit.Assert.IsType<ObjectResult>(result);
            Xunit.Assert.Equal(StatusCodes.Status403Forbidden, statusCodeResult.StatusCode);
            Xunit.Assert.Equal("Niste autorizirani, operater ne postoji u bazi.", statusCodeResult.Value);
        }

        /// <summary>
        /// Testira slučaj kada unesena lozinka ne odgovara pohranjenoj u bazi.
        /// Očekuje se povrat ObjectResult sa statusom Forbidden i odgovarajućom porukom.
        /// </summary>
        [Fact]
        public void GenerirajToken_InvalidPassword_ReturnsForbidden()
        {
            // Arrange
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword("correctPassword");
            var operater = new Operater
            {
                Sifra = 1,
                Email = "test@example.com",
                Lozinka = hashedPassword
            };

            _context.Operateri.Add(operater);
            _context.SaveChanges();

            var operaterDTO = new OperaterDTO("test@example.com", "wrongPassword");

            // Act
            var result = _controller.GenerirajToken(operaterDTO);

            // Assert
            var statusCodeResult = Xunit.Assert.IsType<ObjectResult>(result);
            Xunit.Assert.Equal(StatusCodes.Status403Forbidden, statusCodeResult.StatusCode);
            Xunit.Assert.Equal("Niste autorizirani, lozinka nije valjana.", statusCodeResult.Value);
        }

        /// <summary>
        /// Testira slučaj kada je model operatera neispravan (npr. nedostaje email).
        /// Očekuje se povrat BadRequestObjectResult.
        /// </summary>
        [Fact]
        public void GenerirajToken_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("Email", "Email je obavezan");
            var operaterDTO = new OperaterDTO("", "password");

            // Act
            var result = _controller.GenerirajToken(operaterDTO);

            // Assert
            Xunit.Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
