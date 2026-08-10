using System.Runtime.CompilerServices;
using ProductService.Domain.Exceptions;

namespace ProductService.Domain.Models;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public bool IsDeleted { get; private set; } = false;
    public DateTimeOffset CreatedAt { get; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; private set; }
    public IReadOnlyCollection<ProductCategory> Categories => _categories.AsReadOnly();
    private List<ProductCategory> _categories = new();

    // Constructors
    private Product() { }
    private Product(string name, string? description, decimal price, int stock)
    {
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
    }

    // Entity methods
    public static Product Create(string name, string? description, decimal price, int stock)
    {
        if (!ValidateName(name))
            throw new DomainException("Name can't be empty or exceed 255 characters."); // Might wanna trhow it inside the method

        if (price <= 0)
            throw new DomainException("Price can't be zero or negative.");

        if (stock < 0)
            throw new DomainException("Stock can't be negative.");

        return new Product(name, description, price, stock);
    }
    public void SetName(string newName)
    {
        if (!ValidateName(newName))
            throw new DomainException("Name can't be empty or exceed 255 characters.");

        Name = newName;
        Update();
    }
    public void SetDescription(string? newDescription)
    {
        Description = newDescription;
        Update();
    }
    public void SetPrice(decimal newPrice)
    {
        if (newPrice <= 0)
            throw new DomainException("Price can't be zero or negative.");

        Price = newPrice;
        Update();
    }
    public void SetStock(int newStock)
    {
        if (newStock < 0)
            throw new DomainException("Stock can't be negative.");

        Stock = newStock;
        Update();
    }
    public void AddStock(int toAdd)
    {
        if (toAdd <= 0)
            throw new DomainException("Additional stock can't be negative."); // Want to give information if the operation didn't happen
        
        Stock += toAdd;
        Update();
    }
    public void SubtractStock(int toSubtract)
    {
        if (toSubtract > Stock || toSubtract <= 0)
            throw new DomainException("Subtracted stock can't be negative or exceed overall stock.");
        
        Stock -= toSubtract;
        Update();
    }
    public void Delete()
    {
        if (IsDeleted)
            throw new DomainException("Product is already deleted.");

        IsDeleted = true;
        Update();
    }

    // Helpers
    private static bool ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 255)
            return false;

        return true;
    }
    private void Update()
    {
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    // Relationships
    public void AssignCategory(Guid categoryId)
    {
        if (_categories.Any(pc => pc.CategoryId == categoryId))
            throw new DomainException("Product already has this category assigned.");

        if (IsDeleted)
            throw new DomainException("Can't assign category to deleted product.");

        _categories.Add(ProductCategory.Create(Id, categoryId));
    }
}