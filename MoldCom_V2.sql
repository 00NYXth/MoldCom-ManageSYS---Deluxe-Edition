	CREATE DATABASE MoldCom2;
	USE MoldCom2;

    CREATE SCHEMA vizitator_MoldCom;

ALTER SCHEMA vizitator_MoldCom TRANSFER dbo.Produs;
ALTER SCHEMA vizitator_MoldCom TRANSFER dbo.Furnizor;
ALTER SCHEMA vizitator_MoldCom TRANSFER dbo.Comanda;

-- ==============================
--        CREARE TABELE
-- ==============================

CREATE TABLE Client (
    id_client INT PRIMARY KEY,
    nume NVARCHAR(40),
    prenume NVARCHAR(40),
    telefon NVARCHAR(20),
    email NVARCHAR(100),
    adresa NVARCHAR(100));

CREATE TABLE Angajat (
    id_angajat INT PRIMARY KEY,
    nume NVARCHAR(30),
    prenume NVARCHAR(30),
    functie NVARCHAR(40),
    salariu DECIMAL(10,2),
    data_angajare DATE);

CREATE TABLE Furnizor (
    id_furnizor INT PRIMARY KEY,
    denumire NVARCHAR(100),
    telefon NVARCHAR(30),
    email NVARCHAR(40),
    adresa NVARCHAR(100));

CREATE TABLE Produs (
    id_produs INT PRIMARY KEY,
    denumire NVARCHAR(60),
    pret DECIMAL(10,2),
    stoc INT,
    categorie NVARCHAR(40),
    id_furnizor INT,
    FOREIGN KEY (id_furnizor) REFERENCES Furnizor(id_furnizor) ON DELETE CASCADE ON UPDATE CASCADE);

CREATE TABLE Comanda (
    id_comanda INT PRIMARY KEY,
    id_client INT,
    id_angajat INT,
    data_comanda DATE,
    status NVARCHAR(20),
    FOREIGN KEY (id_client) REFERENCES Client(id_client) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (id_angajat) REFERENCES Angajat(id_angajat) ON DELETE CASCADE ON UPDATE CASCADE);

CREATE TABLE Detaliu_Comanda (
    id_comanda INT,
    id_produs INT,
    cantitate INT,
    subtotal DECIMAL(10,2),
    FOREIGN KEY (id_comanda) REFERENCES Comanda(id_comanda) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (id_produs) REFERENCES Produs(id_produs) ON DELETE CASCADE ON UPDATE CASCADE);

CREATE TABLE Factura (
    id_factura INT PRIMARY KEY,
    id_comanda INT,
    data_emitere DATE,
    total DECIMAL(10,2),
    metoda_plata NVARCHAR(30),
    FOREIGN KEY (id_comanda) REFERENCES Comanda(id_comanda) ON DELETE CASCADE ON UPDATE CASCADE);

    
-- ==============================
--           INSERARE
-- ==============================

INSERT INTO Client (id_client, nume, prenume, telefon, email, adresa) VALUES
(1, 'Popescu', 'Ion', '+37369123456', 'ion.popescu@gmail.com', 'str. Stefan cel Mare 45, Chisinau'),
(2, 'Ionescu', 'Maria', '+37369234567', 'maria.ionescu@yahoo.com', 'bd. Dacia 23, Chisinau'),
(3, 'Rusu', 'Andrei', '+37369345678', 'andrei.rusu@mail.ru', 'str. 31 August 12, Balti'),
(4, 'Moraru', 'Elena', '+37369456789', 'elena.moraru@gmail.com', 'str. Puskin 78, Chisinau'),
(5, 'Ciobanu', 'Vasile', '+37369567890', 'vasile.ciobanu@inbox.ru', 'bd. Negruzzi 34, Chisinau'),
(6, 'Munteanu', 'Ana', '+37369678901', 'ana.munteanu@gmail.com', 'str. Mihai Eminescu 56, Tiraspol'),
(7, 'Popa', 'Constantin', '+37369789012', 'constantin.popa@yahoo.com', 'str. Independentei 89, Chisinau'),
(8, 'Gheorghe', 'Natalia', '+37369890123', 'natalia.gheorghe@mail.md', 'bd. Traian 45, Chisinau'),
(9, 'Stan', 'Dumitru', '+37369901234', 'dumitru.stan@gmail.com', 'str. Armeneasca 23, Chisinau'),
(10, 'Vasile', 'Tatiana', '+37369012345', 'tatiana.vasile@inbox.com', 'str. Ismail 67, Cahul'),
(11, 'Tudor', 'Gheorghe', '+37368123456', 'gheorghe.tudor@gmail.com', 'bd. Renasterii 12, Chisinau'),
(12, 'Lungu', 'Svetlana', '+37368234567', 'svetlana.lungu@yahoo.com', 'str. Albisoara 90, Chisinau'),
(13, 'Mazur', 'Victor', '+37368345678', 'victor.mazur@mail.ru', 'str. Muncesti 45, Chisinau'),
(14, 'Croitor', 'Olga', '+37368456789', 'olga.croitor@gmail.com', 'bd. Cuza Voda 78, Chisinau'),
(15, 'Cojocaru', 'Mihai', '+37368567890', 'mihai.cojocaru@inbox.ru', 'str. Columna 34, Chisinau'),
(16, 'Rosu', 'Lidia', '+37368678901', 'lidia.rosu@gmail.com', 'str. Botanica 56, Chisinau'),
(17, 'Pascari', 'Sergiu', '+37368789012', 'sergiu.pascari@yahoo.com', 'bd. Moscova 23, Chisinau'),
(18, 'Botnaru', 'Ina', '+37368890123', 'ina.botnaru@mail.md', 'str. Tighina 67, Chisinau'),
(19, 'Melnic', 'Petru', '+37368901234', 'petru.melnic@gmail.com', 'str. Florilor 89, Orhei'),
(20, 'Caraseni', 'Oxana', '+37368012345', 'oxana.caraseni@inbox.com', 'bd. Decebal 45, Chisinau'),
(21, 'Guzun', 'Nicolae', '+37367123456', 'nicolae.guzun@gmail.com', 'str. Hincesti 12, Chisinau'),
(22, 'Vrabie', 'Eugenia', '+37367234567', 'eugenia.vrabie@yahoo.com', 'str. Sarmizegetusa 90, Chisinau'),
(23, 'Cucu', 'Alexandru', '+37367345678', 'alexandru.cucu@mail.ru', 'bd. Mircea cel Batran 45, Chisinau'),
(24, 'Balan', 'Galina', '+37367456789', 'galina.balan@gmail.com', 'str. Teatrala 78, Chisinau'),
(25, 'Grosu', 'Valeriu', '+37367567890', 'valeriu.grosu@inbox.ru', 'str. Bucovinei 34, Chisinau'),
(26, 'Diaconu', 'Marina', '+37367678901', 'marina.diaconu@gmail.com', 'bd. Grigore Vieru 56, Chisinau'),
(27, 'Sava', 'Dorin', '+37367789012', 'dorin.sava@yahoo.com', 'str. Trandafirilor 23, Soroca'),
(28, 'Mogildea', 'Ludmila', '+37367890123', 'ludmila.mogildea@mail.md', 'str. Buiucani 67, Chisinau'),
(29, 'Colesnic', 'Vladislav', '+37367901234', 'vladislav.colesnic@gmail.com', 'str. Calea Iesilor 89, Chisinau'),
(30, 'Spataru', 'Veronica', '+37367012345', 'veronica.spataru@inbox.com', 'bd. Cantemir 45, Chisinau'),
(31, 'Chiriac', 'Roman', '+37378123456', 'roman.chiriac@gmail.com', 'str. Ismail 12, Chisinau'),
(32, 'Buzu', 'Alina', '+37378234567', 'alina.buzu@yahoo.com', 'str. Uzinelor 90, Chisinau'),
(33, 'Cazacu', 'Igor', '+37378345678', 'igor.cazacu@mail.ru', 'bd. Iuri Gagarin 45, Chisinau'),
(34, 'Boicu', 'Angela', '+37378456789', 'angela.boicu@gmail.com', 'str. Paris 78, Chisinau'),
(35, 'Tofan', 'Stanislav', '+37378567890', 'stanislav.tofan@inbox.ru', 'str. Vadul lui Voda 34, Chisinau'),
(36, 'Cebotari', 'Liliana', '+37378678901', 'liliana.cebotari@gmail.com', 'bd. Aeroportului 56, Chisinau'),
(37, 'Gutu', 'Denis', '+37378789012', 'denis.gutu@yahoo.com', 'str. Studentilor 23, Chisinau'),
(38, 'Creanga', 'Daniela', '+37378890123', 'daniela.creanga@mail.md', 'str. Ion Creanga 67, Ungheni'),
(39, 'Marandici', 'Pavel', '+37378901234', 'pavel.marandici@gmail.com', 'str. Munca 89, Chisinau'),
(40, 'Baciu', 'Cristina', '+37378012345', 'cristina.baciu@inbox.com', 'bd. Mircea Eliade 45, Chisinau'),
(41, 'Certan', 'Oleg', '+37379123456', 'oleg.certan@gmail.com', 'str. Tineretului 12, Chisinau'),
(42, 'Cojocariu', 'Irina', '+37379234567', 'irina.cojocariu@yahoo.com', 'str. Cuza Voda 90, Chisinau'),
(43, 'Doga', 'Maxim', '+37379345678', 'maxim.doga@mail.ru', 'bd. Stefan cel Mare 145, Chisinau'),
(44, 'Frunze', 'Zinaida', '+37379456789', 'zinaida.frunze@gmail.com', 'str. Mitropolit Dosoftei 78, Chisinau'),
(45, 'Gaidau', 'Ruslan', '+37379567890', 'ruslan.gaidau@inbox.ru', 'str. Petricani 34, Chisinau'),
(46, 'Harea', 'Valentina', '+37379678901', 'valentina.harea@gmail.com', 'bd. Bucuresti 56, Chisinau'),
(47, 'Ivanov', 'Serghei', '+37379789012', 'serghei.ivanov@yahoo.com', 'str. Munca 23, Balti'),
(48, 'Jitariuc', 'Nadia', '+37379890123', 'nadia.jitariuc@mail.md', 'str. Alba Iulia 67, Chisinau'),
(49, 'Leanca', 'Adrian', '+37379901234', 'adrian.leanca@gmail.com', 'str. Vasile Alecsandri 89, Chisinau'),
(50, 'Manole', 'Loredana', '+37379012345', 'loredana.manole@inbox.com', 'bd. Kogalniceanu 45, Chisinau'),
(51, 'Negru', 'Vitalie', '+37376123456', 'vitalie.negru@gmail.com', 'str. Independentei 112, Chisinau'),
(52, 'Olaru', 'Tatiana', '+37376234567', 'tatiana.olaru@yahoo.com', 'str. Calea Orheiului 90, Chisinau'),
(53, 'Pintilie', 'Veaceslav', '+37376345678', 'veaceslav.pintilie@mail.ru', 'bd. Traian 145, Chisinau'),
(54, 'Raileanu', 'Larisa', '+37376456789', 'larisa.raileanu@gmail.com', 'str. Mihai Viteazul 78, Chisinau'),
(55, 'Sajin', 'Dan', '+37376567890', 'dan.sajin@inbox.ru', 'str. Gh. Asachi 34, Chisinau'),
(56, 'Tarlev', 'Svetlana', '+37376678901', 'svetlana.tarlev@gmail.com', 'bd. Dacia 156, Chisinau'),
(57, 'Ursu', 'Anatol', '+37376789012', 'anatol.ursu@yahoo.com', 'str. Costiujeni 23, Chisinau'),
(58, 'Vieru', 'Diana', '+37376890123', 'diana.vieru@mail.md', 'str. Sciusev 67, Chisinau'),
(59, 'Zaporojan', 'Stefan', '+37376901234', 'stefan.zaporojan@gmail.com', 'str. Calea Iesilor 189, Chisinau'),
(60, 'Apostol', 'Ecaterina', '+37376012345', 'ecaterina.apostol@inbox.com', 'bd. Renasterii 45, Chisinau'),
(61, 'Bejan', 'Corneliu', '+37377123456', 'corneliu.bejan@gmail.com', 'str. Armeneasca 212, Chisinau'),
(62, 'Catana', 'Julia', '+37377234567', 'julia.catana@yahoo.com', 'str. Decebal 90, Chisinau'),
(63, 'درگăluț', 'Andrei', '+37377345678', 'andrei.dragalut@mail.ru', 'bd. Stefan cel Mare 245, Chisinau'),
(64, 'Eremia', 'Nadejda', '+37377456789', 'nadejda.eremia@gmail.com', 'str. Mitropolit Varlaam 78, Cahul'),
(65, 'Foca', 'Cristian', '+37377567890', 'cristian.foca@inbox.ru', 'str. Armeneasca 134, Chisinau'),
(66, 'Ghervas', 'Mariana', '+37377678901', 'mariana.ghervas@gmail.com', 'bd. Moscova 156, Chisinau'),
(67, 'Hatman', 'Dumitru', '+37377789012', 'dumitru.hatman@yahoo.com', 'str. Tighina 123, Chisinau'),
(68, 'Istrati', 'Rodica', '+37377890123', 'rodica.istrati@mail.md', 'str. Cuza Voda 167, Chisinau'),
(69, 'Jalba', 'Grigore', '+37377901234', 'grigore.jalba@gmail.com', 'str. Columna 289, Chisinau'),
(70, 'Lacriteanu', 'Silvia', '+37377012345', 'silvia.lacriteanu@inbox.com', 'bd. Cantemir 145, Chisinau'),
(71, 'Malanciuc', 'Iurie', '+37360123456', 'iurie.malanciuc@gmail.com', 'str. Hincesti 212, Chisinau'),
(72, 'Nicolaev', 'Valentina', '+37360234567', 'valentina.nicolaev@yahoo.com', 'str. Petricani 90, Chisinau'),
(73, 'Ostapcov', 'Mihail', '+37360345678', 'mihail.ostapcov@mail.ru', 'bd. Dacia 245, Chisinau'),
(74, 'Paladuta', 'Lucia', '+37360456789', 'lucia.paladuta@gmail.com', 'str. Puskin 178, Chisinau'),
(75, 'Railean', 'Eugen', '+37360567890', 'eugen.railean@inbox.ru', 'str. Botanica 234, Chisinau'),
(76, 'Sadovei', 'Alla', '+37360678901', 'alla.sadovei@gmail.com', 'bd. Traian 245, Chisinau'),
(77, 'Timofte', 'Liviu', '+37360789012', 'liviu.timofte@yahoo.com', 'str. Ismail 223, Soroca'),
(78, 'Ungureanu', 'Vera', '+37360890123', 'vera.ungureanu@mail.md', 'str. Munca 167, Chisinau'),
(79, 'Vicol', 'Viorel', '+37360901234', 'viorel.vicol@gmail.com', 'str. Albisoara 289, Chisinau'),
(80, 'Zagaevschi', 'Liuba', '+37360012345', 'liuba.zagaevschi@inbox.com', 'bd. Mircea Eliade 145, Chisinau'),
(81, 'Ababii', 'Eugen', '+37361123456', 'eugen.ababii@gmail.com', 'str. Trandafirilor 312, Chisinau'),
(82, 'Bostan', 'Ala', '+37361234567', 'ala.bostan@yahoo.com', 'str. Mitropolit Dosoftei 190, Chisinau'),
(83, 'Ciornii', 'Vadim', '+37361345678', 'vadim.ciornii@mail.ru', 'bd. Stefan cel Mare 345, Chisinau'),
(84, 'Dabija', 'Svetlana', '+37361456789', 'svetlana.dabija@gmail.com', 'str. Calea Orheiului 278, Chisinau'),
(85, 'Erhan', 'Constantin', '+37361567890', 'constantin.erhan@inbox.ru', 'str. Armeneasca 334, Chisinau'),
(86, 'Fedas', 'Inna', '+37361678901', 'inna.fedas@gmail.com', 'bd. Dacia 356, Chisinau'),
(87, 'Garbuz', 'Marcel', '+37361789012', 'marcel.garbuz@yahoo.com', 'str. Muncesti 223, Chisinau'),
(88, 'Haruta', 'Nina', '+37361890123', 'nina.haruta@mail.md', 'str. Tighina 267, Balti'),
(89, 'Iachim', 'Veaceslav', '+37361901234', 'veaceslav.iachim@gmail.com', 'str. Columna 389, Chisinau'),
(90, 'Jitari', 'Lidia', '+37361012345', 'lidia.jitari@inbox.com', 'bd. Renasterii 245, Chisinau'),
(91, 'Lazar', 'Valentin', '+37362123456', 'valentin.lazar@gmail.com', 'str. Independentei 412, Chisinau'),
(92, 'Madan', 'Tatiana', '+37362234567', 'tatiana.madan@yahoo.com', 'str. Botanica 390, Chisinau'),
(93, 'Nastas', 'Sergiu', '+37362345678', 'sergiu.nastas@mail.ru', 'bd. Traian 345, Chisinau'),
(94, 'Oprea', 'Anastasia', '+37362456789', 'anastasia.oprea@gmail.com', 'str. Vasile Alecsandri 278, Chisinau'),
(95, 'Plamadeala', 'Dorin', '+37362567890', 'dorin.plamadeala@inbox.ru', 'str. Alba Iulia 334, Chisinau'),
(96, 'Rotaru', 'Luminita', '+37362678901', 'luminita.rotaru@gmail.com', 'bd. Moscova 256, Chisinau'),
(97, 'Sirbu', 'Vlad', '+37362789012', 'vlad.sirbu@yahoo.com', 'str. Calea Iesilor 423, Chisinau'),
(98, 'Toma', 'Xenia', '+37362890123', 'xenia.toma@mail.md', 'str. Mitropolit Varlaam 167, Chisinau'),
(99, 'Usatii', 'Nicolai', '+37362901234', 'nicolai.usatii@gmail.com', 'str. Decebal 289, Ungheni'),
(100, 'Vacarciuc', 'Oksana', '+37362012345', 'oksana.vacarciuc@inbox.com', 'bd. Cantemir 345, Chisinau');

