using AutoMapper;
using Backend.Controllers;
using Backend.Data;
using Backend.Models;
using Backend.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Backend.Tests.Controllers
{
    /// <summary>
    /// Unit testovi za <see cref="PasController"/>.
    /// Svaki test koristi vlastitu in-memory bazu kako bi testovi bili međusobno izolirani.
    /// </summary>
    public class PasControllerTests
    {
        /// <summary>
        /// Kreira in-memory bazu podataka s jedinstvenim imenom.
        /// </summary>
        /// <param name="dbName">Jedinstveno ime baze podataka.</param>
        /// <returns>Instanca <see cref="BackendContext"/> s in-memory bazom.</returns>
        private BackendContext CreateInMemoryContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<BackendContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            return new BackendContext(options);
        }

        /// <summary>
        /// Kreira AutoMapper instancu s mapiranjima potrebnim za testove.
        /// </summary>
        /// <returns>Konfigurirana <see cref="IMapper"/> instanca.</returns>
        private IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Pas, PasDTORead>()
                    .ForMember(d => d.StatusNaziv, o => o.MapFrom(s => s.Status != null ? s.Status.Naziv : null));
                cfg.CreateMap<PasDTOInsertUpdate, Pas>();
                cfg.CreateMap<Pas, PasDTOInsertUpdate>()
                    .ForMember(d => d.StatusSifra, o => o.MapFrom(s => s.Status != null ? s.Status.Sifra : 0));
            });
            return config.CreateMapper();
        }

        /// <summary>
        /// Priprema in-memory bazu s testnim podacima (jedan status i dva psa).
        /// </summary>
        /// <param name="dbName">Jedinstveno ime baze podataka kako bi testovi bili izolirani.</param>
        /// <returns>Tuple s kontekstom baze i mapperom.</returns>
        private (BackendContext context, IMapper mapper) Setup(string dbName)
        {
            var context = CreateInMemoryContext(dbName);
            var mapper = CreateMapper();

            var status = new Status { Sifra = 1, Naziv = "Dostupan" };
            context.Statusi.Add(status);

            context.Psi.AddRange(
                new Pas { Sifra = 1, Ime = "Rex", BrojCipa = "HR100000000000001", Spol = "muški", Status = status },
                new Pas { Sifra = 2, Ime = "Bella", BrojCipa = "HR100000000000002", Spol = "ženski", Status = status }
            );
            context.SaveChanges();

            return (context, mapper);
        }

        // ─── GET (all) ────────────────────────────────────────────────────────────

        /// <summary>
        /// Provjera da GET vraća 200 OK s listom svih pasa.
        /// </summary>
        [Fact]
        public void Get_ReturnsOkWithAllDogs()
        {
            var (ctx, mapper) = Setup("Get_All");
            var controller = new PasController(ctx, mapper);

            var result = controller.Get();

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var list = Assert.IsType<List<PasDTORead>>(ok.Value);
            Assert.Equal(2, list.Count);
        }

        /// <summary>
        /// Provjera da GET vraća pse sortirane abecedno po imenu.
        /// </summary>
        [Fact]
        public void Get_ReturnsDogsOrderedByName()
        {
            var (ctx, mapper) = Setup("Get_Ordered");
            var controller = new PasController(ctx, mapper);

            var result = controller.Get();

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var list = Assert.IsType<List<PasDTORead>>(ok.Value);
            Assert.Equal("Bella", list[0].Ime);
            Assert.Equal("Rex", list[1].Ime);
        }

        // ─── GET by sifra ─────────────────────────────────────────────────────────

        /// <summary>
        /// Provjera da GET po šifri vraća 200 OK i ispravne podatke za postojećeg psa.
        /// </summary>
        [Fact]
        public void GetBySifra_ExistingId_ReturnsOk()
        {
            var (ctx, mapper) = Setup("GetById_Found");
            var controller = new PasController(ctx, mapper);

            var result = controller.GetBySifra(1);

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var dto = Assert.IsType<PasDTOInsertUpdate>(ok.Value);
            Assert.Equal(1, dto.StatusSifra);
        }

        /// <summary>
        /// Provjera da GET po šifri vraća 404 Not Found za nepostojećeg psa.
        /// </summary>
        [Fact]
        public void GetBySifra_NonExistingId_ReturnsNotFound()
        {
            var (ctx, mapper) = Setup("GetById_NotFound");
            var controller = new PasController(ctx, mapper);

            var result = controller.GetBySifra(999);

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        // ─── POST ─────────────────────────────────────────────────────────────────

        /// <summary>
        /// Provjera da POST s ispravnim podacima vraća 201 Created.
        /// </summary>
        [Fact]
        public void Post_ValidDto_Returns201()
        {
            var (ctx, mapper) = Setup("Post_Valid");
            var controller = new PasController(ctx, mapper);

            var dto = new PasDTOInsertUpdate(
                Ime: "Max",
                BrojCipa: "HR100000000000003",
                Datum_Rodjenja: new DateTime(2020, 1, 1),
                Spol: "muški",
                Opis: "Veseo pas",
                Kastracija: false,
                StatusSifra: 1
            );

            var result = controller.Post(dto);

            var created = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status201Created, created.StatusCode);
        }

        /// <summary>
        /// Provjera da POST vraća 404 Not Found kada zadani status ne postoji u bazi.
        /// </summary>
        [Fact]
        public void Post_InvalidStatus_ReturnsNotFound()
        {
            var (ctx, mapper) = Setup("Post_BadStatus");
            var controller = new PasController(ctx, mapper);

            var dto = new PasDTOInsertUpdate(
                Ime: "Max",
                BrojCipa: "HR100000000000004",
                Datum_Rodjenja: new DateTime(2020, 1, 1),
                Spol: "muški",
                Opis: "",
                Kastracija: false,
                StatusSifra: 999
            );

            var result = controller.Post(dto);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        /// <summary>
        /// Provjera da POST vraća 400 Bad Request kada spol nije "muški" ili "ženski".
        /// </summary>
        [Fact]
        public void Post_InvalidSpol_ReturnsBadRequest()
        {
            var (ctx, mapper) = Setup("Post_BadSpol");
            var controller = new PasController(ctx, mapper);

            var dto = new PasDTOInsertUpdate(
                Ime: "Max",
                BrojCipa: "HR100000000000005",
                Datum_Rodjenja: new DateTime(2020, 1, 1),
                Spol: "nepoznato",
                Opis: "",
                Kastracija: false,
                StatusSifra: 1
            );

            var result = controller.Post(dto);

            var bad = Assert.IsType<BadRequestObjectResult>(result);
            Assert.NotNull(bad.Value);
        }

        /// <summary>
        /// Provjera da POST uklanja razmake s početka i kraja broja čipa prije pohrane.
        /// </summary>
        [Fact]
        public void Post_TrimsChipNumber()
        {
            var (ctx, mapper) = Setup("Post_Trim");
            var controller = new PasController(ctx, mapper);

            var dto = new PasDTOInsertUpdate(
                Ime: "Trimmy",
                BrojCipa: "  HR100000000000006  ",
                Datum_Rodjenja: new DateTime(2021, 5, 10),
                Spol: "ženski",
                Opis: "",
                Kastracija: true,
                StatusSifra: 1
            );

            controller.Post(dto);

            var savedDog = ctx.Psi.FirstOrDefault(p => p.Ime == "Trimmy");
            Assert.NotNull(savedDog);
            Assert.Equal("HR100000000000006", savedDog!.BrojCipa);
        }

        // ─── PUT ──────────────────────────────────────────────────────────────────

        /// <summary>
        /// Provjera da PUT vraća 200 OK za uspješno ažuriranje postojećeg psa.
        /// </summary>
        [Fact]
        public void Put_ExistingDog_ReturnsOk()
        {
            var (ctx, mapper) = Setup("Put_Valid");
            var controller = new PasController(ctx, mapper);

            var dto = new PasDTOInsertUpdate(
                Ime: "RexUpdated",
                BrojCipa: "HR100000000000001",
                Datum_Rodjenja: new DateTime(2019, 3, 15),
                Spol: "muški",
                Opis: "Ažuriran opis",
                Kastracija: false,
                StatusSifra: 1
            );

            var result = controller.Put(1, dto);

            Assert.IsType<OkObjectResult>(result);
        }

        /// <summary>
        /// Provjera da PUT vraća 404 Not Found kada pas s danom šifrom ne postoji.
        /// </summary>
        [Fact]
        public void Put_NonExistingDog_ReturnsNotFound()
        {
            var (ctx, mapper) = Setup("Put_NotFound");
            var controller = new PasController(ctx, mapper);

            var dto = new PasDTOInsertUpdate(
                Ime: "Ghost",
                BrojCipa: "HR000000000000000",
                Datum_Rodjenja: new DateTime(2020, 1, 1),
                Spol: "muški",
                Opis: "",
                Kastracija: false,
                StatusSifra: 1
            );

            var result = controller.Put(999, dto);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        /// <summary>
        /// Provjera da PUT vraća 404 Not Found kada zadani status ne postoji u bazi.
        /// </summary>
        [Fact]
        public void Put_InvalidStatus_ReturnsNotFound()
        {
            var (ctx, mapper) = Setup("Put_BadStatus");
            var controller = new PasController(ctx, mapper);

            var dto = new PasDTOInsertUpdate(
                Ime: "Rex",
                BrojCipa: "HR100000000000001",
                Datum_Rodjenja: new DateTime(2019, 3, 15),
                Spol: "muški",
                Opis: "",
                Kastracija: false,
                StatusSifra: 999
            );

            var result = controller.Put(1, dto);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        /// <summary>
        /// Provjera da PUT vraća 400 Bad Request kada spol nije "muški" ili "ženski".
        /// </summary>
        [Fact]
        public void Put_InvalidSpol_ReturnsBadRequest()
        {
            var (ctx, mapper) = Setup("Put_BadSpol");
            var controller = new PasController(ctx, mapper);

            var dto = new PasDTOInsertUpdate(
                Ime: "Rex",
                BrojCipa: "HR100000000000001",
                Datum_Rodjenja: new DateTime(2019, 3, 15),
                Spol: "nepoznato",
                Opis: "",
                Kastracija: false,
                StatusSifra: 1
            );

            var result = controller.Put(1, dto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Provjera da PUT stvarno ažurira ime psa u bazi podataka.
        /// </summary>
        [Fact]
        public void Put_UpdatesNameInDatabase()
        {
            var (ctx, mapper) = Setup("Put_UpdatesDb");
            var controller = new PasController(ctx, mapper);

            var dto = new PasDTOInsertUpdate(
                Ime: "RexNew",
                BrojCipa: "HR100000000000001",
                Datum_Rodjenja: new DateTime(2019, 3, 15),
                Spol: "muški",
                Opis: "",
                Kastracija: false,
                StatusSifra: 1
            );

            controller.Put(1, dto);

            var updated = ctx.Psi.Find(1);
            Assert.Equal("RexNew", updated!.Ime);
        }

        // ─── DELETE ───────────────────────────────────────────────────────────────

        /// <summary>
        /// Provjera da DELETE vraća 200 OK za uspješno brisanje postojećeg psa.
        /// </summary>
        [Fact]
        public void Delete_ExistingDog_ReturnsOk()
        {
            var (ctx, mapper) = Setup("Delete_Valid");
            var controller = new PasController(ctx, mapper);

            var result = controller.Delete(1);

            Assert.IsType<OkObjectResult>(result);
        }

        /// <summary>
        /// Provjera da DELETE vraća 404 Not Found kada pas s danom šifrom ne postoji.
        /// </summary>
        [Fact]
        public void Delete_NonExistingDog_ReturnsNotFound()
        {
            var (ctx, mapper) = Setup("Delete_NotFound");
            var controller = new PasController(ctx, mapper);

            var result = controller.Delete(999);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        /// <summary>
        /// Provjera da DELETE stvarno uklanja psa iz baze podataka.
        /// </summary>
        [Fact]
        public void Delete_RemovesDogFromDatabase()
        {
            var (ctx, mapper) = Setup("Delete_RemovesFromDb");
            var controller = new PasController(ctx, mapper);

            controller.Delete(1);

            Assert.Null(ctx.Psi.Find(1));
            Assert.Equal(1, ctx.Psi.Count());
        }

        // ─── TRAŽI ────────────────────────────────────────────────────────────────

        /// <summary>
        /// Provjera da TraziPsa vraća pse koji odgovaraju zadanom uvjetu.
        /// </summary>
        [Fact]
        public void TraziPsa_ValidTerm_ReturnsMatchingDogs()
        {
            var (ctx, mapper) = Setup("Trazi_Found");
            var controller = new PasController(ctx, mapper);

            var result = controller.TraziPsa("Rex");

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var list = Assert.IsType<List<PasDTORead>>(ok.Value);
            Assert.Single(list);
            Assert.Equal("Rex", list[0].Ime);
        }

        /// <summary>
        /// Provjera da TraziPsa vraća 400 Bad Request kada je uvjet kraći od 3 znaka.
        /// </summary>
        [Fact]
        public void TraziPsa_ShortTerm_ReturnsBadRequest()
        {
            var (ctx, mapper) = Setup("Trazi_Short");
            var controller = new PasController(ctx, mapper);

            var result = controller.TraziPsa("Re");

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        /// <summary>
        /// Provjera da TraziPsa vraća 400 Bad Request kada je uvjet null.
        /// </summary>
        [Fact]
        public void TraziPsa_NullTerm_ReturnsBadRequest()
        {
            var (ctx, mapper) = Setup("Trazi_Null");
            var controller = new PasController(ctx, mapper);

            var result = controller.TraziPsa(null!);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        /// <summary>
        /// Provjera da TraziPsa vraća praznu listu kada nijedan pas ne odgovara uvjetu.
        /// </summary>
        [Fact]
        public void TraziPsa_NoMatch_ReturnsEmptyList()
        {
            var (ctx, mapper) = Setup("Trazi_NoMatch");
            var controller = new PasController(ctx, mapper);

            var result = controller.TraziPsa("Zorro");

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var list = Assert.IsType<List<PasDTORead>>(ok.Value);
            Assert.Empty(list);
        }

        /// <summary>
        /// Provjera da TraziPsa pretražuje bez razlikovanja velikih i malih slova.
        /// </summary>
        [Fact]
        public void TraziPsa_CaseInsensitive_ReturnsMatch()
        {
            var (ctx, mapper) = Setup("Trazi_CaseInsensitive");
            var controller = new PasController(ctx, mapper);

            var result = controller.TraziPsa("rex");

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var list = Assert.IsType<List<PasDTORead>>(ok.Value);
            Assert.Single(list);
        }
    }
}