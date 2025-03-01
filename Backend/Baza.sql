-- Zamjeniti db_a98acf_edunovawp5 s imenom svoje baze

SELECT name, collation_name FROM sys.databases;
GO
ALTER DATABASE db_ab2678_udomime SET SINGLE_USER WITH
ROLLBACK IMMEDIATE;
GO
ALTER DATABASE db_ab2678_udomime COLLATE Croatian_CI_AS;
GO
ALTER DATABASE db_ab2678_udomime SET MULTI_USER;
GO
SELECT name, collation_name FROM sys.databases;
GO


--TABLICA OPERATER

create table operateri(
sifra int not null primary key identity(1,1),
email varchar(50) not null,
lozinka varchar(200) not null
);

-- Lozinka generirana pomoću https://bcrypt-generator.com/
insert into operateri values ('bernarda.lusch@gmail.com',
'$2a$12$JxoxH7uwGv4ErLM7.aPAdenSouiqGwsAWKmTgzIerj3WOFZ6ZnCJm');


--TABLICA STATUS

create table statusi(
sifra int not null primary key identity(1,1),
naziv varchar(30) not null
);





--TABLICA PSI

create table psi(
sifra int not null primary key identity(1,1),
brojcipa char(17) not null,
ime varchar(20) not null,
datum_rodjenja date not null,
spol varchar(10) not null,
CONSTRAINT chk_spol CHECK (spol IN ('muški', 'ženski')),
opis varchar(500) not null,
status int not null references statusi(sifra),
kastracija bit not null,
);

--TABLICA UDOMITELJI

create table udomitelji(
sifra int not null primary key identity(1,1),
ime varchar(20) not null,
prezime varchar(30) not null,
adresa varchar(30) not null,
telefon char(20) not null,
email varchar(30) not null
);

--TABLICA UPITI

create table upiti(
sifra int not null primary key identity(1,1),
pas int not null references psi(sifra),
udomitelj int not null references udomitelji(sifra),
datum_upita date not null,
status_upita varchar(30) not null,
napomene varchar(200)
);

insert into statusi(naziv) values
('udomljen'),
('rezerviran'),
('slobodan'),
('privremeni smještaj');


insert into psi(brojcipa, ime, datum_rodjenja, spol, opis, kastracija, status) VALUES
('HR123456789012345', 'Max', '2010-08-15', 'muški', 'Max je energičan pas koji voli igru i šetnje.', 1, 3),
('HR234567890123456', 'Luna', '2015-01-20', 'ženski',  'Luna je mirna i nježna, voli biti u društvu ljudi.', 1, 3),
('HR345678901234567', 'Leo', '2010-06-30', 'muški',  'Leo je veliki pas koji traži puno prostora i igre.', 1, 3),
('HR456789012345678', 'Bella', '2013-03-12', 'ženski',  'Bella je vesela i hrabra, uvijek spremna za avanture.', 1, 1),
('HR567890123456789', 'Rex', '2011-11-09', 'muški',  'Rex je zaštitnički nastrojen pas, idealan za obitelj.', 1, 3),
('HR678901234567890', 'Maja', '2017-02-05', 'ženski', 'Maja je umiljata i voli biti u centru pažnje.', 1, 2),
('HR789012345678901', 'Bruno', '2012-07-22', 'muški',  'Bruno je prijateljski nastrojen pas koji voli sve ljude.', 0, 3),
('HR890123456789012', 'Zara', '2016-04-11', 'ženski',  'Zara je vesela i razigrana, često trči po dvorištu.', 1, 3),
('HR901234567890123', 'Oscar', '2009-10-02', 'muški',  'Oscar je smiren pas koji voli dugi odmor.', 1, 4),
('HR012345678901234', 'Nina', '2018-06-17', 'ženski',  'Nina je ljubazna i voli društvo drugih pasa.', 0, 1);


insert into udomitelji(ime, prezime, adresa, telefon, email)
VALUES
('Ivan', 'Horvat', 'Osijek, Ulica Mije Đaka 12', '385 98 1234567', 'ivan.horvat@gmail.com'),
('Ana', 'Kovač', 'Vinkovci, Trg Kneza Mihajla 3', '385 91 2345678', 'ana.kovac@gmail.com'),
('Marko', 'Novak', 'Valpovo, Kralja Zvonimira 45', '385 98 3456789', 'marko.novak@gmail.com'),
('Lina', 'Petrović', 'Osijek, Stjepana Radića 14', '385 91 4567890', 'lina.petrovic@gmail.com'),
('Josip', 'Matić', 'Vinkovci, Kolodvorska 9', '385 98 5678901', 'josip.matic@gmail.com'),
('Maja', 'Šimić', 'Valpovo, Dravska 19', '385 91 6789012', 'maja.simic@gmail.com'),
('Tomislav', 'Jurić', 'Osijek, Antuna Mihanovića 7', '385 98 7890123', 'tomislav.juric@gmail.com'),
('Ivana', 'Babić', 'Vinkovci, I.G.Kovačića 25', '385 91 8901234', 'ivana.babic@gmail.com'),
('Petar', 'Soldo', 'Valpovo, Zagrebačka 34', '385 98 9012345', 'petar.soldo@gmail.com'),
('Jelena', 'Vuković', 'Osijek, Trg Sv. Trojstva 8', '385 91 0123456', 'jelena.vukovic@gmail.com');

INSERT INTO upiti (pas, udomitelj, datum_upita, status_upita, napomene)
VALUES
(3, 7, '2024-06-15', 'zaprimljen', 'nema napomene'),
(9, 2, '2024-08-10', 'u obradi', 'nema napomene'),
(1, 5, '2024-07-05', 'obrađen', 'udomljenje'),
(7, 4, '2024-04-20', 'zaprimljen', 'nema napomene'),
(5, 6, '2024-01-25', 'u obradi', 'nema napomene'),
(8, 3, '2024-02-10', 'zaprimljen', 'nema napomene'),
(2, 10, '2024-05-18', 'obrađen', 'na čekanju'),
(10, 1, '2024-03-12', 'zaprimljen', 'nema napomene'),
(4, 9, '2024-06-22', 'u obradi', 'nema napomene'),
(6, 8, '2024-04-15', 'obrađen', 'odbijeno');