-- Inserări pentru tabelul Angajat (100 înregistrări)
INSERT INTO Angajat (id_angajat, nume, prenume, functie, salariu, data_angajare) VALUES
(1, 'Lungu', 'Victor', 'Director General', 25000.00, '2015-01-15'),
(2, 'Groza', 'Elena', 'Director Comercial', 18000.00, '2015-03-20'),
(3, 'Bucur', 'Andrei', 'Manager Vanzari', 12000.00, '2016-05-10'),
(4, 'Sirbu', 'Marina', 'Contabil Sef', 14000.00, '2015-06-01'),
(5, 'Dabija', 'Ion', 'Manager Depozit', 10000.00, '2016-08-15'),
(6, 'Russu', 'Ana', 'Specialist Vanzari', 7500.00, '2017-02-20'),
(7, 'Cojocaru', 'Mihai', 'Specialist Vanzari', 7500.00, '2017-04-10'),
(8, 'Timofte', 'Olga', 'Specialist Vanzari', 7500.00, '2017-06-05'),
(9, 'Gavriliuc', 'Constantin', 'Specialist Vanzari', 7500.00, '2017-09-12'),
(10, 'Moraru', 'Natalia', 'Specialist Vanzari', 7500.00, '2018-01-20'),
(11, 'Popa', 'Vasile', 'Specialist Vanzari', 7500.00, '2018-03-15'),
(12, 'Chiriac', 'Svetlana', 'Specialist Vanzari', 7500.00, '2018-05-25'),
(13, 'Munteanu', 'Alexandru', 'Specialist Vanzari', 7500.00, '2018-07-30'),
(14, 'Cazacu', 'Tatiana', 'Specialist Vanzari', 7500.00, '2018-09-10'),
(15, 'Ciobanu', 'Dumitru', 'Specialist Vanzari', 7500.00, '2018-11-05'),
(16, 'Botnaru', 'Ludmila', 'Contabil', 8000.00, '2016-02-15'),
(17, 'Melnic', 'Sergiu', 'Contabil', 8000.00, '2017-03-20'),
(18, 'Vrabie', 'Ina', 'Contabil', 8000.00, '2018-04-10'),
(19, 'Guzun', 'Petru', 'Administrator Depozit', 9000.00, '2016-07-01'),
(20, 'Grosu', 'Oxana', 'Administrator Depozit', 9000.00, '2017-08-15'),
(21, 'Sava', 'Nicolae', 'Operator Depozit', 6000.00, '2017-10-20'),
(22, 'Diaconu', 'Eugenia', 'Operator Depozit', 6000.00, '2018-01-25'),
(23, 'Colesnic', 'Valeriu', 'Operator Depozit', 6000.00, '2018-03-30'),
(24, 'Mogildea', 'Marina', 'Operator Depozit', 6000.00, '2018-06-05'),
(25, 'Spataru', 'Dorin', 'Operator Depozit', 6000.00, '2018-08-10'),
(26, 'Buzu', 'Galina', 'Operator Depozit', 6000.00, '2018-10-15'),
(27, 'Boicu', 'Vladislav', 'Specialist IT', 11000.00, '2016-04-01'),
(28, 'Tofan', 'Veronica', 'Specialist IT', 11000.00, '2017-05-10'),
(29, 'Cebotari', 'Roman', 'Specialist Resurse Umane', 9000.00, '2016-09-15'),
(30, 'Gutu', 'Alina', 'Specialist Marketing', 8500.00, '2017-11-20'),
(31, 'Creanga', 'Igor', 'Specialist Marketing', 8500.00, '2018-02-25'),
(32, 'Marandici', 'Angela', 'Secretar', 6500.00, '2016-12-01'),
(33, 'Baciu', 'Stanislav', 'Sofer', 6500.00, '2017-01-15'),
(34, 'Certan', 'Liliana', 'Sofer', 6500.00, '2017-03-20'),
(35, 'Cojocariu', 'Denis', 'Sofer', 6500.00, '2017-05-25'),
(36, 'Doga', 'Daniela', 'Sofer', 6500.00, '2017-07-30'),
(37, 'Frunze', 'Pavel', 'Sofer', 6500.00, '2017-09-05'),
(38, 'Gaidau', 'Cristina', 'Agent Curatenie', 5000.00, '2018-01-10'),
(39, 'Harea', 'Oleg', 'Agent Curatenie', 5000.00, '2018-03-15'),
(40, 'Ivanov', 'Irina', 'Agent Securitate', 6000.00, '2016-05-20'),
(41, 'Jitariuc', 'Maxim', 'Agent Securitate', 6000.00, '2017-06-25'),
(42, 'Leanca', 'Zinaida', 'Specialist Achizitii', 8000.00, '2016-08-01'),
(43, 'Manole', 'Ruslan', 'Specialist Achizitii', 8000.00, '2017-09-10'),
(44, 'Negru', 'Valentina', 'Specialist Logistica', 8500.00, '2017-11-15'),
(45, 'Olaru', 'Serghei', 'Specialist Logistica', 8500.00, '2018-01-20'),
(46, 'Pintilie', 'Nadia', 'Operator Call Center', 6000.00, '2018-03-25'),
(47, 'Raileanu', 'Adrian', 'Operator Call Center', 6000.00, '2018-05-30'),
(48, 'Sajin', 'Loredana', 'Operator Call Center', 6000.00, '2018-07-05'),
(49, 'Tarlev', 'Vitalie', 'Operator Call Center', 6000.00, '2018-09-10'),
(50, 'Ursu', 'Tatiana', 'Operator Call Center', 6000.00, '2018-11-15'),
(51, 'Vieru', 'Veaceslav', 'Manager Proiect', 10000.00, '2017-01-20'),
(52, 'Zaporojan', 'Larisa', 'Manager Proiect', 10000.00, '2017-03-25'),
(53, 'Apostol', 'Dan', 'Analist', 9000.00, '2017-05-30'),
(54, 'Bejan', 'Svetlana', 'Analist', 9000.00, '2017-07-05'),
(55, 'Catana', 'Anatol', 'Technician', 7000.00, '2017-09-10'),
(56, 'Dragalut', 'Diana', 'Technician', 7000.00, '2017-11-15'),
(57, 'Eremia', 'Stefan', 'Technician', 7000.00, '2018-01-20'),
(58, 'Foca', 'Ecaterina', 'Specialist Vanzari', 7500.00, '2018-03-25'),
(59, 'Ghervas', 'Corneliu', 'Specialist Vanzari', 7500.00, '2018-05-30'),
(60, 'Hatman', 'Julia', 'Specialist Vanzari', 7500.00, '2018-07-05'),
(61, 'Istrati', 'Andrei', 'Specialist Vanzari', 7500.00, '2018-09-10'),
(62, 'Jalba', 'Nadejda', 'Specialist Vanzari', 7500.00, '2018-11-15'),
(63, 'Lacriteanu', 'Cristian', 'Specialist Vanzari', 7500.00, '2019-01-20'),
(64, 'Malanciuc', 'Mariana', 'Specialist Vanzari', 7500.00, '2019-03-25'),
(65, 'Nicolaev', 'Dumitru', 'Specialist Vanzari', 7500.00, '2019-05-30'),
(66, 'Ostapcov', 'Rodica', 'Specialist Vanzari', 7500.00, '2019-07-05'),
(67, 'Paladuta', 'Grigore', 'Specialist Vanzari', 7500.00, '2019-09-10'),
(68, 'Railean', 'Silvia', 'Specialist Vanzari', 7500.00, '2019-11-15'),
(69, 'Sadovei', 'Iurie', 'Specialist Vanzari', 7500.00, '2020-01-20'),
(70, 'Timofte', 'Valentina', 'Specialist Vanzari', 7500.00, '2020-03-25'),
(71, 'Ungureanu', 'Mihail', 'Operator Depozit', 6000.00, '2019-02-10'),
(72, 'Vicol', 'Lucia', 'Operator Depozit', 6000.00, '2019-04-15'),
(73, 'Zagaevschi', 'Eugen', 'Operator Depozit', 6000.00, '2019-06-20'),
(74, 'Ababii', 'Alla', 'Operator Depozit', 6000.00, '2019-08-25'),
(75, 'Bostan', 'Liviu', 'Operator Depozit', 6000.00, '2019-10-30'),
(76, 'Ciornii', 'Vera', 'Operator Depozit', 6000.00, '2019-12-05'),
(77, 'Dabija', 'Viorel', 'Operator Depozit', 6000.00, '2020-02-10'),
(78, 'Erhan', 'Liuba', 'Operator Depozit', 6000.00, '2020-04-15'),
(79, 'Fedas', 'Eugen', 'Operator Depozit', 6000.00, '2020-06-20'),
(80, 'Garbuz', 'Ala', 'Contabil', 8000.00, '2019-01-15'),
(81, 'Haruta', 'Vadim', 'Contabil', 8000.00, '2019-03-20'),
(82, 'Iachim', 'Svetlana', 'Sofer', 6500.00, '2019-05-25'),
(83, 'Jitari', 'Constantin', 'Sofer', 6500.00, '2019-07-30'),
(84, 'Lazar', 'Inna', 'Sofer', 6500.00, '2019-09-05'),
(85, 'Madan', 'Marcel', 'Sofer', 6500.00, '2019-11-10'),
(86, 'Nastas', 'Nina', 'Operator Call Center', 6000.00, '2020-01-15'),
(87, 'Oprea', 'Veaceslav', 'Operator Call Center', 6000.00, '2020-03-20'),
(88, 'Plamadeala', 'Lidia', 'Operator Call Center', 6000.00, '2020-05-25'),
(89, 'Rotaru', 'Valentin', 'Specialist IT', 11000.00, '2019-02-01'),
(90, 'Sirbu', 'Tatiana', 'Specialist Marketing', 8500.00, '2019-04-05'),
(91, 'Toma', 'Sergiu', 'Specialist Achizitii', 8000.00, '2019-06-10'),
(92, 'Usatii', 'Anastasia', 'Specialist Logistica', 8500.00, '2019-08-15'),
(93, 'Vacarciuc', 'Dorin', 'Technician', 7000.00, '2019-10-20'),
(94, 'Albu', 'Luminita', 'Analist', 9000.00, '2019-12-25'),
(95, 'Bodrug', 'Vlad', 'Manager Proiect', 10000.00, '2020-02-01'),
(96, 'Calmic', 'Xenia', 'Specialist Resurse Umane', 9000.00, '2020-04-05'),
(97, 'Danila', 'Nicolai', 'Agent Securitate', 6000.00, '2020-06-10'),
(98, 'Eftodi', 'Oksana', 'Agent Curatenie', 5000.00, '2020-08-15'),
(99, 'Filip', 'Gheorghe', 'Secretar', 6500.00, '2020-10-20'),
(100, 'Grama', 'Liudmila', 'Specialist Vanzari', 7500.00, '2020-12-25');

INSERT INTO Furnizor (id_furnizor, denumire, telefon, email, adresa) VALUES
(1, 'TechnoImport SRL', '+37322123456', 'office@technoimport.md', 'str. Industriala 45, Chisinau'),
(2, 'ElectroPlus SA', '+37322234567', 'info@electroplus.md', 'bd. Stefan cel Mare 123, Chisinau'),
(3, 'MegaDistribution SRL', '+37322345678', 'sales@megadist.md', 'str. Muncesti 78, Chisinau'),
(4, 'ProComputer SA', '+37322456789', 'contact@procomputer.md', 'bd. Dacia 56, Chisinau'),
(5, 'DigitalWorld SRL', '+37322567890', 'office@digitalworld.md', 'str. Armeneasca 34, Chisinau'),
(6, 'SmartTech SA', '+37322678901', 'info@smarttech.md', 'bd. Traian 89, Chisinau'),
(7, 'GlobalElectronics SRL', '+37322789012', 'sales@globalelec.md', 'str. Columna 67, Chisinau'),
(8, 'UltraComputing SA', '+37322890123', 'contact@ultracomp.md', 'str. Ismail 45, Chisinau'),
(9, 'NextGenTech SRL', '+37322901234', 'office@nextgen.md', 'bd. Moscova 123, Chisinau'),
(10, 'InnovaElectro SA', '+37322012345', 'info@innovaelec.md', 'str. Tighina 78, Chisinau'),
(11, 'ComputerMaster SRL', '+37323123456', 'sales@compmaster.md', 'str. Mihai Eminescu 56, Chisinau'),
(12, 'TechSolutions SA', '+37323234567', 'contact@techsol.md', 'bd. Renasterii 34, Chisinau'),
(13, 'ElectronicSupply SRL', '+37323345678', 'office@elecsupply.md', 'str. Botanica 89, Chisinau'),
(14, 'DigiStore SA', '+37323456789', 'info@digistore.md', 'str. Puskin 67, Chisinau'),
(15, 'TechnoMarket SRL', '+37323567890', 'sales@technomarket.md', 'bd. Cuza Voda 45, Chisinau'),
(16, 'ComputerPro SA', '+37323678901', 'contact@comppro.md', 'str. Albisoara 123, Chisinau'),
(17, 'ElectroDistribution SRL', '+37323789012', 'office@electrodist.md', 'str. Calea Orheiului 78, Chisinau'),
(18, 'TechnoVision SA', '+37323890123', 'info@technovision.md', 'bd. Decebal 56, Chisinau'),
(19, 'DigitalSupply SRL', '+37323901234', 'sales@digisupply.md', 'str. 31 August 34, Balti'),
(20, 'SmartElectronics SA', '+37323012345', 'contact@smartelec.md', 'str. Independentei 89, Chisinau'),
(21, 'ProTech Distribution SRL', '+37324123456', 'office@protechdist.md', 'bd. Cantemir 67, Chisinau'),
(22, 'MegaElectronics SA', '+37324234567', 'info@megaelec.md', 'str. Teatrala 45, Chisinau'),
(23, 'UltraTech SRL', '+37324345678', 'sales@ultratech.md', 'str. Hincesti 123, Chisinau'),
(24, 'ComputerZone SA', '+37324456789', 'contact@compzone.md', 'bd. Grigore Vieru 78, Chisinau'),
(25, 'ElectroMax SRL', '+37324567890', 'office@electromax.md', 'str. Bucovinei 56, Chisinau'),
(26, 'TechWorld SA', '+37324678901', 'info@techworld.md', 'str. Trandafirilor 34, Soroca'),
(27, 'DigitalTech SRL', '+37324789012', 'sales@digitaltech.md', 'bd. Mircea Eliade 89, Chisinau'),
(28, 'SmartComputing SA', '+37324890123', 'contact@smartcomp.md', 'str. Buiucani 67, Chisinau'),
(29, 'ProElectronics SRL', '+37324901234', 'office@proelec.md', 'str. Calea Iesilor 45, Chisinau'),
(30, 'GlobalTech SA', '+37324012345', 'info@globaltech.md', 'str. Paris 123, Chisinau'),
(31, 'InnovaTech SRL', '+37325123456', 'sales@innovatech.md', 'bd. Iuri Gagarin 78, Chisinau'),
(32, 'ComputerStore SA', '+37325234567', 'contact@compstore.md', 'str. Vadul lui Voda 56, Chisinau'),
(33, 'ElectronicWorld SRL', '+37325345678', 'office@elecworld.md', 'bd. Aeroportului 34, Chisinau'),
(34, 'TechSupply SA', '+37325456789', 'info@techsupply.md', 'str. Studentilor 89, Chisinau'),
(35, 'DigitalMarket SRL', '+37325567890', 'sales@digimarket.md', 'str. Ion Creanga 67, Ungheni'),
(36, 'SmartDistribution SA', '+37325678901', 'contact@smartdist.md', 'str. Munca 45, Chisinau'),
(37, 'ProComputing SRL', '+37325789012', 'office@procomp.md', 'bd. Stefan cel Mare 223, Chisinau'),
(38, 'MegaTech SA', '+37325890123', 'info@megatech.md', 'str. Mitropolit Dosoftei 123, Chisinau'),
(39, 'UltraElectronics SRL', '+37325901234', 'sales@ultraelec.md', 'str. Petricani 78, Chisinau'),
(40, 'ComputerPlus SA', '+37325012345', 'contact@compplus.md', 'bd. Bucuresti 56, Chisinau'),
(41, 'ElectroStore SRL', '+37326123456', 'office@electrostore.md', 'str. Alba Iulia 34, Chisinau'),
(42, 'TechnoSupply SA', '+37326234567', 'info@technosupply.md', 'str. Vasile Alecsandri 89, Chisinau'),
(43, 'DigitalPro SRL', '+37326345678', 'sales@digipro.md', 'bd. Kogalniceanu 67, Chisinau'),
(44, 'SmartTechnology SA', '+37326456789', 'contact@smarttech2.md', 'str. Munca 45, Balti'),
(45, 'ProElectro SRL', '+37326567890', 'office@proelectro.md', 'str. Calea Orheiului 123, Chisinau'),
(46, 'GlobalComputing SA', '+37326678901', 'info@globalcomp.md', 'bd. Traian 178, Chisinau'),
(47, 'InnovaComputer SRL', '+37326789012', 'sales@innovacomp.md', 'str. Gh. Asachi 56, Chisinau'),
(48, 'ComputerDirect SA', '+37326890123', 'contact@compdirect.md', 'bd. Dacia 234, Chisinau'),
(49, 'ElectronicMarket SRL', '+37326901234', 'office@elecmarket.md', 'str. Costiujeni 34, Chisinau'),
(50, 'TechDistribution SA', '+37326012345', 'info@techdist.md', 'str. Sciusev 89, Chisinau'),
(51, 'DigitalStore SRL', '+37327123456', 'sales@digistore2.md', 'str. Calea Iesilor 267, Chisinau'),
(52, 'SmartElectro SA', '+37327234567', 'contact@smartelectro.md', 'bd. Renasterii 123, Chisinau'),
(53, 'ProDigital SRL', '+37327345678', 'office@prodigital.md', 'str. Armeneasca 278, Chisinau'),
(54, 'MegaComputing SA', '+37327456789', 'info@megacomp.md', 'str. Decebal 145, Chisinau'),
(55, 'UltraSupply SRL', '+37327567890', 'sales@ultrasupply.md', 'bd. Stefan cel Mare 312, Chisinau'),
(56, 'ComputerMarket SA', '+37327678901', 'contact@compmarket.md', 'str. Mitropolit Varlaam 89, Cahul'),
(57, 'ElectroSupply SRL', '+37327789012', 'office@electrosupply.md', 'str. Armeneasca 189, Chisinau'),
(58, 'TechnoElectro SA', '+37327890123', 'info@technoelectro.md', 'bd. Moscova 223, Chisinau'),
(59, 'DigitalElectronics SRL', '+37327901234', 'sales@digielec.md', 'str. Tighina 178, Chisinau'),
(60, 'SmartSupply SA', '+37327012345', 'contact@smartsupply.md', 'str. Cuza Voda 234, Chisinau'),
(61, 'ProTech SRL', '+37328123456', 'office@protech.md', 'str. Columna 312, Chisinau'),
(62, 'GlobalElectro SA', '+37328234567', 'info@globalelectro.md', 'bd. Cantemir 189, Chisinau'),
(63, 'InnovaElectronics SRL', '+37328345678', 'sales@innovaelec2.md', 'str. Hincesti 278, Chisinau'),
(64, 'ComputerWorld SA', '+37328456789', 'contact@compworld.md', 'str. Petricani 145, Chisinau'),
(65, 'ElectronicTech SRL', '+37328567890', 'office@electech.md', 'bd. Dacia 312, Chisinau'),
(66, 'TechMarket SA', '+37328678901', 'info@techmarket.md', 'str. Puskin 223, Chisinau'),
(67, 'DigitalDistribution SRL', '+37328789012', 'sales@digidist.md', 'str. Botanica 289, Chisinau'),
(68, 'SmartWorld SA', '+37328890123', 'contact@smartworld.md', 'bd. Traian 312, Chisinau'),
(69, 'ProSupply SRL', '+37328901234', 'office@prosupply.md', 'str. Ismail 278, Soroca'),
(70, 'MegaElectro SA', '+37328012345', 'info@megaelectro.md', 'str. Munca 234, Chisinau'),
(71, 'UltraMarket SRL', '+37329123456', 'sales@ultramarket.md', 'str. Albisoara 312, Chisinau'),
(72, 'ComputerElectro SA', '+37329234567', 'contact@compelectro.md', 'bd. Mircea Eliade 189, Chisinau'),
(73, 'ElectroWorld SRL', '+37329345678', 'office@electroworld.md', 'str. Trandafirilor 378, Chisinau'),
(74, 'TechnoDistribution SA', '+37329456789', 'info@technodist.md', 'str. Mitropolit Dosoftei 245, Chisinau'),
(75, 'DigitalComputing SRL', '+37329567890', 'sales@digicomp.md', 'bd. Stefan cel Mare 423, Chisinau'),
(76, 'SmartMarket SA', '+37329678901', 'contact@smartmarket.md', 'str. Calea Orheiului 334, Chisinau'),
(77, 'ProMarket SRL', '+37329789012', 'office@promarket.md', 'str. Armeneasca 389, Chisinau'),
(78, 'GlobalSupply SA', '+37329890123', 'info@globalsupply.md', 'bd. Dacia 423, Chisinau'),
(79, 'InnovaSupply SRL', '+37329901234', 'sales@innovasupply.md', 'str. Muncesti 289, Chisinau'),
(80, 'ComputerSupply SA', '+37329012345', 'contact@compsupply.md', 'str. Tighina 312, Balti'),
(81, 'ElectronicSupplyPlus SRL', '+37320123456', 'office@elecsupplus.md', 'str. Columna 423, Chisinau'),
(82, 'TechStore SA', '+37320234567', 'info@techstore.md', 'bd. Renasterii 278, Chisinau'),
(83, 'DigitalWorld2 SRL', '+37320345678', 'sales@digiworld2.md', 'str. Independentei 489, Chisinau'),
(84, 'SmartStore SA', '+37320456789', 'contact@smartstore.md', 'str. Botanica 423, Chisinau'),
(85, 'ProWorld SRL', '+37320567890', 'office@proworld.md', 'bd. Traian 423, Chisinau'),
(86, 'MegaSupply SA', '+37320678901', 'info@megasupply.md', 'str. Vasile Alecsandri 312, Chisinau'),
(87, 'UltraDistribution SRL', '+37320789012', 'sales@ultradist.md', 'str. Alba Iulia 389, Chisinau'),
(88, 'ComputerTech SA', '+37320890123', 'contact@comptech.md', 'bd. Moscova 312, Chisinau'),
(89, 'ElectroTech SRL', '+37320901234', 'office@electrotech.md', 'str. Calea Iesilor 489, Chisinau'),
(90, 'TechnoWorld SA', '+37320012345', 'info@technoworld.md', 'str. Mitropolit Varlaam 223, Chisinau'),
(91, 'DigitalSupply2 SRL', '+37321123456', 'sales@digisupply2.md', 'str. Decebal 334, Ungheni'),
(92, 'SmartComputer SA', '+37321234567', 'contact@smartcomp2.md', 'bd. Cantemir 389, Chisinau'),
(93, 'ProStore SRL', '+37321345678', 'office@prostore.md', 'str. Hincesti 489, Chisinau'),
(94, 'GlobalMarket SA', '+37321456789', 'info@globalmarket.md', 'str. Petricani 334, Chisinau'),
(95, 'InnovaMarket SRL', '+37321567890', 'sales@innovamarket.md', 'bd. Dacia 489, Chisinau'),
(96, 'ComputerDistribution SA', '+37321678901', 'contact@compdist.md', 'str. Puskin 312, Chisinau'),
(97, 'ElectronicStore SRL', '+37321789012', 'office@elecstore.md', 'str. Botanica 489, Chisinau'),
(98, 'TechSupplyPlus SA', '+37321890123', 'info@techsupplus.md', 'bd. Traian 489, Chisinau'),
(99, 'DigitalMarket2 SRL', '+37321901234', 'sales@digimarket2.md', 'str. Ismail 334, Chisinau'),
(100, 'SmartDistribution2 SA', '+37321012345', 'contact@smartdist2.md', 'str. Munca 389, Chisinau');

