using ProductService.Application.Categories;

namespace ProductService.Application.Products;

public record ProductDTO(Guid Id, string Name, string? Description, decimal Price, int Stock, List<CategoryDTO> Categories);