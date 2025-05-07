using Marketplace.Domain.Entities;

namespace Marketplace.Api.Mappers;

/// <summary>
///     Маппер категорий товаров
/// </summary>
public static class ProductCategoriesMapper
{
    public static ProductCategory? Map(string? category)
    {
        if (string.IsNullOrEmpty(category))
            return null;

        if (Enum.TryParse<ProductCategory>(category, true, out var result))
            return result;

        return ProductCategory.Unknown;
    }
}