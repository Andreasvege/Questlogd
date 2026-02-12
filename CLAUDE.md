# Questlogd - Prosjektspesifikasjon

## Oversikt
Spillbibliotek/backlog-app bygget med Blazor Web App (.NET 10)

---

## Design

### Fargepalett (CSS-variabler)
| Variabel | Farge | Bruk |
|----------|-------|------|
| `--bg-primary` | #1E1E24 | Hovedbakgrunn |
| `--text-primary` | #F0EFF4 | Hovedtekst |
| `--accent-blue` | #3A7CA5 | Highlight/lenker |
| `--accent-red` | #BF1A2F | Highlight/aksent |

### Tema
- **Mørkt tema**
- Minimalistisk men stylized

### Typografi
- **Overskrifter:** Space Grotesk (geometrisk, moderne med karakter)
- **Brødtekst:** Inter (lesbar, ren)
- Google Fonts

### Layout
- **Navigasjon:** Top navbar
- **Spillvisning:** Grid med cover-bilder
- **Stil:** Avrundede hjørner, svevende elementer
- **Responsivitet:** Desktop-first (responsiv senere)

### UI-detaljer
- Subtile skygger med dybde
- Subtile hover-animasjoner
- Floating cards

---

## Sider & Navigasjon

### Navbar
1. **Hjem** - Dashboard/oversikt
2. **Bibliotek** - Alle spill (grid-visning)
3. **Backlog** - Spill man eier men ikke har spilt
4. **Ønskeliste** - Spill man vil ha
5. *(Statistikk - kommer senere)*

### Hjem (Dashboard)
- **Spiller nå** - seksjon med spill du aktivt spiller
- **Nylig spilt** - seksjon med nylig spilte spill
- Evt. rask statistikk/oversikt

### Spilldetaljer-side
Når du klikker på et spill:
- Cover-bilde
- Navn, utvikler, sjanger, utgivelsesdato
- Din status (spiller/fullført/backlog/ønskeliste)
- Din rating (1-10) + anmeldelse
- Timer spilt
- *(Fremtidig: lignende spill)*

---

## Funksjonalitet

### Kjernefunksjoner
- [x] Spillbibliotek (grid-visning med filter)
- [x] Backlog-system
- [x] Ønskeliste
- [x] "Spiller nå" / "Nylig spilt" på hjem-siden
- [x] Fullførte spill
- [x] Anmeldelser med 1-10 rating + tekst
- [x] Spilldetaljer-side
- [x] Logg-side (søk, velg status, legg til anmeldelse)
- [x] Anmeldelser-side (se/rediger alle anmeldelser)
- [x] Statistikk-sidebar på hjem-siden
- [ ] Database *(senere)*

### Spillstatus (enum, nullable)
- `null` - Ikke logget (spill finnes i database, men ikke i brukerens logg)
- `Playing` - Spiller nå
- `Completed` - Fullført
- `Backlog` - I backlog (eier, ikke spilt)
- `Wishlist` - Ønskeliste
- `Dropped` - Droppet

### Brukere
- Én bruker (meg selv) for nå
- Flerbruker med innlogging kommer senere

### Data
- Hardkodet data for nå
- Database (SQLite) kommer senere
- Eksternt API for spillinfo/covers vurderes senere

---

## Teknisk

### Stack
- Blazor Web App (.NET 10)
- Interactive Server-modus
- Ren CSS med variabler (ingen rammeverk)

### Prosjektstruktur
```
Questlogd/
├── Components/
│   ├── Layout/
│   │   ├── MainLayout.razor
│   │   └── NavBar.razor
│   ├── Pages/
│   │   ├── Home.razor
│   │   ├── Library.razor
│   │   ├── Backlog.razor
│   │   ├── Wishlist.razor
│   │   └── GameDetails.razor
│   └── Shared/
│       └── GameCard.razor
├── Models/
│   ├── Game.cs
│   ├── Review.cs
│   └── GameStatus.cs
├── Services/
│   └── GameService.cs
└── wwwroot/
    └── css/
        └── app.css
```

---

## Modeller

### Game
```csharp
- Id: int
- Name: string
- Genre: string
- Developer: string
- ReleaseDate: DateTime
- CoverImageUrl: string
- Status: GameStatus
- Rating: int? (1-10)
- HoursPlayed: int?
- LastPlayed: DateTime?
```

### Review
```csharp
- ReviewId: int
- GameId: int
- ReviewContent: string
- Rating: int (1-10)
- DatePosted: DateTime
```

### GameStatus (enum)
```csharp
Playing, Completed, Backlog, Wishlist, Dropped
```

---

## Kodeprinsipper

### Gjenbrukbare komponenter
- **GameCard** - brukes i Library, Backlog, Wishlist, Home
- **GameGrid** - wrapper for grid-visning av GameCards
- **SectionHeader** - gjenbrukbar seksjonstittel
- **StatusBadge** - viser spillstatus (Playing, Completed, osv.)
- **RatingDisplay** - viser rating (1-10)

### Blazor best practices
- Komponenter med `[Parameter]` for fleksibilitet
- Services for datalogikk (ikke i komponenter)
- Cascading parameters der det gir mening
- Unngå duplisert kode - DRY-prinsippet
- Små, fokuserte komponenter fremfor store monolitter

### Mappestruktur for komponenter
```
Components/
├── Layout/          # Layout-komponenter
├── Pages/           # Sider (med @page)
├── Shared/          # Gjenbrukbare UI-komponenter
│   ├── GameCard.razor
│   ├── GameGrid.razor
│   ├── StatusBadge.razor
│   └── RatingDisplay.razor
└── _Imports.razor   # Felles using-statements
```

---

## Notater
- Erstatt standard Blazor-layout med custom design fra scratch
- Begynn med desktop, legg til responsivitet senere
- Hold backend enkel - hardkodet data først, database senere
- Prioriter gjenbrukbarhet og komponent-arkitektur