INSERT INTO Produs (id_produs, denumire, pret, stoc, categorie, id_furnizor) VALUES
(1, 'Laptop ASUS VivoBook 15', 8500.00, 45, 'Laptopuri', 1),
(2, 'Laptop Lenovo IdeaPad 3', 7200.00, 38, 'Laptopuri', 2),
(3, 'Laptop HP Pavilion 14', 9500.00, 32, 'Laptopuri', 3),
(4, 'Laptop Dell Inspiron 15', 10200.00, 28, 'Laptopuri', 4),
(5, 'Laptop Acer Aspire 5', 7800.00, 42, 'Laptopuri', 5),
(6, 'Desktop HP ProDesk 400', 12000.00, 25, 'Calculatoare Desktop', 6),
(7, 'Desktop Dell OptiPlex 3080', 13500.00, 20, 'Calculatoare Desktop', 7),
(8, 'Desktop Lenovo ThinkCentre M720', 11800.00, 22, 'Calculatoare Desktop', 8),
(9, 'Desktop ASUS ExpertCenter D500', 10500.00, 30, 'Calculatoare Desktop', 9),
(10, 'Desktop Acer Veriton M200', 9800.00, 35, 'Calculatoare Desktop', 10),
(11, 'Monitor Samsung 24" Full HD', 2200.00, 65, 'Monitoare', 11),
(12, 'Monitor LG 27" QHD', 3500.00, 48, 'Monitoare', 12),
(13, 'Monitor Dell 24" Full HD', 2400.00, 55, 'Monitoare', 13),
(14, 'Monitor ASUS 27" 4K', 5200.00, 30, 'Monitoare', 14),
(15, 'Monitor HP 22" Full HD', 1800.00, 72, 'Monitoare', 15),
(16, 'Tastatura Logitech K120', 180.00, 120, 'Periferice', 16),
(17, 'Tastatura Microsoft Wired', 220.00, 95, 'Periferice', 17),
(18, 'Tastatura Razer Cynosa V2', 850.00, 42, 'Periferice', 18),
(19, 'Mouse Logitech M185', 150.00, 135, 'Periferice', 19),
(20, 'Mouse Microsoft Wireless', 280.00, 88, 'Periferice', 20),
(21, 'Mouse Razer DeathAdder', 720.00, 55, 'Periferice', 21),
(22, 'Imprimanta HP LaserJet Pro', 4500.00, 35, 'Imprimante', 22),
(23, 'Imprimanta Canon PIXMA', 3200.00, 42, 'Imprimante', 23),
(24, 'Imprimanta Epson EcoTank', 5800.00, 28, 'Imprimante', 24),
(25, 'Imprimanta Brother DCP', 3800.00, 38, 'Imprimante', 25),
(26, 'Imprimanta Samsung Xpress', 2900.00, 45, 'Imprimante', 26),
(27, 'Router TP-Link Archer C6', 650.00, 75, 'Retea', 27),
(28, 'Router ASUS RT-AC68U', 1200.00, 48, 'Retea', 28),
(29, 'Router D-Link DIR-825', 850.00, 62, 'Retea', 29),
(30, 'Switch TP-Link TL-SG108', 380.00, 85, 'Retea', 30),
(31, 'Switch Cisco SG110-16', 1450.00, 32, 'Retea', 31),
(32, 'HDD Seagate 1TB', 850.00, 95, 'Stocare', 32),
(33, 'HDD Western Digital 2TB', 1200.00, 78, 'Stocare', 33),
(34, 'SSD Kingston 240GB', 680.00, 110, 'Stocare', 34),
(35, 'SSD Samsung 500GB', 1350.00, 85, 'Stocare', 35),
(36, 'SSD Crucial 1TB', 2100.00, 62, 'Stocare', 36),
(37, 'RAM Kingston 8GB DDR4', 720.00, 125, 'Componente PC', 37),
(38, 'RAM Corsair 16GB DDR4', 1450.00, 88, 'Componente PC', 38),
(39, 'Placa Video NVIDIA GTX 1650', 4200.00, 35, 'Componente PC', 39),
(40, 'Placa Video AMD RX 6600', 5800.00, 28, 'Componente PC', 40),
(41, 'Procesor Intel Core i5', 3800.00, 42, 'Componente PC', 41),
(42, 'Procesor AMD Ryzen 5', 3500.00, 48, 'Componente PC', 42),
(43, 'Placa de Baza ASUS Prime', 2200.00, 38, 'Componente PC', 43),
(44, 'Placa de Baza Gigabyte B450', 1850.00, 45, 'Componente PC', 44),
(45, 'Sursa Chieftec 500W', 950.00, 65, 'Componente PC', 45),
(46, 'Sursa Corsair 650W', 1450.00, 52, 'Componente PC', 46),
(47, 'Carcasa Zalman Z3', 780.00, 58, 'Componente PC', 47),
(48, 'Carcasa Cooler Master Q300L', 1100.00, 42, 'Componente PC', 48),
(49, 'Cooler procesor Arctic Freezer', 420.00, 72, 'Componente PC', 49),
(50, 'Cooler procesor Noctua NH-U12S', 1250.00, 35, 'Componente PC', 50),
(51, 'Smartphone Samsung Galaxy A54', 6500.00, 48, 'Telefoane', 51),
(52, 'Smartphone Xiaomi Redmi Note 12', 4200.00, 65, 'Telefoane', 52),
(53, 'Smartphone Apple iPhone 13', 18500.00, 22, 'Telefoane', 53),
(54, 'Smartphone Huawei P40 Lite', 4800.00, 38, 'Telefoane', 54),
(55, 'Smartphone Motorola Moto G52', 3900.00, 55, 'Telefoane', 55),
(56, 'Tableta Samsung Galaxy Tab A8', 3800.00, 42, 'Tablete', 56),
(57, 'Tableta iPad 9th Gen', 8200.00, 28, 'Tablete', 57),
(58, 'Tableta Lenovo Tab M10', 2800.00, 48, 'Tablete', 58),
(59, 'Tableta Huawei MatePad T10', 2200.00, 55, 'Tablete', 59),
(60, 'Tableta Amazon Fire HD 10', 2500.00, 45, 'Tablete', 60),
(61, 'Televizor Samsung 43" Smart TV', 7200.00, 32, 'TV si Audio', 61),
(62, 'Televizor LG 50" 4K Smart', 9500.00, 25, 'TV si Audio', 62),
(63, 'Televizor Sony 55" 4K HDR', 12800.00, 18, 'TV si Audio', 63),
(64, 'Boxe Logitech Z333', 850.00, 65, 'TV si Audio', 64),
(65, 'Boxe Creative Pebble V2', 420.00, 88, 'TV si Audio', 65),
(66, 'Casti Sony WH-1000XM4', 4500.00, 35, 'TV si Audio', 66),
(67, 'Casti JBL Tune 500', 650.00, 72, 'TV si Audio', 67),
(68, 'Casti HyperX Cloud II', 1850.00, 48, 'TV si Audio', 68),
(69, 'Webcam Logitech C920', 1450.00, 55, 'Periferice', 69),
(70, 'Webcam Microsoft LifeCam', 980.00, 65, 'Periferice', 70),
(71, 'Microfon Blue Yeti', 2800.00, 32, 'TV si Audio', 71),
(72, 'Microfon Razer Seiren Mini', 980.00, 45, 'TV si Audio', 72),
(73, 'UPS APC Back-UPS 650VA', 1450.00, 48, 'Alimentare', 73),
(74, 'UPS CyberPower 850VA', 1850.00, 38, 'Alimentare', 74),
(75, 'Stabilizator tensiune 1000VA', 680.00, 62, 'Alimentare', 75),
(76, 'Cablu HDMI 2m', 120.00, 185, 'Accesorii', 76),
(77, 'Cablu USB-C 1m', 85.00, 220, 'Accesorii', 77),
(78, 'Hub USB 4 porturi', 280.00, 95, 'Accesorii', 78),
(79, 'Adaptor USB-C la HDMI', 350.00, 78, 'Accesorii', 79),
(80, 'Card memorie microSD 64GB', 280.00, 145, 'Stocare', 80),
(81, 'Card memorie microSD 128GB', 480.00, 125, 'Stocare', 81),
(82, 'Stick USB 32GB', 150.00, 185, 'Stocare', 82),
(83, 'Stick USB 64GB', 250.00, 155, 'Stocare', 83),
(84, 'Geanta laptop 15.6"', 420.00, 75, 'Accesorii', 84),
(85, 'Rucsac laptop 17"', 680.00, 58, 'Accesorii', 85),
(86, 'Mouse pad gaming RGB', 380.00, 88, 'Accesorii', 86),
(87, 'Suport laptop reglabil', 450.00, 65, 'Accesorii', 87),
(88, 'Lampa LED birou USB', 320.00, 72, 'Accesorii', 88),
(89, 'Baterie externa 10000mAh', 550.00, 95, 'Accesorii', 89),
(90, 'Baterie externa 20000mAh', 850.00, 68, 'Accesorii', 90),
(91, 'Incarcator wireless Samsung', 480.00, 82, 'Accesorii', 91),
(92, 'Incarcator rapid USB-C 65W', 650.00, 75, 'Accesorii', 92),
(93, 'Folie protectie laptop 15.6"', 120.00, 125, 'Accesorii', 93),
(94, 'Husa telefon Samsung A54', 95.00, 168, 'Accesorii', 94),
(95, 'Husa telefon iPhone 13', 180.00, 142, 'Accesorii', 95),
(96, 'Filtru confidentialitate 15.6"', 420.00, 48, 'Accesorii', 96),
(97, 'Suport monitor ajustabil', 780.00, 52, 'Accesorii', 97),
(98, 'Brat monitor articulat', 1250.00, 35, 'Accesorii', 98),
(99, 'Kit curatare electronice', 85.00, 195, 'Accesorii', 99),
(100, 'Protectie supratensiune 6 prize', 320.00, 115, 'Alimentare', 100);

INSERT INTO Comanda (id_comanda, id_client, id_angajat, data_comanda, status) VALUES
(1, 1, 6, '2024-01-05', 'Finalizata'),
(2, 2, 7, '2024-01-08', 'Finalizata'),
(3, 3, 8, '2024-01-12', 'Finalizata'),
(4, 4, 9, '2024-01-15', 'Finalizata'),
(5, 5, 10, '2024-01-18', 'Finalizata'),
(6, 6, 11, '2024-01-22', 'Finalizata'),
(7, 7, 12, '2024-01-25', 'Finalizata'),
(8, 8, 13, '2024-01-28', 'Finalizata'),
(9, 9, 14, '2024-02-02', 'Finalizata'),
(10, 10, 15, '2024-02-05', 'Finalizata'),
(11, 11, 6, '2024-02-08', 'Finalizata'),
(12, 12, 7, '2024-02-12', 'Finalizata'),
(13, 13, 8, '2024-02-15', 'Finalizata'),
(14, 14, 9, '2024-02-18', 'Finalizata'),
(15, 15, 10, '2024-02-22', 'Finalizata'),
(16, 16, 11, '2024-02-25', 'Finalizata'),
(17, 17, 12, '2024-02-28', 'Finalizata'),
(18, 18, 13, '2024-03-03', 'Finalizata'),
(19, 19, 14, '2024-03-06', 'Finalizata'),
(20, 20, 15, '2024-03-10', 'Finalizata'),
(21, 21, 6, '2024-03-13', 'Finalizata'),
(22, 22, 7, '2024-03-16', 'Finalizata'),
(23, 23, 8, '2024-03-20', 'Finalizata'),
(24, 24, 9, '2024-03-23', 'Finalizata'),
(25, 25, 10, '2024-03-26', 'Finalizata'),
(26, 26, 11, '2024-03-30', 'Finalizata'),
(27, 27, 12, '2024-04-02', 'Finalizata'),
(28, 28, 13, '2024-04-05', 'Finalizata'),
(29, 29, 14, '2024-04-09', 'Finalizata'),
(30, 30, 15, '2024-04-12', 'Finalizata'),
(31, 31, 6, '2024-04-15', 'Finalizata'),
(32, 32, 7, '2024-04-19', 'Finalizata'),
(33, 33, 8, '2024-04-22', 'Finalizata'),
(34, 34, 9, '2024-04-25', 'Finalizata'),
(35, 35, 10, '2024-04-29', 'Finalizata'),
(36, 36, 11, '2024-05-02', 'Finalizata'),
(37, 37, 12, '2024-05-05', 'Finalizata'),
(38, 38, 13, '2024-05-09', 'Finalizata'),
(39, 39, 14, '2024-05-12', 'Finalizata'),
(40, 40, 15, '2024-05-15', 'Finalizata'),
(41, 41, 6, '2024-05-19', 'Finalizata'),
(42, 42, 7, '2024-05-22', 'Finalizata'),
(43, 43, 8, '2024-05-25', 'Finalizata'),
(44, 44, 9, '2024-05-29', 'Finalizata'),
(45, 45, 10, '2024-06-01', 'Finalizata'),
(46, 46, 11, '2024-06-04', 'Finalizata'),
(47, 47, 12, '2024-06-08', 'Finalizata'),
(48, 48, 13, '2024-06-11', 'Finalizata'),
(49, 49, 14, '2024-06-14', 'Finalizata'),
(50, 50, 15, '2024-06-18', 'Finalizata'),
(51, 51, 6, '2024-06-21', 'Finalizata'),
(52, 52, 7, '2024-06-24', 'Finalizata'),
(53, 53, 8, '2024-06-28', 'Finalizata'),
(54, 54, 9, '2024-07-01', 'Finalizata'),
(55, 55, 10, '2024-07-04', 'Finalizata'),
(56, 56, 11, '2024-07-08', 'Finalizata'),
(57, 57, 12, '2024-07-11', 'Finalizata'),
(58, 58, 13, '2024-07-14', 'Finalizata'),
(59, 59, 14, '2024-07-18', 'Finalizata'),
(60, 60, 15, '2024-07-21', 'Finalizata'),
(61, 61, 6, '2024-07-24', 'Finalizata'),
(62, 62, 7, '2024-07-28', 'Finalizata'),
(63, 63, 8, '2024-07-31', 'Finalizata'),
(64, 64, 9, '2024-08-03', 'Finalizata'),
(65, 65, 10, '2024-08-07', 'Finalizata'),
(66, 66, 11, '2024-08-10', 'Finalizata'),
(67, 67, 12, '2024-08-13', 'Finalizata'),
(68, 68, 13, '2024-08-17', 'Finalizata'),
(69, 69, 14, '2024-08-20', 'Finalizata'),
(70, 70, 15, '2024-08-23', 'Finalizata'),
(71, 71, 6, '2024-08-27', 'Finalizata'),
(72, 72, 7, '2024-08-30', 'Finalizata'),
(73, 73, 8, '2024-09-02', 'Finalizata'),
(74, 74, 9, '2024-09-06', 'Finalizata'),
(75, 75, 10, '2024-09-09', 'Finalizata'),
(76, 76, 11, '2024-09-12', 'Finalizata'),
(77, 77, 12, '2024-09-16', 'Finalizata'),
(78, 78, 13, '2024-09-19', 'Finalizata'),
(79, 79, 14, '2024-09-22', 'Finalizata'),
(80, 80, 15, '2024-09-26', 'In procesare'),
(81, 81, 6, '2024-09-29', 'In procesare'),
(82, 82, 7, '2024-10-02', 'In procesare'),
(83, 83, 8, '2024-10-03', 'In procesare'),
(84, 84, 9, '2024-10-04', 'In procesare'),
(85, 85, 10, '2024-10-05', 'In procesare'),
(86, 86, 11, '2024-10-06', 'In procesare'),
(87, 87, 12, '2024-10-07', 'Noua'),
(88, 88, 13, '2024-10-07', 'Noua'),
(89, 89, 14, '2024-10-08', 'Noua'),
(90, 90, 15, '2024-10-08', 'Noua'),
(91, 91, 6, '2024-10-08', 'Noua'),
(92, 92, 7, '2024-10-08', 'Noua'),
(93, 93, 8, '2024-10-08', 'Noua'),
(94, 94, 9, '2024-10-08', 'Noua'),
(95, 95, 10, '2024-10-08', 'Noua'),
(96, 96, 11, '2024-10-08', 'Noua'),
(97, 97, 12, '2024-10-08', 'Noua'),
(98, 98, 13, '2024-10-08', 'Noua'),
(99, 99, 14, '2024-10-08', 'Noua'),
(100, 100, 15, '2024-10-08', 'Noua');

