using Moq;
using ProductService.Application.Products.Commands;
using ProductService.Domain.Abstractions;
using ProductService.Domain.Models;

namespace ProductService.Application.Test.ProductTests;

public class DeleteProductCommandTests
{
    private readonly Mock<IRepository<Product>> _repository;
    private readonly Mock<IUnitOfWork> _uow;
    private readonly DeleteProductHandler _handler;
    public DeleteProductCommandTests()
    {
        _repository = new();
        _uow = new();
        _handler = new(_repository.Object, _uow.Object);
    }

    [Fact]
    public async Task DeleteProduct_Returns_SuccessfulResult()
    {
        // Arrange
        Guid guid = new();
        var product = Product.Create("Test", null, 12.99m, 10);
        var command = new DeleteProductCommand(guid);
        _repository.Setup(r => r.GetAsync(guid)).ReturnsAsync(product);
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(product.IsDeleted);
        _uow.Verify(u => u.CommitAsync(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task DeleteProduct_Returns_FailedResult()
    {
        // Arrange
        Guid guid = new();
        var command = new DeleteProductCommand(guid);
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        // Assert
        Assert.True(result.IsFail);
        _repository.Verify(r => r.GetAsync(guid), Times.Once());
        _uow.Verify(u => u.CommitAsync(CancellationToken.None), Times.Never());
    }
}