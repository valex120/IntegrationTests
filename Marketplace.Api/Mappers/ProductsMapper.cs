using Marketplace.Client.Contracts;
using Marketplace.Domain.Entities;

namespace Marketplace.Api.Mappers;

/// <summary>
///     Маппер товаров
/// </summary>
public static class ProductsMapper
{
    public static Product MapToEntity(this CreateProductRequest request, DateTimeOffset createAt)
    {
        return new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Price = request.Price,
            Article = request.Article,
            Category = ProductCategoriesMapper.Map(request.Category) ?? ProductCategory.Unknown,
            CreatedAt = createAt,
            UpdatedAt = createAt
        };
    }

    public static Product MapToEntity(this UpdateProductRequest request)
    {
        return new Product
        {
            Id = request.Id,
            Name = request.Name,
            Price = request.Price,
            Category = ProductCategoriesMapper.Map(request.Category) ?? ProductCategory.Unknown
        };
    }

    public static ProductDto MapToDto(this Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Article = product.Article,
            Category = product.Category.ToString(),
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt
        };
    }
}