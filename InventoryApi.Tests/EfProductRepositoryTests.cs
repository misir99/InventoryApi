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

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenProductDoesNotExist()
    {
        //arange
        using var context = CreateContext();
        var repo = new EfProductRepository(context);

        //act
        var result = await repo.GetByIdAsync(999);

        //assert

        Assert.Null(result);
    }

    [Fact]

    public async Task DeleteAsync_ThrowProductNotFoundException_WhenMissing()
    {
        //arrange
        using var context = CreateContext();
        var repo = new EfProductRepository(context);
        
        //act
        var exception = await Assert.ThrowsAsync<ProductNotFoundException>(() => repo.DeleteAsync(999));


            }
    [Fact]
    public async Task UpdateAsync_ChangesPrice()
    {
        // Arrange
        using var context = CreateContext();
        var repo = new EfProductRepository(context);

        var product = new Product
        {
            Name = "Miš",
            Price = 25,
            Category = "Elektronika",
            Quantity = 10
        };
        await repo.AddAsync(product);

        var changed = new Product
        {
            Id = product.Id,
            Name = "Miš",
            Price = 30,
            Category = "Elektronika",
            Quantity = 10
        };

        // Act
        await repo.UpdateAsync(changed);

        // Assert
        var updated = await repo.GetByIdAsync(product.Id);
        Assert.NotNull(updated);
        Assert.Equal(30, updated.Price);
    }
}