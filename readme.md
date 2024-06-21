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

##### Administratoren erstellen

Es gibt zwei Methoden wie ein Administrator erstellt werden kann. 
1. Das erste Profil das erstellt wird, ist immer ein Administrator. Dieses Profil ist auch für die zweite Methode sehr wichtig. Aus diesem Grund sollte das Passwort zu diesem Profil immer bekannt sein.
2. Diese Methode funktioniert mithilfe eines anderen Administratoren-Profil. Ein Administrator kann aus einer Liste aller Nutzer, die keine Administratoren-Rechte haben, auswählen. Danach kann er, wenn er das Passwort des ersten Nutzers kennt, das ausgewählte Profil zum Administrator befördern.


