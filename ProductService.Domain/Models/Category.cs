using ProductService.Domain.Exceptions;

namespace ProductService.Domain.Models;

public class Category
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public bool IsDeleted { get; private set; } = false;
    public DateTimeOffset CreatedAt { get; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; private set; }
    public List<Product> Products { get; } = new();
    private Category() { }
    private Category(string name)
    {
        Name = name;
    }

    public static Category Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 255)
            throw new DomainException("Name can't be empty or exceed 255 characters.");

        return new Category(name);
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 255)
            throw new DomainException("Name can't be empty or exceed 255 characters.");

        Name = name;
        UpdatedAt = DateTimeOffset.UtcNow; 
    }
    public void Delete()
    {
        if (IsDeleted)
            throw new DomainException("Category is already deleted.");

        IsDeleted = true;
        UpdatedAt = DateTimeOffset.UtcNow; 
    }
}