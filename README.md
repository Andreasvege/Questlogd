# Questlogd

En spillbibliotek- og backlog-app for gamere som vil holde oversikt over spillene sine. Logg spill, sett status, skriv anmeldelser og hold styr på backlogen din.

![.NET 10](https://img.shields.io/badge/.NET-10.0-purple)
![Blazor](https://img.shields.io/badge/Blazor-Server-blue)

## Funksjoner

- **Spillbibliotek** - Se alle loggede spill i et grid med filtrering etter status
- **Backlog** - Hold oversikt over spill du eier men ikke har startet
- **Wishlist** - Samle spill du har lyst til a skaffe deg
- **Anmeldelser** - Gi rating (1-10) og skriv anmeldelser med inline-redigering
- **Dashboard** - Se hva du spiller na, nylig spilte spill og statistikk
- **Logg spill** - Sok, velg status, legg til rating og anmeldelse

## Tech stack

- **Blazor Web App** med Interactive Server-modus
- **.NET 10**
- **Bootstrap 5** (morkt tema)
- In-memory data (database kommer senere)

## Kom i gang

### Forutsetninger

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

### Kjor prosjektet

```bash
dotnet run
```

Appen starter pa `https://localhost:7042` (eller `http://localhost:5279`).

## Prosjektstruktur

```
Questlogd/
├── Components/
│   ├── Layout/        # MainLayout, NavBar
│   ├── Pages/         # Home, Library, GameDetails, Log, Backlog, Wishlist, Reviews
│   └── Shared/        # GameCard, GameGrid, StatusBadge, RatingDisplay, SectionHeader
├── Models/            # Game, GameStatus, Review
├── Services/          # GameService (in-memory CRUD)
└── wwwroot/           # CSS, statiske filer
```

## Spillstatus

| Status | Beskrivelse |
|--------|-------------|
| Playing | Spiller aktivt |
| Completed | Fullfort |
| Backlog | Eier, men ikke startet |
| Wishlist | Onskeliste |
| Dropped | Droppet |

## Veikart

- [ ] Database (SQLite)
- [ ] Statistikk-side med grafer
- [ ] Flerbruker med innlogging
- [ ] Eksternt API for spillinfo/covers
- [ ] Responsivt design for mobil
