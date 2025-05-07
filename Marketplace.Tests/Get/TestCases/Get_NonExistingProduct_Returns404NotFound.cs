using System.Net;
using Marketplace.Tests.Get.TestCaseEntities;

namespace Marketplace.Tests.Get.TestCases;

/// <summary>
///     Get. Передан идентификатор несуществующего товара. Возвращает 404 NotFound "ProductNotFound".
/// </summary>
public class Get_NonExistingProduct_Returns404NotFound
{
    private const string TestId = "Get_NonExistingProduct_Returns404NotFound";

    private const string Description = @"
Get. Передан идентификатор несуществующего товара. Возвращает 404 NotFound ""ProductNotFound"".

Входящие параметры:
    - новый случайный Guid

Действие:
    - Вызов GET api/v1/products/{id}

Ожидаемые значения:
    - Код ответа 404 NotFound
    - Сообщение 'ProductNotFound'
";
    public static GetProductTestCase Get()
    {
        return new GetProductTestCase
        {
            TestId = TestId,
            Description = Description,
            Parameters = new GetProductTestCaseParameters
            {
                ProductId = Guid.NewGuid()
            },
            Expectations = new GetProductTestCaseExpectations
            {
                HttpStatusCode = HttpStatusCode.NotFound,
                Error = "ProductNotFound"
            }
        };
    }
}
