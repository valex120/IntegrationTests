using Marketplace.Api.Mappers;
using Marketplace.Client.Contracts;
using Marketplace.Tests.Update.TestCaseEntities;
using Marketplace.Tests.Utils;

namespace Marketplace.Tests.Update.TestCases;

/// <summary>
///     Update. Переданы корректные данные. Обновляет данные товара.
/// </summary>
public class Update_ValidParameters_UpdatesProduct
{
    private const string TestId = "Update_ValidParameters_ReturnsProduct";

    private const string Description = @"
Update. Переданы корректные данные. Обновляет данные товара.

Состояние БД:
    - какой-то товар с идентификатором

Входящие параметры:
    - идентификатор товара из БД
    - новая цена, новое название и новая категория товара

Действие:
    - Вызов PATCH api/v1/products

Ожидаемые значения:
    - По запросу GET api/v1/products товар с новыми данными
";
    public static UpdateProductTestCase Get()
    {
        var now = DateTimeOffset.UtcNow.Truncate();
        var product = new CreateProductRequest
        {
            Article = Guid.NewGuid().ToString(),
            Name = "Name",
            Price = 10000,
            Category = "Children"
        };
        var updateRequest = new UpdateProductRequest
        {
            Name = TestId,
            Price = 100500,
            Category = "Women"
        };

        var expectedProduct = updateRequest.MapToEntity();
        expectedProduct.Article = product.Article;
        expectedProduct.CreatedAt = now;
        expectedProduct.UpdatedAt = now;

        return new UpdateProductTestCase
        {
            TestId = TestId,
            Description = Description,
            ExistingProduct = product,
            MocksData = new Tests.TestCaseEntities.MocksData
            {
                UtcNow = now,
            },
            Parameters = new UpdateProductTestCaseParameters
            {
                UpdateRequest = updateRequest
            },
            Expectations = new UpdateProductTestCaseExpectations
            {
                Product = expectedProduct.MapToDto()
            }
        };
    }
}
