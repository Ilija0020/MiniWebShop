# MiniWebShop API 🎸

Ovo je backend deo Web aplikacije za prodaju muzičkih albuma, razvijen kao deo strukturiranog kursa programiranja. Projekat je izgrađen korišćenjem **ASP.NET Web API** tehnologije i **SQLite** baze podataka.

## 🚀 Tehnologije
* **C# / .NET 8** - Osnovni programski jezik i framework.
* **SQLite** - Relaciona baza podataka za čuvanje podataka o umetnicima, albumima i porudžbinama.
* **Microsoft.Data.Sqlite** - NuGet paket za komunikaciju sa bazom.
* **Repository Pattern** - Arhitekturalni obrazac za odvajanje logike pristupa podacima od kontrolera.

## 📂 Struktura Projekta
* `Models/` - Sadrži C# klase (entitete) koje preslikavaju tabele iz baze (Artist, Album, User, itd.).
* `Repositories/` - Sadrži logiku za rad sa bazom podataka (CRUD operacije, SQL upiti).
* `Controllers/` - Upravlja HTTP zahtevima i komunicira sa repozitorijumima.
* `data/` - Sadrži SQLite bazu podataka (`database.db`).

## 🛠️ Podešavanje i Pokretanje
1. **NuGet Paketi**: Potrebno je instalirati `Microsoft.Data.Sqlite` paket.
2. **Baza Podataka**: U Visual Studiju, podesiti fajl `data/database.db` na:
   - `Build Action: Content`
   - `Copy to Output Directory: Copy if newer`
3. **Konfiguracija**: Proveriti `appsettings.json` da li je `ConnectionString` ispravno usmeren na `data/database.db`.

## 📈 Trenutni Status
- [x] Dizajn baze podataka i testni podaci.
- [x] Implementirani modeli sa `required` i `nullable` parametrima.
- [x] Implementiran `ArtistRepository` sa punim CRUD operacijama i paginacijom.
- [ ] Implementacija ostalih repozitorijuma i kontrolera.