INSERT INTO Detaliu_Comanda (id_comanda, id_produs, cantitate, subtotal) VALUES
(1, 1, 1, 8500.00),
(1, 11, 1, 2200.00),
(2, 51, 1, 6500.00),
(2, 94, 2, 190.00),
(3, 6, 1, 12000.00),
(3, 13, 2, 4800.00),
(4, 22, 1, 4500.00),
(4, 76, 3, 360.00),
(5, 61, 1, 7200.00),
(5, 64, 1, 850.00),
(6, 2, 1, 7200.00),
(6, 16, 1, 180.00),
(6, 19, 1, 150.00),
(7, 32, 2, 1700.00),
(7, 34, 1, 680.00),
(8, 52, 1, 4200.00),
(8, 89, 1, 550.00),
(9, 7, 1, 13500.00),
(9, 14, 1, 5200.00),
(10, 23, 1, 3200.00),
(10, 77, 5, 425.00),
(11, 62, 1, 9500.00),
(12, 3, 1, 9500.00),
(12, 12, 1, 3500.00),
(13, 27, 2, 1300.00),
(13, 30, 1, 380.00),
(14, 53, 1, 18500.00),
(15, 8, 1, 11800.00),
(15, 15, 2, 3600.00),
(16, 66, 1, 4500.00),
(17, 24, 1, 5800.00),
(18, 4, 1, 10200.00),
(18, 17, 1, 220.00),
(19, 35, 1, 1350.00),
(19, 37, 2, 1440.00),
(20, 54, 1, 4800.00),
(20, 84, 1, 420.00),
(21, 63, 1, 12800.00),
(22, 9, 1, 10500.00),
(22, 11, 1, 2200.00),
(23, 39, 1, 4200.00),
(23, 41, 1, 3800.00),
(24, 25, 1, 3800.00),
(25, 56, 1, 3800.00),
(25, 80, 2, 560.00),
(26, 5, 1, 7800.00),
(26, 18, 1, 850.00),
(27, 28, 1, 1200.00),
(27, 78, 2, 560.00),
(28, 55, 2, 7800.00),
(29, 10, 1, 9800.00),
(29, 13, 1, 2400.00),
(30, 68, 1, 1850.00),
(31, 26, 1, 2900.00),
(32, 57, 1, 8200.00),
(33, 1, 1, 8500.00),
(33, 69, 1, 1450.00),
(34, 36, 1, 2100.00),
(34, 38, 1, 1450.00),
(35, 61, 1, 7200.00),
(36, 40, 1, 5800.00),
(36, 43, 1, 2200.00),
(37, 2, 1, 7200.00),
(37, 20, 1, 280.00),
(38, 58, 1, 2800.00),
(38, 82, 3, 450.00),
(39, 73, 1, 1450.00),
(40, 6, 1, 12000.00),
(41, 51, 1, 6500.00),
(42, 22, 1, 4500.00),
(42, 99, 2, 170.00),
(43, 42, 1, 3500.00),
(43, 44, 1, 1850.00),
(44, 62, 1, 9500.00),
(45, 3, 1, 9500.00),
(46, 59, 1, 2200.00),
(46, 83, 2, 500.00),
(47, 29, 2, 1700.00),
(48, 52, 1, 4200.00),
(49, 74, 1, 1850.00),
(50, 7, 1, 13500.00),
(51, 67, 2, 1300.00),
(52, 23, 1, 3200.00),
(53, 4, 1, 10200.00),
(54, 90, 1, 850.00),
(55, 60, 1, 2500.00),
(56, 8, 1, 11800.00),
(57, 45, 2, 1900.00),
(57, 46, 1, 1450.00),
(58, 53, 1, 18500.00),
(59, 24, 1, 5800.00),
(60, 61, 1, 7200.00),
(61, 31, 2, 2900.00),
(62, 54, 1, 4800.00),
(63, 9, 1, 10500.00),
(64, 71, 1, 2800.00),
(65, 25, 1, 3800.00),
(66, 5, 1, 7800.00),
(67, 63, 1, 12800.00),
(68, 47, 2, 1560.00),
(68, 49, 1, 420.00),
(69, 55, 1, 3900.00),
(70, 10, 1, 9800.00),
(71, 56, 1, 3800.00),
(72, 33, 1, 1200.00),
(72, 37, 1, 720.00),
(73, 26, 1, 2900.00),
(74, 66, 1, 4500.00),
(75, 1, 1, 8500.00),
(76, 57, 1, 8200.00),
(77, 72, 1, 980.00),
(78, 27, 1, 650.00),
(79, 85, 1, 680.00),
(80, 2, 1, 7200.00),
(81, 52, 1, 4200.00),
(82, 6, 1, 12000.00),
(83, 22, 1, 4500.00),
(84, 61, 1, 7200.00),
(85, 51, 1, 6500.00),
(86, 3, 1, 9500.00),
(87, 62, 1, 9500.00),
(88, 7, 1, 13500.00),
(89, 53, 1, 18500.00),
(90, 4, 1, 10200.00),
(91, 23, 1, 3200.00),
(92, 8, 1, 11800.00),
(93, 63, 1, 12800.00),
(94, 24, 1, 5800.00),
(95, 9, 1, 10500.00),
(96, 54, 1, 4800.00),
(97, 5, 1, 7800.00),
(98, 25, 1, 3800.00),
(99, 10, 1, 9800.00),
(100, 55, 1, 3900.00);

INSERT INTO Factura (id_factura, id_comanda, data_emitere, total, metoda_plata) VALUES
(1, 1, '2024-01-05', 10700.00, 'Card Bancar'),
(2, 2, '2024-01-08', 6690.00, 'Numerar'),
(3, 3, '2024-01-12', 16800.00, 'Card Bancar'),
(4, 4, '2024-01-15', 4860.00, 'Transfer Bancar'),
(5, 5, '2024-01-18', 8050.00, 'Card Bancar'),
(6, 6, '2024-01-22', 7530.00, 'Numerar'),
(7, 7, '2024-01-25', 2380.00, 'Card Bancar'),
(8, 8, '2024-01-28', 4750.00, 'Transfer Bancar'),
(9, 9, '2024-02-02', 18700.00, 'Card Bancar'),
(10, 10, '2024-02-05', 3625.00, 'Numerar'),
(11, 11, '2024-02-08', 9500.00, 'Card Bancar'),
(12, 12, '2024-02-12', 13000.00, 'Transfer Bancar'),
(13, 13, '2024-02-15', 1680.00, 'Numerar'),
(14, 14, '2024-02-18', 18500.00, 'Card Bancar'),
(15, 15, '2024-02-22', 15400.00, 'Card Bancar'),
(16, 16, '2024-02-25', 4500.00, 'Transfer Bancar'),
(17, 17, '2024-02-28', 5800.00, 'Card Bancar'),
(18, 18, '2024-03-03', 10420.00, 'Numerar'),
(19, 19, '2024-03-06', 2790.00, 'Card Bancar'),
(20, 20, '2024-03-10', 5220.00, 'Transfer Bancar'),
(21, 21, '2024-03-13', 12800.00, 'Card Bancar'),
(22, 22, '2024-03-16', 12700.00, 'Card Bancar'),
(23, 23, '2024-03-20', 8000.00, 'Numerar'),
(24, 24, '2024-03-23', 3800.00, 'Transfer Bancar'),
(25, 25, '2024-03-26', 4360.00, 'Card Bancar'),
(26, 26, '2024-03-30', 8650.00, 'Numerar'),
(27, 27, '2024-04-02', 1760.00, 'Card Bancar'),
(28, 28, '2024-04-05', 7800.00, 'Transfer Bancar'),
(29, 29, '2024-04-09', 12200.00, 'Card Bancar'),
(30, 30, '2024-04-12', 1850.00, 'Numerar'),
(31, 31, '2024-04-15', 2900.00, 'Card Bancar'),
(32, 32, '2024-04-19', 8200.00, 'Transfer Bancar'),
(33, 33, '2024-04-22', 9950.00, 'Card Bancar'),
(34, 34, '2024-04-25', 3550.00, 'Numerar'),
(35, 35, '2024-04-29', 7200.00, 'Card Bancar'),
(36, 36, '2024-05-02', 8000.00, 'Transfer Bancar'),
(37, 37, '2024-05-05', 7480.00, 'Card Bancar'),
(38, 38, '2024-05-09', 3250.00, 'Numerar'),
(39, 39, '2024-05-12', 1450.00, 'Card Bancar'),
(40, 40, '2024-05-15', 12000.00, 'Transfer Bancar'),
(41, 41, '2024-05-19', 6500.00, 'Card Bancar'),
(42, 42, '2024-05-22', 4670.00, 'Numerar'),
(43, 43, '2024-05-25', 5350.00, 'Card Bancar'),
(44, 44, '2024-05-29', 9500.00, 'Transfer Bancar'),
(45, 45, '2024-06-01', 9500.00, 'Card Bancar'),
(46, 46, '2024-06-04', 2700.00, 'Numerar'),
(47, 47, '2024-06-08', 1700.00, 'Card Bancar'),
(48, 48, '2024-06-11', 4200.00, 'Transfer Bancar'),
(49, 49, '2024-06-14', 1850.00, 'Card Bancar'),
(50, 50, '2024-06-18', 13500.00, 'Card Bancar'),
(51, 51, '2024-06-21', 1300.00, 'Numerar'),
(52, 52, '2024-06-24', 3200.00, 'Transfer Bancar'),
(53, 53, '2024-06-28', 10200.00, 'Card Bancar'),
(54, 54, '2024-07-01', 850.00, 'Numerar'),
(55, 55, '2024-07-04', 2500.00, 'Card Bancar'),
(56, 56, '2024-07-08', 11800.00, 'Transfer Bancar'),
(57, 57, '2024-07-11', 3350.00, 'Card Bancar'),
(58, 58, '2024-07-14', 18500.00, 'Card Bancar'),
(59, 59, '2024-07-18', 5800.00, 'Numerar'),
(60, 60, '2024-07-21', 7200.00, 'Transfer Bancar'),
(61, 61, '2024-07-24', 2900.00, 'Card Bancar'),
(62, 62, '2024-07-28', 4800.00, 'Numerar'),
(63, 63, '2024-07-31', 10500.00, 'Card Bancar'),
(64, 64, '2024-08-03', 2800.00, 'Transfer Bancar'),
(65, 65, '2024-08-07', 3800.00, 'Card Bancar'),
(66, 66, '2024-08-10', 7800.00, 'Numerar'),
(67, 67, '2024-08-13', 12800.00, 'Card Bancar'),
(68, 68, '2024-08-17', 1980.00, 'Transfer Bancar'),
(69, 69, '2024-08-20', 3900.00, 'Card Bancar'),
(70, 70, '2024-08-23', 9800.00, 'Numerar'),
(71, 71, '2024-08-27', 3800.00, 'Card Bancar'),
(72, 72, '2024-08-30', 1920.00, 'Transfer Bancar'),
(73, 73, '2024-09-02', 2900.00, 'Card Bancar'),
(74, 74, '2024-09-06', 4500.00, 'Numerar'),
(75, 75, '2024-09-09', 8500.00, 'Card Bancar'),
(76, 76, '2024-09-12', 8200.00, 'Transfer Bancar'),
(77, 77, '2024-09-16', 980.00, 'Card Bancar'),
(78, 78, '2024-09-19', 650.00, 'Numerar'),
(79, 79, '2024-09-22', 680.00, 'Card Bancar'),
(80, 80, '2024-09-26', 7200.00, 'Transfer Bancar'),
(81, 81, '2024-09-29', 4200.00, 'Card Bancar'),
(82, 82, '2024-10-02', 12000.00, 'Numerar'),
(83, 83, '2024-10-03', 4500.00, 'Card Bancar'),
(84, 84, '2024-10-06', 2750.00, 'Transfer Bancar'),
(85, 85, '2024-10-09', 8600.00, 'Numerar'),
(86, 86, '2024-10-12', 9400.00, 'Card Bancar'),
(87, 87, '2024-10-15', 3150.00, 'Transfer Bancar'),
(88, 88, '2024-10-18', 7200.00, 'Card Bancar'),
(89, 89, '2024-10-21', 15200.00, 'Numerar'),
(90, 90, '2024-10-24', 3600.00, 'Card Bancar'),
(91, 91, '2024-10-27', 9800.00, 'Transfer Bancar'),
(92, 92, '2024-10-30', 4500.00, 'Card Bancar'),
(93, 93, '2024-11-02', 7600.00, 'Numerar'),
(94, 94, '2024-11-05', 12800.00, 'Card Bancar'),
(95, 95, '2024-11-08', 6300.00, 'Transfer Bancar'),
(96, 96, '2024-11-11', 2250.00, 'Card Bancar'),
(97, 97, '2024-11-14', 18400.00, 'Numerar'),
(98, 98, '2024-11-17', 3250.00, 'Card Bancar'),
(99, 99, '2024-11-20', 10500.00, 'Transfer Bancar'),
(100, 100, '2024-11-23', 8900.00, 'Card Bancar');


-- ==============================
--           INTEROGARI
-- ==============================

-- Se vor cauta ce  comenzi are fiecare client
SELECT c.id_comanda, cl.nume, cl.prenume, c.data_comanda
FROM Comanda c
JOIN Client cl ON c.id_client = cl.id_client;

-- Vom utiliza un Join multimplu pentru a colecta date din mai multe tabele
SELECT cl.nume, p.denumire, dc.cantitate, p.pret, dc.cantitate * p.pret AS total
FROM Client cl
JOIN Comanda c ON cl.id_client = c.id_client
JOIN Detaliu_Comanda dc ON c.id_comanda = dc.id_comanda
JOIN Produs p ON dc.id_produs = p.id_produs;

-- Vom cauta clienti unde suma comenzii lor este mai mare de 10000
SELECT cl.id_client, cl.nume, SUM(p.pret * dc.cantitate) AS total_comenzi
FROM Client cl
JOIN Comanda c ON cl.id_client = c.id_client
JOIN Detaliu_Comanda dc ON c.id_comanda = dc.id_comanda
JOIN Produs p ON dc.id_produs = p.id_produs
GROUP BY cl.id_client, cl.nume
HAVING SUM(p.pret * dc.cantitate) > 10000;

-- Selectarea clientilor unde in email se contine @gmail.com
SELECT * FROM Client
WHERE email LIKE '%@gmail.com%';

-- Vom evita selecta toti clientii carora numele se incepe cu M sau C

SELECT * FROM Client
WHERE nume NOT LIKE 'M%' AND nume NOT LIKE 'C%';

-- Subinterogare pentru a vedea produsele care au pretul mai mare ca cel mediu
SELECT Denumire, Pret
FROM Produs
WHERE pret > (SELECT AVG(Pret) FROM Produs)

-- Folosirea Functiilor de agregare
SELECT id_comanda, YEAR(data_comanda) AS An, LEN(nume) AS LungimeNume
FROM Comanda c
JOIN Client cl ON c.id_client = cl.id_client;


-- ==============================
--         LOGIN\USER
-- ==============================
CREATE LOGIN vizitator_acI
WITH PASSWORD = '1234';

CREATE LOGIN Casier_lvl1
WITH PASSWORD = 'admin1';

CREATE LOGIN Manager
WITH PASSWORD = 'Moldova2000';

CREATE LOGIN Admin_db
WITH PASSWORD = 'Fairy991';

-- Crearea unui USER pentru BD
CREATE USER vizitator_acI
FOR LOGIN vizitator_acI;

CREATE USER Casier_lvl1
FOR LOGIN Casier_lvl1;

CREATE USER Manager
FOR LOGIN Manager;

CREATE USER Operator_db
FOR LOGIN Admin_db;


-- ==============================
--            ROLURI
-- ==============================
CREATE ROLE rol_vizitator
CREATE ROLE rol_casier
CREATE ROLE rol_manager_filiala
CREATE ROLE rol_operator

-- Pentru a vedea cine si ce permisiuni are
SELECT
    -- Coloanele echivalente cu PrincipalName și PrincipalType
    pr.name AS PrincipalName,
    pr.type_desc AS PrincipalType,
    
    -- Coloanele echivalente cu ObjectName și ObjectType
    OBJECT_SCHEMA_NAME(pe.major_id) AS ObjectSchemaName,
    OBJECT_NAME(pe.major_id) AS ObjectName,
    o.type_desc AS ObjectType,
    
    -- Coloanele echivalente cu permission_name și PermissionState
    pe.permission_name,
    pe.state_desc AS PermissionState
FROM
    sys.database_principals pr
JOIN
    sys.database_permissions pe ON pr.principal_id = pe.grantee_principal_id
LEFT JOIN
    sys.objects o ON pe.major_id = o.object_id AND pe.class = 1 -- Se alătură doar pentru obiecte (tabele, view-uri, etc.)
WHERE
    pr.type IN ('S', 'U', 'R', 'G') -- S=SQL User, U=Windows User, R=Database Role, G=Windows Group
    AND pr.name NOT IN ('public', 'guest', 'INFORMATION_SCHEMA', 'sys', 'dbo') -- Exclude entitățile de sistem
ORDER BY
    PrincipalName, ObjectName, permission_name;

-- Utilizatorul vizitator va avea rolul dat
ALTER ROLE rol_vizitator ADD MEMBER vizitator_acI;
ALTER ROLE rol_casier ADD MEMBER casier_lvl1;
ALTER ROLE rol_manager_filiala ADD MEMBER Manager;
ALTER ROLE rol_operator ADD MEMBER Operator_db;

-- Acordam permisiuni pentru roluri
GRANT SELECT ON dbo.Produs TO rol_vizitator; --vizitator

GRANT SELECT, INSERT ON Client TO rol_casier; --casier
GRANT SELECT ON vizitator_MoldCom.Produs TO rol_casier;
GRANT SELECT, INSERT ON vizitator_MoldCom.Comanda TO rol_casier;
GRANT SELECT, INSERT ON Detaliu_Comanda TO rol_casier;
GRANT SELECT, INSERT ON Factura TO rol_casier;
GRANT UPDATE (status) ON vizitator_MoldCom.Comanda TO rol_casier;

GRANT SELECT, INSERT, UPDATE, DELETE ON vizitator_MoldCom.Furnizor TO rol_operator; --operator
GRANT SELECT, INSERT, UPDATE ON vizitator_MoldCom.Produs TO rol_operator;
GRANT SELECT ON vizitator_MoldCom.Comanda TO rol_operator;
GRANT SELECT ON Detaliu_Comanda TO rol_operator;
GRANT SELECT ON Factura TO rol_operator;
GRANT SELECT, UPDATE ON Client TO rol_operator;

GRANT SELECT, INSERT, UPDATE, DELETE ON Client TO rol_manager_filiala; --manager
GRANT SELECT, INSERT, UPDATE, DELETE ON vizitator_MoldCom.Furnizor TO rol_manager_filiala;
GRANT SELECT, INSERT, UPDATE, DELETE ON vizitator_MoldCom.Produs TO rol_manager_filiala;
GRANT SELECT, INSERT, UPDATE, DELETE ON vizitator_MoldCom.Comanda TO rol_manager_filiala;
GRANT SELECT, INSERT, UPDATE, DELETE ON Detaliu_Comanda TO rol_manager_filiala;
GRANT SELECT, INSERT, UPDATE, DELETE ON Factura TO rol_manager_filiala;

-- |======================================|
-- | Acordare Permisiuni EXECUTARE FN/SP  |
-- |======================================|

-- Vizitator
GRANT EXECUTE ON dbo.FN_StatusStoc TO rol_vizitator;
GRANT EXECUTE ON dbo.FN_PretDupaDiscount TO rol_vizitator;
GRANT SELECT ON dbo.FN_ProduseDeLaFurnizor TO rol_vizitator;

-- Casier
GRANT EXECUTE ON dbo.SP_SeteazaStatusComanda TO rol_casier;
GRANT EXECUTE ON dbo.FN_NumeComplet TO rol_casier;
GRANT EXECUTE ON dbo.FN_ValoareTVA TO rol_casier;
GRANT EXECUTE ON dbo.FN_StatusStoc TO rol_casier;
GRANT EXECUTE ON dbo.FN_PretDupaDiscount TO rol_casier;

--Operator
GRANT EXECUTE ON dbo.SP_ReceptieMarfa TO rol_operator;
GRANT EXECUTE ON dbo.SP_AplicaReducere TO rol_operator;
GRANT EXECUTE ON dbo.SP_SeteazaStatusComanda TO rol_operator;
GRANT EXECUTE ON dbo.FN_ValoareStoc TO rol_operator;
GRANT SELECT ON dbo.FN_ProduseDeLaFurnizor TO rol_operator;
GRANT EXECUTE ON dbo.FN_StatusStoc TO rol_operator;

-- Manager (FULL ACCES)
GRANT EXECUTE ON dbo.SP_AngajareRapida TO rol_manager_filiala;     
GRANT EXECUTE ON dbo.SP_DaBonus TO rol_manager_filiala;             
GRANT EXECUTE ON dbo.SP_RaportVanzariAngajati TO rol_manager_filiala; 
GRANT EXECUTE ON dbo.SP_StergeClientSigur TO rol_manager_filiala;

GRANT EXECUTE ON dbo.SP_ReceptieMarfa TO rol_manager_filiala;
GRANT EXECUTE ON dbo.SP_AplicaReducere TO rol_manager_filiala;
GRANT EXECUTE ON dbo.SP_SeteazaStatusComanda TO rol_manager_filiala;

GRANT EXECUTE ON dbo.FN_AniVechime TO rol_manager_filiala;
GRANT EXECUTE ON dbo.FN_NumeComplet TO rol_manager_filiala;
GRANT EXECUTE ON dbo.FN_ValoareTVA TO rol_manager_filiala;
GRANT EXECUTE ON dbo.FN_ValoareStoc TO rol_manager_filiala;
GRANT EXECUTE ON dbo.FN_StatusStoc TO rol_manager_filiala;
GRANT SELECT ON dbo.FN_ProduseDeLaFurnizor TO rol_manager_filiala;

