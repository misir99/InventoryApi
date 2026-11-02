# InventoryApi

Mali projekt koji sam napravio da naučim backend u C#-u. To je API za vođenje zaliha proizvoda: može se dodati, pregledati, izmijeniti i obrisati proizvod. Podaci su u SQL Server bazi, a uz API sam napravio i jednostavnu web stranicu koja ga koristi.

**Demo:** http://inventoryapi2262.runasp.net/

Počeo sam kao konzolna aplikacija s ručnim SQL-om, pa sam isti projekt postepeno prepravljao: prvo repository pattern, zatim Entity Framework Core, async/await, na kraju Web API i stranica. Konzolna verzija je u posebnom repozitoriju: [InventoryManager](https://github.com/misir99/InventoryManager).

## Što je unutra

- C# i .NET 8
- ASP.NET Core Web API
- Entity Framework Core (SQL Server)
- Repository pattern i dependency injection
- Validacija unosa i DTO klase
- Swagger za testiranje (samo lokalno)
- Obični HTML i JavaScript za stranicu (`fetch`)
- Objavljeno na MonsterASP.NET (besplatni hosting) s bazom u oblaku

## Kako je posloženo

- `Models` – klasa `Product` (redak u bazi)
- `Dtos` – `ProductDto` (ono što klijent šalje, s validacijom)
- `Data` – `AppDbContext`
- `Repositories` – sučelje `IProductRepository` i `EfProductRepository`
- `Exceptions` – `ProductNotFoundException`
- `Controllers` – `ProductsController`
- `wwwroot` – stranica (`index.html`) s dodavanjem, izmjenom i brisanjem

## Endpointi

| Metoda | Adresa | Što radi |
|---|---|---|
| GET | `/api/products` | vraća sve proizvode |
| GET | `/api/products/{id}` | vraća jedan proizvod (404 ako ga nema) |
| POST | `/api/products` | dodaje proizvod (201, ili 400 ako unos nije valjan) |
| PUT | `/api/products/{id}` | mijenja proizvod (204, 400 ili 404) |
| DELETE | `/api/products/{id}` | briše proizvod (204 ili 404) |

## Kako pokrenuti lokalno

Treba ti SQL Server. Napravi bazu `InventoryDb`. Tablicu `Products` aplikacija sama napravi pri prvom pokretanju (`EnsureCreated`), a ako je želiš ručno:

```sql
CREATE TABLE Products (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    Category NVARCHAR(50) NOT NULL,
    Quantity INT NOT NULL DEFAULT 0
);
```

U `appsettings.json` je connection string koji koristi `localhost` i Windows autentikaciju. Ako ti server drukčije radi, promijeni ga.

Zatim pokreni projekt (F5 u Visual Studiju ili `dotnet run`). Stranica je na korijenu (`https://localhost:<port>/`), a Swagger na `/swagger`.

## Objava

Na serveru se koristi datoteka `appsettings.Production.json` s connection stringom prema bazi u oblaku. Ona je namjerno u `.gitignore` jer sadrži lozinku, pa je nema u repozitoriju. Ako želiš objaviti svoju verziju, napravi je ovako:

```json
{
  "ConnectionStrings": {
    "InventoryDb": "tvoj connection string"
  }
}
```

## Ograničenja i što je ostalo

- nema prijave korisnika: na demo stranici svatko može dodavati i brisati proizvode
- tablica se pravi s `EnsureCreated`, a za pravi projekt bih koristio EF migracije
- nema testova
- Docker
