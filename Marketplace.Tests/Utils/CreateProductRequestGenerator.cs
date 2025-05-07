using Marketplace.Client.Contracts;

namespace Marketplace.Tests.Utils;

/// <summary>
///     Генератор запросов на создание товаров
/// </summary>
public class CreateProductRequestGenerator
{
    public static CreateProductRequest Get(string testId)
    {
        return new CreateProductRequest
        {
            Article = Guid.NewGuid().ToString(),
            Name = testId,
            Price = 10000,
            Category = "Children"
        };
    }
}