-- !!!! Retragere drepturi IN CAZ DE URGENTA !!!! 

REVOKE DELETE ON Client FROM rol_manager_filiala;
REVOKE DELETE ON Comanda FROM rol_manager_filiala;
REVOKE DELETE ON Factura FROM rol_manager_filiala;
REVOKE UPDATE ON Angajat FROM rol_manager_filiala;
REVOKE UPDATE ON vizitator_MoldCom.Produs FROM rol_operator;
GRANT UPDATE ON vizitator_MoldCom.Produs TO rol_operator
GRANT ALTER ANY ROLE TO rol_manager_filiala;
-- managerul va putea retrage drepturile oricui




-- ==========================================
--    CREAREA INFRASTRUCTURII DE CRIPTARE
-- ==========================================

-- Creare master key
CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'gaina12';

-- Creare certificate pentru criptare
CREATE CERTIFICATE MoldCom_CRIPT
WITH SUBJECT = 'Criptare date sensibile MoldCom';

-- Creare cheie simetrica AES 256
CREATE SYMMETRIC KEY MoldCom_KEY
WITH ALGORITHM = AES_256
ENCRYPTION BY CERTIFICATE MoldCom_CRIPT;


-- ================================
--   CRIPTAREA TABELULUI CLIENT
-- ================================

-- Adaugam coloana care va contine email-ul criptat
ALTER TABLE Client ADD 
    email_criptata VARBINARY(MAX);

-- Deschidem cheia pentru operatiile de criptare
OPEN SYMMETRIC KEY MoldCom_KEY
DECRYPTION BY CERTIFICATE MoldCom_CRIPT;

-- Criptam email-urile din Client 
UPDATE Client
SET email_criptata = EncryptByKey(
    Key_GUID('MoldCom_KEY'), 
    email
);

-- Inchidem cheia
CLOSE SYMMETRIC KEY MoldCom_KEY;

SELECT 
    id_client,
    nume,
    prenume,
    telefon,
    email_criptata AS email,
    adresa
FROM Client; 

-- ================================
--  CRIPTAREA TABELULUI FURNIZOR
-- ================================

-- Adaugam coloana criptata pentru email in Furnizor
ALTER TABLE Furnizor ADD 
    email_criptata VARBINARY(MAX);

-- Deschidem cheia
OPEN SYMMETRIC KEY MoldCom_KEY
DECRYPTION BY CERTIFICATE MoldCom_CRIPT;

-- Criptam email-urile
UPDATE Furnizor
SET email_criptata = EncryptByKey(
    Key_GUID('MoldCom_KEY'), 
    email
);

SELECT 
    id_furnizor,
    denumire,
    telefon,
    email_criptata AS email,
    adresa
FROM vizitator_MoldCom.Furnizor;
-- Inchidem cheia
CLOSE SYMMETRIC KEY MoldCom_KEY;


-- ================================
--      DECRIPTAREA DATELOR
-- ================================

-- DECRIPTARE CLIENT - Email
OPEN SYMMETRIC KEY MoldCom_KEY
DECRYPTION BY CERTIFICATE MoldCom_CRIPT;

SELECT 
    id_client,
    nume,
    prenume,
    telefon,
    CAST(DecryptByKey(email_criptata) AS NVARCHAR(100)) AS email,
    adresa
FROM Client;

CLOSE SYMMETRIC KEY MoldCom_KEY;

-- DECRIPTARE FURNIZOR - Email
OPEN SYMMETRIC KEY MoldCom_KEY
DECRYPTION BY CERTIFICATE MoldCom_CRIPT;

SELECT 
    id_furnizor,
    denumire,
    telefon,
    CAST(DecryptByKey(email_criptata) AS NVARCHAR(40)) AS email,
    adresa
FROM vizitator_MoldCom.Furnizor;

CLOSE SYMMETRIC KEY MoldCom_KEY;

-- ==============================
--           SCHEMA
-- ==============================
CREATE SCHEMA vizitator_MoldCom;

ALTER SCHEMA vizitator_MoldCom TRANSFER dbo.Produs;
ALTER SCHEMA vizitator_MoldCom TRANSFER dbo.Furnizor;
ALTER SCHEMA vizitator_MoldCom TRANSFER dbo.Comanda;

GRANT SELECT ON SCHEMA::vizitator_MoldCom TO vizitator_acI;


-- ==============================
--            INDEX
-- ==============================

CREATE NONCLUSTERED INDEX idx_Detal_comanda
ON Detaliu_Comanda(cantitate); 

CREATE NONCLUSTERED INDEX idx_Client_nume
ON Client(nume);

CREATE NONCLUSTERED INDEX idx_Client_prenume
ON Client(prenume);

CREATE NONCLUSTERED INDEX idx_Angajat_salariu
ON Angajat(salariu);

CREATE NONCLUSTERED INDEX idx_Factura_total
ON Factura(total);

CREATE NONCLUSTERED INDEX idx_DetaliuComanda_sub
ON Detaliu_Comanda(subtotal);

CREATE NONCLUSTERED INDEX idx_Client_telefon
ON Client(telefon);


-- ==============================
--         CONSTRANGERI
-- ==============================

-- Numele si prenumele (CLIENT) nu poate fi mai scurt de 1 caracter si nu poate contine cifre
ALTER TABLE Client
ADD CONSTRAINT ver_Client_Nume CHECK (LEN(nume) > 1 AND nume NOT LIKE '%[0-9]%'),
CONSTRAINT ver_Client_Prenume CHECK(LEN(prenume) > 1 AND prenume NOT LIKE '%[0-9]%');

--  Numele si prenumele (CLIENT) nu poate fi mai scurt de 1 caracter si nu poate contine cifre
ALTER TABLE Angajat
ADD CONSTRAINT ver_Angajat_Nume CHECK(LEN(nume) > 1 AND nume NOT LIKE '%[0-9]%'),
CONSTRAINT ver_Angajat_Prenume CHECK(LEN(prenume) > 1 AND prenume NOT LIKE '%[0-9]%');

-- Email urile trebuei sa contina numaidecat @ si .
ALTER TABLE Client
ADD CONSTRAINT ver_Client_Email CHECK(email LIKE '%@%.%');

ALTER TABLE Furnizor
ADD CONSTRAINT ver_Furnizor_Email CHECK(email LIKE '%@%.%');


-- Totaul din factura nu poate fi negativ
ALTER TABLE Factura
ADD CONSTRAINT ver_Factura_Total CHECK (total >= 0);

-- Compania a fost creata in 1992
ALTER TABLE Angajat
ADD CONSTRAINT ver_Angajat_DataAngajare CHECK(data_angajare >= '1992-02-12');

-- Nu poate fi produs cu pret negativ sau stoc negativ
ALTER TABLE Produs
ADD CONSTRAINT chk_Produs_Pozitiv CHECK (stoc >= 0 AND pret > 0);


--Status ul comenzii poate fi doar dintr o lista predefinita
ALTER TABLE Comanda
ADD CONSTRAINT chk_Comanda_Status CHECK (status IN ('Noua', 'In procesare', 'Finalizata', 'Anulata'));

-- Salariul angajatului nu poate depasi 100k
ALTER TABLE Angajat
ADD CONSTRAINT chk_Angajat_SalariuMax CHECK (salariu <= 100000);

-- Cantitatea produselor comandate trebuie sa fie intre 1 si 1000 buc.
ALTER TABLE Detaliu_Comanda
ADD CONSTRAINT chk_Cantitate CHECK (cantitate BETWEEN 1 AND 1000);


-- ==============================
--            SYNONYMS
-- ==============================

CREATE SYNONYM Produse FOR vizitator_MoldCom.Produs;

CREATE SYNONYM Employees FOR dbo.Angajat;

CREATE SYNONYM Destinatari FOR dbo.Client;

CREATE SYNONYM Incasari FOR dbo.Factura;

CREATE SYNONYM Parteneri FOR vizitator_MoldCom.Furnizor;

CREATE SYNONYM IstoricVanzari FOR vizitator_MoldCom.Comanda;

CREATE SYNONYM CosBunuri FOR dbo.Detaliu_Comanda;

CREATE SYNONYM Inventar FOR vizitator_MoldCom.Produs;

CREATE SYNONYM AddClient FOR dbo.SP_AdaugaClientNou;


-- ==============================
--             VEDERI
-- ==============================

CREATE VIEW Agenda_Angajati AS
SELECT 
    nume, 
    prenume, 
    functie, 
    'MoldCom Office' AS locatie -- Coloana virtuala
FROM dbo.Angajat;

CREATE VIEW Catalog_produse AS 
SELECT 
    P.denumire AS Produs, 
    P.pret, 
    P.stoc, 
    P.categorie, 
    F.denumire AS Furnizor, 
    F.telefon AS Telefon_Furnizor
FROM vizitator_MoldCom.Produs P
JOIN vizitator_MoldCom.Furnizor F ON P.id_furnizor = F.id_furnizor;


CREATE VIEW V_Stoc_Critic AS
SELECT 
    id_produs, 
    denumire, 
    stoc, 
    pret
FROM vizitator_MoldCom.Produs
WHERE stoc < 10;

CREATE VIEW Istoric_Comenzi AS 
SELECT 
    CL.nume + ' ' + CL.prenume AS Client,
    C.data_comanda,
    C.status,
    F.total AS Valoare_Factura
FROM vizitator_MoldCom.Comanda C
JOIN dbo.Client CL ON C.id_client = CL.id_client
LEFT JOIN dbo.Factura F ON C.id_comanda = F.id_comanda;

CREATE VIEW Top_Vanzari AS
SELECT 
    P.denumire, 
    SUM(D.cantitate) AS Unitati_Vandute, 
    SUM(D.subtotal) AS Total_Incasari
FROM dbo.Detaliu_Comanda D
JOIN vizitator_MoldCom.Produs P ON D.id_produs = P.id_produs
GROUP BY P.denumire;

CREATE VIEW V_Facturi_Recente AS
SELECT 
    id_factura, 
    data_emitere, 
    total, 
    metoda_plata
FROM dbo.Factura
WHERE data_emitere >= DATEADD(day, -30, GETDATE());

CREATE VIEW V_Valoare_Inventar AS
SELECT 
    categorie, 
    COUNT(*) AS Numar_Produse, 
    SUM(stoc) AS Total_Bucati, 
    SUM(pret * stoc) AS Valoare_Totala_Stoc
FROM vizitator_MoldCom.Produs
GROUP BY categorie;

CREATE VIEW V_Echipa_IT AS
SELECT * FROM dbo.Angajat
WHERE functie LIKE '%IT%' 
OR functie LIKE '%Technician%' 
OR functie LIKE '%Admin%';

CREATE VIEW V_Livrari_Active AS
SELECT 
    C.id_comanda, 
    CL.nume AS Destinatar, 
    CL.adresa AS Adresa_Livrare, 
    CL.telefon,
    C.status
FROM vizitator_MoldCom.Comanda C
JOIN dbo.Client CL ON C.id_client = CL.id_client
WHERE C.status IN ('Noua', 'In procesare');

SELECT * FROM V_Livrari_Active;



-- ==============================
--           TRANZACTII
-- ==============================

BEGIN TRANSACTION;

BEGIN TRY
    -- 1. Cream comanda (ID 101, Client 5, Angajat 10, Azi)
    INSERT INTO vizitator_MoldCom.Comanda (id_comanda, id_client, id_angajat, data_comanda, status)
    VALUES (101, 5, 10, GETDATE(), 'Noua');

    -- 2. Adaugam produsul in comanda (Comanda 101, Produs 1, 2 bucati)
    INSERT INTO Detaliu_Comanda (id_comanda, id_produs, cantitate, subtotal)
    VALUES (101, 1, 2, 17000.00); -- 2 * 8500 (Pret Laptop ASUS)

    -- 3. Scadem stocul produsului vandut
    UPDATE vizitator_MoldCom.Produs
    SET stoc = stoc - 2
    WHERE id_produs = 1;

    COMMIT TRANSACTION;
    PRINT 'Comanda a fost plasata si stocul actualizat cu succes.';
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    PRINT 'A aparut o eroare. Tranzactia a fost anulata.';
END CATCH;


BEGIN TRANSACTION;

-- Marim pretul cu 10% la Laptopuri
UPDATE vizitator_MoldCom.Produs
SET pret = pret * 1.10
WHERE categorie = 'Laptopuri';

-- Verificare de siguranta
IF EXISTS (SELECT 1 FROM vizitator_MoldCom.Produs WHERE pret > 50000)
BEGIN
    ROLLBACK TRANSACTION;
    PRINT 'Tranzactie anulata: Un pret a depasit limita admisa de 50.000!';
END
ELSE
BEGIN
    COMMIT TRANSACTION;
    PRINT 'Preturile la laptopuri au fost actualizate.';
END;


BEGIN TRANSACTION;

DECLARE @IdComandaDeAnulat INT = 90;

-- 1. Actualizam statusul comenzii
UPDATE vizitator_MoldCom.Comanda
SET status = 'Anulata'
WHERE id_comanda = @IdComandaDeAnulat;

-- 2. Returnam produsele in stoc pe baza detaliilor comenzii
UPDATE P
SET P.stoc = P.stoc + DC.cantitate
FROM vizitator_MoldCom.Produs P
JOIN Detaliu_Comanda DC ON P.id_produs = DC.id_produs
WHERE DC.id_comanda = @IdComandaDeAnulat;

COMMIT TRANSACTION;
PRINT 'Comanda a fost anulata si produsele au fost returnate in stoc.';


-- ==============================
--           PROCEDURI
-- ==============================

-- Se va adauga un agajat nou
CREATE PROCEDURE SP_AngajareRapida
    @ID INT,
    @Nume NVARCHAR(30),
    @Prenume NVARCHAR(30),
    @Functie NVARCHAR(40),
    @Salariu DECIMAL(10,2)
AS
BEGIN
    INSERT INTO Angajat (id_angajat, nume, prenume, functie, salariu, data_angajare)
    VALUES (@ID, @Nume, @Prenume, @Functie, @Salariu, GETDATE());
    PRINT 'Angajatul ' + @Nume + ' a fost inregistrat cu data de azi.';
END;

EXEC SP_AngajareRapida 2000, 'Lazari', 'Daniel_', 'Sofer', 10000;

-- Se adauga marfa in stoc
CREATE PROCEDURE SP_ReceptieMarfa
    @IdProdus INT,
    @CantitateIntrata INT
AS
BEGIN
    UPDATE vizitator_MoldCom.Produs
    SET stoc = stoc + @CantitateIntrata
    WHERE id_produs = @IdProdus;
END;

EXEC SP_ReceptieMarfa 97, 200;


-- Setare Status comanda
CREATE PROCEDURE SP_SeteazaStatusComanda
    @IdComanda INT,
    @StatusNou NVARCHAR(20)
AS
BEGIN
    -- Validam ca statusul e unul permis
    IF @StatusNou IN ('Noua', 'In procesare', 'Finalizata', 'Anulata')
    BEGIN
        UPDATE vizitator_MoldCom.Comanda
        SET status = @StatusNou
        WHERE id_comanda = @IdComanda;
    END
    ELSE
    BEGIN
        PRINT 'Eroare: Status invalid!';
    END
END;

EXEC SP_SeteazaStatusComanda 99, 'Anulata';

-- Aplicare reducere rapida (0.5 = 50%)
CREATE PROCEDURE SP_AplicaReducere
    @Categoria NVARCHAR(40),
    @Procent DECIMAL(5,2)
AS
BEGIN
    UPDATE vizitator_MoldCom.Produs
    SET pret = pret - (pret * @Procent)
    WHERE categorie = @Categoria;
    
    PRINT 'Preturile au fost actualizate.';
END;
EXEC SP_AplicaReducere 'Stocare', 0.2; -- 20% 

-- Afiseaza topul angajatilor
CREATE PROCEDURE SP_RaportVanzariAngajati
AS
BEGIN
    SELECT 
        A.nume, 
        A.prenume, 
        COUNT(C.id_comanda) AS Comenzi_Procesate,
        SUM(F.total) AS Total_Incasat
    FROM Angajat A
    JOIN vizitator_MoldCom.Comanda C ON A.id_angajat = C.id_angajat
    JOIN Factura F ON C.id_comanda = F.id_comanda
    GROUP BY A.nume, A.prenume
    ORDER BY Total_Incasat DESC;
END;
EXEC SP_RaportVanzariAngajati

--Stergere clienti care nu au comenzi
CREATE PROCEDURE SP_StergeClientSigur
    @IdClient INT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM vizitator_MoldCom.Comanda WHERE id_client = @IdClient)
    BEGIN
        PRINT 'Eroare: Clientul are comenzi in istoric si nu poate fi sters.';
    END
    ELSE
    BEGIN
        DELETE FROM Client WHERE id_client = @IdClient;
        PRINT 'Client sters cu succes.';
    END
END;

EXEC SP_StergeClientSigur 3000;

SELECT
    C.nume,
    C.prenume,
    C.email
FROM
    Client C
LEFT JOIN
    vizitator_MoldCom.Comanda CO ON C.id_client = CO.id_client
WHERE
    CO.id_comanda IS NULL;


-- Adaugare bonus angajat
CREATE PROCEDURE SP_DaBonus
    @IdAngajat INT,
    @SumaBonus DECIMAL(10,2)
AS
BEGIN
    UPDATE Angajat 
    SET salariu = salariu + @SumaBonus 
    WHERE id_angajat = @IdAngajat;
END;

EXEC SP_DaBonus 82, 4000; 

-- ===============[Proceduri de Afisare]=============== --

CREATE PROCEDURE SP_view_angajati
AS BEGIN
SELECT * FROM dbo.Angajat;
END;

CREATE PROCEDURE SP_view_Client
AS BEGIN 
SELECT * FROM dbo.Client;
END;

CREATE PROCEDURE SP_view_Detalii_Commanda
AS BEGIN
SELECT * FROM dbo.Detaliu_Comanda;
END;


CREATE PROCEDURE SP_view_Factura
AS BEGIN
SELECT * FROM dbo.Factura;
END;


CREATE PROCEDURE SP_view_Comanda
AS BEGIN
SELECT * FROM vizitator_MoldCom.Comanda;
END;

CREATE PROCEDURE SP_view_Furnizor
AS BEGIN 
SELECT denumire, telefon, adresa FROM vizitator_MoldCom.Furnizor;
END;

CREATE PROCEDURE SP_view_Produs
AS BEGIN 
SELECT * FROM vizitator_MoldCom.Produs;
END;








-- ==============================
--             FUNCTII
-- ==============================


CREATE FUNCTION FN_NumeComplet (@Nume NVARCHAR(40), @Prenume NVARCHAR(40))
RETURNS NVARCHAR(100)
AS
BEGIN
    RETURN @Nume + ' ' + @Prenume;
END;
--TEST
SELECT dbo.FN_NumeComplet('Tractoristul', 'Kolea') AS NumePrenume;

CREATE FUNCTION FN_ValoareTVA (@Pret DECIMAL(10,2))
RETURNS DECIMAL(10,2)
AS
BEGIN
    RETURN @Pret * 0.20;
END;
--TEST
SELECT dbo.FN_ValoareTVA(21400) AS TVA;

CREATE FUNCTION FN_StatusStoc (@Stoc INT)
RETURNS NVARCHAR(20)
AS
BEGIN
    DECLARE @Mesaj NVARCHAR(20);
    IF @Stoc = 0 SET @Mesaj = 'Epuizat';
    ELSE IF @Stoc < 10 SET @Mesaj = 'Limitat';
    ELSE SET @Mesaj = 'Stoc OK';
    RETURN @Mesaj;
END;
--TEST
SELECT dbo.FN_StatusStoc(2) AS Status_Stoc;

CREATE FUNCTION FN_AniVechime (@DataAngajarii DATE)
RETURNS INT
AS
BEGIN
    RETURN DATEDIFF(YEAR, @DataAngajarii, GETDATE());
END;
--TEST
SELECT dbo.FN_AniVechime('2007-02-13') AS Vechime;

