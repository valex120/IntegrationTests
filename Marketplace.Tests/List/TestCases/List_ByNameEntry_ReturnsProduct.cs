using Marketplace.Api.Mappers;
using Marketplace.Client.Contracts;
using Marketplace.Tests.List.TestCaseEntities;
using Marketplace.Tests.Utils;

namespace Marketplace.Tests.List.TestCases;

/// <summary>
///     List. Передана вхождение в название "Книга". В БД есть товар с "Книга" в названии. Возвращает товары с "Книга" в названии, включая заведённый.
/// </summary>
public class List_ByNameEntry_ReturnsProduct
{
    private const string TestId = "List_ByNameEntry_ReturnsProduct";

    private const string Description = @"
 List. Передана вхождение в название ""Книга"". В БД есть товар с ""Книга"" в названии. Возвращает товары с ""Книга"" в названии, включая заведённый.

Состояние БД:
    - Товар с именем ""Большая Книга Рецептов""
    - Товар с именем ""Футбольный мяч""

Входящие параметры:
    - Вхождние в имя ""Книга""

Действие:
    - Вызов GET api/v1/products

Ожидаемые значения:
    - Возвращает товар с именем ""Большая Книга Рецептов""
    - Не возвращает товары с именем ""Футбольный мяч""
";
    public static ListProductTestCase Get()
    {
        var now = DateTimeOffset.UtcNow.Truncate();
        var product1 = CreateProductRequestGenerator.Get(TestId);
        product1.Name = "Большая Книга Рецептов";
        var product2 = CreateProductRequestGenerator.Get(TestId);
        product2.Name = "Футбольный мяч";
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
                    Name = "Книга"
                }
            },
            Expectations = new ListProductTestCaseExpectations
            {
                IncludedProducts = new[] { product1.MapToEntity(now).MapToDto() },
                ExcludedProducts = new[] { product2.MapToEntity(now).MapToDto() }
            }
        };
    }
}
