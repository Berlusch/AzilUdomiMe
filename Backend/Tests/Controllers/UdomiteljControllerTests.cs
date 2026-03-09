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
    /// Unit testovi za <see cref="UdomiteljController"/>.
    /// Testovi pokrivaju sve CRUD operacije i pomoćne metode kontrolera.
    /// Koristi se in-memory baza podataka kako bi testovi bili izolirani i brzi.
    /// </summary>
    public class UdomiteljControllerTests : IDisposable
    {
        /// <summary>Kontekst in-memory baze podataka koji se koristi u testovima.</summary>
        private readonly BackendContext _context;

        /// <summary>Mock objekt za AutoMapper koji simulira mapiranje između modela i DTO-ova.</summary>
        private readonly Mock<IMapper> _mapperMock;

        /// <summary>Instanca kontrolera koja se testira.</summary>
        private readonly UdomiteljController _controller;

        /// <summary>
        /// Inicijalizira in-memory bazu, mock mapper i kontroler prije svakog testa.
        /// Svaki test dobiva svježu, izoliranu instancu baze podataka.
        /// </summary>
        public UdomiteljControllerTests()
        {
            var options = new DbContextOptionsBuilder<BackendContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new BackendContext(options);
            _mapperMock = new Mock<IMapper>();
            _controller = new UdomiteljController(_context, _mapperMock.Object);
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
        /// Stvara i vraća primjer <see cref="Udomitelj"/> objekta s predefiniranim podacima.
        /// Koristi se kao temelj za postavljanje stanja baze u testovima.
        /// </summary>
        /// <param name="sifra">Šifra udomitelja (zadano 1).</param>
        /// <returns>Novi <see cref="Udomitelj"/> objekt.</returns>
        private static Udomitelj DajUdomitelja(int sifra = 1) => new()
        {
            Sifra = sifra,
            Ime = "Marko",
            Prezime = "Horvat",
            Adresa = "Ilica 1, Zagreb",
            Telefon = "+38592234567",
            Email = "marko.horvat@example.com"
        };

        /// <summary>
        /// Stvara i vraća primjer <see cref="UdomiteljDTORead"/> objekta koji odgovara
        /// udomitelju stvorenom metodom <see cref="DajUdomitelja"/>.
        /// </summary>
        /// <param name="sifra">Šifra udomitelja (zadano 1).</param>
        /// <returns>Novi <see cref="UdomiteljDTORead"/> objekt.</returns>
        private static UdomiteljDTORead DajUdomiteljDTORead(int sifra = 1) => new(
            sifra,
            "Marko",
            "Horvat",
            "Ilica 1, Zagreb",
            "+38592234567",
            "marko.horvat@example.com"
        );

        /// <summary>
        /// Stvara i vraća primjer <see cref="UdomiteljDTOInsertUpdate"/> objekta
        /// s validnim podacima za unos ili ažuriranje.
        /// </summary>
        /// <returns>Novi <see cref="UdomiteljDTOInsertUpdate"/> objekt.</returns>
        private static UdomiteljDTOInsertUpdate DajUdomiteljDTOInsertUpdate() => new(
            "Marko",
            "Horvat",
            "Ilica 1, Zagreb",
            "+38592234567",
            "marko.horvat@example.com"
        );

        #endregion

        #region GET - Dohvat svih udomitelja

        /// <summary>
        /// Testira da metoda <c>Get()</c> vraća <see cref="OkObjectResult"/> (HTTP 200)
        /// kada u bazi postoje udomitelji.
        /// </summary>
        [Fact]
        public void Get_KadaPostojeUdomitelji_VracaOk()
        {
            // Arrange
            var udomitelj = DajUdomitelja();
            _context.Udomitelji.Add(udomitelj);
            _context.SaveChanges();

            var listaDTO = new List<UdomiteljDTORead> { DajUdomiteljDTORead() };
            _mapperMock
                .Setup(m => m.Map<List<UdomiteljDTORead>>(It.IsAny<object>()))
                .Returns(listaDTO);

            // Act
            var result = _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var povrat = Assert.IsType<List<UdomiteljDTORead>>(okResult.Value);
            Assert.Single(povrat);
        }

        /// <summary>
        /// Testira da metoda <c>Get()</c> vraća praznu listu kada u bazi nema udomitelja.
        /// Rezultat treba biti <see cref="OkObjectResult"/> (HTTP 200) s praznom listom.
        /// </summary>
        [Fact]
        public void Get_KadaNemaUdomitelja_VracaPraznaListu()
        {
            // Arrange
            _mapperMock
                .Setup(m => m.Map<List<UdomiteljDTORead>>(It.IsAny<object>()))
                .Returns(new List<UdomiteljDTORead>());

            // Act
            var result = _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var povrat = Assert.IsType<List<UdomiteljDTORead>>(okResult.Value);
            Assert.Empty(povrat);
        }

        #endregion

        #region GET by Šifra - Dohvat udomitelja prema šifri

        /// <summary>
        /// Testira da metoda <c>GetBySifra()</c> vraća <see cref="OkObjectResult"/> (HTTP 200)
        /// s ispravnim udomiteljem kada udomitelj s traženom šifrom postoji u bazi.
        /// </summary>
        [Fact]
        public void GetBySifra_KadaUdomiteljPostoji_VracaOk()
        {
            // Arrange
            var udomitelj = DajUdomitelja();
            _context.Udomitelji.Add(udomitelj);
            _context.SaveChanges();

            var dto = DajUdomiteljDTORead();
            _mapperMock
                .Setup(m => m.Map<UdomiteljDTORead>(It.IsAny<Udomitelj>()))
                .Returns(dto);

            // Act
            var result = _controller.GetBySifra(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var povrat = Assert.IsType<UdomiteljDTORead>(okResult.Value);
            Assert.Equal(dto.Sifra, povrat.Sifra);
        }

        /// <summary>
        /// Testira da metoda <c>GetBySifra()</c> vraća <see cref="NotFoundObjectResult"/> (HTTP 404)
        /// kada udomitelj s traženom šifrom ne postoji u bazi.
        /// </summary>
        [Fact]
        public void GetBySifra_KadaUdomiteljNePostoji_VracaNotFound()
        {
            // Act
            var result = _controller.GetBySifra(999);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        #endregion

        #region POST - Dodavanje udomitelja

        /// <summary>
        /// Testira da metoda <c>Post()</c> uspješno dodaje novog udomitelja,
        /// vraća HTTP 201 (Created) i da je udomitelj stvarno pohranjen u bazi.
        /// </summary>
        [Fact]
        public void Post_SValidnimPodacima_VracaCreated()
        {
            // Arrange
            var dto = DajUdomiteljDTOInsertUpdate();
            var udomitelj = DajUdomitelja();
            var dtoRead = DajUdomiteljDTORead();

            _mapperMock
                .Setup(m => m.Map<Udomitelj>(dto))
                .Returns(udomitelj);
            _mapperMock
                .Setup(m => m.Map<UdomiteljDTORead>(It.IsAny<Udomitelj>()))
                .Returns(dtoRead);

            // Act
            var result = _controller.Post(dto);

            // Assert
            var statusResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(201, statusResult.StatusCode);
            Assert.Equal(1, _context.Udomitelji.Count());
        }

        /// <summary>
        /// Testira da metoda <c>Post()</c> vraća <see cref="BadRequestObjectResult"/> (HTTP 400)
        /// kada ModelState nije validan (npr. nedostaju obavezna polja).
        /// </summary>
        [Fact]
        public void Post_SNevažećimModelState_VracaBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("Ime", "Ime obavezno");
            var dto = DajUdomiteljDTOInsertUpdate();

            // Act
            var result = _controller.Post(dto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        #endregion

        #region PUT - Ažuriranje udomitelja

        /// <summary>
        /// Testira da metoda <c>Put()</c> uspješno ažurira postojećeg udomitelja
        /// i vraća <see cref="OkObjectResult"/> (HTTP 200) s porukom uspjeha.
        /// </summary>
        [Fact]
        public void Put_KadaUdomiteljPostoji_VracaOk()
        {
            // Arrange
            var udomitelj = DajUdomitelja();
            _context.Udomitelji.Add(udomitelj);
            _context.SaveChanges();

            var dto = DajUdomiteljDTOInsertUpdate();

            _mapperMock
                .Setup(m => m.Map(dto, It.IsAny<Udomitelj>()))
                .Returns(udomitelj);

            // Act
            var result = _controller.Put(1, dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        /// <summary>
        /// Testira da metoda <c>Put()</c> vraća <see cref="NotFoundObjectResult"/> (HTTP 404)
        /// kada udomitelj s navedenom šifrom ne postoji u bazi.
        /// </summary>
        [Fact]
        public void Put_KadaUdomiteljNePostoji_VracaNotFound()
        {
            // Arrange
            var dto = DajUdomiteljDTOInsertUpdate();

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
            _controller.ModelState.AddModelError("Email", "Email nije ispravan");
            var dto = DajUdomiteljDTOInsertUpdate();

            // Act
            var result = _controller.Put(1, dto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        #endregion

        #region DELETE - Brisanje udomitelja

        /// <summary>
        /// Testira da metoda <c>Delete()</c> uspješno briše postojećeg udomitelja,
        /// vraća <see cref="OkObjectResult"/> (HTTP 200) i da udomitelj više ne postoji u bazi.
        /// </summary>
        [Fact]
        public void Delete_KadaUdomiteljPostoji_VracaOkIBrise()
        {
            // Arrange
            var udomitelj = DajUdomitelja();
            _context.Udomitelji.Add(udomitelj);
            _context.SaveChanges();

            // Act
            var result = _controller.Delete(1);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(0, _context.Udomitelji.Count());
        }

        /// <summary>
        /// Testira da metoda <c>Delete()</c> vraća <see cref="NotFoundObjectResult"/> (HTTP 404)
        /// kada udomitelj s navedenom šifrom ne postoji u bazi.
        /// </summary>
        [Fact]
        public void Delete_KadaUdomiteljNePostoji_VracaNotFound()
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

        #region TraziUdomitelja - Pretraga prema uvjetu

        /// <summary>
        /// Testira da metoda <c>TraziUdomitelja()</c> vraća <see cref="OkObjectResult"/> (HTTP 200)
        /// s listom udomitelja kada uvjet pretrage pronađe rezultate.
        /// </summary>
        [Fact]
        public void TraziUdomitelja_SValidnimUvjetom_VracaOk()
        {
            // Arrange
            var udomitelj = DajUdomitelja();
            _context.Udomitelji.Add(udomitelj);
            _context.SaveChanges();

            var listaDTO = new List<UdomiteljDTORead> { DajUdomiteljDTORead() };
            _mapperMock
                .Setup(m => m.Map<List<UdomiteljDTORead>>(It.IsAny<object>()))
                .Returns(listaDTO);

            // Act
            var result = _controller.TraziUdomitelja("Marko");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var povrat = Assert.IsType<List<UdomiteljDTORead>>(okResult.Value);
            Assert.NotEmpty(povrat);
        }

        /// <summary>
        /// Testira da metoda <c>TraziUdomitelja()</c> vraća <see cref="BadRequestObjectResult"/> (HTTP 400)
        /// kada je uvjet pretrage kraći od 3 znaka, što je minimalni zahtjev.
        /// </summary>
        [Fact]
        public void TraziUdomitelja_SKratkimUvjetom_VracaBadRequest()
        {
            // Act
            var result = _controller.TraziUdomitelja("Ma");

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        /// <summary>
        /// Testira da metoda <c>TraziUdomitelja()</c> vraća <see cref="BadRequestObjectResult"/> (HTTP 400)
        /// kada je uvjet pretrage <c>null</c>.
        /// </summary>
        [Fact]
        public void TraziUdomitelja_SNullUvjetom_VracaBadRequest()
        {
            // Act
            var result = _controller.TraziUdomitelja(null!);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        /// <summary>
        /// Testira da metoda <c>TraziUdomitelja()</c> vraća praznu listu
        /// kada uvjet pretrage ne odgovara niti jednom udomitelju u bazi.
        /// </summary>
        [Fact]
        public void TraziUdomitelja_KadaNemaPodataka_VracaPraznaListu()
        {
            // Arrange
            _mapperMock
                .Setup(m => m.Map<List<UdomiteljDTORead>>(It.IsAny<object>()))
                .Returns(new List<UdomiteljDTORead>());

            // Act
            var result = _controller.TraziUdomitelja("Nepostojece");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var povrat = Assert.IsType<List<UdomiteljDTORead>>(okResult.Value);
            Assert.Empty(povrat);
        }

        #endregion
    }
}