CREATE FUNCTION FN_PretDupaDiscount (@PretInitial DECIMAL(10,2), @ProcentDiscount DECIMAL(5,2))
RETURNS DECIMAL(10,2)
AS
BEGIN
    RETURN @PretInitial - (@PretInitial * @ProcentDiscount / 100);
END;
--TEST
SELECT dbo.FN_PretDupaDiscount(45000, 45) AS Discount;


CREATE FUNCTION FN_ValoareStoc (@Pret DECIMAL(10,2), @Stoc INT)
RETURNS DECIMAL(10,2)
AS
BEGIN
    RETURN @Pret * @Stoc;
END;
--TEST
SELECT dbo.FN_ValoareStoc (2500, 6) AS Valoare;


CREATE FUNCTION FN_ProduseDeLaFurnizor (@IdFurnizor INT)
RETURNS TABLE
AS
RETURN(
    SELECT id_produs, denumire, stoc, pret 
    FROM vizitator_MoldCom.Produs
    WHERE id_furnizor = @IdFurnizor);
--TEST
SELECT * FROM dbo.FN_ProduseDeLaFurnizor(5);



-- ==============================
--            TRIGGER
-- ==============================

-- Nu se permite scaderea abuziva a salariului
CREATE TRIGGER VerificareSalariu
ON dbo.Angajat
AFTER UPDATE
AS
BEGIN
    IF EXISTS(
    SELECT 1
    FROM inserted unu
    JOIN deleted doi ON unu.id_angajat = doi.id_angajat
    WHERE unu.salariu < doi.salariu)
    BEGIN RAISERROR('Nu poti schimba salariul', 16,1);
    ROLLBACK TRANSACTION
    END
END;

-- Nu se accepta furnizorii cu domeniul specificat
CREATE TRIGGER StopEmail
ON vizitator_MoldCom.Furnizor
FOR INSERT, UPDATE
AS BEGIN
IF EXISTS (
SELECT 1 FROM inserted
WHERE email LIKE '%@tempmail.com%' OR email LIKE '%@spam.me%')
BEGIN
    RAISERROR('Nu poti adauga acest email', 16,1);
    ROLLBACK TRANSACTION
    END
END;


-- Facturile nu se modifica, doar se emit altele noi
CREATE TRIGGER StopModifyFactura
ON dbo.Factura
FOR UPDATE, DELETE
AS 
BEGIN
RAISERROR('Facturile nu se pot modifica sau sterge', 16, 1);
ROLLBACK TRANSACTION;
END;

UPDATE Factura
SET metoda_plata = 'CRIPTO'
WHERE id_factura = 5;

-- Avertizare Stoc
CREATE TRIGGER NotificareStoc
ON vizitator_MoldCom.Produs
AFTER UPDATE 
AS BEGIN
    DECLARE @NumeP NVARCHAR(65);
    DECLARE @StocNou INT;
    
    SELECT @NumeP = denumire, @StocNou = stoc
    FROM inserted
    IF @StocNou < 5 AND @StocNou >= 0
    BEGIN
        PRINT 'WARNING: Produsul: ' +@NumeP+'a ajuns la stoc critic. Comandati marfa!!';
    END
END;

-- Interzicerea modificarii data_angajare
CREATE TRIGGER StopModify_DataAngajare
ON dbo.Angajat
AFTER UPDATE
AS BEGIN
    IF UPDATE(data_angajare)
    BEGIN RAISERROR('Nu este permisa modificare datii de angajare', 16,1);
    ROLLBACK TRANSACTION;
    END
END;

-- Finalizare automata la emitearea facturii
CREATE TRIGGER AutoFinzalizare
ON dbo.Factura
AFTER INSERT
AS BEGIN
    UPDATE vizitator_MoldCom.Comanda
    SET status = 'Finalizata'
    FROM vizitator_MoldCom.Comanda C
    JOIN inserted i ON C.id_comanda = i.id_comanda;
    PRINT 'Comanda a fost marcata automat, factura a fost emisa';
END;



-- ==============================
--          TRANZACTII
-- ==============================


BEGIN TRANSACTION VanzareNoua;
BEGIN TRY
    INSERT INTO vizitator_MoldCom.Comanda (id_comanda, id_client, id_angajat, data_comanda, status)
    VALUES (500, 1, 6, GETDATE(), 'Noua');

    -- 2. Adaugam produsul 1 (Laptop) in comanda
    INSERT INTO Detaliu_Comanda (id_comanda, id_produs, cantitate, subtotal)
    VALUES (500, 1, 1, 8500.00);

    -- 3. Scadem 1 bucata din stocul produsului 1
    UPDATE vizitator_MoldCom.Produs 
    SET stoc = stoc - 1 
    WHERE id_produs = 1;

    COMMIT TRANSACTION VanzareNoua;
    PRINT 'Tranzactia 1 (Vanzare) reusita.';
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION VanzareNoua;
    PRINT 'Eroare la tranzactia 1.';
END CATCH;


-- Promovarea si ridicarea salariului unui angajat
BEGIN TRANSACTION PromovareAngajat
BEGIN TRY
     UPDATE Angajat
     SET functie = 'S Specialist IT', 
         Salariu = 20000
         WHERE id_angajat = 27;
COMMIT TRAN PromovareAngajat
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION PromovareAngajat
    PRINT 'Eroare la actualizare'; 
END CATCH;



BEGIN TRANSACTION Galea_Anulare81
BEGIN TRY

    DECLARE @ID_Produs INT;

-- Actualizam status
    UPDATE vizitator_MoldCom.Comanda  
    SET status = 'Anulata'
    WHERE id_comanda = 81;

    SET @ID_Produs = (
    SELECT id_produs
    FROM Detaliu_Comanda
    WHERE id_comanda = 81);

    UPDATE vizitator_MoldCom.Produs
    SET stoc = stoc + 1
    WHERE id_produs = @ID_Produs;

COMMIT TRAN Galea_Anulare81;
END TRY
BEGIN CATCH
ROLLBACK TRANSACTION Galea_Anulare81
PRINT 'Tranzactia nu a putut fi executata :('
END CATCH;

SELECT * FROM vizitator_MoldCom.Produs


BEGIN TRAN StergereClient 
BEGIN TRY

DELETE FROM Client
WHERE id_client = 100;

COMMIT TRANSACTION StergeClient;
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION StergeClient
    PRINT 'Nu am putut sterge acest client';
END CATCH;


-- Schimbarea furnizorului
BEGIN TRANSACTION SchimbFurnizor
BEGIN TRY

    UPDATE Produs
    SET id_furnizor = 5
    WHERE id_produs = 1;

COMMIT TRANSACTION SchimbFurnizor
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION SchimbFurnizor
    PRINT 'Eroare la schimbarea furnizorului!!!';
END CATCH;


BEGIN TRANSACTION AdaugareClient_Comanda
BEGIN TRY 

 INSERT INTO Client(id_client, nume, prenume, telefon, email, adresa)  
 VALUES (1001, 'Cebotari', 'Valera', '+37369384764', 'danieladarii@gmail.com', 'Camin1  CEITI');

 INSERT INTO Comanda(id_comanda, id_client, id_angajat, data_comanda, status)
 VALUES
 (911, 1001, 10, GETDATE(), 'Noua'); 

COMMIT TRAN AdaugareClient_Comanda
END TRY
BEGIN CATCH
    ROLLBACK TRAN AdaugareClient_Comanda;
    PRINT 'EROARE -> Nu poti adauga acest client la noi';
END CATCH;



-- ==============================
--          BACKUP
-- ==============================



BACKUP MASTER KEY TO FILE = 'C:\Backup\MoldCom_MasterKey.key'
ENCRYPTION BY PASSWORD = '1991';
GO

BACKUP CERTIFICATE MoldCom_CRIPT
TO FILE = 'C:\Backup\MoldCom_Certificate.cer'
WITH PRIVATE KEY (
    FILE = 'C:\Backup\MoldCom_Certificate.pvk',
    ENCRYPTION BY PASSWORD = '1991'
);
GO

USE master;

BACKUP DATABASE MoldCom2
TO DISK = 'C:\Backup\MoldCom2.bak'
WITH INIT;

-- Compression nu este suportat pe EXPRESS



RESTORE MASTER KEY FROM FILE = 'C:\Backup\MoldCom_MasterKey.key'
DECRYPTION BY PASSWORD = '1991' -- Parola setată la backup
ENCRYPTION BY PASSWORD = '1991'; -- Setează o parolă nouă 
GO

-- Deschide cheia si o leagă de Service Master Key 
OPEN MASTER KEY DECRYPTION BY PASSWORD = '1991';
ALTER MASTER KEY ADD ENCRYPTION BY SERVICE MASTER KEY;
GO

CREATE CERTIFICATE MoldCom_CRIPT
FROM FILE = 'C:\Backup\MoldCom_Certificate.cer'
WITH PRIVATE KEY (
    FILE = 'C:\Backup\MoldCom_Certificate.pvk',
    DECRYPTION BY PASSWORD = '1991' -- Parola folosită la backup-ul certificatului
);
GO

RESTORE DATABASE MoldCom2
FROM DISK = 'C:\Backup\MoldCom2.bak'
WITH  RECOVERY; 
GO

-- =========================================================================================================================================================
-- Adaptarea codului pt Interfata

-- ============================================================
--   MoldCom2 — Adaptare completă pentru interfața C# WinForms
--   Autor: Generat automat pe baza schemei existente
--   Versiune: 1.0
-- ============================================================
--
--  CUPRINS:
--   [1] Adăugare coloană Descriere în tabelul Produs
--   [2] Creare tabel Utilizatori cu FK → Client și Angajat
--   [3] Inserare descrieri pentru toate produsele (100 înregistrări)
--   [4] Inserare utilizatori de test pentru fiecare rol
--   [5] Stored Procedures cerute de DatabaseHelper.cs
--        · sp_GetAllProduse
--        · sp_SearchProduse
--        · sp_SearchProduseEmployee
--        · sp_AddProdus
--        · sp_UpdateProdus
--        · sp_DeleteProdus
--        · sp_UpdateStocProdus
--        · sp_ScadeStocProdus
--        · sp_Login
--        · sp_GetAllUtilizatori
--        · sp_AddUtilizator
--        · sp_UpdateUtilizator
--        · sp_DeleteUtilizator
--        · sp_GetRaportDepozit
--        · sp_GetProduseStocCritic
--   [6] Extindere SP_AdaugaClientNou (necesar sinonimului AddClient)
-- ============================================================

USE MoldCom2;
GO

-- ============================================================
--  [1]  ADĂUGARE COLOANĂ  Descriere_Produs  ÎN  Produs
-- ============================================================

IF NOT EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id = OBJECT_ID('vizitator_MoldCom.Produs')
      AND name = 'Descriere_Produs'
)
BEGIN
    ALTER TABLE vizitator_MoldCom.Produs
    ADD Descriere_Produs NVARCHAR(500) NULL;
    PRINT '[OK] Coloana Descriere_Produs adăugată în vizitator_MoldCom.Produs';
END
ELSE
    PRINT '[SKIP] Coloana Descriere_Produs există deja.';
GO

-- ============================================================
--  [2]  CREARE TABEL  Utilizatori
--       · FK → dbo.Client    (id_client   poate fi NULL – angajați)
--       · FK → dbo.Angajat   (id_angajat  poate fi NULL – clienți simpli)
-- ============================================================

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Utilizatori' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    CREATE TABLE dbo.Utilizatori (
        ID          INT IDENTITY(1,1) PRIMARY KEY,
        Username    NVARCHAR(60)  NOT NULL UNIQUE,
        Password    NVARCHAR(100) NOT NULL,       -- stocați hash în producție
        Rol         NVARCHAR(30)  NOT NULL        -- 'Admin','Manager','Casier','Operator','Vizitator'
                        CONSTRAINT chk_Util_Rol CHECK (Rol IN ('Admin','Manager','Casier','Operator','Vizitator')),
        id_client   INT NULL,
        id_angajat  INT NULL,
        CONSTRAINT FK_Utilizatori_Client
            FOREIGN KEY (id_client)  REFERENCES dbo.Client(id_client)
            ON DELETE SET NULL ON UPDATE CASCADE,
        CONSTRAINT FK_Utilizatori_Angajat
            FOREIGN KEY (id_angajat) REFERENCES dbo.Angajat(id_angajat)
            ON DELETE SET NULL ON UPDATE CASCADE
    );
    PRINT '[OK] Tabelul dbo.Utilizatori creat.';
END
ELSE
    PRINT '[SKIP] Tabelul dbo.Utilizatori există deja.';
GO

-- ============================================================
--  [3]  INSERARE DESCRIERI  pentru cele 100 de produse
-- ============================================================

