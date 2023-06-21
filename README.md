# DBMS_Projektarbeit
## Anforderung

**Bewertung:**

Bewertet werden:

 -     die Facharbeit

-      die Präsentation

-      die Überprüfung des Individualwissens

**Themenkreis, Aufgaben- und Problemstellung:**

Ihr Unternehmen plant die Erstellung eines eigenen DBMS zur Verwaltung der Artikel in Ihrem Lager und zur Unterstützung der Abteilung Einkauf. Das DBMS soll als relationale DB in C# unter Verwendung von MS-Visual Studio codiert werden. Sie sollen weiterhin MS-ACCESS, ADO.NET sowie MySQL oder SQLServer verwenden, da diese Systeme bereits im Unternehmen eingeführt und auch vorhanden sind. Selbstverständlich können Sie auch weitere Systeme nutzen, die im Betrieb vorhanden sind. Führen Sie das Projekt in Form einer vollständigen Handlung, also über alle Zyklen (Planen, Erstellen, Testen, Validieren, Reflektieren, Dokumentieren) aus, um das entstandene Softwareprodukt handhab- und erweiterbar zu machen. Ihre Dokumentation soll zudem so gestaltet werden, dass der Leiter der Informatikabteilung neue Mitarbeiter auch ohne Ihre Präsenz schulen und zur Erweiterung des Systems befähigen kann.


Projektarbeit: DBMS für Abteilung Einkauf in der Firma Contoso AG

Für die Abteilung Einkauf der Firma Contoso AG aus Betzdorf soll ein System entwickelt werden,
um Bestandsartikel zu verwalten und Lagerbestände zu bearbeiten.

Notwendige Funktionen:
-	Frontend zur Anzeige von Artikeln mit Artikelbestand
-	Hinzufügen neuer Artikel
-	Entfernen von Artikeln
-	Ändern von Artikeln
-	Bestand erhöhen
-	Bestand verringern

Technische Umsetzung:
-	Datenbanksystem: MySQL (MariaDB) / SQL Express
-	Webanwendung in C# mit Blazor

Dokumentation:
-	Lasten- und Pflichtenheft
-	Dokumentation der technischen Funktionsweise
-	Bedienungsanleitung
-	Projektpräsentation


Wir haben ein onedrive dokument für lastenheft und pflichtenheft und doku 
https://drive.google.com/drive/folders/1PGp7BTA1M0Y4znF4NEqkd_SAdCxyeI0j?usp=share_link

---
## Durchführung der Programmierung:

