using Marketplace.Api.Mappers;
using Marketplace.Tests.Get.TestCaseEntities;
using Marketplace.Tests.Utils;

namespace Marketplace.Tests.Get.TestCases;

/// <summary>
///     Get. Передан идентификатор существующего товара. Возвращает товар.
/// </summary>
public class Get_ExistingProduct_ReturnsProduct
{
    private const string TestId = "Get_ExistingProduct_ReturnsProduct";

    private const string Description = @"
Get. Передан идентификатор существующего товара. Возвращает товар.

Состояние БД:
    - какой-то товар с идентификатором

Входящие параметры:
    - идентификатор товара из БД

Действие:
    - Вызов GET api/v1/products/{id}

Ожидаемые значения:
    - Возвращает товар по идентификатору
";
    public static GetProductTestCase Get()
    {
        var now = DateTimeOffset.UtcNow.Truncate();
        var product = CreateProductRequestGenerator.Get(TestId);
        return new GetProductTestCase
        {
            TestId = TestId,
            Description = Description,
            ExistingProduct = product,
            MocksData  = new Tests.TestCaseEntities.MocksData
            {
                UtcNow = now,
            },
            Expectations = new GetProductTestCaseExpectations
            {
                Product = product.MapToEntity(now).MapToDto()
            }
        };
    }
}
