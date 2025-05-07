using Marketplace.Tests.Create.TestCaseEntities;
using Marketplace.Tests.Utils;

namespace Marketplace.Tests.Delete.TestCases;

/// <summary>
///     Delete. Передан идентификатор существующего товара. Удаляет товар.
/// </summary>
public class Delete_ExistingProduct_DeletesProduct
{
    private const string TestId = "Delete_ExistingProduct_DeletesProduct";

    private const string Description = @"
Delete. Передан идентификатор существующего товара. Удаляет товар.

Состояние БД:
    - какой-то товар с идентификатором

Входящие параметры:
    - идентификатор товара из БД

Действие:
    - Вызов DELETE api/v1/products

Ожидаемые значения:
    - По запросу GET api/v1/products возвращается 404
";
    public static DeleteProductTestCase Get()
    {
        return new DeleteProductTestCase
        {
            TestId = TestId,
            Description = Description,
            ExistingProduct = CreateProductRequestGenerator.Get(TestId)
        };
    }
}
