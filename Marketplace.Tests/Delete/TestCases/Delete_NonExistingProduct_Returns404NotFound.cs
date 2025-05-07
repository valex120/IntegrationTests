using System.Net;
using Marketplace.Tests.Create.TestCaseEntities;

namespace Marketplace.Tests.Delete.TestCases;

/// <summary>
///     Delete. Передан идентификатор несуществующего товара. Возвращает 404 NotFound "ProductNotFound".
/// </summary>
public class Delete_NonExistingProduct_Returns404NotFound
{
    private const string TestId = "Delete_NonExistingProduct_Returns404NotFound";

    private const string Description = @"
Delete. Передан идентификатор несуществующего товара. Возвращает 404 NotFound ""ProductNotFound"".

Входящие параметры:
    - новый случайный Guid

Действие:
    - Вызов DELETE api/v1/products

Ожидаемые значения:
    - Код ответа 404 NotFound
    - Сообщение 'ProductNotFound'
";
    public static DeleteProductTestCase Get()
    {
        return new DeleteProductTestCase
        {
            TestId = TestId,
            Description = Description,
            Parameters = new DeleteProductTestCaseParameters
            {
                ProductId = Guid.NewGuid()
            },
            Expectations = new DeleteProductTestCaseExpectations
            {
                HttpStatusCode = HttpStatusCode.NotFound,
                Error = "ProductNotFound"
            }
        };
    }
}
