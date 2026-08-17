using Moq;
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
    }
}