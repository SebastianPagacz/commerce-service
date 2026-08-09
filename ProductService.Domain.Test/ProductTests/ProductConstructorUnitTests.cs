using ProductService.Domain.Exceptions;
using ProductService.Domain.Models;

namespace ProductService.Domain.Test.ProductTests;

public class ProductConstructorUnitTests
{
    private readonly string _invalidNameMessage = "Name can't be empty or exceed 255 characters.";
    private readonly string _invalidPriceMessage = "Price can't be zero or negative.";
    private readonly string _invalidStockMessage = "Stock can't be negative.";

    [Fact]
    public void CreateProduct_Returns_CorrectProduct()
    {
        // Arrange & Act
        var product = Product.Create("Test", null, 12.99m, 20);
        // Assert
        Assert.Equal("Test", product.Name);
        Assert.Equal(12.99m, product.Price);
        Assert.Equal(20, product.Stock);
        Assert.False(product.IsDeleted);
        Assert.Null(product.UpdatedAt);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("   ")]
    public void CreateProduct_WithInvalidName_Returns_CorrectExcetpion(string name)
    {
        // Arrange & Act
        var action = Assert.Throws<DomainException>(() => Product.Create(name, null, 12.99m, 20));
        // Assert
        Assert.Equal(_invalidNameMessage, action.Message);
    }

    [Fact]
    public void CreateProduct_WithTooLongName_Returns_CorrectExcetpion()
    {
        // Arrange
        var name = new string('a', 256);
        // Act
        var action = Assert.Throws<DomainException>(() => Product.Create(name, null, 12.99m, 20));
        // Assert
        Assert.Equal(_invalidNameMessage, action.Message);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-0.01)]
    public void CreateProduct_WithInvalidPrice_Returns_CorrectExcetpion(decimal price)
    {
        // Arrange & Act
        var action = Assert.Throws<DomainException>(() => Product.Create("Test", null, price, 20));
        // Assert
        Assert.Equal(_invalidPriceMessage, action.Message);
    }

    [Theory]
    [InlineData(-10)]
    [InlineData(-1)]
    public void CreateProduct_WithInvalidStock_Returns_CorrectExcetpion(int stock)
    {
        // Arrange & Act
        var action = Assert.Throws<DomainException>(() => Product.Create("Test", null, 12.99m, stock));
        // Assert
        Assert.Equal(_invalidStockMessage, action.Message);
    }
}