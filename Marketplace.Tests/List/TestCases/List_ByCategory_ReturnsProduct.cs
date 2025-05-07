using Marketplace.Api.Mappers;
using Marketplace.Client.Contracts;
using Marketplace.Tests.List.TestCaseEntities;
using Marketplace.Tests.Utils;

namespace Marketplace.Tests.List.TestCases;

/// <summary>
///     List. Передана категория товара Children. В БД есть товар категории Children. Возвращает товары категории Children, включая заведённый.
/// </summary>
public class List_ByCategory_ReturnsProduct
{
    private const string TestId = "List_ByCategory_ReturnsProduct";

    private const string Description = @"
List. Передана категория товара Children. В БД есть товар категории Children. Возвращает товары категории Children, включая заведённый.

Состояние БД:
    - Товар с категорией Children
    - Товар с категорией Women
    - Товар с категорией Men

Входящие параметры:
    - Категория Children

Действие:
    - Вызов GET api/v1/products

Ожидаемые значения:
    - Возвращает товар с категорией Children
    - Не возвращает товары с категорией Women и Men
";
    public static ListProductTestCase Get()
    {
        var now = DateTimeOffset.UtcNow.Truncate();
        var product1 = CreateProductRequestGenerator.Get(TestId);
        product1.Category = "Children";
        var product2 = CreateProductRequestGenerator.Get(TestId);
        product2.Category = "Women";
        var product3 = CreateProductRequestGenerator.Get(TestId);
        product3.Category = "Women";
        return new ListProductTestCase
        {
            TestId = TestId,
            Description = Description,
            StorageState = new Tests.TestCaseEntities.ProductsStorageState
            {
                Products = new[] { product1, product2 }
            },
            MocksData = new Tests.TestCaseEntities.MocksData
            {
                UtcNow = now,
            },
            Parameters = new ListProductTestCaseParameters
            {
                Request = new ListProductsRequest
                {
                    Category = "Children"
                }
            },
            Expectations = new ListProductTestCaseExpectations
            {
                IncludedProducts = new[] { product1.MapToEntity(now).MapToDto() },
                ExcludedProducts = new[]
                {
                    product2.MapToEntity(now).MapToDto(),
                    product3.MapToEntity(now).MapToDto()
                }
            }
        };
    }
}
