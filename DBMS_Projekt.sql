
-- Skript für DBMS_Projekt


-- Erstellen der Tablle Lieferant
CREATE TABLE Lieferant(
	LieferantID INTEGER NOT NULL PRIMARY KEY,
	Telefonnummer VARCHAR(40),
	Mailadresse VARCHAR(80),
	Strasse VARCHAR(80),
	Hausnummer VARCHAR(20),
	PLZ VARCHAR(20),
	Ort VARCHAR(80),
	Land VARCHAR(80)
);

-- Erstellen der Tabelle Artikelart
CREATE TABLE Artikelart(
	ArtikelartID INTEGER NOT NULL PRIMARY KEY,
	ArtikelBezeichnung VARCHAR(80)
);


-- Erstellen der Tabelle Artikel
CREATE TABLE Artikel(
	ArtikelID INTEGER NOT NULL PRIMARY KEY,
	Bezeichnung VARCHAR(80),
	Artikelart INTEGER,
	MengeIst INTEGER,
	MengeSoll INTEGER,
	MengeBestellt INTEGER,
	MengeAbruf INTEGER,
	Lieferant INTEGER,
	Preis DOUBLE,
	FOREIGN KEY (Lieferant) REFERENCES Lieferant(LieferantID),
	FOREIGN KEY (Artikelart) REFERENCES Artikelart(ArtikelartID)
	);
	
	