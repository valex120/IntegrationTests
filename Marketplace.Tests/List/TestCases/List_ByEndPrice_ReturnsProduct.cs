using Marketplace.Api.Mappers;
using Marketplace.Client.Contracts;
using Marketplace.Tests.List.TestCaseEntities;
using Marketplace.Tests.Utils;

namespace Marketplace.Tests.List.TestCases;

/// <summary>
///     List. Передана конечная цена 10. В БД есть товар с ценой 9. Возвращает товары менее 10, включая заведённый.
/// </summary>
public class List_ByEndPrice_ReturnsProduct
{
    private const string TestId = "List_ByEndPrice_ReturnsProduct";

    private const string Description = @"
List. Передана конечная цена 10. В БД есть товар с ценой 9. Возвращает товары менее 10, включая заведённый.

Состояние БД:
    - Товар с ценой 9
    - Товар с ценой 11

Входящие параметры:
    - Конечная цена 10

Действие:
    - Вызов GET api/v1/products

Ожидаемые значения:
    - Возвращает товар с ценой 9
    - Не возвращает товар с ценой 11
";
    public static ListProductTestCase Get()
    {
        var now = DateTimeOffset.UtcNow.Truncate();
        var product1 = CreateProductRequestGenerator.Get(TestId);
        product1.Price = 9;
        var product2 = CreateProductRequestGenerator.Get(TestId);
        product2.Price = 11;
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
                    EndPrice = 10
                }
            },
            Expectations = new ListProductTestCaseExpectations
            {
                IncludedProducts = new[] { product1.MapToEntity(now).MapToDto() },
                ExcludedProducts = new[] { product2.MapToEntity(now).MapToDto() },
            }
        };
    }
}
