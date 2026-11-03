using InventoryApi.Data;
using InventoryApi.Exceptions;
using InventoryApi.Models;
using InventoryApi.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InventoryApi.Tests;

public class EfProductRepositoryTests
{
    private static AppDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task AddAsync_SavesProduct()
    {
        // Arrange
        using var context = CreateContext();
        var repo = new EfProductRepository(context);

        // Act
        await repo.AddAsync(new Product
        {
            Name = "Miš",
            Price = 25,
            Category = "Elektronika",
            Quantity = 10
        });

        // Assert
        var all = await repo.GetAllAsync();
        Assert.Single(all);
        Assert.Equal("Miš", all[0].Name);
    }
}