using ProductService.Domain.Exceptions;
using ProductService.Domain.Models;

namespace ProductService.Domain.Test.ProductTests;

public class RelationTests
{
    [Fact]
    public void ProductAssignCategory_Correctly_AssignsACategory()
    {
        // Arrange
        var product = Product.Create("Test", null, 12.99m, 20);
        var category = Category.Create("Test Cat");
        // Act
        Assert.Empty(product.Categories);
        product.AssignCategory(category);
        // Assert
        Assert.NotEmpty(product.Categories);
    }

    [Fact]
    public void ProductAssignCategory_Throws_CorrectException_OnDuplicateCategory()
    {
        // Arrange
        var product = Product.Create("Test", null, 12.99m, 20);
        var category = Category.Create("Test Cat");
        // Act
        Assert.Empty(product.Categories);
        product.AssignCategory(category);
        var action = Assert.Throws<DomainException>(() => product.AssignCategory(category));
        // Assert
        Assert.Equal("Product already has this category assigned.", action.Message);
    }

    [Fact]
    public void ProductAssignCategory_ThrowsCorrectException_OnDeletedProduct()
    {
        // Arrange
        var product = Product.Create("Test", null, 12.99m, 20);
        var category = Category.Create("Test Cat");
        // Act
        Assert.Empty(product.Categories);
        product.Delete();
        var action = Assert.Throws<DomainException>(() => product.AssignCategory(category));
        // Assert
        Assert.Equal("Can't assign category to deleted product.", action.Message);
    }
}