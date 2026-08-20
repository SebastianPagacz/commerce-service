using Moq;
using ProductService.Application.Categories;
using ProductService.Application.Common;
using ProductService.Application.Products;
using ProductService.Application.Products.Queries;
using ProductService.Application.Services.QueryServices;

namespace ProductService.Application.Test.ProductTests;

public class GetProductsTests
{
    private readonly Mock<IProductQueryService> _query;
    private readonly GetProductByIdHandler _idHandler;
    private readonly GetProductsHandler _handler;
    public GetProductsTests()
    {
        _query = new();
        _idHandler = new(_query.Object);
        _handler = new(_query.Object);
    }

    [Fact]
    public async Task GetProducts_Returns_CorrectPagedResult()
    {
        // Arrange
        var query = new GetProductsQuery("desc", "name", 10, 1);
        _query.Setup(q => q.GetAllExistingProductsAsync(
            query.SortOrder, 
            query.SortColumn, 
            query.PageSize, 
            query.PageNumber, 
            CancellationToken.None)).ReturnsAsync(PagedResult<List<ProductDTO>>.Create([], query.PageSize, query.PageNumber, 0));
        // Act
        var result = await _handler.Handle(query, CancellationToken.None);
        // Assert
        Assert.Empty(result.Values);
        Assert.Equal(10, result.PageSize);
        Assert.Equal(1, result.PageNumber);
        _query.Verify(q => q.GetAllExistingProductsAsync("desc", "name", 10, 1, CancellationToken.None), Times.Once());
    }

    [Fact]
    public async Task GetProductById_Returns_CorrectSuccessfulResult()
    {
        // Arrange
        Guid guid = new();
        var query = new GetProductByIdQuery(guid);
        _query.Setup(q => q.GetExistingProductAsync(guid, CancellationToken.None))
            .ReturnsAsync(new ProductDTO(It.IsAny<Guid>(), "Test", null, 12.99m, 10, It.IsAny<List<CategoryDTO>>()));
        // Act
        var result = await _idHandler.Handle(query, CancellationToken.None);
        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("Test", result.Value.Name);
        Assert.Null(result.Value.Description);
        Assert.Equal(12.99m, result.Value.Price);
        Assert.Equal(10, result.Value.Stock);
        _query.Verify(q => q.GetExistingProductAsync(guid, CancellationToken.None), Times.Once());
    }

    [Fact]
    public async Task GetProductbyId_Returns_CorrectFailedResult()
    {
        // Arrange
        Guid guid = new();
        var query = new GetProductByIdQuery(guid);
        // Act
        var result = await _idHandler.Handle(query, CancellationToken.None);
        // Assert
        Assert.True(result.IsFail);
        Assert.Equal("Product was not found", result.Message);
        _query.Verify(q => q.GetExistingProductAsync(guid, CancellationToken.None), Times.Once());
    }
}