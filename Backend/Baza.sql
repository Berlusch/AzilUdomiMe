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



--TABLICA STATUS

create table status(
sifra int not null primary key identity(1,1),
naziv varchar(30) not null
);

--TABLICA VELI�INA

create table velicina(
sifra int not null primary key identity(1,1),
naziv varchar(30)
);

--TABLICA BOJA

create table boja(
sifra int not null primary key identity(1,1),
naziv varchar(10)
);

--TABLICA PSI

create table psi(
sifra int not null primary key identity(1,1),
brojcipa char(17) not null,
ime varchar(20) not null,
datum_rodjenja date not null,
spol varchar(10) not null,
CONSTRAINT chk_spol CHECK (spol IN ('mu�ki', '�enski')),
velicina int not null references velicina(sifra),
boja int not null references boja(sifra),
mojaprica varchar(300) not null,
status int not null references status(sifra),
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

insert into status(naziv) values
('udomljen'),
('rezerviran'),
('slobodan'),
('privremeni smje�taj');

insert into velicina(naziv) values
('mali: do 5 kg'),
('srednji: 5-25 kg'),
('veliki: >25 kg');

insert into boja(naziv) values
('bijeli'),
('crni'),
('sme�i'),
('�areni');

insert into psi(brojcipa, ime, datum_rodjenja, spol, velicina, boja, mojaprica, kastracija, status) VALUES
('HR123456789012345', 'Max', '2010-08-15', 'mu�ki', 2, 1, 'Max je energi�an pas koji voli igru i �etnje.', 1, 3),
('HR234567890123456', 'Luna', '2015-01-20', '�enski', 1, 2, 'Luna je mirna i nje�na, voli biti u dru�tvu ljudi.', 1, 3),
('HR345678901234567', 'Leo', '2010-06-30', 'mu�ki', 3, 3, 'Leo je veliki pas koji tra�i puno prostora i igre.', 1, 3),
('HR456789012345678', 'Bella', '2013-03-12', '�enski', 2, 4, 'Bella je vesela i hrabra, uvijek spremna za avanture.', 1, 1),
('HR567890123456789', 'Rex', '2011-11-09', 'mu�ki', 3, 3, 'Rex je za�titni�ki nastrojen pas, idealan za obitelj.', 1, 3),
('HR678901234567890', 'Maja', '2017-02-05', '�enski', 1, 1, 'Maja je umiljata i voli biti u centru pa�nje.', 1, 2),
('HR789012345678901', 'Bruno', '2012-07-22', 'mu�ki', 2, 2, 'Bruno je prijateljski nastrojen pas koji voli sve ljude.', 0, 3),
('HR890123456789012', 'Zara', '2016-04-11', '�enski', 1, 2, 'Zara je vesela i razigrana, �esto tr�i po dvori�tu.', 1, 3),
('HR901234567890123', 'Oscar', '2009-10-02', 'mu�ki', 3, 3, 'Oscar je smiren pas koji voli dugi odmor.', 1, 4),
('HR012345678901234', 'Nina', '2018-06-17', '�enski', 1, 1, 'Nina je ljubazna i voli dru�tvo drugih pasa.', 0, 1),
('HR112345678901235', 'Dino', '2014-12-08', 'mu�ki', 2, 2, 'Dino je odan pas koji se �esto igra s djecom.', 1, 3),
('HR223456789012346', 'Kira', '2013-09-03', '�enski', 2, 4, 'Kira je srame�ljiva, ali odana prijateljica.', 1, 3),
('HR334567890123457', 'Toby', '2016-11-10', 'mu�ki', 1, 3, 'Toby je aktivan pas koji u�iva u tr�anju i lovu na lopticu.', 0, 3),
('HR445678901234568', 'Rita', '2012-03-25', '�enski', 2, 1, 'Rita je vesela, uvijek spremna za igru.', 1, 1),
('HR556789012345679', 'Maks', '2011-07-15', 'mu�ki', 3, 2, 'Maks je veliki, ljubazan pas koji se voli opu�tati.', 1, 3),
('HR667890123456780', 'Fiona', '2016-05-09', '�enski', 2, 3, 'Fiona je smirena i vrlo pa�ljiva prema djeci.', 0, 2),
('HR778901234567891', 'Gustav', '2013-04-17', 'mu�ki', 2, 2, 'Gustav je veselo naravnog temperamenta, pravi pas za obitelj.', 1, 3),
('HR889012345678902', 'Vera', '2014-01-30', '�enski', 1, 1, 'Vera je nje�na i voli biti u dru�tvu svojih vlasnika.', 1, 3),
('HR990123456789013', 'Vuk', '2010-09-05', 'mu�ki', 3, 3, 'Vuk je vrlo energi�an, voli biti vani i tr�ati po prirodi.', 0, 3);


insert into udomitelji(ime, prezime, adresa, telefon, email)
VALUES
('Ivan', 'Horvat', 'Osijek, Ulica Mije �aka 12', '385 98 1234567', 'ivan.horvat@gmail.com'),
('Ana', 'Kova�', 'Vinkovci, Trg Kneza Mihajla 3', '385 91 2345678', 'ana.kovac@gmail.com'),
('Marko', 'Novak', 'Valpovo, Kralja Zvonimira 45', '385 98 3456789', 'marko.novak@gmail.com'),
('Lina', 'Petrovi�', 'Osijek, Stjepana Radi�a 14', '385 91 4567890', 'lina.petrovic@gmail.com'),
('Josip', 'Mati�', 'Vinkovci, Kolodvorska 9', '385 98 5678901', 'josip.matic@gmail.com'),
('Maja', '�imi�', 'Valpovo, Dravska 19', '385 91 6789012', 'maja.simic@gmail.com'),
('Tomislav', 'Juri�', 'Osijek, Antuna Mihanovi�a 7', '385 98 7890123', 'tomislav.juric@gmail.com'),
('Ivana', 'Babi�', 'Vinkovci, I.G.Kova�i�a 25', '385 91 8901234', 'ivana.babic@gmail.com'),
('Petar', 'Soldo', 'Valpovo, Zagreba�ka 34', '385 98 9012345', 'petar.soldo@gmail.com'),
('Jelena', 'Vukovi�', 'Osijek, Trg Sv. Trojstva 8', '385 91 0123456', 'jelena.vukovic@gmail.com');

INSERT INTO upiti (pas, udomitelj, datum_upita, status_upita, napomene)
VALUES
(3, 7, '2024-06-15', 'zaprimljen', 'nema napomene'),
(9, 2, '2024-08-10', 'u obradi', 'nema napomene'),
(1, 5, '2024-07-05', 'obra�en', 'udomljenje'),
(7, 4, '2024-04-20', 'zaprimljen', 'nema napomene'),
(5, 6, '2024-01-25', 'u obradi', 'nema napomene'),
(8, 3, '2024-02-10', 'zaprimljen', 'nema napomene'),
(2, 10, '2024-05-18', 'obra�en', 'na �ekanju'),
(10, 1, '2024-03-12', 'zaprimljen', 'nema napomene'),
(4, 9, '2024-06-22', 'u obradi', 'nema napomene'),
(6, 8, '2024-04-15', 'obra�en', 'odbijeno');
