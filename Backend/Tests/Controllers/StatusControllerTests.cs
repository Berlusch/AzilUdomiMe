using AutoMapper;
using Backend.Controllers;
using Backend.Data;
using Backend.Models;
using Backend.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Backend.Tests.Controllers
{
    /// <summary>
    /// Unit testovi za <see cref="StatusController"/>.
    /// Testovi pokrivaju sve CRUD operacije kontrolera.
    /// Koristi se in-memory baza podataka kako bi testovi bili izolirani i brzi.
    /// </summary>
    public class StatusControllerTests : IDisposable
    {
        /// <summary>Kontekst in-memory baze podataka koji se koristi u testovima.</summary>
        private readonly BackendContext _context;

        /// <summary>Mock objekt za AutoMapper koji simulira mapiranje između modela i DTO-ova.</summary>
        private readonly Mock<IMapper> _mapperMock;

        /// <summary>Instanca kontrolera koja se testira.</summary>
        private readonly StatusController _controller;

        /// <summary>
        /// Inicijalizira in-memory bazu, mock mapper i kontroler prije svakog testa.
        /// Svaki test dobiva svježu, izoliranu instancu baze podataka.
        /// </summary>
        public StatusControllerTests()
        {
            var options = new DbContextOptionsBuilder<BackendContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new BackendContext(options);
            _mapperMock = new Mock<IMapper>();
            _controller = new StatusController(_context, _mapperMock.Object);
        }

        /// <summary>
        /// Čisti resurse nakon svakog testa kako bi se spriječilo curenje memorije
        /// i međusobno interferiranje testova.
        /// </summary>
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        #region Pomoćne metode

        /// <summary>
        /// Stvara i vraća primjer <see cref="Status"/> objekta s predefiniranim podacima.
        /// </summary>
        /// <param name="sifra">Šifra statusa (zadano 1).</param>
        /// <returns>Novi <see cref="Status"/> objekt.</returns>
        private static Status DajStatus(int sifra = 1) => new()
        {
            Sifra = sifra,
            Naziv = "Aktivan"
        };

        /// <summary>
        /// Stvara i vraća primjer <see cref="StatusDTORead"/> objekta.
        /// </summary>
        /// <param name="sifra">Šifra statusa (zadano 1).</param>
        /// <returns>Novi <see cref="StatusDTORead"/> objekt.</returns>
        private static StatusDTORead DajStatusDTORead(int sifra = 1) => new(sifra, "Aktivan");

        /// <summary>
        /// Stvara i vraća primjer <see cref="StatusDTOInsertUpdate"/> objekta s validnim podacima.
        /// </summary>
        /// <returns>Novi <see cref="StatusDTOInsertUpdate"/> objekt.</returns>
        private static StatusDTOInsertUpdate DajStatusDTOInsertUpdate() => new("Aktivan");

        #endregion

        #region GET - Dohvat svih statusa

        /// <summary>
        /// Testira da metoda <c>Get()</c> vraća <see cref="OkObjectResult"/> (HTTP 200)
        /// kada u bazi postoje statusi.
        /// </summary>
        [Fact]
        public void Get_KadaPostojeStatusi_VracaOk()
        {
            // Arrange
            _context.Statusi.Add(DajStatus());
            _context.SaveChanges();

            var listaDTO = new List<StatusDTORead> { DajStatusDTORead() };
            _mapperMock
                .Setup(m => m.Map<List<StatusDTORead>>(It.IsAny<object>()))
                .Returns(listaDTO);

            // Act
            var result = _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var povrat = Assert.IsType<List<StatusDTORead>>(okResult.Value);
            Assert.Single(povrat);
        }

        /// <summary>
        /// Testira da metoda <c>Get()</c> vraća praznu listu kada u bazi nema statusa.
        /// Rezultat treba biti <see cref="OkObjectResult"/> (HTTP 200) s praznom listom.
        /// </summary>
        [Fact]
        public void Get_KadaNemaStatusa_VracaPraznaListu()
        {
            // Arrange
            _mapperMock
                .Setup(m => m.Map<List<StatusDTORead>>(It.IsAny<object>()))
                .Returns(new List<StatusDTORead>());

            // Act
            var result = _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var povrat = Assert.IsType<List<StatusDTORead>>(okResult.Value);
            Assert.Empty(povrat);
        }

        #endregion

        #region GET by Šifra - Dohvat statusa prema šifri

        /// <summary>
        /// Testira da metoda <c>GetBySifra()</c> vraća <see cref="OkObjectResult"/> (HTTP 200)
        /// s ispravnim statusom kada status s traženom šifrom postoji u bazi.
        /// </summary>
        [Fact]
        public void GetBySifra_KadaStatusPostoji_VracaOk()
        {
            // Arrange
            _context.Statusi.Add(DajStatus());
            _context.SaveChanges();

            var dto = DajStatusDTORead();
            _mapperMock
                .Setup(m => m.Map<StatusDTORead>(It.IsAny<Status>()))
                .Returns(dto);

            // Act
            var result = _controller.GetBySifra(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var povrat = Assert.IsType<StatusDTORead>(okResult.Value);
            Assert.Equal(dto.Sifra, povrat.Sifra);
        }

        /// <summary>
        /// Testira da metoda <c>GetBySifra()</c> vraća <see cref="NotFoundObjectResult"/> (HTTP 404)
        /// kada status s traženom šifrom ne postoji u bazi.
        /// </summary>
        [Fact]
        public void GetBySifra_KadaStatusNePostoji_VracaNotFound()
        {
            // Act
            var result = _controller.GetBySifra(999);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        #endregion

        #region POST - Dodavanje statusa

        /// <summary>
        /// Testira da metoda <c>Post()</c> uspješno dodaje novi status,
        /// vraća HTTP 201 (Created) i da je status stvarno pohranjen u bazi.
        /// </summary>
        [Fact]
        public void Post_SValidnimPodacima_VracaCreated()
        {
            // Arrange
            var dto = DajStatusDTOInsertUpdate();
            var status = DajStatus();
            var dtoRead = DajStatusDTORead();

            _mapperMock
                .Setup(m => m.Map<Status>(dto))
                .Returns(status);
            _mapperMock
                .Setup(m => m.Map<StatusDTORead>(It.IsAny<Status>()))
                .Returns(dtoRead);

            // Act
            var result = _controller.Post(dto);

            // Assert
            var statusResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(201, statusResult.StatusCode);
            Assert.Equal(1, _context.Statusi.Count());
        }

        /// <summary>
        /// Testira da metoda <c>Post()</c> vraća <see cref="BadRequestObjectResult"/> (HTTP 400)
        /// kada ModelState nije validan (npr. nedostaje naziv).
        /// </summary>
        [Fact]
        public void Post_SNevažećimModelState_VracaBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("Naziv", "Naziv obavezan");
            var dto = DajStatusDTOInsertUpdate();

            // Act
            var result = _controller.Post(dto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        #endregion

        #region PUT - Ažuriranje statusa

        /// <summary>
        /// Testira da metoda <c>Put()</c> uspješno ažurira postojeći status
        /// i vraća <see cref="OkObjectResult"/> (HTTP 200) s porukom uspjeha.
        /// </summary>
        [Fact]
        public void Put_KadaStatusPostoji_VracaOk()
        {
            // Arrange
            var status = DajStatus();
            _context.Statusi.Add(status);
            _context.SaveChanges();

            var dto = DajStatusDTOInsertUpdate();
            _mapperMock
                .Setup(m => m.Map(dto, It.IsAny<Status>()))
                .Returns(status);

            // Act
            var result = _controller.Put(1, dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        /// <summary>
        /// Testira da metoda <c>Put()</c> vraća <see cref="NotFoundObjectResult"/> (HTTP 404)
        /// kada status s navedenom šifrom ne postoji u bazi.
        /// </summary>
        [Fact]
        public void Put_KadaStatusNePostoji_VracaNotFound()
        {
            // Arrange
            var dto = DajStatusDTOInsertUpdate();

            // Act
            var result = _controller.Put(999, dto);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        /// <summary>
        /// Testira da metoda <c>Put()</c> vraća <see cref="BadRequestObjectResult"/> (HTTP 400)
        /// kada ModelState nije validan.
        /// </summary>
        [Fact]
        public void Put_SNevažećimModelState_VracaBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("Naziv", "Naziv obavezan");
            var dto = DajStatusDTOInsertUpdate();

            // Act
            var result = _controller.Put(1, dto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        #endregion

        #region DELETE - Brisanje statusa

        /// <summary>
        /// Testira da metoda <c>Delete()</c> uspješno briše postojeći status,
        /// vraća <see cref="OkObjectResult"/> (HTTP 200) i da status više ne postoji u bazi.
        /// </summary>
        [Fact]
        public void Delete_KadaStatusPostoji_VracaOkIBrise()
        {
            // Arrange
            _context.Statusi.Add(DajStatus());
            _context.SaveChanges();

            // Act
            var result = _controller.Delete(1);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(0, _context.Statusi.Count());
        }

        /// <summary>
        /// Testira da metoda <c>Delete()</c> vraća <see cref="NotFoundObjectResult"/> (HTTP 404)
        /// kada status s navedenom šifrom ne postoji u bazi.
        /// </summary>
        [Fact]
        public void Delete_KadaStatusNePostoji_VracaNotFound()
        {
            // Act
            var result = _controller.Delete(999);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        /// <summary>
        /// Testira da metoda <c>Delete()</c> vraća <see cref="BadRequestObjectResult"/> (HTTP 400)
        /// kada ModelState nije validan.
        /// </summary>
        [Fact]
        public void Delete_SNevažećimModelState_VracaBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("sifra", "Šifra je obavezna");

            // Act
            var result = _controller.Delete(1);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        #endregion
    }
}