BEGIN TRANSACTION InseriereDescrieri;
BEGIN TRY

    -- Laptopuri (1-5)
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Laptop performant cu procesor Intel Core i5 de generația a 11-a, ecran Full HD 15.6", RAM 8GB DDR4 și stocare SSD 512GB. Ideal pentru birou și studiu.' WHERE id_produs = 1;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Laptop accesibil cu procesor AMD Ryzen 5, ecran HD 15.6", RAM 8GB și HDD 1TB. Baterie de lungă durată, perfectă pentru uz zilnic.' WHERE id_produs = 2;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Laptop compact și elegant cu procesor Intel Core i7, display FHD 14", RAM 16GB și SSD 512GB. Design subțire cu autonomie excelentă.' WHERE id_produs = 3;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Laptop de business robust cu procesor Intel Core i7 de generația a 12-a, RAM 16GB DDR4, SSD NVMe 512GB și baterie 56Wh. Carcasă din aluminiu.' WHERE id_produs = 4;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Laptop versatil cu procesor AMD Ryzen 5 5500U, ecran IPS Full HD 15.6", RAM 8GB și SSD 512GB. Raport calitate-preț excelent.' WHERE id_produs = 5;

    -- Calculatoare Desktop (6-10)
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Calculator desktop de birou cu procesor Intel Core i5, RAM 8GB, SSD 256GB. Carcasă compactă tip Small Form Factor, consum redus de energie.' WHERE id_produs = 6;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Desktop profesional cu procesor Intel Core i7 de generatia a 10-a, RAM 16GB DDR4, SSD 512GB. Certificat pentru utilizare în medii enterprise.' WHERE id_produs = 7;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Desktop compact ThinkCentre cu procesor Intel Core i5, RAM 8GB ECC, SSD 256GB. Proiectat pentru fiabilitate maximă în medii de afaceri.' WHERE id_produs = 8;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Calculator desktop cu procesor Intel Core i5 de generatia a 10-a, RAM 8GB DDR4, HDD 1TB + SSD 256GB. Recomandat pentru sarcini de productivitate.' WHERE id_produs = 9;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Desktop accesibil cu procesor Intel Core i3, RAM 4GB DDR4, HDD 1TB. Carcasă Micro-Tower, potrivit pentru activități administrative de bază.' WHERE id_produs = 10;

    -- Monitoare (11-15)
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Monitor Full HD 24" cu panou VA, luminozitate 250 cd/m², timp de răspuns 5ms, port HDMI și VGA incluse. Cadru subțire, ochi puțin obosiți.' WHERE id_produs = 11;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Monitor QHD 27" IPS cu rezoluție 2560×1440, 75Hz, acoperire 99% sRGB. Perfect pentru designeri grafici și editare foto-video.' WHERE id_produs = 12;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Monitor profesional 24" IPS cu garanție de 3 ani on-site, acoperire 99% sRGB, port USB-C și HDMI, ajustare completă pe înălțime și pivotare.' WHERE id_produs = 13;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Monitor 4K UHD 27" cu panou IPS, 60Hz, HDR 400, acoperire 90% DCI-P3. Conexiuni DisplayPort, HDMI × 2, USB Hub 4 porturi integrat.' WHERE id_produs = 14;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Monitor compact 22" Full HD cu panou IPS, consum de doar 20W, protecție anti-reflexie și filtru lumină albastră. Ideal pentru spații mici.' WHERE id_produs = 15;

    -- Periferice – Tastaturi (16-18)
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Tastatură cu fir USB, layout RO/EN, taste silențioase cu profil scăzut. Rezistentă la stropire, design ergonomic, compatibilă cu Windows și Linux.' WHERE id_produs = 16;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Tastatură cu fir USB de la Microsoft, taste cu presă redusă, design rezistent. Compatibilă cu Windows 10/11, layout internațional, plug-and-play.' WHERE id_produs = 17;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Tastatură gaming cu membrană și iluminare LED verde. Taste anti-ghosting pentru 10 taste simultane, carcasă din plastic armat, cablu împletit 1.8m.' WHERE id_produs = 18;

    -- Periferice – Mouse-uri (19-21)
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Mouse wireless 2.4GHz cu receptor nano USB, senzor optic 1000 DPI, durată baterie 12 luni. Ergonomic, ușor și silențios, design ambidextru.' WHERE id_produs = 19;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Mouse wireless Bluetooth și 2.4GHz cu senzor optic precis 1800 DPI, 3 butoane programabile. Compatibil cu laptop și desktop, baterie 12 luni.' WHERE id_produs = 20;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Mouse gaming cu senzor optic 3600 DPI, 7 butoane programabile, iluminare RGB Chroma. Carcasă ergonomică cu grip lateral din cauciuc, cablu împletit.' WHERE id_produs = 21;

    -- Imprimante (22-26)
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Imprimantă laser monocrom cu viteza 30 ppm, rezoluție 600×600 dpi, toner cu 1000 pagini inclus. Wi-Fi, Ethernet, USB, duplex automat și ecran LCD.' WHERE id_produs = 22;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Imprimantă cu jet de cerneală color, ADF 20 coli, scanner CIS A4 600 dpi, Wi-Fi Direct și compatibilitate AirPrint/Mopria. Cartuşe hibride individuale.' WHERE id_produs = 23;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Imprimantă multifuncțională cu rezervoare mari de cerneală (EcoTank), cost per pagină extrem de mic. Printare, copiere, scanare, Wi-Fi, ecran LCD 6.1cm.' WHERE id_produs = 24;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Centru multifuncțional 3-în-1 (print, scan, copiere), laser color cu duplex automat, ADF 20 coli, Wi-Fi, Ethernet. Toner pentru 1000 pagini inclus.' WHERE id_produs = 25;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Imprimantă laser compactă monocrom, viteza 22 ppm, rezoluție 1200 dpi, toner starter inclus. USB 2.0, consum energie 8W în standby.' WHERE id_produs = 26;

    -- Rețea – Routere și Switch-uri (27-31)
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Router wireless dual-band AC1200, 4 antene externe, MU-MIMO, port USB pentru NAS, 4 porturi LAN Gigabit. Aplicație Tether pentru management ușor.' WHERE id_produs = 27;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Router high-end dual-band AC1900, procesor dual-core 1GHz, AiProtection powered by Trend Micro, VPN server integrat, 4 antene externe 3dBi.' WHERE id_produs = 28;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Router wireless dual-band AC1200, 4 porturi LAN Gigabit, USB 3.0 pentru partajare fișiere, QoS avansat și control parental. Setup simplu via aplicație.' WHERE id_produs = 29;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Switch desktop cu 8 porturi Gigabit, plug-and-play, carcasă metalică robustă, consum de doar 4.2W, IGMP Snooping pentru trafic video optimizat.' WHERE id_produs = 30;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Switch gestionat cu 16 porturi Gigabit, consolă web intuitivă, VLAN 802.1q, QoS, RSTP și monitorizare SNMP. Ideal pentru rețele SMB.' WHERE id_produs = 31;

    -- Stocare HDD/SSD (32-36)
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'HDD intern 3.5" SATA 6Gb/s, 1TB, 7200 RPM, cache 64MB. Garantie 2 ani, recomandat pentru desktop-uri și sisteme de supraveghere.' WHERE id_produs = 32;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'HDD intern 3.5" SATA III, 2TB, 5400 RPM, cache 256MB, optimizat pentru stocare NAS și backup. Certificat WD NASware 3.0, garantie 3 ani.' WHERE id_produs = 33;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'SSD 2.5" SATA III, 240GB, viteze de citire 500 MB/s și scriere 450 MB/s. Factor de formă standard, compatibil cu laptopuri și desktop-uri.' WHERE id_produs = 34;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'SSD NVMe M.2 PCIe 3.0, 500GB, viteze citire 3500 MB/s și scriere 2900 MB/s. V-NAND Samsung, garantie 5 ani, ideal pentru gaming și editare video.' WHERE id_produs = 35;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'SSD 2.5" SATA III, 1TB, viteze citire 540 MB/s, scriere 500 MB/s. Micron 3D NAND, MTF de 1.92 milioane ore, garantie 5 ani.' WHERE id_produs = 36;

    -- Componente PC (37-50)
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Memorie RAM DDR4 8GB, 3200 MHz, CL22, tensiune 1.2V. Compatibilă cu plăci de baza Intel și AMD, garantie pe viață. Plug-and-play.' WHERE id_produs = 37;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Kit RAM Vengeance LPX 16GB (2×8GB) DDR4, 3600 MHz, CL18, XMP 2.0. Profil low-profile, compatibil cu cooler-e mari, heatspreader din aluminiu.' WHERE id_produs = 38;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Placă video NVIDIA GeForce GTX 1650, 4GB GDDR6, 128-bit, Ray Tracing. Consum 75W (fara conector extern), potrivita pentru gaming 1080p si content creation.' WHERE id_produs = 39;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Placă video AMD Radeon RX 6600, 8GB GDDR6, 128-bit. Performanță excelentă 1080p/1440p, FSR 2.0 suportat, consum 132W, dual-BIOS.' WHERE id_produs = 40;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Procesor Intel Core i5-12400F, 6 nuclee/12 fire, frecvență de baza 2.5GHz, Turbo 4.4GHz, cache L3 18MB. Excelent pentru gaming și productivitate.' WHERE id_produs = 41;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Procesor AMD Ryzen 5 5600X, 6 nuclee/12 fire, 3.7GHz baza / 4.6GHz Turbo, cache L3 32MB, PCIe 4.0. Include cooler Wraith Stealth în cutie.' WHERE id_produs = 42;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Placă de baza ATX socket AM4, chipset B550, DDR4 dual-channel pana la 4600MHz OC, 2×M.2 NVMe, USB 3.2 Gen2, Ethernet 2.5Gb.' WHERE id_produs = 43;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Placă de baza Micro-ATX socket AM4, chipset B450, DDR4 pana la 3200MHz, 1×M.2, 4×SATA, DVI-D + HDMI, USB 3.2 Gen1.' WHERE id_produs = 44;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Sursă de alimentare 500W, certificare 80+ Bronze, ventilator 120mm, protecție OVP/OCP/SCP, cabluri modulare parțial. Garantie 3 ani.' WHERE id_produs = 45;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Sursă modulară 650W, 80+ Gold, ventilator 135mm cu control termic, protecție completă, cabluri plate anti-curl. Garantie 5 ani Corsair.' WHERE id_produs = 46;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Carcasă Mid-Tower ATX cu 2 ventilatoare frontale 120mm preinstalate, panoul lateral din sticlă temperată, 2×USB 3.0 front, suport radiator 360mm.' WHERE id_produs = 47;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Carcasă Micro-ATX compactă cu panou lateral acrilic, 1 ventilator 120mm preinstalat, suport radiator 240mm, 2×USB 3.0 + USB-C front.' WHERE id_produs = 48;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Cooler de procesor cu ventilator 92mm PWM, contact direct prin 4 heatpipe-uri, compatibil Intel LGA1700/1200 si AMD AM4/AM5. Pasta termică inclusă.' WHERE id_produs = 49;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Cooler premium cu ventilator NF-F12 iPPC 120mm, 6 heatpipe-uri nichel, compatibil cu toate socket-urile moderne. Nivel zgomot sub 22dB.' WHERE id_produs = 50;

    -- Telefoane (51-55)
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Smartphone 5G cu ecran Super AMOLED 6.4" 120Hz, cameră tripla 50+12+5MP, baterie 5000mAh cu incarcare 25W, procesor Exynos 1380, 8GB RAM.' WHERE id_produs = 51;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Smartphone cu ecran AMOLED 6.67" 120Hz, cameră principală 50MP, 5000mAh, incarcare 33W, Snapdragon 685, 4GB RAM + 128GB stocare.' WHERE id_produs = 52;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'iPhone cu cip A15 Bionic, ecran Super Retina XDR 6.1" OLED, cameră duala 12MP cu mod cinematic, 5G, baterie care tine toată ziua. iOS 16.' WHERE id_produs = 53;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Smartphone cu ecran LCD 6.4" 60Hz, Kirin 659, cameră principală 48MP AI, baterie 4200mAh, USB-C, stocare 128GB expansibilă via microSD.' WHERE id_produs = 54;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Smartphone 4G cu ecran AMOLED 6.6" 90Hz, Snapdragon 680, cameră 50MP, baterie 5000mAh cu incarcare 33W TurboPower, 4GB RAM + 128GB.' WHERE id_produs = 55;

    -- Tablete (56-60)
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Tabletă Android 11 cu ecran LCD TFT 10.5" WUXGA, Unisoc T618, 3GB RAM, 32GB stocare, baterie 7040mAh, Wi-Fi 5, Bluetooth 5.0.' WHERE id_produs = 56;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'iPad cu cip A13 Bionic, ecran Retina 10.2" True Tone, compatibil Apple Pencil gen 1 și Smart Keyboard. Wi-Fi 6, autonomie 10 ore, 64GB.' WHERE id_produs = 57;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Tabletă Android 11 cu ecran IPS 10.1" Full HD, MediaTek Helio P22T, 4GB RAM, 64GB stocare, baterie 7000mAh, slot SIM dual (4G opțional).' WHERE id_produs = 58;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Tabletă compactă cu ecran IPS 9.7", Kirin 710A, 2GB RAM, 32GB stocare, baterie 5100mAh, Wi-Fi dual-band, suport stilou M-Pencil (separat).' WHERE id_produs = 59;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Tabletă Fire HD cu ecran 10.1" 1080p, MediaTek MT8183, 3GB RAM, 32GB stocare, baterie 12 ore, acces la Amazon Store, control parental inclus.' WHERE id_produs = 60;

    -- TV și Audio (61-72)
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Smart TV LED 43" 4K Crystal UHD, procesor Crystal 4K, HDR10+, Tizen OS, alexa Built-in, AirPlay 2. 3×HDMI, 2×USB, Ethernet, Wi-Fi.' WHERE id_produs = 61;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Smart TV OLED 50" 4K, procesor α7 Gen5 AI, webOS 22, ThinQ AI cu Magic Remote, Dolby Vision IQ și Dolby Atmos, Gaming Mode 120Hz.' WHERE id_produs = 62;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Smart TV 55" 4K HDR OLED, procesor X1 Ultimate, Android TV, Google Assistant, Dolby Vision/Atmos, HDMI 2.1 × 4, Acoustic Surface Audio+.' WHERE id_produs = 63;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Set boxe stereo 2.1 cu subwoofer 8W + sateliți 2×5W, conector 3.5mm, control volum frontal, potrivit pentru PC și laptop. Garantie 2 ani.' WHERE id_produs = 64;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Boxe desktop compacte stereo 2.0, driver 2×4.4W, conector USB alimentare + jack 3.5mm audio, design modern minimalist. Plug-and-play.' WHERE id_produs = 65;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Căști over-ear wireless cu anulare activă a zgomotului (ANC) la nivel industrial, Bluetooth 5.0, autonomie 30 ore, sunet Hi-Res certificat.' WHERE id_produs = 66;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Căști on-ear cu fir 3.5mm, driver 32mm, răspuns în frecvența 20Hz-20kHz, microfon inline cu buton de apel, design pliant compact.' WHERE id_produs = 67;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Căști gaming surround virtual 7.1, driver 50mm, microfon detașabil cu anulare zgomot, USB + jack 3.5mm, pernițe din piele și memorie.' WHERE id_produs = 68;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Webcam Full HD 1080p 30fps, câmp vizual 78°, autofocus, microfon stereo cu anulare zgomot, clip universal pentru monitor. Plug-and-play USB.' WHERE id_produs = 69;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Webcam HD 720p 30fps cu TrueColor pentru culori fidele și microfon omni-directional integrat. Compatibil cu Teams, Zoom și Skype. USB 2.0.' WHERE id_produs = 70;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Microfon USB condenser cu 4 modele polare (cardioid, bidirecțional, omnidirecțional, stereo), câștig ajustabil, monitorizare directă fara latența.' WHERE id_produs = 71;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Microfon USB cardioid compact cu capsulă condenser 14mm, nivel zgomot propriu <15dBa, braț de birou inclus. Plug-and-play pe Windows și Mac.' WHERE id_produs = 72;

    -- Alimentare (73-75, 100)
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'UPS Line-Interactive 650VA/400W, baterie 12V/7Ah înlocuibilă de utilizator, protecție la 8 prize, port USB de management, software PowerChute.' WHERE id_produs = 73;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'UPS cu tehnologie GreenPower 850VA/510W, 12 prize NEMA protejate, LCD cu indicatori, AVR, port USB, compatibil cu PowerPanel Business Edition.' WHERE id_produs = 74;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Stabilizator de tensiune electronică 1000VA cu reglaj automat (AVR) ±10%, timp de comutare <20ms, 2 prize Schuko, indicator LED status.' WHERE id_produs = 75;

    -- Accesorii (76-99)
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Cablu HDMI 2.0 de 2 metri, suportă 4K@60Hz și HDR, conector placat cu aur, blindaj dublu anti-interferențe, compatibil cu TV, monitor, console.' WHERE id_produs = 76;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Cablu USB-C 1m, suportă incarcare 60W Power Delivery și transfer date 480 Mbps. Manta din nylon împletit, conector zincat rezistent la îndoire.' WHERE id_produs = 77;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Hub USB 3.0 cu 4 porturi și indicator LED. Transfer date pana la 5 Gbps pe port, alimentare prin cablu USB-A, compatibil cu Windows/Mac/Linux.' WHERE id_produs = 78;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Adaptor USB-C la HDMI 4K@30Hz, plug-and-play, compatibil cu MacBook, iPad Pro și laptopuri cu Thunderbolt 3/4. Carcasă din aliaj de aluminiu.' WHERE id_produs = 79;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Card microSDXC 64GB UHS-I clasa 10 / U3 / V30, viteza citire 100 MB/s, scriere 60 MB/s. Rezistent la apă, temperaturi extreme și radiații X.' WHERE id_produs = 80;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Card microSDXC 128GB UHS-I clasa 10 / U3 / V30 / A2, citire 130 MB/s, scriere 80 MB/s. Adaptor SD inclus, ideal pentru camere acțiune și drone.' WHERE id_produs = 81;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Stick USB 3.0 de 32GB cu design capac retractabil, viteze citire 80 MB/s, scriere 20 MB/s. Compatibil Windows, Mac, Linux și smart TV.' WHERE id_produs = 82;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Stick USB 3.1 de 64GB ultra-subțire, citire 100 MB/s, scriere 30 MB/s. Indicator LED de activitate, capac inclus, design compact care nu blochează porturile vecine.' WHERE id_produs = 83;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Geantă laptop 15.6" cu compartiment principal padded, buzunar frontal organizator, curea de umar reglabilă, material rezistent la stropire.' WHERE id_produs = 84;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Rucsac laptop 17" cu compartiment ergonomic padded, spate ventilat, port USB extern de incarcare, volum 30L, material rezistent la apă.' WHERE id_produs = 85;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Mouse pad gaming extins (800×300×3mm) cu iluminare RGB 14 moduri, suprafața textilă optimizată pentru senzori optici și laser. USB powered.' WHERE id_produs = 86;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Suport laptop reglabil cu 6 unghiuri (15°-30°), aluminiu anodizat, sistem de ventilare activ cu 2 ventilatoare USB, se pliaza compact.' WHERE id_produs = 87;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Lampă LED de birou alimentată prin USB, 3 niveluri de luminozitate, lumina caldă 3000K, flexibilă la 360°. Nu ocupă priză, consumă doar 5W.' WHERE id_produs = 88;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Power bank 10000mAh cu 2 ieșiri USB-A 5V/2.4A, 1 intrare Micro-USB + 1 USB-C, indicator LED nivel baterie. Certificat CE, greutate 220g.' WHERE id_produs = 89;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Power bank 20000mAh cu Quick Charge 3.0, 2×USB-A + 1×USB-C PD 18W, display LED digital, incarcare simultana 3 dispozitive. Greutate 420g.' WHERE id_produs = 90;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Incarcator wireless Qi 15W compatibil cu Samsung Galaxy S/A, iPhone 13/14 (cu carcasă Qi), suportă incarcare prin carcasă pana la 5mm grosime.' WHERE id_produs = 91;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Incarcator USB-C GaN 65W cu Quick Charge 4.0 și Power Delivery 3.0. Un singur port USB-C, compact de 45g, compatibil cu laptop, tableta, telefon.' WHERE id_produs = 92;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Folie de protecție anti-zero și anti-amprente pentru laptop 15.6" universală. Material TPU auto-vindecare, nu lasă bule, se aplica și se scoate ușor.' WHERE id_produs = 93;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Husă transparentă premium pentru Samsung Galaxy A54, TPU flexibil anti-ingalbenire, colțuri armate anti-șoc 2m drop-test, butoane acoperite.' WHERE id_produs = 94;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Husă pentru iPhone 13 MagSafe compatibilă, silicon lichid anti-alunecare, protecție camerei ridicată +2mm, microfibra interior. Disponibil mai multe culori.' WHERE id_produs = 95;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Filtru de confidențialitate 15.6" cu unghi de vizualizare ±30°, anti-reflexie, protecție UV, se atașează magnetic sau cu clipsuri pe marginea ecranului.' WHERE id_produs = 96;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Suport monitor ajustabil cu brat articulat simplu, înălțime 0-40cm, rotire 360°, înclinare ±45°, compatibil cu orice monitor VESA 75×75/100×100.' WHERE id_produs = 97;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Brat monitor cu articulatie dubla, montare pe blat sau prindere prin gaura, VESA 75/100, sarcina maxima 9kg, gestionare cabluri integrata.' WHERE id_produs = 98;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Kit curatare electronice cu spray 100ml anti-static, pana cu fibra optica, 10 șervețele pre-umezite și 5 bețisoare cu varf din microfibra.' WHERE id_produs = 99;
    UPDATE vizitator_MoldCom.Produs SET Descriere_Produs = 'Prelungitor de rețea cu 6 prize Schuko, protecție supratensiune (>300 jouli), separator magnetotermic, cablu 1.5m secțiune 1.5mm², cu întrerupator.' WHERE id_produs = 100;

    COMMIT TRANSACTION InseriereDescrieri;
    PRINT '[OK] Descrierile au fost inserate pentru toate cele 100 de produse.';

END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION InseriereDescrieri;
    PRINT '[EROARE] Inserare descrieri eșuată: ' + ERROR_MESSAGE();
END CATCH;
GO

-- ============================================================
--  [4]  INSERARE UTILIZATORI DE TEST
--       · 1 utilizator Admin (fara legatura cu Client/Angajat)
--       · 1 Manager  → legat de Angajat 1
--       · 1 Casier   → legat de Angajat 6
--       · 1 Operator → legat de Angajat 19
--       · 1 Vizitator→ legat de Client 1
-- ============================================================

BEGIN TRANSACTION InseriereUtilizatori;
BEGIN TRY

    IF NOT EXISTS (SELECT 1 FROM dbo.Utilizatori WHERE Username = 'admin')
        INSERT INTO dbo.Utilizatori (Username, Password, Rol, id_client, id_angajat)
        VALUES ('admin', 'Admin@2024!', 'Admin', NULL, NULL);

    IF NOT EXISTS (SELECT 1 FROM dbo.Utilizatori WHERE Username = 'manager_lungu')
        INSERT INTO dbo.Utilizatori (Username, Password, Rol, id_client, id_angajat)
        VALUES ('manager_lungu', 'Manager@2024!', 'Manager', NULL, 1);  -- Lungu Victor, Director General

    IF NOT EXISTS (SELECT 1 FROM dbo.Utilizatori WHERE Username = 'casier_russu')
        INSERT INTO dbo.Utilizatori (Username, Password, Rol, id_client, id_angajat)
        VALUES ('casier_russu', 'Casier@2024!', 'Casier', NULL, 6);    -- Russu Ana, Specialist Vanzari

    IF NOT EXISTS (SELECT 1 FROM dbo.Utilizatori WHERE Username = 'operator_guzun')
        INSERT INTO dbo.Utilizatori (Username, Password, Rol, id_client, id_angajat)
        VALUES ('operator_guzun', 'Operator@2024!', 'Operator', NULL, 19); -- Guzun Petru, Admin Depozit

    IF NOT EXISTS (SELECT 1 FROM dbo.Utilizatori WHERE Username = 'client_popescu')
        INSERT INTO dbo.Utilizatori (Username, Password, Rol, id_client, id_angajat)
        VALUES ('client_popescu', 'Client@2024!', 'Vizitator', 1, NULL); -- Popescu Ion

    COMMIT TRANSACTION InseriereUtilizatori;
    PRINT '[OK] Utilizatorii de test au fost inserati.';

END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION InseriereUtilizatori;
    PRINT '[EROARE] Inserare utilizatori eșuata: ' + ERROR_MESSAGE();
END CATCH;
GO

-- ============================================================
--  [5]  STORED PROCEDURES  pentru DatabaseHelper.cs
-- ============================================================

-- ── Produse ──────────────────────────────────────────────────

-- Mapare câmpuri C# → SQL:
--   Cod       → CAST(id_produs AS NVARCHAR)
--   Nume      → denumire
--   Categorie → categorie
--   Pret      → pret
--   Descriere → Descriere_Produs
--   Cantitate → stoc
--   Locatie   → NULL (coloana nu există, returnăm '' )

-- sp_GetAllProduse
IF OBJECT_ID('dbo.sp_GetAllProduse', 'P') IS NOT NULL DROP PROCEDURE dbo.sp_GetAllProduse;
GO
CREATE PROCEDURE dbo.sp_GetAllProduse
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        CAST(id_produs AS NVARCHAR(20))  AS Cod,
        denumire                          AS Nume,
        categorie                         AS Categorie,
        pret                              AS Pret,
        ISNULL(Descriere_Produs, '')      AS Descriere,
        stoc                              AS Cantitate,
        CAST(id_furnizor AS NVARCHAR(20)) AS Locatie   -- folosim ID furnizor ca "locație"
    FROM vizitator_MoldCom.Produs
    ORDER BY id_produs;
END;
GO

-- sp_SearchProduse  (căutare pentru clienți – doar produse cu stoc > 0)
IF OBJECT_ID('dbo.sp_SearchProduse', 'P') IS NOT NULL DROP PROCEDURE dbo.sp_SearchProduse;
GO
CREATE PROCEDURE dbo.sp_SearchProduse
    @SearchTerm NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        CAST(id_produs AS NVARCHAR(20))  AS Cod,
        denumire                          AS Nume,
        categorie                         AS Categorie,
        pret                              AS Pret,
        ISNULL(Descriere_Produs, '')      AS Descriere,
        stoc                              AS Cantitate,
        CAST(id_furnizor AS NVARCHAR(20)) AS Locatie
    FROM vizitator_MoldCom.Produs
    WHERE stoc > 0
      AND (
            denumire          LIKE '%' + @SearchTerm + '%'
         OR categorie         LIKE '%' + @SearchTerm + '%'
         OR Descriere_Produs  LIKE '%' + @SearchTerm + '%'
      )
    ORDER BY denumire;
END;
GO

-- sp_SearchProduseEmployee  (căutare pentru angajați – include stoc 0)
IF OBJECT_ID('dbo.sp_SearchProduseEmployee', 'P') IS NOT NULL DROP PROCEDURE dbo.sp_SearchProduseEmployee;
GO
CREATE PROCEDURE dbo.sp_SearchProduseEmployee
    @SearchTerm NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        CAST(id_produs AS NVARCHAR(20))  AS Cod,
        denumire                          AS Nume,
        categorie                         AS Categorie,
        pret                              AS Pret,
        ISNULL(Descriere_Produs, '')      AS Descriere,
        stoc                              AS Cantitate,
        CAST(id_furnizor AS NVARCHAR(20)) AS Locatie
    FROM vizitator_MoldCom.Produs
    WHERE denumire         LIKE '%' + @SearchTerm + '%'
       OR categorie        LIKE '%' + @SearchTerm + '%'
       OR Descriere_Produs LIKE '%' + @SearchTerm + '%'
       OR CAST(id_produs AS NVARCHAR) = @SearchTerm
    ORDER BY denumire;