### Datenbankerstellung
![image](https://github.com/JackScheff420/DBMS_Projektarbeit/assets/89392765/efe582cd-6de4-4749-aa9b-4286a6e43162)

Datenbank erstellt

Datenbankserver erstellt mit SQL Express 
Mit der Datenbank verbunden (sqlexpress01)
Datenbanktabelle erstellt mit einfachem Befehl 

```sql
Create Table Book(
Id BigInt Identity(1,1) Primary Key Not Null,
Name Varchar(200) Not Null,
Author Varchar(100) Not Null,
Quantity int,
Price float Not Null,
Available bit)
```

in der Datenbank einen neuen User angelegt unter "sicherheit -> anmeldungen": testUser 
(siehe appsettings.json connection string)

---

### Blazor Server App erstellen

Blazor Server App erstellt
![image](https://github.com/JackScheff420/DBMS_Projektarbeit/assets/89392765/fc8388b9-f65d-42c6-bee5-1fca12fbc1c9)

Und mit Github verbunden um Inhalte in einer Versionsverwaltung hochzuladen

---

### Datenbank Kontext und Model Klasse erstellen

Folgende Kommands ausgeführt um NuGet Packages zu installiern:
- **Install-Package Microsoft.EntityFrameworkCore.Tools -Version 5.0.6**: Das Package kreiert den Datenbank Kontext und Modell Klassen aus der Datenbank

- **Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 5.0.6**: Das Package ist ein provider der erlaubt das Entity Framework Core mit dem SQL Server arbeiten kann

Mit dem folgenden Command wurde der DbKontext und Model Klassen erzeugen
```aspx
Scaffold-DbContext “Data Source=localhost\SQLEXPRESS01;Database=Library;TrustServerCertificate=true; User ID=testUser; Password=test1234” Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
```
Nach dem ausführen des Befehels wurde der LibraryContext.cs und die Book.cs erstellt unter dem Models Ordner
In der appsettings.json ist der connection string enthalten da sensitive daten nicht in den anderen Dateien stehen sollten. In der appsettings kann man sie leichter schützen, worauf hier allerdings verzichtet wurde
Der DBKontext musste noch als Service hinzugefügt werden mit der AddDbContext methode in der Program.cs
Die DbContext-Klasse ist eine Kernkomponente des Entity Framework, das für die Kommunikation mit der Datenbank verantwortlich ist.

---

### Data Access Layer erstellen

Die benötigten Daten wurden mithilfe eines Interfaces definiert um sie später zu fetchen
Die `ILibraryService.cs` ist dafür zuständig

```csharp
namespace DBMS_Projektarbeit.Models
{
    public interface ILibraryService
    {
        IEnumerable<Book> GetBooks();
        void InsertBook(Book employee);
        void UpdateBook(long id, Book employee);
        Book SingleBook(long id);
        void DeleteBook(long id);
    }
}
```

Die Datai `LibraryService.cs` implementiert die ILibraryService. Die Klasse dient als data access layer und ist dafür verantwortlich die Daten aus der Datenbank zu holen
Die Klasse `LibraryService` implementiert die Schnittstelle `ILibraryService` und stellt die erforderlichen Methoden zum Abrufen, Einfügen, Aktualisieren und Löschen von Büchern in der Datenbank bereit

Der LibraryService sowie der ILibrary service wurden auch noch in der `programm.cs` Datei eingefügt 

---

### Visualisierung

Um eine visualisierende Komponente hinzuzufügen musste bei den Abhängigkeiten mit NuGet Packages das "Syncfusion.Blazor" Packet hinzugefügt werden

In der `_imports.razor` musste die Syncfusion.Blazor Komponente noch genutzt werden
```c
@using Syncfusion.Blazor
```

Damit der Syle und die Visualisierung funktioniert muss folgendes in der `_host.cshtml` und in der `_layout.cshtml` hinzugefügt werden
```html
<head>
    <link href="_content/Syncfusion.Blazor/styles/bootstrap5.css" rel="stylesheet" />
    <script src="_content/Syncfusion.Blazor.Core/scripts/syncfusion-blazor.min.js" type="text/javascript"></script>
</head>
```

Verschiedene Themes werden mit der Komponente übergeben, allerdings wurde hier das Bootstrap Theme verwendet

Dann wurde die Grid Komponente in den Code eingefügt um die Komponente anzuzeigen.
In der `Index.razor` konnte nun folgender Code eingefügt werden:
```aspx
<SfGrid DataSource="@LibraryBooks" TValue="Book">
    <GridColumns>
        <GridColumn Field="@nameof(Book.Id)" IsPrimaryKey="true" IsIdentity="true" Visible="false"></GridColumn>
        <GridColumn Field="@nameof(Book.Name)" Width="150"></GridColumn>
        <GridColumn Field="@nameof(Book.Author)" Width="150"></GridColumn>
        <GridColumn Field="@nameof(Book.Quantity)" Width="90" TextAlign="TextAlign.Right"></GridColumn>
        <GridColumn Field="@nameof(Book.Price)" Width="90" Format="C2" TextAlign="TextAlign.Right"></GridColumn>
        <GridColumn Field="@nameof(Book.Available)" DisplayAsCheckBox="true" Width="70"></GridColumn>
    </GridColumns>
</SfGrid>

@code
{
    public IEnumerable<Book> LibraryBooks { get; set; }
    protected override void OnInitialized()
    {
        LibraryBooks = LibraryService.GetBooks();
    }
}
```
Der Code stellt ein Raster (Grid) dar, das für eine Sammlung von Büchern konfiguriert ist. Es erlaubt, die Bücherdaten in einer tabellarischen Form anzuzeigen und zu bearbeiten.

Damit die Daten nun in der Tabelle angezeigt wurden, musste der LibraryService in die razor page injected werden. Mit der DataSource  des SfGrid Elements wird festgelegt woher die Daten geholt werden sollen.
LibraryBooks wird unten im Code neu festgelegt als aufruf von der `GetBooks()` methode

Mit den GridColumn Elementen wird festgelegt welche Felder welche Daten erhalten und wie sie angezeigt werden sollen (bsp. Width, Visible usw.)
Mit Field=@nameof... wird der Name der Spalte festgelegt 

![image](https://github.com/JackScheff420/DBMS_Projektarbeit/assets/89392765/8f190486-8e19-4d51-b447-dc2a29fb5658)

Die Daten konnten nun angezeigt werden allerdings noch nicht manipuliert werden. 
Um diese funktionen einzufügen musste in dem @code abschnitt des Codes folgendes hinzugefügt werden:

_Code zum hinzufügen von Zeilen_ 
```csharp
public void ActionBeginHandler(ActionEventArgs<Book> Args)
{
    if (Args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Save))
    {
        if (Args.Action == "Add")
        {
            LibraryService.InsertBook(Args.Data);
        }
    }
}
```

_Code zum Updaten_
```csharp
public void ActionBeginHandler(ActionEventArgs<Book> Args)
{
    if (Args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Save))
    {
        if (Args.Action == "Edit")
        {
            LibraryService.UpdateBook(Args.Data.Id, Args.Data);
        }
    }
}
```

_Code zum löschen_
```csharp
public void ActionBeginHandler(ActionEventArgs<Book> Args)
{
    if (Args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Delete))
    {
        LibraryService.DeleteBook(Args.Data.Id);
    }
}
```

