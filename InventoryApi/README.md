# InventoryApi

Mali projekt koji sam napravio da naučim backend u C#-u. To je API za vođenje zaliha proizvoda: može se dodati, pregledati, izmijeniti i obrisati proizvod. Podaci su u SQL Server bazi, a uz API sam napravio i jednostavnu web stranicu koja ga koristi.

Počeo sam kao konzolna aplikacija s ručnim SQL-om, pa sam isti projekt postepeno prepravljao: prvo repository pattern, zatim Entity Framework Core, async/await, na kraju Web API i stranica. Konzolna verzija je u posebnom repozitoriju: [InventoryManager](https://github.com/misir99/InventoryManager).

## Što je unutra

- C# i .NET 8
- ASP.NET Core Web API
- Entity Framework Core (SQL Server)
- Repository pattern i dependency injection
- Swagger za testiranje
- Obični HTML i JavaScript za stranicu

## Kako je posloženo

- `Models` – klasa `Product`
- `Data` – `AppDbContext`
- `Repositories` – sučelje `IProductRepository` i `EfProductRepository`
- `Exceptions` – `ProductNotFoundException`
- `Controllers` – `ProductsController`
- `wwwroot` – stranica (`index.html`)

## Endpointi

| Metoda | Adresa | Što radi |
|---|---|---|
| GET | `/api/products` | vraća sve proizvode |
| GET | `/api/products/{id}` | vraća jedan proizvod (404 ako ga nema) |
| POST | `/api/products` | dodaje proizvod (201) |
| PUT | `/api/products/{id}` | mijenja proizvod (204, 400 ili 404) |
| DELETE | `/api/products/{id}` | briše proizvod (204 ili 404) |

## Kako pokrenuti

Treba ti SQL Server. Napravi bazu `InventoryDb` i tablicu:

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

## Što je ostalo

- izmjena proizvoda na stranici (API to već ima, stranica još ne)
- validacija unosa i DTO klase
- objava online i Docker