END;
GO

-- sp_AddProdus
IF OBJECT_ID('dbo.sp_AddProdus', 'P') IS NOT NULL DROP PROCEDURE dbo.sp_AddProdus;
GO
CREATE PROCEDURE dbo.sp_AddProdus
    @Cod        NVARCHAR(20),
    @Nume       NVARCHAR(60),
    @Categorie  NVARCHAR(40),
    @Pret       DECIMAL(10,2),
    @Descriere  NVARCHAR(500),
    @Cantitate  INT,
    @Locatie    NVARCHAR(100)   -- interpretat ca ID furnizor (numeric); ignorat dacă nu e numeric
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @IdFurnizor INT = TRY_CAST(@Locatie AS INT);

    -- Dacă ID-ul furnizorului nu există, folosim furnizorul 1 ca default
    IF NOT EXISTS (SELECT 1 FROM vizitator_MoldCom.Furnizor WHERE id_furnizor = @IdFurnizor)
        SET @IdFurnizor = 1;

    INSERT INTO vizitator_MoldCom.Produs
        (id_produs, denumire, pret, stoc, categorie, Descriere_Produs, id_furnizor)
    VALUES
        (TRY_CAST(@Cod AS INT), @Nume, @Pret, @Cantitate, @Categorie, @Descriere, @IdFurnizor);
END;
GO

-- sp_UpdateProdus
IF OBJECT_ID('dbo.sp_UpdateProdus', 'P') IS NOT NULL DROP PROCEDURE dbo.sp_UpdateProdus;
GO
CREATE PROCEDURE dbo.sp_UpdateProdus
    @Cod        NVARCHAR(20),
    @Nume       NVARCHAR(60),
    @Categorie  NVARCHAR(40),
    @Pret       DECIMAL(10,2),
    @Descriere  NVARCHAR(500),
    @Cantitate  INT,
    @Locatie    NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @IdFurnizor INT = TRY_CAST(@Locatie AS INT);
    IF NOT EXISTS (SELECT 1 FROM vizitator_MoldCom.Furnizor WHERE id_furnizor = @IdFurnizor)
        SET @IdFurnizor = NULL;  -- nu schimba furnizorul dacă nu e valid

    UPDATE vizitator_MoldCom.Produs
    SET
        denumire         = @Nume,
        pret             = @Pret,
        stoc             = @Cantitate,
        categorie        = @Categorie,
        Descriere_Produs = @Descriere,
        id_furnizor      = ISNULL(@IdFurnizor, id_furnizor)
    WHERE id_produs = TRY_CAST(@Cod AS INT);
END;
GO

-- sp_DeleteProdus
IF OBJECT_ID('dbo.sp_DeleteProdus', 'P') IS NOT NULL DROP PROCEDURE dbo.sp_DeleteProdus;
GO
CREATE PROCEDURE dbo.sp_DeleteProdus
    @Cod NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    -- Verificăm mai întai dacă produsul apare în comenzi active
    IF EXISTS (
        SELECT 1 FROM dbo.Detaliu_Comanda DC
        JOIN vizitator_MoldCom.Comanda C ON DC.id_comanda = C.id_comanda
        WHERE DC.id_produs = TRY_CAST(@Cod AS INT)
          AND C.status IN ('Noua', 'In procesare')
    )
    BEGIN
        RAISERROR('Produsul are comenzi active și nu poate fi șters.', 16, 1);
        RETURN;
    END;

    DELETE FROM vizitator_MoldCom.Produs WHERE id_produs = TRY_CAST(@Cod AS INT);
END;
GO

-- sp_UpdateStocProdus  (setează stocul la o valoare exactă)
IF OBJECT_ID('dbo.sp_UpdateStocProdus', 'P') IS NOT NULL DROP PROCEDURE dbo.sp_UpdateStocProdus;
GO
CREATE PROCEDURE dbo.sp_UpdateStocProdus
    @Cod            NVARCHAR(20),
    @CantitateNoua  INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE vizitator_MoldCom.Produs
    SET stoc = @CantitateNoua
    WHERE id_produs = TRY_CAST(@Cod AS INT);
END;
GO

-- sp_ScadeStocProdus  (scade din stocul existent)
IF OBJECT_ID('dbo.sp_ScadeStocProdus', 'P') IS NOT NULL DROP PROCEDURE dbo.sp_ScadeStocProdus;
GO
CREATE PROCEDURE dbo.sp_ScadeStocProdus
    @Cod        NVARCHAR(20),
    @Cantitate  INT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @StocCurent INT;
    SELECT @StocCurent = stoc FROM vizitator_MoldCom.Produs WHERE id_produs = TRY_CAST(@Cod AS INT);

    IF @StocCurent IS NULL
    BEGIN
        RAISERROR('Produsul nu a fost găsit.', 16, 1); RETURN;
    END;
    IF @StocCurent < @Cantitate
    BEGIN
        RAISERROR('Stoc insuficient pentru operație.', 16, 1); RETURN;
    END;

    UPDATE vizitator_MoldCom.Produs
    SET stoc = stoc - @Cantitate
    WHERE id_produs = TRY_CAST(@Cod AS INT);
END;
GO

-- ── Utilizatori / Autentificare ────────────────────────────────

-- sp_Login
IF OBJECT_ID('dbo.sp_Login', 'P') IS NOT NULL DROP PROCEDURE dbo.sp_Login;
GO
CREATE PROCEDURE dbo.sp_Login
    @Username NVARCHAR(60),
    @Password NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID, Username, Password, Rol
    FROM dbo.Utilizatori
    WHERE Username = @Username
      AND Password = @Password;   -- în producție: comparați hash-uri
END;
GO

-- sp_GetAllUtilizatori
IF OBJECT_ID('dbo.sp_GetAllUtilizatori', 'P') IS NOT NULL DROP PROCEDURE dbo.sp_GetAllUtilizatori;
GO
CREATE PROCEDURE dbo.sp_GetAllUtilizatori
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID, Username, Password, Rol, id_client, id_angajat
    FROM dbo.Utilizatori
    ORDER BY Rol, Username;
END;
GO

-- sp_AddUtilizator
IF OBJECT_ID('dbo.sp_AddUtilizator', 'P') IS NOT NULL DROP PROCEDURE dbo.sp_AddUtilizator;
GO
CREATE PROCEDURE dbo.sp_AddUtilizator
    @Username   NVARCHAR(60),
    @Password   NVARCHAR(100),
    @Rol        NVARCHAR(30),
    @id_client  INT = NULL,
    @id_angajat INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.Utilizatori (Username, Password, Rol, id_client, id_angajat)
    VALUES (@Username, @Password, @Rol, @id_client, @id_angajat);
END;
GO

-- sp_UpdateUtilizator
IF OBJECT_ID('dbo.sp_UpdateUtilizator', 'P') IS NOT NULL DROP PROCEDURE dbo.sp_UpdateUtilizator;
GO
CREATE PROCEDURE dbo.sp_UpdateUtilizator
    @ID         INT,
    @Username   NVARCHAR(60),
    @Password   NVARCHAR(100),
    @Rol        NVARCHAR(30),
    @id_client  INT = NULL,
    @id_angajat INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.Utilizatori
    SET Username   = @Username,
        Password   = @Password,
        Rol        = @Rol,
        id_client  = @id_client,
        id_angajat = @id_angajat
    WHERE ID = @ID;
END;
GO

-- sp_DeleteUtilizator
IF OBJECT_ID('dbo.sp_DeleteUtilizator', 'P') IS NOT NULL DROP PROCEDURE dbo.sp_DeleteUtilizator;
GO
CREATE PROCEDURE dbo.sp_DeleteUtilizator
    @ID INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM dbo.Utilizatori WHERE ID = @ID;
END;
GO

-- ── Rapoarte ──────────────────────────────────────────────────

-- sp_GetRaportDepozit  → returnează (TotalProduseUnice, TotalBucatiStoc, ValoareTotalaDepozit)
IF OBJECT_ID('dbo.sp_GetRaportDepozit', 'P') IS NOT NULL DROP PROCEDURE dbo.sp_GetRaportDepozit;
GO
CREATE PROCEDURE dbo.sp_GetRaportDepozit
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        COUNT(*)              AS TotalProduseUnice,
        SUM(stoc)             AS TotalBucatiStoc,
        SUM(pret * stoc)      AS ValoareTotalaDepozit
    FROM vizitator_MoldCom.Produs;
END;
GO

-- sp_GetProduseStocCritic  → produse cu stoc < 10
--   returnează (Cod, Nume, Cantitate) conform MapProdus din GetProduseStocCritic
IF OBJECT_ID('dbo.sp_GetProduseStocCritic', 'P') IS NOT NULL DROP PROCEDURE dbo.sp_GetProduseStocCritic;
GO
CREATE PROCEDURE dbo.sp_GetProduseStocCritic
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        CAST(id_produs AS NVARCHAR(20)) AS Cod,
        denumire                         AS Nume,
        stoc                             AS Cantitate
    FROM vizitator_MoldCom.Produs
    WHERE stoc < 10
    ORDER BY stoc ASC;
END;
GO

-- ============================================================
--  [6]  SP_AdaugaClientNou  (necesar pentru sinonimul AddClient)
-- ============================================================

IF OBJECT_ID('dbo.SP_AdaugaClientNou', 'P') IS NOT NULL DROP PROCEDURE dbo.SP_AdaugaClientNou;
GO
CREATE PROCEDURE dbo.SP_AdaugaClientNou
    @Nume    NVARCHAR(40),
    @Prenume NVARCHAR(40),
    @Telefon NVARCHAR(20),
    @Email   NVARCHAR(100),
    @Adresa  NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @NewID INT = (SELECT ISNULL(MAX(id_client), 0) + 1 FROM dbo.Client);

    INSERT INTO dbo.Client (id_client, nume, prenume, telefon, email, adresa)
    VALUES (@NewID, @Nume, @Prenume, @Telefon, @Email, @Adresa);

    SELECT @NewID AS id_client_nou;
    PRINT 'Client nou adăugat cu ID: ' + CAST(@NewID AS NVARCHAR);
END;
GO

-- ============================================================
--  VERIFICARE FINALĂ
-- ============================================================

PRINT '============================================================';
PRINT ' VERIFICARE STRUCTURĂ DUPĂ MIGRARE';
PRINT '============================================================';

-- Coloana Descriere
SELECT
    'Coloane Produs' AS Verificare,
    name             AS Coloana,
    TYPE_NAME(system_type_id) AS Tip
FROM sys.columns
WHERE object_id = OBJECT_ID('vizitator_MoldCom.Produs')
  AND name IN ('id_produs','denumire','pret','stoc','categorie','Descriere_Produs','id_furnizor');

-- Tabelul Utilizatori
SELECT
    'Tabel Utilizatori' AS Verificare,
    name                AS Coloana,
    TYPE_NAME(system_type_id) AS Tip,
    is_nullable
FROM sys.columns
WHERE object_id = OBJECT_ID('dbo.Utilizatori');

-- FK-uri pe Utilizatori
SELECT
    'FK Utilizatori' AS Verificare,
    fk.name          AS FK_Nume,
    tp.name          AS Tabel_Parinte,
    cp.name          AS Coloana_Parinte
FROM sys.foreign_keys fk
JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
JOIN sys.tables tp               ON fk.referenced_object_id = tp.object_id
JOIN sys.columns cp              ON fkc.referenced_object_id = cp.object_id
                                 AND fkc.referenced_column_id = cp.column_id
WHERE fk.parent_object_id = OBJECT_ID('dbo.Utilizatori');

-- Produse cu descriere
SELECT
    'Produse cu descriere' AS Verificare,
    COUNT(*)               AS Total,
    SUM(CASE WHEN Descriere_Produs IS NOT NULL THEN 1 ELSE 0 END) AS Cu_Descriere
FROM vizitator_MoldCom.Produs;

-- Stored Procedures create
SELECT
    'SP create' AS Verificare,
    name        AS Procedura,
    create_date AS Creat_La
FROM sys.procedures
WHERE name IN (
    'sp_GetAllProduse','sp_SearchProduse','sp_SearchProduseEmployee',
    'sp_AddProdus','sp_UpdateProdus','sp_DeleteProdus',
    'sp_UpdateStocProdus','sp_ScadeStocProdus',
    'sp_Login','sp_GetAllUtilizatori','sp_AddUtilizator',
    'sp_UpdateUtilizator','sp_DeleteUtilizator',
    'sp_GetRaportDepozit','sp_GetProduseStocCritic',
    'SP_AdaugaClientNou'
)
ORDER BY name;

PRINT '[DONE] Migrarea bazei de date MoldCom2 s-a finalizat cu succes.';
GO

-- Proceduri V2 

USE MoldCom2;
GO

-- ──────────────────────────────────────────────────────────
--  sp_GetClientByUserId
--  Returnează datele din dbo.Client legate de un utilizator
-- ──────────────────────────────────────────────────────────
IF OBJECT_ID('dbo.sp_GetClientByUserId', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GetClientByUserId;
GO
CREATE PROCEDURE dbo.sp_GetClientByUserId
    @IdUtilizator INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        C.nume,
        C.prenume,
        C.telefon,
        C.email,
        C.adresa
    FROM dbo.Utilizatori U
    JOIN dbo.Client C ON U.id_client = C.id_client
    WHERE U.ID = @IdUtilizator;
END;
GO

-- ──────────────────────────────────────────────────────────
--  sp_UpdateDateClient
--  Actualizează datele personale ale unui client
-- ──────────────────────────────────────────────────────────
IF OBJECT_ID('dbo.sp_UpdateDateClient', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_UpdateDateClient;
GO
CREATE PROCEDURE dbo.sp_UpdateDateClient
    @IdUtilizator INT,
    @Nume         NVARCHAR(40),
    @Prenume      NVARCHAR(40),
    @Telefon      NVARCHAR(20),
    @Email        NVARCHAR(100),
    @Adresa       NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    IF @Email NOT LIKE '%@%.%'
    BEGIN
        RAISERROR('Email invalid.', 16, 1);
        RETURN;
    END;

    DECLARE @IdClient INT;
    SELECT @IdClient = id_client FROM dbo.Utilizatori WHERE ID = @IdUtilizator;

    IF @IdClient IS NULL
    BEGIN
        RAISERROR('Utilizatorul nu are un cont de client asociat.', 16, 1);
        RETURN;
    END;

    UPDATE dbo.Client
    SET
        nume    = @Nume,
        prenume = @Prenume,
        telefon = @Telefon,
        email   = @Email,
        adresa  = @Adresa
    WHERE id_client = @IdClient;
END;
GO

-- ──────────────────────────────────────────────────────────
--  sp_SchimbaParola
--  Actualizează parola unui utilizator din tabelul Utilizatori
-- ──────────────────────────────────────────────────────────
IF OBJECT_ID('dbo.sp_SchimbaParola', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_SchimbaParola;
GO
CREATE PROCEDURE dbo.sp_SchimbaParola
    @ID         INT,
    @ParolaNoua NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    IF LEN(@ParolaNoua) < 8
    BEGIN
        RAISERROR('Parola trebuie sa aiba minim 8 caractere.', 16, 1);
        RETURN;
    END;

    UPDATE dbo.Utilizatori
    SET Password = @ParolaNoua
    WHERE ID = @ID;
END;
GO

-- Actualizare pentru 
-- -- ============================================================
--  FIX: sp_GetClientByUserId  +  sp_UpdateDateClient
--  Problema: utilizatorii de tip Vizitator pot avea id_client NULL
--  in tabelul Utilizatori, deci SP-urile vechi esueaza.
--  Solutia: UPSERT – daca clientul nu exista, il cream automat.
-- ============================================================

USE MoldCom2;
GO

-- ──────────────────────────────────────────────────────────
--  sp_GetClientByUserId  (versiune reparata)
--  Returneaza datele din Client legate de un utilizator.
--  Daca utilizatorul nu are inca un rand in Client,
--  returneaza un rand gol (nu NULL) ca sa nu ramanа
--  campurile din UI goale cu eroare.
-- ──────────────────────────────────────────────────────────
IF OBJECT_ID('dbo.sp_GetClientByUserId', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GetClientByUserId;
GO
CREATE PROCEDURE dbo.sp_GetClientByUserId
    @IdUtilizator INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @IdClient INT;
    SELECT @IdClient = id_client
    FROM dbo.Utilizatori
    WHERE ID = @IdUtilizator;

    -- Daca utilizatorul are deja un id_client asociat, returneaza datele
    IF @IdClient IS NOT NULL
    BEGIN
        SELECT
            C.nume,
            C.prenume,
            C.telefon,
            C.email,
            C.adresa
        FROM dbo.Client C
        WHERE C.id_client = @IdClient;
        RETURN;
    END;

    -- Daca nu are id_client, returneaza un rand gol
    -- (aplicatia va afisa campuri libere, gata de completat)
    SELECT
        '' AS nume,
        '' AS prenume,
        '' AS telefon,
        '' AS email,
        '' AS adresa;
END;
GO


-- ──────────────────────────────────────────────────────────
--  sp_UpdateDateClient  (versiune reparata cu UPSERT)
--  Daca utilizatorul nu are inca un rand in Client,
--  il cream acum si legam id_client in Utilizatori.
-- ──────────────────────────────────────────────────────────
IF OBJECT_ID('dbo.sp_UpdateDateClient', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_UpdateDateClient;
GO
CREATE PROCEDURE dbo.sp_UpdateDateClient
    @IdUtilizator INT,
    @Nume         NVARCHAR(40),
    @Prenume      NVARCHAR(40),
    @Telefon      NVARCHAR(20),
    @Email        NVARCHAR(100),
    @Adresa       NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    -- Validare email de baza
    IF @Email NOT LIKE '%@%.%'
    BEGIN
        RAISERROR('Email invalid.', 16, 1);
        RETURN;
    END;

    DECLARE @IdClient INT;
    SELECT @IdClient = id_client
    FROM dbo.Utilizatori
    WHERE ID = @IdUtilizator;

    IF @IdClient IS NOT NULL
    BEGIN
        -- Utilizatorul are deja un rand in Client -> UPDATE simplu
        UPDATE dbo.Client
        SET
            nume    = @Nume,
            prenume = @Prenume,
            telefon = @Telefon,
            email   = @Email,
            adresa  = @Adresa
        WHERE id_client = @IdClient;
    END
    ELSE
    BEGIN
        -- Nu are inca un rand in Client -> INSERT + legam FK in Utilizatori

        -- Gasim un id_client liber (MAX + 1; safe pentru volume mici)
        DECLARE @NewId INT;
        SELECT @NewId = ISNULL(MAX(id_client), 0) + 1
        FROM dbo.Client;

        INSERT INTO dbo.Client (id_client, nume, prenume, telefon, email, adresa)
        VALUES (@NewId, @Nume, @Prenume, @Telefon, @Email, @Adresa);

        -- Legam noul client de utilizator
        UPDATE dbo.Utilizatori
        SET id_client = @NewId
        WHERE ID = @IdUtilizator;
    END;
END;
GO


-- ──────────────────────────────────────────────────────────
--  sp_SchimbaParola  (pastrata neschimbata, inclusa
--  pentru completitudine / re-rulare sigura)
-- ──────────────────────────────────────────────────────────
IF OBJECT_ID('dbo.sp_SchimbaParola', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_SchimbaParola;
GO
CREATE PROCEDURE dbo.sp_SchimbaParola
    @ID         INT,
    @ParolaNoua NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    IF LEN(@ParolaNoua) < 8
    BEGIN
        RAISERROR('Parola trebuie sa aiba minim 8 caractere.', 16, 1);
        RETURN;
    END;

    UPDATE dbo.Utilizatori
    SET Password = @ParolaNoua
    WHERE ID = @ID;
END;
GO

PRINT '[DONE] Procedurile sp_GetClientByUserId si sp_UpdateDateClient au fost reparate cu succes.';
GO