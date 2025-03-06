<a name='assembly'></a>
# Backend

## Contents

- [AutorizacijaController](#T-Backend-Controllers-AutorizacijaController 'Backend.Controllers.AutorizacijaController')
  - [#ctor(context)](#M-Backend-Controllers-AutorizacijaController-#ctor-Backend-Data-BackendContext- 'Backend.Controllers.AutorizacijaController.#ctor(Backend.Data.BackendContext)')
  - [_context](#F-Backend-Controllers-AutorizacijaController-_context 'Backend.Controllers.AutorizacijaController._context')
  - [GenerirajToken(operater)](#M-Backend-Controllers-AutorizacijaController-GenerirajToken-Backend-Models-DTO-OperaterDTO- 'Backend.Controllers.AutorizacijaController.GenerirajToken(Backend.Models.DTO.OperaterDTO)')
- [AutorizacijaControllerTests](#T-Backend-Tests-Controllers-AutorizacijaControllerTests 'Backend.Tests.Controllers.AutorizacijaControllerTests')
  - [#ctor()](#M-Backend-Tests-Controllers-AutorizacijaControllerTests-#ctor 'Backend.Tests.Controllers.AutorizacijaControllerTests.#ctor')
  - [GenerirajToken_InvalidEmail_ReturnsForbidden()](#M-Backend-Tests-Controllers-AutorizacijaControllerTests-GenerirajToken_InvalidEmail_ReturnsForbidden 'Backend.Tests.Controllers.AutorizacijaControllerTests.GenerirajToken_InvalidEmail_ReturnsForbidden')
  - [GenerirajToken_InvalidModel_ReturnsBadRequest()](#M-Backend-Tests-Controllers-AutorizacijaControllerTests-GenerirajToken_InvalidModel_ReturnsBadRequest 'Backend.Tests.Controllers.AutorizacijaControllerTests.GenerirajToken_InvalidModel_ReturnsBadRequest')
  - [GenerirajToken_InvalidPassword_ReturnsForbidden()](#M-Backend-Tests-Controllers-AutorizacijaControllerTests-GenerirajToken_InvalidPassword_ReturnsForbidden 'Backend.Tests.Controllers.AutorizacijaControllerTests.GenerirajToken_InvalidPassword_ReturnsForbidden')
  - [GenerirajToken_ValidCredentials_ReturnsOkWithToken()](#M-Backend-Tests-Controllers-AutorizacijaControllerTests-GenerirajToken_ValidCredentials_ReturnsOkWithToken 'Backend.Tests.Controllers.AutorizacijaControllerTests.GenerirajToken_ValidCredentials_ReturnsOkWithToken')
- [BackendContext](#T-Backend-Data-BackendContext 'Backend.Data.BackendContext')
  - [#ctor(opcije)](#M-Backend-Data-BackendContext-#ctor-Microsoft-EntityFrameworkCore-DbContextOptions{Backend-Data-BackendContext}- 'Backend.Data.BackendContext.#ctor(Microsoft.EntityFrameworkCore.DbContextOptions{Backend.Data.BackendContext})')
  - [Operateri](#P-Backend-Data-BackendContext-Operateri 'Backend.Data.BackendContext.Operateri')
  - [Psi](#P-Backend-Data-BackendContext-Psi 'Backend.Data.BackendContext.Psi')
  - [Statusi](#P-Backend-Data-BackendContext-Statusi 'Backend.Data.BackendContext.Statusi')
  - [Udomitelji](#P-Backend-Data-BackendContext-Udomitelji 'Backend.Data.BackendContext.Udomitelji')
  - [Upiti](#P-Backend-Data-BackendContext-Upiti 'Backend.Data.BackendContext.Upiti')
  - [OnModelCreating(modelBuilder)](#M-Backend-Data-BackendContext-OnModelCreating-Microsoft-EntityFrameworkCore-ModelBuilder- 'Backend.Data.BackendContext.OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder)')
- [BackendController](#T-Backend-Controllers-BackendController 'Backend.Controllers.BackendController')
  - [#ctor(context,mapper)](#M-Backend-Controllers-BackendController-#ctor-Backend-Data-BackendContext,AutoMapper-IMapper- 'Backend.Controllers.BackendController.#ctor(Backend.Data.BackendContext,AutoMapper.IMapper)')
  - [_context](#F-Backend-Controllers-BackendController-_context 'Backend.Controllers.BackendController._context')
  - [_mapper](#F-Backend-Controllers-BackendController-_mapper 'Backend.Controllers.BackendController._mapper')
- [BackendExtensions](#T-Backend-Extensions-BackendExtensions 'Backend.Extensions.BackendExtensions')
  - [AddBackendCORS(Services)](#M-Backend-Extensions-BackendExtensions-AddBackendCORS-Microsoft-Extensions-DependencyInjection-IServiceCollection- 'Backend.Extensions.BackendExtensions.AddBackendCORS(Microsoft.Extensions.DependencyInjection.IServiceCollection)')
  - [AddBackendSecurity(Services)](#M-Backend-Extensions-BackendExtensions-AddBackendSecurity-Microsoft-Extensions-DependencyInjection-IServiceCollection- 'Backend.Extensions.BackendExtensions.AddBackendSecurity(Microsoft.Extensions.DependencyInjection.IServiceCollection)')
  - [AddBackendSwaggerGen(Services)](#M-Backend-Extensions-BackendExtensions-AddBackendSwaggerGen-Microsoft-Extensions-DependencyInjection-IServiceCollection- 'Backend.Extensions.BackendExtensions.AddBackendSwaggerGen(Microsoft.Extensions.DependencyInjection.IServiceCollection)')
- [BackendMappingProfile](#T-Backend-Mapping-BackendMappingProfile 'Backend.Mapping.BackendMappingProfile')
  - [#ctor()](#M-Backend-Mapping-BackendMappingProfile-#ctor 'Backend.Mapping.BackendMappingProfile.#ctor')
- [Entitet](#T-Backend-Models-Entitet 'Backend.Models.Entitet')
  - [Sifra](#P-Backend-Models-Entitet-Sifra 'Backend.Models.Entitet.Sifra')
- [Operater](#T-Backend-Models-Operater 'Backend.Models.Operater')
  - [Email](#P-Backend-Models-Operater-Email 'Backend.Models.Operater.Email')
  - [Lozinka](#P-Backend-Models-Operater-Lozinka 'Backend.Models.Operater.Lozinka')
- [OperaterDTO](#T-Backend-Models-DTO-OperaterDTO 'Backend.Models.DTO.OperaterDTO')
  - [#ctor(Email,Password)](#M-Backend-Models-DTO-OperaterDTO-#ctor-System-String,System-String- 'Backend.Models.DTO.OperaterDTO.#ctor(System.String,System.String)')
  - [Email](#P-Backend-Models-DTO-OperaterDTO-Email 'Backend.Models.DTO.OperaterDTO.Email')
  - [Password](#P-Backend-Models-DTO-OperaterDTO-Password 'Backend.Models.DTO.OperaterDTO.Password')
- [Pas](#T-Backend-Models-Pas 'Backend.Models.Pas')
  - [BrojCipa](#P-Backend-Models-Pas-BrojCipa 'Backend.Models.Pas.BrojCipa')
  - [Datum_Rodjenja](#P-Backend-Models-Pas-Datum_Rodjenja 'Backend.Models.Pas.Datum_Rodjenja')
  - [Ime](#P-Backend-Models-Pas-Ime 'Backend.Models.Pas.Ime')
  - [Kastracija](#P-Backend-Models-Pas-Kastracija 'Backend.Models.Pas.Kastracija')
  - [Opis](#P-Backend-Models-Pas-Opis 'Backend.Models.Pas.Opis')
  - [Spol](#P-Backend-Models-Pas-Spol 'Backend.Models.Pas.Spol')
  - [Status](#P-Backend-Models-Pas-Status 'Backend.Models.Pas.Status')
- [PasController](#T-Backend-Controllers-PasController 'Backend.Controllers.PasController')
  - [#ctor(context,mapper)](#M-Backend-Controllers-PasController-#ctor-Backend-Data-BackendContext,AutoMapper-IMapper- 'Backend.Controllers.PasController.#ctor(Backend.Data.BackendContext,AutoMapper.IMapper)')
  - [Delete(sifra)](#M-Backend-Controllers-PasController-Delete-System-Int32- 'Backend.Controllers.PasController.Delete(System.Int32)')
  - [Get()](#M-Backend-Controllers-PasController-Get 'Backend.Controllers.PasController.Get')
  - [GetBySifra(sifra)](#M-Backend-Controllers-PasController-GetBySifra-System-Int32- 'Backend.Controllers.PasController.GetBySifra(System.Int32)')
  - [Post(dto)](#M-Backend-Controllers-PasController-Post-Backend-Models-DTO-PasDTOInsertUpdate- 'Backend.Controllers.PasController.Post(Backend.Models.DTO.PasDTOInsertUpdate)')
  - [Put(sifra,dto)](#M-Backend-Controllers-PasController-Put-System-Int32,Backend-Models-DTO-PasDTOInsertUpdate- 'Backend.Controllers.PasController.Put(System.Int32,Backend.Models.DTO.PasDTOInsertUpdate)')
  - [TraziPsa(uvjet)](#M-Backend-Controllers-PasController-TraziPsa-System-String- 'Backend.Controllers.PasController.TraziPsa(System.String)')
- [PasDTOInsertUpdate](#T-Backend-Models-DTO-PasDTOInsertUpdate 'Backend.Models.DTO.PasDTOInsertUpdate')
  - [#ctor(Ime,BrojCipa,Datum_Rodjenja,Spol,Opis,Kastracija,StatusSifra)](#M-Backend-Models-DTO-PasDTOInsertUpdate-#ctor-System-String,System-String,System-DateTime,System-String,System-String,System-Boolean,System-Int32- 'Backend.Models.DTO.PasDTOInsertUpdate.#ctor(System.String,System.String,System.DateTime,System.String,System.String,System.Boolean,System.Int32)')
  - [BrojCipa](#P-Backend-Models-DTO-PasDTOInsertUpdate-BrojCipa 'Backend.Models.DTO.PasDTOInsertUpdate.BrojCipa')
  - [Datum_Rodjenja](#P-Backend-Models-DTO-PasDTOInsertUpdate-Datum_Rodjenja 'Backend.Models.DTO.PasDTOInsertUpdate.Datum_Rodjenja')
  - [Ime](#P-Backend-Models-DTO-PasDTOInsertUpdate-Ime 'Backend.Models.DTO.PasDTOInsertUpdate.Ime')
  - [Kastracija](#P-Backend-Models-DTO-PasDTOInsertUpdate-Kastracija 'Backend.Models.DTO.PasDTOInsertUpdate.Kastracija')
  - [Opis](#P-Backend-Models-DTO-PasDTOInsertUpdate-Opis 'Backend.Models.DTO.PasDTOInsertUpdate.Opis')
  - [Spol](#P-Backend-Models-DTO-PasDTOInsertUpdate-Spol 'Backend.Models.DTO.PasDTOInsertUpdate.Spol')
  - [StatusSifra](#P-Backend-Models-DTO-PasDTOInsertUpdate-StatusSifra 'Backend.Models.DTO.PasDTOInsertUpdate.StatusSifra')
- [PasDTORead](#T-Backend-Models-DTO-PasDTORead 'Backend.Models.DTO.PasDTORead')
  - [#ctor(Sifra,Ime,BrojCipa,Datum_Rodjenja,Spol,Opis,Kastracija,StatusNaziv)](#M-Backend-Models-DTO-PasDTORead-#ctor-System-Int32,System-String,System-String,System-DateTime,System-String,System-String,System-Boolean,System-String- 'Backend.Models.DTO.PasDTORead.#ctor(System.Int32,System.String,System.String,System.DateTime,System.String,System.String,System.Boolean,System.String)')
  - [BrojCipa](#P-Backend-Models-DTO-PasDTORead-BrojCipa 'Backend.Models.DTO.PasDTORead.BrojCipa')
  - [Datum_Rodjenja](#P-Backend-Models-DTO-PasDTORead-Datum_Rodjenja 'Backend.Models.DTO.PasDTORead.Datum_Rodjenja')
  - [Ime](#P-Backend-Models-DTO-PasDTORead-Ime 'Backend.Models.DTO.PasDTORead.Ime')
  - [Kastracija](#P-Backend-Models-DTO-PasDTORead-Kastracija 'Backend.Models.DTO.PasDTORead.Kastracija')
  - [Opis](#P-Backend-Models-DTO-PasDTORead-Opis 'Backend.Models.DTO.PasDTORead.Opis')
  - [Sifra](#P-Backend-Models-DTO-PasDTORead-Sifra 'Backend.Models.DTO.PasDTORead.Sifra')
  - [Spol](#P-Backend-Models-DTO-PasDTORead-Spol 'Backend.Models.DTO.PasDTORead.Spol')
  - [StatusNaziv](#P-Backend-Models-DTO-PasDTORead-StatusNaziv 'Backend.Models.DTO.PasDTORead.StatusNaziv')
- [PocetnaController](#T-Backend-Controllers-PocetnaController 'Backend.Controllers.PocetnaController')
  - [#ctor(_context)](#M-Backend-Controllers-PocetnaController-#ctor-Backend-Data-BackendContext,AutoMapper-IMapper- 'Backend.Controllers.PocetnaController.#ctor(Backend.Data.BackendContext,AutoMapper.IMapper)')
  - [DostupniPsi()](#M-Backend-Controllers-PocetnaController-DostupniPsi 'Backend.Controllers.PocetnaController.DostupniPsi')
  - [TraziStranicenje(stranica)](#M-Backend-Controllers-PocetnaController-TraziStranicenje-System-Int32- 'Backend.Controllers.PocetnaController.TraziStranicenje(System.Int32)')
  - [TraziUdomljenePse()](#M-Backend-Controllers-PocetnaController-TraziUdomljenePse 'Backend.Controllers.PocetnaController.TraziUdomljenePse')
- [Status](#T-Backend-Models-Status 'Backend.Models.Status')
  - [Naziv](#P-Backend-Models-Status-Naziv 'Backend.Models.Status.Naziv')
- [StatusController](#T-Backend-Controllers-StatusController 'Backend.Controllers.StatusController')
  - [#ctor(context,mapper)](#M-Backend-Controllers-StatusController-#ctor-Backend-Data-BackendContext,AutoMapper-IMapper- 'Backend.Controllers.StatusController.#ctor(Backend.Data.BackendContext,AutoMapper.IMapper)')
  - [Delete(sifra)](#M-Backend-Controllers-StatusController-Delete-System-Int32- 'Backend.Controllers.StatusController.Delete(System.Int32)')
  - [Get()](#M-Backend-Controllers-StatusController-Get 'Backend.Controllers.StatusController.Get')
  - [GetBySifra(sifra)](#M-Backend-Controllers-StatusController-GetBySifra-System-Int32- 'Backend.Controllers.StatusController.GetBySifra(System.Int32)')
  - [Post(dto)](#M-Backend-Controllers-StatusController-Post-Backend-Models-DTO-StatusDTOInsertUpdate- 'Backend.Controllers.StatusController.Post(Backend.Models.DTO.StatusDTOInsertUpdate)')
  - [Put(sifra,dto)](#M-Backend-Controllers-StatusController-Put-System-Int32,Backend-Models-DTO-StatusDTOInsertUpdate- 'Backend.Controllers.StatusController.Put(System.Int32,Backend.Models.DTO.StatusDTOInsertUpdate)')
- [StatusDTOInsertUpdate](#T-Backend-Models-DTO-StatusDTOInsertUpdate 'Backend.Models.DTO.StatusDTOInsertUpdate')
  - [#ctor(Naziv)](#M-Backend-Models-DTO-StatusDTOInsertUpdate-#ctor-System-String- 'Backend.Models.DTO.StatusDTOInsertUpdate.#ctor(System.String)')
  - [Naziv](#P-Backend-Models-DTO-StatusDTOInsertUpdate-Naziv 'Backend.Models.DTO.StatusDTOInsertUpdate.Naziv')
- [StatusDTORead](#T-Backend-Models-DTO-StatusDTORead 'Backend.Models.DTO.StatusDTORead')
  - [#ctor(Sifra,Naziv)](#M-Backend-Models-DTO-StatusDTORead-#ctor-System-Int32,System-String- 'Backend.Models.DTO.StatusDTORead.#ctor(System.Int32,System.String)')
  - [Naziv](#P-Backend-Models-DTO-StatusDTORead-Naziv 'Backend.Models.DTO.StatusDTORead.Naziv')
  - [Sifra](#P-Backend-Models-DTO-StatusDTORead-Sifra 'Backend.Models.DTO.StatusDTORead.Sifra')
- [Udomitelj](#T-Backend-Models-Udomitelj 'Backend.Models.Udomitelj')
  - [Adresa](#P-Backend-Models-Udomitelj-Adresa 'Backend.Models.Udomitelj.Adresa')
  - [Email](#P-Backend-Models-Udomitelj-Email 'Backend.Models.Udomitelj.Email')
  - [Ime](#P-Backend-Models-Udomitelj-Ime 'Backend.Models.Udomitelj.Ime')
  - [Prezime](#P-Backend-Models-Udomitelj-Prezime 'Backend.Models.Udomitelj.Prezime')
  - [Telefon](#P-Backend-Models-Udomitelj-Telefon 'Backend.Models.Udomitelj.Telefon')
- [UdomiteljController](#T-Backend-Controllers-UdomiteljController 'Backend.Controllers.UdomiteljController')
  - [#ctor(context,mapper)](#M-Backend-Controllers-UdomiteljController-#ctor-Backend-Data-BackendContext,AutoMapper-IMapper- 'Backend.Controllers.UdomiteljController.#ctor(Backend.Data.BackendContext,AutoMapper.IMapper)')
  - [Delete(sifra)](#M-Backend-Controllers-UdomiteljController-Delete-System-Int32- 'Backend.Controllers.UdomiteljController.Delete(System.Int32)')
  - [Get()](#M-Backend-Controllers-UdomiteljController-Get 'Backend.Controllers.UdomiteljController.Get')
  - [GetBySifra(sifra)](#M-Backend-Controllers-UdomiteljController-GetBySifra-System-Int32- 'Backend.Controllers.UdomiteljController.GetBySifra(System.Int32)')
  - [Post(dto)](#M-Backend-Controllers-UdomiteljController-Post-Backend-Models-DTO-UdomiteljDTOInsertUpdate- 'Backend.Controllers.UdomiteljController.Post(Backend.Models.DTO.UdomiteljDTOInsertUpdate)')
  - [Put(sifra,dto)](#M-Backend-Controllers-UdomiteljController-Put-System-Int32,Backend-Models-DTO-UdomiteljDTOInsertUpdate- 'Backend.Controllers.UdomiteljController.Put(System.Int32,Backend.Models.DTO.UdomiteljDTOInsertUpdate)')
  - [TraziUdomiteljStranicenje(stranica,uvjet)](#M-Backend-Controllers-UdomiteljController-TraziUdomiteljStranicenje-System-Int32,System-String- 'Backend.Controllers.UdomiteljController.TraziUdomiteljStranicenje(System.Int32,System.String)')
  - [TraziUdomitelja(uvjet)](#M-Backend-Controllers-UdomiteljController-TraziUdomitelja-System-String- 'Backend.Controllers.UdomiteljController.TraziUdomitelja(System.String)')
- [UdomiteljDTOInsertUpdate](#T-Backend-Models-DTO-UdomiteljDTOInsertUpdate 'Backend.Models.DTO.UdomiteljDTOInsertUpdate')
  - [#ctor(Ime,Prezime,Adresa,Telefon,Email)](#M-Backend-Models-DTO-UdomiteljDTOInsertUpdate-#ctor-System-String,System-String,System-String,System-String,System-String- 'Backend.Models.DTO.UdomiteljDTOInsertUpdate.#ctor(System.String,System.String,System.String,System.String,System.String)')
  - [Adresa](#P-Backend-Models-DTO-UdomiteljDTOInsertUpdate-Adresa 'Backend.Models.DTO.UdomiteljDTOInsertUpdate.Adresa')
  - [Email](#P-Backend-Models-DTO-UdomiteljDTOInsertUpdate-Email 'Backend.Models.DTO.UdomiteljDTOInsertUpdate.Email')
  - [Ime](#P-Backend-Models-DTO-UdomiteljDTOInsertUpdate-Ime 'Backend.Models.DTO.UdomiteljDTOInsertUpdate.Ime')
  - [Prezime](#P-Backend-Models-DTO-UdomiteljDTOInsertUpdate-Prezime 'Backend.Models.DTO.UdomiteljDTOInsertUpdate.Prezime')
  - [Telefon](#P-Backend-Models-DTO-UdomiteljDTOInsertUpdate-Telefon 'Backend.Models.DTO.UdomiteljDTOInsertUpdate.Telefon')
- [UdomiteljDTORead](#T-Backend-Models-DTO-UdomiteljDTORead 'Backend.Models.DTO.UdomiteljDTORead')
  - [#ctor(Sifra,Ime,Prezime,Adresa,Telefon,Email)](#M-Backend-Models-DTO-UdomiteljDTORead-#ctor-System-Int32,System-String,System-String,System-String,System-String,System-String- 'Backend.Models.DTO.UdomiteljDTORead.#ctor(System.Int32,System.String,System.String,System.String,System.String,System.String)')
  - [Adresa](#P-Backend-Models-DTO-UdomiteljDTORead-Adresa 'Backend.Models.DTO.UdomiteljDTORead.Adresa')
  - [Email](#P-Backend-Models-DTO-UdomiteljDTORead-Email 'Backend.Models.DTO.UdomiteljDTORead.Email')
  - [Ime](#P-Backend-Models-DTO-UdomiteljDTORead-Ime 'Backend.Models.DTO.UdomiteljDTORead.Ime')
  - [Prezime](#P-Backend-Models-DTO-UdomiteljDTORead-Prezime 'Backend.Models.DTO.UdomiteljDTORead.Prezime')
  - [Sifra](#P-Backend-Models-DTO-UdomiteljDTORead-Sifra 'Backend.Models.DTO.UdomiteljDTORead.Sifra')
  - [Telefon](#P-Backend-Models-DTO-UdomiteljDTORead-Telefon 'Backend.Models.DTO.UdomiteljDTORead.Telefon')
- [Upit](#T-Backend-Models-Upit 'Backend.Models.Upit')
  - [DatumUpita](#P-Backend-Models-Upit-DatumUpita 'Backend.Models.Upit.DatumUpita')
  - [Napomene](#P-Backend-Models-Upit-Napomene 'Backend.Models.Upit.Napomene')
  - [Pas](#P-Backend-Models-Upit-Pas 'Backend.Models.Upit.Pas')
  - [StatusUpita](#P-Backend-Models-Upit-StatusUpita 'Backend.Models.Upit.StatusUpita')
  - [Udomitelj](#P-Backend-Models-Upit-Udomitelj 'Backend.Models.Upit.Udomitelj')
- [UpitController](#T-Backend-Controllers-UpitController 'Backend.Controllers.UpitController')
  - [#ctor(context,mapper)](#M-Backend-Controllers-UpitController-#ctor-Backend-Data-BackendContext,AutoMapper-IMapper- 'Backend.Controllers.UpitController.#ctor(Backend.Data.BackendContext,AutoMapper.IMapper)')
  - [Delete(sifra)](#M-Backend-Controllers-UpitController-Delete-System-Int32- 'Backend.Controllers.UpitController.Delete(System.Int32)')
  - [Get()](#M-Backend-Controllers-UpitController-Get 'Backend.Controllers.UpitController.Get')
  - [GetBySifra(sifra)](#M-Backend-Controllers-UpitController-GetBySifra-System-Int32- 'Backend.Controllers.UpitController.GetBySifra(System.Int32)')
  - [Post(dto)](#M-Backend-Controllers-UpitController-Post-Backend-Models-DTO-UpitDTOInsertUpdate- 'Backend.Controllers.UpitController.Post(Backend.Models.DTO.UpitDTOInsertUpdate)')
  - [Put(sifra,dto)](#M-Backend-Controllers-UpitController-Put-System-Int32,Backend-Models-DTO-UpitDTOInsertUpdate- 'Backend.Controllers.UpitController.Put(System.Int32,Backend.Models.DTO.UpitDTOInsertUpdate)')
- [UpitDTORead](#T-Backend-Models-DTO-UpitDTORead 'Backend.Models.DTO.UpitDTORead')
  - [#ctor(Sifra,PasIme,UdomiteljImePrezime,DatumUpita,StatusUpita,Napomene)](#M-Backend-Models-DTO-UpitDTORead-#ctor-System-Int32,System-String,System-String,System-DateTime,System-String,System-String- 'Backend.Models.DTO.UpitDTORead.#ctor(System.Int32,System.String,System.String,System.DateTime,System.String,System.String)')
  - [DatumUpita](#P-Backend-Models-DTO-UpitDTORead-DatumUpita 'Backend.Models.DTO.UpitDTORead.DatumUpita')
  - [Napomene](#P-Backend-Models-DTO-UpitDTORead-Napomene 'Backend.Models.DTO.UpitDTORead.Napomene')
  - [PasIme](#P-Backend-Models-DTO-UpitDTORead-PasIme 'Backend.Models.DTO.UpitDTORead.PasIme')
  - [Sifra](#P-Backend-Models-DTO-UpitDTORead-Sifra 'Backend.Models.DTO.UpitDTORead.Sifra')
  - [StatusUpita](#P-Backend-Models-DTO-UpitDTORead-StatusUpita 'Backend.Models.DTO.UpitDTORead.StatusUpita')
  - [UdomiteljImePrezime](#P-Backend-Models-DTO-UpitDTORead-UdomiteljImePrezime 'Backend.Models.DTO.UpitDTORead.UdomiteljImePrezime')

<a name='T-Backend-Controllers-AutorizacijaController'></a>
## AutorizacijaController `type`

##### Namespace

Backend.Controllers

##### Summary

Kontroler za autorizaciju korisnika.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [T:Backend.Controllers.AutorizacijaController](#T-T-Backend-Controllers-AutorizacijaController 'T:Backend.Controllers.AutorizacijaController') | Kontekst baze podataka. |

##### Remarks

Inicijalizira novu instancu klase [AutorizacijaController](#T-Backend-Controllers-AutorizacijaController 'Backend.Controllers.AutorizacijaController').

<a name='M-Backend-Controllers-AutorizacijaController-#ctor-Backend-Data-BackendContext-'></a>
### #ctor(context) `constructor`

##### Summary

Kontroler za autorizaciju korisnika.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [Backend.Data.BackendContext](#T-Backend-Data-BackendContext 'Backend.Data.BackendContext') | Kontekst baze podataka. |

##### Remarks

Inicijalizira novu instancu klase [AutorizacijaController](#T-Backend-Controllers-AutorizacijaController 'Backend.Controllers.AutorizacijaController').

<a name='F-Backend-Controllers-AutorizacijaController-_context'></a>
### _context `constants`

##### Summary

Kontekst baze podataka

<a name='M-Backend-Controllers-AutorizacijaController-GenerirajToken-Backend-Models-DTO-OperaterDTO-'></a>
### GenerirajToken(operater) `method`

##### Summary

Generira token za autorizaciju.

##### Returns

JWT token ako je autorizacija uspješna, inače vraća status 403.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| operater | [Backend.Models.DTO.OperaterDTO](#T-Backend-Models-DTO-OperaterDTO 'Backend.Models.DTO.OperaterDTO') | DTO objekt koji sadrži email i lozinku operatera. |

##### Remarks

Primjer zahtjeva:

```json
{
  "email": "pero.peric@gmail.com",
  "password": "nekalozinka"
}
```

<a name='T-Backend-Tests-Controllers-AutorizacijaControllerTests'></a>
## AutorizacijaControllerTests `type`

##### Namespace

Backend.Tests.Controllers

##### Summary

Testovi za AutorizacijaController.
Ova klasa sadrži testove koji provjeravaju ispravnost generiranja JWT tokena za autorizaciju.

<a name='M-Backend-Tests-Controllers-AutorizacijaControllerTests-#ctor'></a>
### #ctor() `constructor`

##### Summary

Inicijalizira novu instancu klase AutorizacijaControllerTests.
Konfigurira kontekst baze vrsta podataka i inicijalizira kontroler.

##### Parameters

This constructor has no parameters.

<a name='M-Backend-Tests-Controllers-AutorizacijaControllerTests-GenerirajToken_InvalidEmail_ReturnsForbidden'></a>
### GenerirajToken_InvalidEmail_ReturnsForbidden() `method`

##### Summary

Testira slučaj kada uneseni email nije pronađen u bazi.
Očekuje se povrat ObjectResult sa statusom Forbidden i odgovarajućom porukom.

##### Parameters

This method has no parameters.

<a name='M-Backend-Tests-Controllers-AutorizacijaControllerTests-GenerirajToken_InvalidModel_ReturnsBadRequest'></a>
### GenerirajToken_InvalidModel_ReturnsBadRequest() `method`

##### Summary

Testira slučaj kada je model operatera neispravan (npr. nedostaje email).
Očekuje se povrat BadRequestObjectResult.

##### Parameters

This method has no parameters.

<a name='M-Backend-Tests-Controllers-AutorizacijaControllerTests-GenerirajToken_InvalidPassword_ReturnsForbidden'></a>
### GenerirajToken_InvalidPassword_ReturnsForbidden() `method`

##### Summary

Testira slučaj kada unesena lozinka ne odgovara pohranjenoj u bazi.
Očekuje se povrat ObjectResult sa statusom Forbidden i odgovarajućom porukom.

##### Parameters

This method has no parameters.

<a name='M-Backend-Tests-Controllers-AutorizacijaControllerTests-GenerirajToken_ValidCredentials_ReturnsOkWithToken'></a>
### GenerirajToken_ValidCredentials_ReturnsOkWithToken() `method`

##### Summary

Testira generiranje tokena za valjane vjerodajnice.
Očekuje se povrat OkObjectResult s ne-praznim tokenom.

##### Parameters

This method has no parameters.

<a name='T-Backend-Data-BackendContext'></a>
## BackendContext `type`

##### Namespace

Backend.Data

##### Summary

Kontekst baze podataka za aplikaciju Udomi me.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| opcije | [T:Backend.Data.BackendContext](#T-T-Backend-Data-BackendContext 'T:Backend.Data.BackendContext') | Opcije za konfiguraciju konteksta. |

##### Remarks

Konstruktor koji prima opcije za konfiguraciju konteksta.

<a name='M-Backend-Data-BackendContext-#ctor-Microsoft-EntityFrameworkCore-DbContextOptions{Backend-Data-BackendContext}-'></a>
### #ctor(opcije) `constructor`

##### Summary

Kontekst baze podataka za aplikaciju Udomi me.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| opcije | [Microsoft.EntityFrameworkCore.DbContextOptions{Backend.Data.BackendContext}](#T-Microsoft-EntityFrameworkCore-DbContextOptions{Backend-Data-BackendContext} 'Microsoft.EntityFrameworkCore.DbContextOptions{Backend.Data.BackendContext}') | Opcije za konfiguraciju konteksta. |

##### Remarks

Konstruktor koji prima opcije za konfiguraciju konteksta.

<a name='P-Backend-Data-BackendContext-Operateri'></a>
### Operateri `property`

##### Summary

Skup podataka za entitet Operater.

<a name='P-Backend-Data-BackendContext-Psi'></a>
### Psi `property`

##### Summary

Skup podataka za entitet Pas.

<a name='P-Backend-Data-BackendContext-Statusi'></a>
### Statusi `property`

##### Summary

Skup podataka za entitet Status.

<a name='P-Backend-Data-BackendContext-Udomitelji'></a>
### Udomitelji `property`

##### Summary

Skup podataka za entitet Udomitelj.

<a name='P-Backend-Data-BackendContext-Upiti'></a>
### Upiti `property`

##### Summary

Skup podataka za entitet Upit.

<a name='M-Backend-Data-BackendContext-OnModelCreating-Microsoft-EntityFrameworkCore-ModelBuilder-'></a>
### OnModelCreating(modelBuilder) `method`

##### Summary

Konfiguracija modela prilikom kreiranja baze podataka.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| modelBuilder | [Microsoft.EntityFrameworkCore.ModelBuilder](#T-Microsoft-EntityFrameworkCore-ModelBuilder 'Microsoft.EntityFrameworkCore.ModelBuilder') | Graditelj modela. |

<a name='T-Backend-Controllers-BackendController'></a>
## BackendController `type`

##### Namespace

Backend.Controllers

##### Summary

Apstraktna klasa BackendController koja služi kao osnovna klasa za sve kontrolere u aplikaciji.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [T:Backend.Controllers.BackendController](#T-T-Backend-Controllers-BackendController 'T:Backend.Controllers.BackendController') | Instanca BackendContext klase koja se koristi za pristup bazi podataka. |

<a name='M-Backend-Controllers-BackendController-#ctor-Backend-Data-BackendContext,AutoMapper-IMapper-'></a>
### #ctor(context,mapper) `constructor`

##### Summary

Apstraktna klasa BackendController koja služi kao osnovna klasa za sve kontrolere u aplikaciji.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [Backend.Data.BackendContext](#T-Backend-Data-BackendContext 'Backend.Data.BackendContext') | Instanca BackendContext klase koja se koristi za pristup bazi podataka. |
| mapper | [AutoMapper.IMapper](#T-AutoMapper-IMapper 'AutoMapper.IMapper') | Instanca IMapper sučelja koja se koristi za mapiranje objekata. |

<a name='F-Backend-Controllers-BackendController-_context'></a>
### _context `constants`

##### Summary

Kontekst baze podataka.

<a name='F-Backend-Controllers-BackendController-_mapper'></a>
### _mapper `constants`

##### Summary

Mapper za mapiranje objekata.

<a name='T-Backend-Extensions-BackendExtensions'></a>
## BackendExtensions `type`

##### Namespace

Backend.Extensions

##### Summary

Klasa koja sadrži proširenja za Udomi me aplikaciju (Backend).

<a name='M-Backend-Extensions-BackendExtensions-AddBackendCORS-Microsoft-Extensions-DependencyInjection-IServiceCollection-'></a>
### AddBackendCORS(Services) `method`

##### Summary

Dodaje konfiguraciju za CORS.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| Services | [Microsoft.Extensions.DependencyInjection.IServiceCollection](#T-Microsoft-Extensions-DependencyInjection-IServiceCollection 'Microsoft.Extensions.DependencyInjection.IServiceCollection') | Instanca IServiceCollection. |

<a name='M-Backend-Extensions-BackendExtensions-AddBackendSecurity-Microsoft-Extensions-DependencyInjection-IServiceCollection-'></a>
### AddBackendSecurity(Services) `method`

##### Summary

Dodaje konfiguraciju za sigurnost.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| Services | [Microsoft.Extensions.DependencyInjection.IServiceCollection](#T-Microsoft-Extensions-DependencyInjection-IServiceCollection 'Microsoft.Extensions.DependencyInjection.IServiceCollection') | Instanca IServiceCollection. |

<a name='M-Backend-Extensions-BackendExtensions-AddBackendSwaggerGen-Microsoft-Extensions-DependencyInjection-IServiceCollection-'></a>
### AddBackendSwaggerGen(Services) `method`

##### Summary

Dodaje konfiguraciju za Swagger dokumentaciju.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| Services | [Microsoft.Extensions.DependencyInjection.IServiceCollection](#T-Microsoft-Extensions-DependencyInjection-IServiceCollection 'Microsoft.Extensions.DependencyInjection.IServiceCollection') | Instanca IServiceCollection. |

<a name='T-Backend-Mapping-BackendMappingProfile'></a>
## BackendMappingProfile `type`

##### Namespace

Backend.Mapping

##### Summary

Klasa za definiranje mapiranja između modela i DTO objekata.

<a name='M-Backend-Mapping-BackendMappingProfile-#ctor'></a>
### #ctor() `constructor`

##### Summary

Konstruktor u kojem se definiraju mapiranja.

##### Parameters

This constructor has no parameters.

<a name='T-Backend-Models-Entitet'></a>
## Entitet `type`

##### Namespace

Backend.Models

##### Summary

Apstraktna klasa koja predstavlja entitet s jedinstvenim identifikatorom.

<a name='P-Backend-Models-Entitet-Sifra'></a>
### Sifra `property`

##### Summary

Jedinstveni identifikator entiteta.

<a name='T-Backend-Models-Operater'></a>
## Operater `type`

##### Namespace

Backend.Models

##### Summary

Operater koji se koristi za prijavu u sustav.

<a name='P-Backend-Models-Operater-Email'></a>
### Email `property`

##### Summary

Email operatera.

<a name='P-Backend-Models-Operater-Lozinka'></a>
### Lozinka `property`

##### Summary

Lozinka operatera.

<a name='T-Backend-Models-DTO-OperaterDTO'></a>
## OperaterDTO `type`

##### Namespace

Backend.Models.DTO

##### Summary

DTO (Data Transfer Object) za operatera.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| Email | [T:Backend.Models.DTO.OperaterDTO](#T-T-Backend-Models-DTO-OperaterDTO 'T:Backend.Models.DTO.OperaterDTO') |  |

<a name='M-Backend-Models-DTO-OperaterDTO-#ctor-System-String,System-String-'></a>
### #ctor(Email,Password) `constructor`

##### Summary

DTO (Data Transfer Object) za operatera.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| Email | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| Password | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='P-Backend-Models-DTO-OperaterDTO-Email'></a>
### Email `property`

##### Summary



<a name='P-Backend-Models-DTO-OperaterDTO-Password'></a>
### Password `property`

##### Summary



<a name='T-Backend-Models-Pas'></a>
## Pas `type`

##### Namespace

Backend.Models

##### Summary

Klasa koja predstavlja psa.

<a name='P-Backend-Models-Pas-BrojCipa'></a>
### BrojCipa `property`

##### Summary

Broj čipa psa.

<a name='P-Backend-Models-Pas-Datum_Rodjenja'></a>
### Datum_Rodjenja `property`

##### Summary

Datum rođenja psa.

<a name='P-Backend-Models-Pas-Ime'></a>
### Ime `property`

##### Summary

Ime psa.

<a name='P-Backend-Models-Pas-Kastracija'></a>
### Kastracija `property`

##### Summary

Kastracija psa.

<a name='P-Backend-Models-Pas-Opis'></a>
### Opis `property`

##### Summary

Opis psa.

<a name='P-Backend-Models-Pas-Spol'></a>
### Spol `property`

##### Summary

Spol psa.

<a name='P-Backend-Models-Pas-Status'></a>
### Status `property`

##### Summary

Status psa (vanjski ključ).

<a name='T-Backend-Controllers-PasController'></a>
## PasController `type`

##### Namespace

Backend.Controllers

##### Summary

Kontroler za upravljanje psima.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [T:Backend.Controllers.PasController](#T-T-Backend-Controllers-PasController 'T:Backend.Controllers.PasController') | Kontekst baze podataka. |

<a name='M-Backend-Controllers-PasController-#ctor-Backend-Data-BackendContext,AutoMapper-IMapper-'></a>
### #ctor(context,mapper) `constructor`

##### Summary

Kontroler za upravljanje psima.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [Backend.Data.BackendContext](#T-Backend-Data-BackendContext 'Backend.Data.BackendContext') | Kontekst baze podataka. |
| mapper | [AutoMapper.IMapper](#T-AutoMapper-IMapper 'AutoMapper.IMapper') | Mapper za mapiranje objekata. |

<a name='M-Backend-Controllers-PasController-Delete-System-Int32-'></a>
### Delete(sifra) `method`

##### Summary

Briše psa prema šifri.

##### Returns

Status brisanja.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sifra | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Šifra psa. |

<a name='M-Backend-Controllers-PasController-Get'></a>
### Get() `method`

##### Summary

Dohvaća sve pse.

##### Returns

Lista pasa.

##### Parameters

This method has no parameters.

<a name='M-Backend-Controllers-PasController-GetBySifra-System-Int32-'></a>
### GetBySifra(sifra) `method`

##### Summary

Dohvaća psa prema šifri.

##### Returns

Pas.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sifra | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Šifra psa. |

<a name='M-Backend-Controllers-PasController-Post-Backend-Models-DTO-PasDTOInsertUpdate-'></a>
### Post(dto) `method`

##### Summary

Dodaje novog psa.

##### Returns

Status kreiranja.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dto | [Backend.Models.DTO.PasDTOInsertUpdate](#T-Backend-Models-DTO-PasDTOInsertUpdate 'Backend.Models.DTO.PasDTOInsertUpdate') | Podatci o psu. |

<a name='M-Backend-Controllers-PasController-Put-System-Int32,Backend-Models-DTO-PasDTOInsertUpdate-'></a>
### Put(sifra,dto) `method`

##### Summary

Ažurira psa prema šifri.

##### Returns

Status ažuriranja.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sifra | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Šifra psa. |
| dto | [Backend.Models.DTO.PasDTOInsertUpdate](#T-Backend-Models-DTO-PasDTOInsertUpdate 'Backend.Models.DTO.PasDTOInsertUpdate') | Podaci o psu. |

<a name='M-Backend-Controllers-PasController-TraziPsa-System-String-'></a>
### TraziPsa(uvjet) `method`

##### Summary

Traži pse prema uvjetu.

##### Returns

Lista pasa.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| uvjet | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Uvjet pretrage. |

<a name='T-Backend-Models-DTO-PasDTOInsertUpdate'></a>
## PasDTOInsertUpdate `type`

##### Namespace

Backend.Models.DTO

##### Summary

DTO za unos i ažuriranje podataka o psu.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| Ime | [T:Backend.Models.DTO.PasDTOInsertUpdate](#T-T-Backend-Models-DTO-PasDTOInsertUpdate 'T:Backend.Models.DTO.PasDTOInsertUpdate') | Ime psa (obavezno). |

<a name='M-Backend-Models-DTO-PasDTOInsertUpdate-#ctor-System-String,System-String,System-DateTime,System-String,System-String,System-Boolean,System-Int32-'></a>
### #ctor(Ime,BrojCipa,Datum_Rodjenja,Spol,Opis,Kastracija,StatusSifra) `constructor`

##### Summary

DTO za unos i ažuriranje podataka o psu.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| Ime | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Ime psa (obavezno). |
| BrojCipa | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Broj čipa psa (obavezno, započinje s HR i ima 15 znamenki). |
| Datum_Rodjenja | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | Datum rođenja psa. |
| Spol | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Spol psa (muški ili ženski). |
| Opis | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Opis psa. |
| Kastracija | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Indikator je li pas kastriran ili ne. |
| StatusSifra | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Status psa. |

<a name='P-Backend-Models-DTO-PasDTOInsertUpdate-BrojCipa'></a>
### BrojCipa `property`

##### Summary

Broj čipa psa (obavezno, započinje s HR i ima 15 znamenki).

<a name='P-Backend-Models-DTO-PasDTOInsertUpdate-Datum_Rodjenja'></a>
### Datum_Rodjenja `property`

##### Summary

Datum rođenja psa.

<a name='P-Backend-Models-DTO-PasDTOInsertUpdate-Ime'></a>
### Ime `property`

##### Summary

Ime psa (obavezno).

<a name='P-Backend-Models-DTO-PasDTOInsertUpdate-Kastracija'></a>
### Kastracija `property`

##### Summary

Indikator je li pas kastriran ili ne.

<a name='P-Backend-Models-DTO-PasDTOInsertUpdate-Opis'></a>
### Opis `property`

##### Summary

Opis psa.

<a name='P-Backend-Models-DTO-PasDTOInsertUpdate-Spol'></a>
### Spol `property`

##### Summary

Spol psa (muški ili ženski).

<a name='P-Backend-Models-DTO-PasDTOInsertUpdate-StatusSifra'></a>
### StatusSifra `property`

##### Summary

Status psa.

<a name='T-Backend-Models-DTO-PasDTORead'></a>
## PasDTORead `type`

##### Namespace

Backend.Models.DTO

##### Summary

DTO za čitanje podataka o psu.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| Sifra | [T:Backend.Models.DTO.PasDTORead](#T-T-Backend-Models-DTO-PasDTORead 'T:Backend.Models.DTO.PasDTORead') | Jedinstvena šifra psa. |

<a name='M-Backend-Models-DTO-PasDTORead-#ctor-System-Int32,System-String,System-String,System-DateTime,System-String,System-String,System-Boolean,System-String-'></a>
### #ctor(Sifra,Ime,BrojCipa,Datum_Rodjenja,Spol,Opis,Kastracija,StatusNaziv) `constructor`

##### Summary

DTO za čitanje podataka o psu.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| Sifra | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Jedinstvena šifra psa. |
| Ime | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Ime psa. |
| BrojCipa | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Broj čipa psa. |
| Datum_Rodjenja | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | Datum rođenja psa. |
| Spol | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Spol psa. |
| Opis | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Opis psa. |
| Kastracija | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Indikator je li pas kastriran ili ne. |
| StatusNaziv | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Naziv statusa psa. |

<a name='P-Backend-Models-DTO-PasDTORead-BrojCipa'></a>
### BrojCipa `property`

##### Summary

Broj čipa psa.

<a name='P-Backend-Models-DTO-PasDTORead-Datum_Rodjenja'></a>
### Datum_Rodjenja `property`

##### Summary

Datum rođenja psa.

<a name='P-Backend-Models-DTO-PasDTORead-Ime'></a>
### Ime `property`

##### Summary

Ime psa.

<a name='P-Backend-Models-DTO-PasDTORead-Kastracija'></a>
### Kastracija `property`

##### Summary

Indikator je li pas kastriran ili ne.

<a name='P-Backend-Models-DTO-PasDTORead-Opis'></a>
### Opis `property`

##### Summary

Opis psa.

<a name='P-Backend-Models-DTO-PasDTORead-Sifra'></a>
### Sifra `property`

##### Summary

Jedinstvena šifra psa.

<a name='P-Backend-Models-DTO-PasDTORead-Spol'></a>
### Spol `property`

##### Summary

Spol psa.

<a name='P-Backend-Models-DTO-PasDTORead-StatusNaziv'></a>
### StatusNaziv `property`

##### Summary

Naziv statusa psa.

<a name='T-Backend-Controllers-PocetnaController'></a>
## PocetnaController `type`

##### Namespace

Backend.Controllers

##### Summary

Kontroler za početne operacije.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| _context | [T:Backend.Controllers.PocetnaController](#T-T-Backend-Controllers-PocetnaController 'T:Backend.Controllers.PocetnaController') | Kontekst baze podataka. |

<a name='M-Backend-Controllers-PocetnaController-#ctor-Backend-Data-BackendContext,AutoMapper-IMapper-'></a>
### #ctor(_context) `constructor`

##### Summary

Kontroler za početne operacije.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| _context | [Backend.Data.BackendContext](#T-Backend-Data-BackendContext 'Backend.Data.BackendContext') | Kontekst baze podataka. |

<a name='M-Backend-Controllers-PocetnaController-DostupniPsi'></a>
### DostupniPsi() `method`

##### Summary

Dohvaća sve pse iz baze.

##### Returns

Popis svih pasa u bazi.

##### Parameters

This method has no parameters.

<a name='M-Backend-Controllers-PocetnaController-TraziStranicenje-System-Int32-'></a>
### TraziStranicenje(stranica) `method`

##### Summary

Traži pse sa statusom "slobodan" ili "privremeni smještaj" te ih prikazuje s paginacijom.

##### Returns

Objekt koji sadrži listu pasa i ukupan broj stranica.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| stranica | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Broj stranice (počinje od 1). |

<a name='M-Backend-Controllers-PocetnaController-TraziUdomljenePse'></a>
### TraziUdomljenePse() `method`

##### Summary

Traži udomljene pse te utvrđuje njihov broj.

##### Returns

Broj udomljenih pasa.

##### Parameters

This method has no parameters.

<a name='T-Backend-Models-Status'></a>
## Status `type`

##### Namespace

Backend.Models

##### Summary

Klasa koja predstavlja status.

<a name='P-Backend-Models-Status-Naziv'></a>
### Naziv `property`

##### Summary

Naziv statusa.

<a name='T-Backend-Controllers-StatusController'></a>
## StatusController `type`

##### Namespace

Backend.Controllers

##### Summary

Kontroler za upravljanje statusima.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [T:Backend.Controllers.StatusController](#T-T-Backend-Controllers-StatusController 'T:Backend.Controllers.StatusController') | Kontekst baze podataka. |

<a name='M-Backend-Controllers-StatusController-#ctor-Backend-Data-BackendContext,AutoMapper-IMapper-'></a>
### #ctor(context,mapper) `constructor`

##### Summary

Kontroler za upravljanje statusima.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [Backend.Data.BackendContext](#T-Backend-Data-BackendContext 'Backend.Data.BackendContext') | Kontekst baze podataka. |
| mapper | [AutoMapper.IMapper](#T-AutoMapper-IMapper 'AutoMapper.IMapper') | Mapper za mapiranje objekata. |

<a name='M-Backend-Controllers-StatusController-Delete-System-Int32-'></a>
### Delete(sifra) `method`

##### Summary

Briše status prema šifri.

##### Returns

Status brisanja.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sifra | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Šifra statusa. |

<a name='M-Backend-Controllers-StatusController-Get'></a>
### Get() `method`

##### Summary

Dohvaća sve statuse.

##### Returns

Lista statusa.

##### Parameters

This method has no parameters.

<a name='M-Backend-Controllers-StatusController-GetBySifra-System-Int32-'></a>
### GetBySifra(sifra) `method`

##### Summary

Dohvaća status prema šifri.

##### Returns

Status.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sifra | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Šifra statusa. |

<a name='M-Backend-Controllers-StatusController-Post-Backend-Models-DTO-StatusDTOInsertUpdate-'></a>
### Post(dto) `method`

##### Summary

Dodaje novi status.

##### Returns

Status kreiranja.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dto | [Backend.Models.DTO.StatusDTOInsertUpdate](#T-Backend-Models-DTO-StatusDTOInsertUpdate 'Backend.Models.DTO.StatusDTOInsertUpdate') | Podaci o statusu. |

<a name='M-Backend-Controllers-StatusController-Put-System-Int32,Backend-Models-DTO-StatusDTOInsertUpdate-'></a>
### Put(sifra,dto) `method`

##### Summary

Ažurira status prema šifri.

##### Returns

Status ažuriranja.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sifra | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Šifra statusa. |
| dto | [Backend.Models.DTO.StatusDTOInsertUpdate](#T-Backend-Models-DTO-StatusDTOInsertUpdate 'Backend.Models.DTO.StatusDTOInsertUpdate') | Podaci o statusu. |

<a name='T-Backend-Models-DTO-StatusDTOInsertUpdate'></a>
## StatusDTOInsertUpdate `type`

##### Namespace

Backend.Models.DTO

##### Summary

DTO za unos i ažuriranje statusa.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| Naziv | [T:Backend.Models.DTO.StatusDTOInsertUpdate](#T-T-Backend-Models-DTO-StatusDTOInsertUpdate 'T:Backend.Models.DTO.StatusDTOInsertUpdate') | Naziv statusa |

<a name='M-Backend-Models-DTO-StatusDTOInsertUpdate-#ctor-System-String-'></a>
### #ctor(Naziv) `constructor`

##### Summary

DTO za unos i ažuriranje statusa.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| Naziv | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Naziv statusa |

<a name='P-Backend-Models-DTO-StatusDTOInsertUpdate-Naziv'></a>
### Naziv `property`

##### Summary

Naziv statusa

<a name='T-Backend-Models-DTO-StatusDTORead'></a>
## StatusDTORead `type`

##### Namespace

Backend.Models.DTO

##### Summary

DTO za čitanje podataka o statusu.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| Sifra | [T:Backend.Models.DTO.StatusDTORead](#T-T-Backend-Models-DTO-StatusDTORead 'T:Backend.Models.DTO.StatusDTORead') | Jedinstvena šifra statusa. |

<a name='M-Backend-Models-DTO-StatusDTORead-#ctor-System-Int32,System-String-'></a>
### #ctor(Sifra,Naziv) `constructor`

##### Summary

DTO za čitanje podataka o statusu.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| Sifra | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Jedinstvena šifra statusa. |
| Naziv | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Naziv statusa. |

<a name='P-Backend-Models-DTO-StatusDTORead-Naziv'></a>
### Naziv `property`

##### Summary

Naziv statusa.

<a name='P-Backend-Models-DTO-StatusDTORead-Sifra'></a>
### Sifra `property`

##### Summary

Jedinstvena šifra statusa.

<a name='T-Backend-Models-Udomitelj'></a>
## Udomitelj `type`

##### Namespace

Backend.Models

##### Summary

Klasa koja predstavlja udomitelja.

<a name='P-Backend-Models-Udomitelj-Adresa'></a>
### Adresa `property`

##### Summary

Adresa udomitelja.

<a name='P-Backend-Models-Udomitelj-Email'></a>
### Email `property`

##### Summary

Email udomitelja.

<a name='P-Backend-Models-Udomitelj-Ime'></a>
### Ime `property`

##### Summary

Ime udomitelja.

<a name='P-Backend-Models-Udomitelj-Prezime'></a>
### Prezime `property`

##### Summary

Prezime udomitelja.

<a name='P-Backend-Models-Udomitelj-Telefon'></a>
### Telefon `property`

##### Summary

Telefon udomitelja.

<a name='T-Backend-Controllers-UdomiteljController'></a>
## UdomiteljController `type`

##### Namespace

Backend.Controllers

##### Summary

Kontroler za upravljanje udomiteljima u aplikaciji.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [T:Backend.Controllers.UdomiteljController](#T-T-Backend-Controllers-UdomiteljController 'T:Backend.Controllers.UdomiteljController') | Kontekst baze podataka. |

<a name='M-Backend-Controllers-UdomiteljController-#ctor-Backend-Data-BackendContext,AutoMapper-IMapper-'></a>
### #ctor(context,mapper) `constructor`

##### Summary

Kontroler za upravljanje udomiteljima u aplikaciji.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [Backend.Data.BackendContext](#T-Backend-Data-BackendContext 'Backend.Data.BackendContext') | Kontekst baze podataka. |
| mapper | [AutoMapper.IMapper](#T-AutoMapper-IMapper 'AutoMapper.IMapper') | Mapper za mapiranje objekata. |

<a name='M-Backend-Controllers-UdomiteljController-Delete-System-Int32-'></a>
### Delete(sifra) `method`

##### Summary

Briše udomitelja prema šifri.

##### Returns

Status brisanja.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sifra | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Šifra udomitelja. |

<a name='M-Backend-Controllers-UdomiteljController-Get'></a>
### Get() `method`

##### Summary

Dohvaća sve udomitelje.

##### Returns

Lista udomitelja.

##### Parameters

This method has no parameters.

<a name='M-Backend-Controllers-UdomiteljController-GetBySifra-System-Int32-'></a>
### GetBySifra(sifra) `method`

##### Summary

Dohvaća udomitelja prema šifri.

##### Returns

Udomitelj.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sifra | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Šifra udomitelja. |

<a name='M-Backend-Controllers-UdomiteljController-Post-Backend-Models-DTO-UdomiteljDTOInsertUpdate-'></a>
### Post(dto) `method`

##### Summary

Dodaje novog udomitelja.

##### Returns

Status kreiranja.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dto | [Backend.Models.DTO.UdomiteljDTOInsertUpdate](#T-Backend-Models-DTO-UdomiteljDTOInsertUpdate 'Backend.Models.DTO.UdomiteljDTOInsertUpdate') | Podaci o udomitelju. |

<a name='M-Backend-Controllers-UdomiteljController-Put-System-Int32,Backend-Models-DTO-UdomiteljDTOInsertUpdate-'></a>
### Put(sifra,dto) `method`

##### Summary

Ažurira udomitelja prema šifri.

##### Returns

Status ažuriranja.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sifra | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Šifra udomitelja. |
| dto | [Backend.Models.DTO.UdomiteljDTOInsertUpdate](#T-Backend-Models-DTO-UdomiteljDTOInsertUpdate 'Backend.Models.DTO.UdomiteljDTOInsertUpdate') | Podatci o udomitelju. |

<a name='M-Backend-Controllers-UdomiteljController-TraziUdomiteljStranicenje-System-Int32,System-String-'></a>
### TraziUdomiteljStranicenje(stranica,uvjet) `method`

##### Summary

Traži udomitelje s paginacijom.

##### Returns

Lista udomitelja.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| stranica | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Broj stranice. |
| uvjet | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Uvjet pretrage. |

<a name='M-Backend-Controllers-UdomiteljController-TraziUdomitelja-System-String-'></a>
### TraziUdomitelja(uvjet) `method`

##### Summary

Traži udomitelja prema uvjetu.

##### Returns

Traženi udomitelj

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| uvjet | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Uvjet |

<a name='T-Backend-Models-DTO-UdomiteljDTOInsertUpdate'></a>
## UdomiteljDTOInsertUpdate `type`

##### Namespace

Backend.Models.DTO

##### Summary

DTO za unos i ažuriranje udomitelja.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| Ime | [T:Backend.Models.DTO.UdomiteljDTOInsertUpdate](#T-T-Backend-Models-DTO-UdomiteljDTOInsertUpdate 'T:Backend.Models.DTO.UdomiteljDTOInsertUpdate') | Ime udomitelja (obavezno) |

<a name='M-Backend-Models-DTO-UdomiteljDTOInsertUpdate-#ctor-System-String,System-String,System-String,System-String,System-String-'></a>
### #ctor(Ime,Prezime,Adresa,Telefon,Email) `constructor`

##### Summary

DTO za unos i ažuriranje udomitelja.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| Ime | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Ime udomitelja (obavezno) |
| Prezime | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Prezime udomitelja (obavezno) |
| Adresa | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Adresa udomitelja (obavezno) |
| Telefon | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Telefon udomitelja (obavezno) |
| Email | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Email udomitelja (obavezno, u ispravnom formatu) |

<a name='P-Backend-Models-DTO-UdomiteljDTOInsertUpdate-Adresa'></a>
### Adresa `property`

##### Summary

Adresa udomitelja (obavezno)

<a name='P-Backend-Models-DTO-UdomiteljDTOInsertUpdate-Email'></a>
### Email `property`

##### Summary

Email udomitelja (obavezno, u ispravnom formatu)

<a name='P-Backend-Models-DTO-UdomiteljDTOInsertUpdate-Ime'></a>
### Ime `property`

##### Summary

Ime udomitelja (obavezno)

<a name='P-Backend-Models-DTO-UdomiteljDTOInsertUpdate-Prezime'></a>
### Prezime `property`

##### Summary

Prezime udomitelja (obavezno)

<a name='P-Backend-Models-DTO-UdomiteljDTOInsertUpdate-Telefon'></a>
### Telefon `property`

##### Summary

Telefon udomitelja (obavezno)

<a name='T-Backend-Models-DTO-UdomiteljDTORead'></a>
## UdomiteljDTORead `type`

##### Namespace

Backend.Models.DTO

##### Summary

DTO za čitanje podataka o udomitelju.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| Sifra | [T:Backend.Models.DTO.UdomiteljDTORead](#T-T-Backend-Models-DTO-UdomiteljDTORead 'T:Backend.Models.DTO.UdomiteljDTORead') | Jedinstvena šifra udomitelja. |

<a name='M-Backend-Models-DTO-UdomiteljDTORead-#ctor-System-Int32,System-String,System-String,System-String,System-String,System-String-'></a>
### #ctor(Sifra,Ime,Prezime,Adresa,Telefon,Email) `constructor`

##### Summary

DTO za čitanje podataka o udomitelju.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| Sifra | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Jedinstvena šifra udomitelja. |
| Ime | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Ime udomitelja. |
| Prezime | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Prezime udomitelja. |
| Adresa | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Adresa udomitelja. |
| Telefon | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Telefon udomitelja. |
| Email | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Email udomitelja. |

<a name='P-Backend-Models-DTO-UdomiteljDTORead-Adresa'></a>
### Adresa `property`

##### Summary

Adresa udomitelja.

<a name='P-Backend-Models-DTO-UdomiteljDTORead-Email'></a>
### Email `property`

##### Summary

Email udomitelja.

<a name='P-Backend-Models-DTO-UdomiteljDTORead-Ime'></a>
### Ime `property`

##### Summary

Ime udomitelja.

<a name='P-Backend-Models-DTO-UdomiteljDTORead-Prezime'></a>
### Prezime `property`

##### Summary

Prezime udomitelja.

<a name='P-Backend-Models-DTO-UdomiteljDTORead-Sifra'></a>
### Sifra `property`

##### Summary

Jedinstvena šifra udomitelja.

<a name='P-Backend-Models-DTO-UdomiteljDTORead-Telefon'></a>
### Telefon `property`

##### Summary

Telefon udomitelja.

<a name='T-Backend-Models-Upit'></a>
## Upit `type`

##### Namespace

Backend.Models

##### Summary

Klasa koja predstavlja upit.

<a name='P-Backend-Models-Upit-DatumUpita'></a>
### DatumUpita `property`

##### Summary

Datum upita.

<a name='P-Backend-Models-Upit-Napomene'></a>
### Napomene `property`

##### Summary

Napomene.

<a name='P-Backend-Models-Upit-Pas'></a>
### Pas `property`

##### Summary

Pas za kojeg je postavljen upit.

<a name='P-Backend-Models-Upit-StatusUpita'></a>
### StatusUpita `property`

##### Summary

Status upita.

<a name='P-Backend-Models-Upit-Udomitelj'></a>
### Udomitelj `property`

##### Summary

Udomitelj koji postavlja upit.

<a name='T-Backend-Controllers-UpitController'></a>
## UpitController `type`

##### Namespace

Backend.Controllers

##### Summary

Kontroler za upravljanje upitima u aplikaciji.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [T:Backend.Controllers.UpitController](#T-T-Backend-Controllers-UpitController 'T:Backend.Controllers.UpitController') | Instanca BackendContext klase koja se koristi za pristup bazi podataka. |

<a name='M-Backend-Controllers-UpitController-#ctor-Backend-Data-BackendContext,AutoMapper-IMapper-'></a>
### #ctor(context,mapper) `constructor`

##### Summary

Kontroler za upravljanje upitima u aplikaciji.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [Backend.Data.BackendContext](#T-Backend-Data-BackendContext 'Backend.Data.BackendContext') | Instanca BackendContext klase koja se koristi za pristup bazi podataka. |
| mapper | [AutoMapper.IMapper](#T-AutoMapper-IMapper 'AutoMapper.IMapper') | Instanca IMapper sučelja koja se koristi za mapiranje objekata. |

<a name='M-Backend-Controllers-UpitController-Delete-System-Int32-'></a>
### Delete(sifra) `method`

##### Summary

Briše upit prema šifri.

##### Returns

Status brisanja.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sifra | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Šifra upita. |

<a name='M-Backend-Controllers-UpitController-Get'></a>
### Get() `method`

##### Summary

Dohvaća sve upite.

##### Returns

Lista upita.

##### Parameters

This method has no parameters.

<a name='M-Backend-Controllers-UpitController-GetBySifra-System-Int32-'></a>
### GetBySifra(sifra) `method`

##### Summary

Dohvaća upit prema šifri.

##### Returns

Upit sa zadanom šifrom.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sifra | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Šifra upita. |

<a name='M-Backend-Controllers-UpitController-Post-Backend-Models-DTO-UpitDTOInsertUpdate-'></a>
### Post(dto) `method`

##### Summary

Dodaje novi upit.

##### Returns

Status kreiranja.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dto | [Backend.Models.DTO.UpitDTOInsertUpdate](#T-Backend-Models-DTO-UpitDTOInsertUpdate 'Backend.Models.DTO.UpitDTOInsertUpdate') | Podaci o upitu. |

<a name='M-Backend-Controllers-UpitController-Put-System-Int32,Backend-Models-DTO-UpitDTOInsertUpdate-'></a>
### Put(sifra,dto) `method`

##### Summary

Ažurira postojeći upit.

##### Returns

Status ažuriranja.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sifra | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Šifra upita. |
| dto | [Backend.Models.DTO.UpitDTOInsertUpdate](#T-Backend-Models-DTO-UpitDTOInsertUpdate 'Backend.Models.DTO.UpitDTOInsertUpdate') | Podaci o upitu. |

<a name='T-Backend-Models-DTO-UpitDTORead'></a>
## UpitDTORead `type`

##### Namespace

Backend.Models.DTO

##### Summary

DTO za čitanje podataka o upitu.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| Sifra | [T:Backend.Models.DTO.UpitDTORead](#T-T-Backend-Models-DTO-UpitDTORead 'T:Backend.Models.DTO.UpitDTORead') | Jedinstveni identifikator upita. |

<a name='M-Backend-Models-DTO-UpitDTORead-#ctor-System-Int32,System-String,System-String,System-DateTime,System-String,System-String-'></a>
### #ctor(Sifra,PasIme,UdomiteljImePrezime,DatumUpita,StatusUpita,Napomene) `constructor`

##### Summary

DTO za čitanje podataka o upitu.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| Sifra | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Jedinstveni identifikator upita. |
| PasIme | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Ime psa. |
| UdomiteljImePrezime | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Ime i prezime udomitelja. |
| DatumUpita | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | Datum upita. |
| StatusUpita | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Status upita. |
| Napomene | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Napomene. |

<a name='P-Backend-Models-DTO-UpitDTORead-DatumUpita'></a>
### DatumUpita `property`

##### Summary

Datum upita.

<a name='P-Backend-Models-DTO-UpitDTORead-Napomene'></a>
### Napomene `property`

##### Summary

Napomene.

<a name='P-Backend-Models-DTO-UpitDTORead-PasIme'></a>
### PasIme `property`

##### Summary

Ime psa.

<a name='P-Backend-Models-DTO-UpitDTORead-Sifra'></a>
### Sifra `property`

##### Summary

Jedinstveni identifikator upita.

<a name='P-Backend-Models-DTO-UpitDTORead-StatusUpita'></a>
### StatusUpita `property`

##### Summary

Status upita.

<a name='P-Backend-Models-DTO-UpitDTORead-UdomiteljImePrezime'></a>
### UdomiteljImePrezime `property`

##### Summary

Ime i prezime udomitelja.
