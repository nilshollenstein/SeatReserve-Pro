# Glossar

## Begriffe zur Datensicherheit

#### Hashing

Hashing ist ein Methode um Daten zu schützen.  
Hierbei wird ein Text mithilfe eines Algorithmuses in eine Zeichenfolge umgewandelt. Diese kann man nicht mehr zurückberechnen.  
Durch dies ist man in der Lage Passwörter so zu speichern, da der potentielle Angreifer keinen Schlüssel finden kann, sondern mit Bruteforce oder ähnlichen Methoden arbeiten.

#### BCRypt

[BCrypt](https://en.wikipedia.org/wiki/Bcrypt) ist ein speziell für das sichere Speichern von Passwörtern entwickelter Algorithmus. Der Algorithmus integriert automatisch einen Salt und verwendet einen einstellbaren Kostenfaktor, welcher die Berechnung des Hashes absichtlich verlangsamt und somit sicherer gegen Angriffe macht.  
Beispielhash: `$2a$12$R9h/cIPz0gi.URNNX3kh2OPST9/PgBkqquzi.Ss7KIUgO2t0jWMUW`  
In C#/.Net kann BCrypt mit dem BCrypt.Net-Next Nuget-Paket verwendet werden.  
[BCrypt.Net-Next Nuget](https://www.nuget.org/packages/BCrypt.Net-Next/)

#### SHA-512

[SHA-512](https://en.wikipedia.org/wiki/SHA-2) ist ein kryptographischer Hash-Algorithmus aus der SHA-2-Familie, der Daten in einen 512-Bit langen Hash umwandelt. Er wird verwendet, um die Integrität von Daten zu prüfen, da jede kleine Änderung in den Originaldaten zu einem vollständig veränderten Hash führt. SHA-512 wird häufig für Sicherheitsanwendungen und Protokolle, wie z.B. TLS und SSL, verwendet.

## Begriffe zur Datenbank

#### Npgsql

Npgsql ist ein Nuget für .Net  
Dieses ermöglicht es dem Anwender, PostgreSQL Abfragen an einen PostgreSQL-Server zu senden, sowie andere Datenbankoperationen durchführen.  
[Npgsql Nuget](https://www.npgsql.org/doc/index.html)
