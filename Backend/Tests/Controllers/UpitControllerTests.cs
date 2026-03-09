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
    /// Unit testovi za <see cref="UpitController"/>.
    /// Testovi pokrivaju sve CRUD operacije kontrolera.
    /// Budući da <c>Post</c> i <c>Put</c> provjeravaju postojanje <see cref="Pas"/> i
    /// <see cref="Udomitelj"/> entiteta, testovi uključuju i scenarije kada ti entiteti
    /// ne postoje u bazi.
    /// Koristi se in-memory baza podataka kako bi testovi bili izolirani i brzi.
    /// </summary>
    public class UpitControllerTests : IDisposable
    {
        /// <summary>Kontekst in-memory baze podataka koji se koristi u testovima.</summary>
        private readonly BackendContext _context;

        /// <summary>Mock objekt za AutoMapper koji simulira mapiranje između modela i DTO-ova.</summary>
        private readonly Mock<IMapper> _mapperMock;

        /// <summary>Instanca kontrolera koja se testira.</summary>
        private readonly UpitController _controller;

        /// <summary>
        /// Inicijalizira in-memory bazu, mock mapper i kontroler prije svakog testa.
        /// Svaki test dobiva svježu, izoliranu instancu baze podataka.
        /// </summary>
        public UpitControllerTests()
        {
            var options = new DbContextOptionsBuilder<BackendContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new BackendContext(options);
            _mapperMock = new Mock<IMapper>();
            _controller = new UpitController(_context, _mapperMock.Object);
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
        /// Dodaje međusobno povezane entitete (<see cref="Status"/>, <see cref="Pas"/>,
        /// <see cref="Udomitelj"/> i <see cref="Upit"/>) u bazu koristeći <b>iste instance</b>
        /// objekata kako bi se izbjegao konflikt EF Core trackera pri dodavanju
        /// više entiteta koji dijele isti strani ključ.
        /// </summary>
        /// <returns>Tuple s dodanim entitetima: (status, pas, udomitelj, upit).</returns>
        private (Status status, Pas pas, Udomitelj udomitelj, Upit upit) DodajUBazu()
        {
            var status = new Status { Sifra = 1, Naziv = "Aktivan" };
            _context.Statusi.Add(status);

            var pas = new Pas
            {
                Sifra = 1,
                Ime = "Rex",
                BrojCipa = "123456789",
                Datum_Rodjenja = new DateTime(2020, 1, 1),
                Spol = "M",
                Opis = "Ljubazan pas",
                Kastracija = false,
                Status = status
            };
            _context.Psi.Add(pas);

            var udomitelj = new Udomitelj
            {
                Sifra = 1,
                Ime = "Marko",
                Prezime = "Horvat",
                Adresa = "Ilica 1, Zagreb",
                Telefon = "+38592234567",
                Email = "marko.horvat@example.com"
            };
            _context.Udomitelji.Add(udomitelj);

            var upit = new Upit
            {
                Sifra = 1,
                Pas = pas,
                Udomitelj = udomitelj,
                DatumUpita = new DateTime(2024, 6, 1),
                StatusUpita = "Na čekanju",
                SadrzajUpita = "Zanima me udomljavanje psa."
            };
            _context.Upiti.Add(upit);
            _context.SaveChanges();

            return (status, pas, udomitelj, upit);
        }

        /// <summary>
        /// Stvara i vraća primjer <see cref="UpitDTORead"/> objekta s predefiniranim podacima.
        /// </summary>
        /// <param name="sifra">Šifra upita (zadano 1).</param>
        private static UpitDTORead DajUpitDTORead(int sifra = 1) => new(
            sifra,
            "Rex",
            "Marko Horvat",
            new DateTime(2024, 6, 1),
            "Na čekanju",
            "Zanima me udomljavanje psa."
        );

        /// <summary>
        /// Stvara i vraća primjer <see cref="UpitDTOInsertUpdate"/> objekta s validnim podacima.
        /// </summary>
        private static UpitDTOInsertUpdate DajUpitDTOInsertUpdate() => new(
            PasSifra: 1,
            UdomiteljSifra: 1,
            DatumUpita: new DateTime(2024, 6, 1),
            StatusUpita: "Na čekanju",
            SadrzajUpita: "Zanima me udomljavanje psa."
        );

        #endregion

        #region GET - Dohvat svih upita

        /// <summary>
        /// Testira da metoda <c>Get()</c> vraća <see cref="OkObjectResult"/> (HTTP 200)
        /// kada u bazi postoje upiti.
        /// </summary>
        [Fact]
        public void Get_KadaPostojeUpiti_VracaOk()
        {
            // Arrange
            DodajUBazu();

            var listaDTO = new List<UpitDTORead> { DajUpitDTORead() };
            _mapperMock
                .Setup(m => m.Map<List<UpitDTORead>>(It.IsAny<object>()))
                .Returns(listaDTO);

            // Act
            var result = _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var povrat = Assert.IsType<List<UpitDTORead>>(okResult.Value);
            Assert.Single(povrat);
        }

        /// <summary>
        /// Testira da metoda <c>Get()</c> vraća praznu listu kada u bazi nema upita.
        /// Rezultat treba biti <see cref="OkObjectResult"/> (HTTP 200) s praznom listom.
        /// </summary>
        [Fact]
        public void Get_KadaNemaUpita_VracaPraznaListu()
        {
            // Arrange
            _mapperMock
                .Setup(m => m.Map<List<UpitDTORead>>(It.IsAny<object>()))
                .Returns(new List<UpitDTORead>());

            // Act
            var result = _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var povrat = Assert.IsType<List<UpitDTORead>>(okResult.Value);
            Assert.Empty(povrat);
        }

        #endregion

        #region GET by Šifra - Dohvat upita prema šifri

        /// <summary>
        /// Testira da metoda <c>GetBySifra()</c> vraća <see cref="OkObjectResult"/> (HTTP 200)
        /// s ispravnim upitom kada upit s traženom šifrom postoji u bazi.
        /// </summary>
        [Fact]
        public void GetBySifra_KadaUpitPostoji_VracaOk()
        {
            // Arrange
            DodajUBazu();

            var dto = DajUpitDTOInsertUpdate();
            _mapperMock
                .Setup(m => m.Map<UpitDTOInsertUpdate>(It.IsAny<Upit>()))
                .Returns(dto);

            // Act
            var result = _controller.GetBySifra(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<UpitDTOInsertUpdate>(okResult.Value);
        }

        /// <summary>
        /// Testira da metoda <c>GetBySifra()</c> vraća <see cref="NotFoundObjectResult"/> (HTTP 404)
        /// kada upit s traženom šifrom ne postoji u bazi.
        /// </summary>
        [Fact]
        public void GetBySifra_KadaUpitNePostoji_VracaNotFound()
        {
            // Act
            var result = _controller.GetBySifra(999);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        #endregion

        #region POST - Dodavanje upita

        /// <summary>
        /// Testira da metoda <c>Post()</c> uspješno dodaje novi upit
        /// i vraća HTTP 201 (Created).
        /// </summary>
        [Fact]
        public void Post_SValidnimPodacima_VracaCreated()
        {
            // Arrange
            var (_, pas, udomitelj, _) = DodajUBazu();
            _context.Upiti.RemoveRange(_context.Upiti);
            _context.SaveChanges();

            var dto = DajUpitDTOInsertUpdate();
            var upit = new Upit
            {
                Sifra = 2,
                Pas = pas,
                Udomitelj = udomitelj,
                DatumUpita = new DateTime(2024, 6, 1),
                StatusUpita = "Na čekanju",
                SadrzajUpita = "Zanima me udomljavanje psa."
            };
            var dtoRead = DajUpitDTORead(2);

            _mapperMock
                .Setup(m => m.Map<Upit>(dto))
                .Returns(upit);
            _mapperMock
                .Setup(m => m.Map<UpitDTORead>(It.IsAny<Upit>()))
                .Returns(dtoRead);

            // Act
            var result = _controller.Post(dto);

            // Assert
            var statusResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(201, statusResult.StatusCode);
        }

        /// <summary>
        /// Testira da metoda <c>Post()</c> vraća <see cref="NotFoundObjectResult"/> (HTTP 404)
        /// kada pas s navedenom šifrom ne postoji u bazi.
        /// </summary>
        [Fact]
        public void Post_KadaPasNePostoji_VracaNotFound()
        {
            // Arrange — udomitelj postoji, ali pas ne postoji
            _context.Udomitelji.Add(new Udomitelj
            {
                Sifra = 1,
                Ime = "Marko",
                Prezime = "Horvat",
                Adresa = "Ilica 1, Zagreb",
                Telefon = "+38592234567",
                Email = "marko.horvat@example.com"
            });
            _context.SaveChanges();

            // Act
            var result = _controller.Post(DajUpitDTOInsertUpdate());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        /// <summary>
        /// Testira da metoda <c>Post()</c> vraća <see cref="NotFoundObjectResult"/> (HTTP 404)
        /// kada udomitelj s navedenom šifrom ne postoji u bazi.
        /// </summary>
        [Fact]
        public void Post_KadaUdomiteljNePostoji_VracaNotFound()
        {
            // Arrange — pas postoji, ali udomitelj ne postoji
            var status = new Status { Sifra = 1, Naziv = "Aktivan" };
            _context.Statusi.Add(status);
            _context.Psi.Add(new Pas
            {
                Sifra = 1,
                Ime = "Rex",
                BrojCipa = "123456789",
                Datum_Rodjenja = new DateTime(2020, 1, 1),
                Spol = "M",
                Opis = "Ljubazan pas",
                Kastracija = false,
                Status = status
            });
            _context.SaveChanges();

            // Act
            var result = _controller.Post(DajUpitDTOInsertUpdate());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        /// <summary>
        /// Testira da metoda <c>Post()</c> vraća <see cref="BadRequestObjectResult"/> (HTTP 400)
        /// kada ModelState nije validan.
        /// </summary>
        [Fact]
        public void Post_SNevažećimModelState_VracaBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("PasSifra", "Šifra psa obavezna");

            // Act
            var result = _controller.Post(DajUpitDTOInsertUpdate());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        #endregion

        #region PUT - Ažuriranje upita

        /// <summary>
        /// Testira da metoda <c>Put()</c> uspješno ažurira postojeći upit
        /// i vraća <see cref="OkObjectResult"/> (HTTP 200) s porukom uspjeha.
        /// </summary>
        [Fact]
        public void Put_SValidnimPodacima_VracaOk()
        {
            // Arrange
            var (_, _, _, upit) = DodajUBazu();

            var dto = DajUpitDTOInsertUpdate();
            _mapperMock
                .Setup(m => m.Map(dto, It.IsAny<Upit>()))
                .Returns(upit);

            // Act
            var result = _controller.Put(1, dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        /// <summary>
        /// Testira da metoda <c>Put()</c> vraća <see cref="NotFoundObjectResult"/> (HTTP 404)
        /// kada upit s navedenom šifrom ne postoji u bazi.
        /// </summary>
        [Fact]
        public void Put_KadaUpitNePostoji_VracaNotFound()
        {
            // Act
            var result = _controller.Put(999, DajUpitDTOInsertUpdate());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        /// <summary>
        /// Testira da metoda <c>Put()</c> vraća <see cref="NotFoundObjectResult"/> (HTTP 404)
        /// kada pas s navedenom šifrom (PasSifra: 99) ne postoji u bazi.
        /// </summary>
        [Fact]
        public void Put_KadaPasNePostoji_VracaNotFound()
        {
            // Arrange
            var (_, _, _, upit) = DodajUBazu();

            var dto = new UpitDTOInsertUpdate(
                PasSifra: 99,
                UdomiteljSifra: 1,
                DatumUpita: new DateTime(2024, 6, 1),
                StatusUpita: "Na čekanju",
                SadrzajUpita: "Zanima me udomljavanje psa."
            );
            _mapperMock
                .Setup(m => m.Map(dto, It.IsAny<Upit>()))
                .Returns(upit);

            // Act
            var result = _controller.Put(1, dto);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        /// <summary>
        /// Testira da metoda <c>Put()</c> vraća <see cref="NotFoundObjectResult"/> (HTTP 404)
        /// kada udomitelj s navedenom šifrom (UdomiteljSifra: 99) ne postoji u bazi.
        /// </summary>
        [Fact]
        public void Put_KadaUdomiteljNePostoji_VracaNotFound()
        {
            // Arrange
            var (_, _, _, upit) = DodajUBazu();

            var dto = new UpitDTOInsertUpdate(
                PasSifra: 1,
                UdomiteljSifra: 99,
                DatumUpita: new DateTime(2024, 6, 1),
                StatusUpita: "Na čekanju",
                SadrzajUpita: "Zanima me udomljavanje psa."
            );
            _mapperMock
                .Setup(m => m.Map(dto, It.IsAny<Upit>()))
                .Returns(upit);

            // Act
            var result = _controller.Put(1, dto);

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
            _controller.ModelState.AddModelError("PasSifra", "Šifra psa obavezna");

            // Act
            var result = _controller.Put(1, DajUpitDTOInsertUpdate());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        #endregion

        #region DELETE - Brisanje upita

        /// <summary>
        /// Testira da metoda <c>Delete()</c> uspješno briše postojeći upit,
        /// vraća <see cref="OkObjectResult"/> (HTTP 200) i da upit više ne postoji u bazi.
        /// </summary>
        [Fact]
        public void Delete_KadaUpitPostoji_VracaOkIBrise()
        {
            // Arrange
            DodajUBazu();

            // Act
            var result = _controller.Delete(1);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(0, _context.Upiti.Count());
        }

        /// <summary>
        /// Testira da metoda <c>Delete()</c> vraća <see cref="NotFoundObjectResult"/> (HTTP 404)
        /// kada upit s navedenom šifrom ne postoji u bazi.
        /// </summary>
        [Fact]
        public void Delete_KadaUpitNePostoji_VracaNotFound()
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