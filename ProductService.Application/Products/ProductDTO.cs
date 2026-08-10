namespace ProductService.Application.Products;

public record ProductDTO(string Name, string? Description, decimal Price, int Stock)
{

}