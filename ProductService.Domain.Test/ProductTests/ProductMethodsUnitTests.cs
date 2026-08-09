using ProductService.Domain.Exceptions;
using ProductService.Domain.Models;

namespace ProductService.Domain.Test.ProductTests;

public class ProductMethodsUnitTests
{
    private readonly string _invalidNameMessage = "Name can't be empty or exceed 255 characters.";
    private readonly string _invalidPriceMessage = "Price can't be zero or negative.";
    private readonly string _invalidStockMessage = "Stock can't be negative.";
    private readonly string _invalidAddStockMessage = "Additional stock can't be negative.";
    private readonly string _invalidSubStockMessage = "Subtracted stock can't be negative or exceed overall stock.";
    private readonly string _productDeleted = "Product is already deleted.";

    [Theory]
    [InlineData("Test Name", "Test Description", 10, 12)]
    [InlineData("Very cool product", "Nice", 0.99, 100)]
    [InlineData("a", " ", 50, 1000)]
    [InlineData("#", "", 0.01, 1)]
    [InlineData("Test", null, 19841.12, 100)]
    public void ProductSetMethods_ChangeProperties_Correctly(string name, string? description, decimal price, int stock)
    {
        // Arrange
        var product = Product.Create("Test", null, 12.99m, 20);
        // Act
        product.SetName(name);
        product.SetDescription(description);
        product.SetPrice(price);
        product.SetStock(stock);
        // Assert
        Assert.Equal(name, product.Name);
        Assert.Equal(description, product.Description);
        Assert.Equal(price, product.Price);
        Assert.Equal(stock, product.Stock);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("  ")]
    public void ProductSetName_Throws_CorrectException_WithInvalidData(string name)
    {
        // Arrange
        var product = Product.Create("Test", null, 12.99m, 20);
        // Act
        var action = Assert.Throws<DomainException>(() => product.SetName(name));
        // Assert
        Assert.Equal(_invalidNameMessage, action.Message);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    [InlineData(-0.001)] // This should not be possible, I need ot define places on the db level I assume
    public void ProductSetPrice_Throws_CorrectException_WithInvalidData(decimal price)
    {
        // Arrange
        var product = Product.Create("Test", null, 12.99m, 20);
        // Act
        var action = Assert.Throws<DomainException>(() => product.SetPrice(price));
        // Assert
        Assert.Equal(_invalidPriceMessage, action.Message);
    }

    [Theory]
    [InlineData(-10)]
    [InlineData(-1)]
    public void ProductSetStock_Throws_CorrectException_WithInvalidData(int stock)
    {
        // Arrange
        var product = Product.Create("Test", null, 12.99m, 20);
        // Act
        var action = Assert.Throws<DomainException>(() => product.SetStock(stock));
        // Assert
        Assert.Equal(_invalidStockMessage, action.Message);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(-100)]
    public void ProductAddStock_Throws_CorrectException_WithInvalidValue(int stock)
    {
        // Arrange
        var product = Product.Create("Test", null, 12.99m, 20);
        // Act
        var action = Assert.Throws<DomainException>(() => product.AddStock(stock));
        // Assert
        Assert.Equal(_invalidAddStockMessage, action.Message);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(-100)]
    public void ProductSubtractStock_Throws_CorrectException_WithInvalidValue(int stock)
    {
        // Arrange
        var product = Product.Create("Test", null, 12.99m, 20);
        // Act
        var action = Assert.Throws<DomainException>(() => product.SubtractStock(stock));
        // Assert
        Assert.Equal(_invalidSubStockMessage, action.Message);
    }

    [Fact]
    public void ProductDelete_Sets_CorrectCorrectValue_ToTheProperty()
    {
        // Arrange
        var product = Product.Create("Test", null, 12.99m, 20);
        // Act
        product.Delete();
        // Assert
        Assert.True(product.IsDeleted);
    }

    [Fact]
    public void ProductDelete_Throws_CorrectException_OnAlreadyDeletedProduct()
    {
        // Arrange
        var product = Product.Create("Test", null, 12.99m, 20);
        product.Delete();
        // Act
        var action = Assert.Throws<DomainException>(() => product.Delete());
        // Assert
        Assert.Equal(_productDeleted, action.Message);
    }
}