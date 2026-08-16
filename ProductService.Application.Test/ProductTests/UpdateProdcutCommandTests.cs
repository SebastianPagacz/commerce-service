using Moq;
using ProductService.Application.Products.Commands;
using ProductService.Domain.Abstractions;
using ProductService.Domain.Models;

namespace ProductService.Application.Test.ProductTests;

public class UpdateProdcutCommandTests
{
    private readonly UpdateProductHandler _handler;
    private readonly Mock<IRepository<Product>> _repository;
    private readonly Mock<IUnitOfWork> _uow;
    public UpdateProdcutCommandTests()
    {
        _repository = new();
        _uow = new();
        _handler = new(_repository.Object, _uow.Object);
    }

    [Fact]
    public async Task UpdateProductCommand_Returns_CorrectSuccessfulResult()
    {
        // Arrange
        var guid = new Guid();
        var product = Product.Create("Test", null, 12.99m, 12);
        var command = new UpdateProductCommand(guid, "Test2", "Test2", 10, 10);
        // Act
        _repository.Setup(r => r.GetAsync(guid, CancellationToken.None)).ReturnsAsync(product);
        var result = await _handler.Handle(command, CancellationToken.None);
        // Assert
        _uow.Verify(u => u.CommitAsync(CancellationToken.None), Times.Once());
        _repository.Verify(r => r.GetAsync(guid, CancellationToken.None), Times.Once());
        // Object assertion
        Assert.Equal("Test2", product.Name);
        Assert.Equal("Test2", product.Description);
        Assert.Equal(10, product.Price);
        Assert.Equal(10, product.Stock);
        
        Assert.True(result.IsSuccess);
        Assert.Equal(guid, result.Value);
    }

    [Fact]
    public async Task UpdateProductCommand_Returns_FailedResult()
    {
        // Arrange
        var command = new UpdateProductCommand(new Guid(), "Test2", "Test2", 10, 10);
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        // Assert
        Assert.True(result.IsFail);
        _repository.Verify(r => r.GetAsync(It.IsAny<Guid>(), CancellationToken.None), Times.Once);
        _uow.Verify(u => u.CommitAsync(CancellationToken.None), Times.Never());
    }
}