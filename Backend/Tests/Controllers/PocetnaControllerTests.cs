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
    /// Unit testovi za <see cref="PocetnaController"/>.
    /// Testovi pokrivaju dohvat psa po šifri, paginaciju, računanje stranica,
    /// unos upita putem obrasca te dohvat udomljenih pasa.
    /// Koristi se in-memory baza podataka kako bi testovi bili izolirani i brzi.
    /// </summary>
    public class PocetnaControllerTests : IDisposable
    {
        /// <summary>Kontekst in-memory baze podataka koji se koristi u testovima.</summary>
        private readonly BackendContext _context;

        /// <summary>Mock objekt za AutoMapper koji simulira mapiranje između modela i DTO-ova.</summary>
        private readonly Mock<IMapper> _mapperMock;

        /// <summary>Instanca kontrolera koja se testira.</summary>
        private readonly PocetnaController _controller;

        /// <summary>
        /// Inicijalizira in-memory bazu, mock mapper i kontroler prije svakog testa.
        /// Svaki test dobiva svježu, izoliranu instancu baze podataka.
        /// </summary>
        public PocetnaControllerTests()
        {
            var options = new DbContextOptionsBuilder<BackendContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new BackendContext(options);
            _mapperMock = new Mock<IMapper>();
            _controller = new PocetnaController(_context, _mapperMock.Object);
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
        /// Dodaje <see cref="Status"/> i <see cref="Pas"/> u bazu koristeći iste instance
        /// kako bi se izbjegao EF Core tracking konflikt.
        /// </summary>
        /// <param name="statusNaziv">Naziv statusa psa (npr. "slobodan", "udomljen").</param>
        /// <param name="pasSifra">Šifra psa (zadano 1).</param>
        /// <returns>Tuple s dodanim entitetima: (status, pas).</returns>
        private (Status status, Pas pas) DodajPsaUBazu(string statusNaziv = "slobodan", int pasSifra = 1)
        {
            var status = new Status { Sifra = pasSifra, Naziv = statusNaziv };
            _context.Statusi.Add(status);

            var pas = new Pas
            {
                Sifra = pasSifra,
                Ime = "Rex",
                BrojCipa = "123456789",
                Datum_Rodjenja = new DateTime(2020, 1, 1),
                Spol = "M",
                Opis = "Ljubazan pas",
                Kastracija = false,
                Status = status
            };
            _context.Psi.Add(pas);
            _context.SaveChanges();

            return (status, pas);
        }

        /// <summary>
        /// Stvara i vraća primjer <see cref="PasDTORead"/> objekta s predefiniranim podacima.
        /// </summary>
        /// <param name="statusNaziv">Naziv statusa psa (zadano "slobodan").</param>
        private static PasDTORead DajPasDTORead(string statusNaziv = "slobodan") => new(
            1, "Rex", "123456789", new DateTime(2020, 1, 1), "M", "Ljubazan pas", false, statusNaziv
        );

        /// <summary>
        /// Stvara i vraća primjer <see cref="UpitObrazacDTO"/> objekta s validnim podacima.
        /// </summary>
        private static UpitObrazacDTO DajUpitObrazacDTO() => new(
            PasSifra: 1,
            Ime: "Ana",
            Prezime: "Anić",
            Email: "ana.anic@example.com",
            SadrzajUpita: "Zanima me udomljavanje.",
            Telefon: "0921234567",
            Adresa: "Vukovarska 1, Zagreb"
        );

        #endregion

        #region GetPasPoSifri - Dohvat psa prema šifri

        /// <summary>
        /// Testira da metoda <c>GetPasPoSifri()</c> vraća <see cref="OkObjectResult"/> (HTTP 200)
        /// s ispravnim psom kada pas s traženom šifrom postoji u bazi.
        /// </summary>
        [Fact]
        public void GetPasPoSifri_KadaPasPostoji_VracaOk()
        {
            // Arrange
            DodajPsaUBazu();

            var dto = DajPasDTORead();
            _mapperMock
                .Setup(m => m.Map<PasDTORead>(It.IsAny<Pas>()))
                .Returns(dto);

            // Act
            var result = _controller.GetPasPoSifri(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<PasDTORead>(okResult.Value);
        }

        /// <summary>
        /// Testira da metoda <c>GetPasPoSifri()</c> vraća <see cref="NotFoundObjectResult"/> (HTTP 404)
        /// kada pas s traženom šifrom ne postoji u bazi.
        /// </summary>
        [Fact]
        public void GetPasPoSifri_KadaPasNePostoji_VracaNotFound()
        {
            // Act
            var result = _controller.GetPasPoSifri(999);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        #endregion

        #region TraziStranicenje - Paginacija pasa

        /// <summary>
        /// Testira da metoda <c>TraziStranicenje()</c> vraća <see cref="OkObjectResult"/> (HTTP 200)
        /// s listom pasa kada postoje psi sa statusom "slobodan".
        /// </summary>
        [Fact]
        public void TraziStranicenje_KadaPostojeSlobodniPsi_VracaOk()
        {
            // Arrange
            DodajPsaUBazu("slobodan");

            var listaDTO = new List<PasDTORead> { DajPasDTORead("slobodan") };
            _mapperMock
                .Setup(m => m.Map<List<PasDTORead>>(It.IsAny<object>()))
                .Returns(listaDTO);

            // Act
            var result = _controller.TraziStranicenje(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        /// <summary>
        /// Testira da metoda <c>TraziStranicenje()</c> vraća <see cref="OkObjectResult"/> (HTTP 200)
        /// s odgovarajućom porukom kada nema pasa s traženim statusom.
        /// </summary>
        [Fact]
        public void TraziStranicenje_KadaNemaPasa_VracaOkSPorukom()
        {
            // Arrange — pas postoji ali ima status koji nije tražen
            DodajPsaUBazu("udomljen");

            // Act
            var result = _controller.TraziStranicenje(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        /// <summary>
        /// Testira da metoda <c>TraziStranicenje()</c> vraća <see cref="BadRequestObjectResult"/> (HTTP 400)
        /// kada je broj stranice manji od 1.
        /// </summary>
        [Fact]
        public void TraziStranicenje_SNevažećimBrojemStranice_VracaBadRequest()
        {
            // Act
            var result = _controller.TraziStranicenje(0);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Testira da metoda <c>TraziStranicenje()</c> vraća <see cref="OkObjectResult"/> (HTTP 200)
        /// s psima koji imaju status "privremeni smještaj".
        /// </summary>
        [Fact]
        public void TraziStranicenje_KadaPostojePrivremeniSmjestaj_VracaOk()
        {
            // Arrange
            DodajPsaUBazu("privremeni smještaj");

            var listaDTO = new List<PasDTORead> { DajPasDTORead("privremeni smještaj") };
            _mapperMock
                .Setup(m => m.Map<List<PasDTORead>>(It.IsAny<object>()))
                .Returns(listaDTO);

            // Act
            var result = _controller.TraziStranicenje(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        #endregion

        #region IzracunajUkupnoStranica - Računanje ukupnog broja stranica

        /// <summary>
        /// Testira da metoda <c>IzracunajUkupnoStranica()</c> vraća <see cref="OkObjectResult"/> (HTTP 200)
        /// s ispravnim brojem stranica kada u bazi postoje slobodni psi.
        /// </summary>
        [Fact]
        public void IzracunajUkupnoStranica_KadaPostojeSlobodniPsi_VracaOk()
        {
            // Arrange — dodajemo 5 slobodnih pasa, uz poStranici=4 trebaju biti 2 stranice
            var status = new Status { Sifra = 10, Naziv = "slobodan" };
            _context.Statusi.Add(status);
            for (int i = 1; i <= 5; i++)
            {
                _context.Psi.Add(new Pas
                {
                    Sifra = i,
                    Ime = $"Pas{i}",
                    BrojCipa = $"10000{i}",
                    Datum_Rodjenja = new DateTime(2020, 1, 1),
                    Spol = "M",
                    Opis = "Opis",
                    Kastracija = false,
                    Status = status
                });
            }
            _context.SaveChanges();

            // Act
            var result = _controller.IzracunajUkupnoStranica();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(2, okResult.Value);
        }

        /// <summary>
        /// Testira da metoda <c>IzracunajUkupnoStranica()</c> vraća 0 stranica
        /// kada u bazi nema pasa sa statusom "slobodan" ili "privremeni smještaj".
        /// </summary>
        [Fact]
        public void IzracunajUkupnoStranica_KadaNemaSlobodnihPasa_VracaNula()
        {
            // Arrange
            DodajPsaUBazu("udomljen");

            // Act
            var result = _controller.IzracunajUkupnoStranica();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(0, okResult.Value);
        }

        #endregion

        #region Post (UpitObrazac) - Unos upita putem obrasca

        /// <summary>
        /// Testira da metoda <c>Post()</c> uspješno kreira upit i novog udomitelja
        /// kada udomitelj s navedenim emailom još ne postoji u bazi,
        /// te vraća <see cref="OkObjectResult"/> (HTTP 200).
        /// </summary>
        [Fact]
        public void Post_NovogUdomitelja_KreiraSvePodatkeIVracaOk()
        {
            // Arrange
            DodajPsaUBazu();

            var dto = DajUpitObrazacDTO();
            var upit = new Upit
            {
                Sifra = 1,
                Pas = _context.Psi.First(),
                Udomitelj = new Udomitelj
                {
                    Ime = dto.Ime,
                    Prezime = dto.Prezime,
                    Email = dto.Email,
                    Adresa = dto.Adresa,
                    Telefon = dto.Telefon
                },
                DatumUpita = DateTime.Now,
                StatusUpita = "Na čekanju",
                SadrzajUpita = dto.SadrzajUpita
            };

            _mapperMock
                .Setup(m => m.Map<Upit>(dto))
                .Returns(upit);

            // Act
            var result = _controller.Post(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
            Assert.Equal(1, _context.Udomitelji.Count());
        }

        /// <summary>
        /// Testira da metoda <c>Post()</c> ne kreira novog udomitelja kada udomitelj
        /// s istim emailom već postoji u bazi, već koristi postojećeg udomitelja.
        /// </summary>
        [Fact]
        public void Post_PostojecegUdomitelja_KoristiPostojecegIVracaOk()
        {
            // Arrange
            DodajPsaUBazu();

            var postojeciUdomitelj = new Udomitelj
            {
                Sifra = 1,
                Ime = "Ana",
                Prezime = "Anić",
                Email = "ana.anic@example.com",
                Adresa = "Vukovarska 1, Zagreb",
                Telefon = "0921234567"
            };
            _context.Udomitelji.Add(postojeciUdomitelj);
            _context.SaveChanges();

            var dto = DajUpitObrazacDTO();
            var upit = new Upit
            {
                Sifra = 1,
                Pas = _context.Psi.First(),
                Udomitelj = postojeciUdomitelj,
                DatumUpita = DateTime.Now,
                StatusUpita = "Na čekanju",
                SadrzajUpita = dto.SadrzajUpita
            };

            _mapperMock
                .Setup(m => m.Map<Upit>(dto))
                .Returns(upit);

            // Act
            var result = _controller.Post(dto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            // Provjeri da i dalje postoji samo 1 udomitelj (nije kreiran novi)
            Assert.Equal(1, _context.Udomitelji.Count());
        }

        /// <summary>
        /// Testira da metoda <c>Post()</c> vraća <see cref="BadRequestObjectResult"/> (HTTP 400)
        /// kada isti upit (isti sadržaj, pas i email udomitelja) već postoji u bazi.
        /// </summary>
        [Fact]
        public void Post_DuplikatUpita_VracaBadRequest()
        {
            // Arrange
            var (_, pas) = DodajPsaUBazu();

            var udomitelj = new Udomitelj
            {
                Sifra = 1,
                Ime = "Ana",
                Prezime = "Anić",
                Email = "ana.anic@example.com",
                Adresa = "Vukovarska 1, Zagreb",
                Telefon = "0921234567"
            };
            _context.Udomitelji.Add(udomitelj);

            _context.Upiti.Add(new Upit
            {
                Sifra = 1,
                Pas = pas,
                Udomitelj = udomitelj,
                DatumUpita = DateTime.Now,
                StatusUpita = "Na čekanju",
                SadrzajUpita = "Zanima me udomljavanje."
            });
            _context.SaveChanges();

            var dto = DajUpitObrazacDTO();

            // Act
            var result = _controller.Post(dto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Testira da metoda <c>Post()</c> vraća <see cref="NotFoundObjectResult"/> (HTTP 404)
        /// kada pas s navedenom šifrom ne postoji u bazi.
        /// </summary>
        [Fact]
        public void Post_KadaPasNePostoji_VracaNotFound()
        {
            // Arrange — baza je prazna, pas ne postoji
            var dto = DajUpitObrazacDTO();

            // Act
            var result = _controller.Post(dto);

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
            _controller.ModelState.AddModelError("Email", "Email obavezan");

            // Act
            var result = _controller.Post(DajUpitObrazacDTO());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        #endregion

        #region TraziUdomljenePse - Dohvat broja udomljenih pasa

        /// <summary>
        /// Testira da metoda <c>TraziUdomljenePse()</c> vraća <see cref="OkObjectResult"/> (HTTP 200)
        /// s ispravnim brojem udomljenih pasa.
        /// </summary>
        [Fact]
        public void TraziUdomljenePse_KadaPostojeUdomljeniPsi_VracaTacanBroj()
        {
            // Arrange
            DodajPsaUBazu("udomljen");

            // Act
            var result = _controller.TraziUdomljenePse();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(1, okResult.Value);
        }

        /// <summary>
        /// Testira da metoda <c>TraziUdomljenePse()</c> vraća 0
        /// kada u bazi nema udomljenih pasa.
        /// </summary>
        [Fact]
        public void TraziUdomljenePse_KadaNemaUdomljenihPasa_VracaNula()
        {
            // Arrange
            DodajPsaUBazu("slobodan");

            // Act
            var result = _controller.TraziUdomljenePse();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(0, okResult.Value);
        }

        #endregion
    }
}