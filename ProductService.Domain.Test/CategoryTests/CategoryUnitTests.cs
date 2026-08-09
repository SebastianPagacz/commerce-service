using ProductService.Domain.Exceptions;
using ProductService.Domain.Models;

namespace ProductService.Domain.Test.CategoryTests;

public class CategoryUnitTests
{
    private readonly string _invalidNameMessage = "Name can't be empty or exceed 255 characters.";
    [Fact]
    public void CategoryCreate_Returns_CorrectValue()
    {
        // Arrange & Act
        var category = Category.Create("Test");
        // Assert
        Assert.Equal("Test", category.Name);
        Assert.False(category.IsDeleted);
        Assert.Null(category.UpdatedAt);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("   ")]
    public void CategoryCreate_Throws_CorrectException(string name)
    {
        // Arrange & Act
        var action = Assert.Throws<DomainException>(() => Category.Create(name));
        // Assert
        Assert.Equal(action.Message, _invalidNameMessage);
    }

    [Fact]
    public void CategoryCeate_Throws_CorrectException_OnNameTooLong()
    {
        // Arrange
        var name = new string('a', 256);
        // Act
        var action = Assert.Throws<DomainException>(() => Category.Create(name));
        // Assert
        Assert.Equal(_invalidNameMessage, action.Message);
    }

    [Fact]
    public void CategorySetName_Throws_CorrectException_OnNameTooLong()
    {
        // Arrange
        var name = new string('a', 256);
        var category = Category.Create("Test");
        // Act
        var action = Assert.Throws<DomainException>(() => category.SetName(name));
        // Assert
        Assert.Equal(_invalidNameMessage, action.Message);
    }

    [Theory]
    [InlineData("Test")]
    [InlineData("Test 123")]
    [InlineData("#")]
    public void CategorySetName_ChangesName_WithCorrectData(string name)
    {
        // Arrange
        var category = Category.Create("Test");
        // Act
        category.SetName(name);
        // Assert
        Assert.Equal(name, category.Name);
    }

    [Fact]
    public void CategoryDelete_SetsProperty_Correctly()
    {
        // Arrange
        var category = Category.Create("Test");
        // Act
        category.Delete();
        // Assert
        Assert.True(category.IsDeleted);
    }

    [Fact]
    public void CategoryDelete_Throws_CorrectException()
    {
        // Arrange
        var category = Category.Create("Test");
        category.Delete();
        // Act
        var action = Assert.Throws<DomainException>(() => category.Delete());
        // Assert
        Assert.Equal("Category is already deleted.", action.Message);
    }
}