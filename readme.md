# SeatReserve-Pro

## Beschreibung

Dieses Projekt ist dazu da, einem Nutzer die Möglichkeit zu geben, Sitzplätze in einem Bus zu reservieren. Dafür muss der Benutzer ein Profil erstellen, welches einen Benutzernamen und ein Passwort erfordert. Danach kann er als normaler Benutzer Sitze reservieren und auch diese Reservationen wieder absagen. Es gibt auch Administratoren, welche angepasste Möglichkeiten haben, mit den Bussen zu interagieren.

### Funktionen der Profile

#### Normale Benutzer

- Profil kann einfach erstellt werden
- Kann Sitzplätze in Bussen reservieren
- Kann seine Reservationen absagen

#### Administrator

- Muss zuerst als normales Profil erstellt werden, erhält dann die Berechtigungen von einem anderen Administrator
- Kann keine Sitzplätze reservieren
- Kann alle Reservationen absagen
- Kann andere Nutzer zum Administrator benennen, benötigt das Passwort des ersten Profils

### Administratoren erstellen

Es gibt zwei Methoden wie ein Administrator erstellt werden kann:

1. Das erste Profil das erstellt wird, ist immer ein Administrator. Dieses Profil ist auch für die zweite Methode sehr wichtig. Aus diesem Grund sollte das Passwort zu diesem Profil immer bekannt sein.

2. Diese Methode funktioniert mithilfe eines anderen Administratoren-Profil. Ein Administrator kann aus einer Liste aller Nutzer, die keine Administratoren-Rechte haben, auswählen. Danach kann er, wenn er das Passwort des ersten Nutzers kennt, das ausgewählte Profil zum Administrator befördern.

## Nutzen des Projektes

Um das Projekt zu nutzen muss man die Solution mithilfe von Visual Studio aus diesem Repository klonen.  

Das benötigte ERD und SQL für die Datenbank sind im Ordner ./Database zu finden. Das Programm läuft über eine PostgreSQL Datenbank. Dem Repository liegt ein Docker-Compose File bei, das einen Docker erstellt, der auf die gezeigten Beispieldaten passt. Die Datenbank selber (SeatReserve-Pro) muss noch erstellt werden. Dies kann mit einem Programm wie [pgAdmin](https://www.pgadmin.org/download/) gelöst werden.  

Um das Programm auf diese zu verbinden, muss man zwei Mal einen String bearbeiten. Diese sind in den Files [DBOperations.cs](./SeatReserve-Library/DBOperations/DBOperations.cs) und [SeatReserve-ProDBService.cs](./SeatReserve-Pro_DBService/SeatReserve-ProDBService.cs) zu finden. Die Variable hat den Namen **connectionString**.  

Dieser String enthält folgende Informationen:  

- Host der Datenbank, z.B. localhost:5432
- Benutzername des Datenbanknutzers, z.B. postgres
- Password des Datenbanknutzers, z.B. postgres
- Datenbankname, z.B. SeatReserve-Pro

Die Beispieldaten oben sind die, die in den beiden Files eingetragen sind.

### Starten des Projektes

Es gibt, wenn alle Vorbereitungen abgeschlossen sind mehrere Methoden das Programm zu starten.

1. Starten mithilfe von Visual Studio:  In diesem Fall wird die Anwendung mithilfe des grünen Knopfes in Visual Studio gestartet.  
 1.1 Starten im Debug Mode, dabei kann der Anwender den Code debuggen  
 1.2 Starten im Release Mode, dieser lässt nur das Programm laufen, ohne das Programm debuggen zu können
2. Starten mithilfe von publish, In diesem Fall wird die Software mithilfe der Publish-Funktion von Visual Studio kompiliert  
 2.1 Als erstes muss das Programm gepublished werden. Hierfür muss man in Visual Studio das Programm auswählen.Danach muss man auf Build -> Publish Selection -> Folder  
 2.2 Wenn nun alles konfiguriert und gepublished wurde, kann man im ausgewählten Ordner eine .exe-Datei finden. Diese kann dann ausgeführt werden

## Mitarbeitende

Code: [Nils Hollenstein](https://github.com/nilshollenstein)  
Tester: [Matian Dauti](https://github.com/Matianz30), [Mischa Barmettler](https://github.com/Mischa50)  
Glossar und Readme: [Nils Hollenstein](https://github.com/nilshollenstein)
