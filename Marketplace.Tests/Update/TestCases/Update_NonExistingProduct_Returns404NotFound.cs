using System.Net;
using Marketplace.Tests.Update.TestCaseEntities;

namespace Marketplace.Tests.Update.TestCases;

/// <summary>
///     Update. Передан идентификатор несуществующего товара. Возвращает 404 NotFound "ProductNotFound".
/// </summary>
public class Update_NonExistingProduct_Returns404NotFound
{
    private const string TestId = "Update_NonExistingProduct_Returns404NotFound";

    private const string Description = @"
Update. Передан идентификатор несуществующего товара. Возвращает 404 NotFound ""ProductNotFound"".

Входящие параметры:
    - новый случайный Guid

Действие:
    - Вызов PATCH api/v1/products

Ожидаемые значения:
    - Код ответа 404 NotFound
    - Сообщение 'ProductNotFound'
";
    public static UpdateProductTestCase Get()
    {
        return new UpdateProductTestCase
        {
            TestId = TestId,
            Description = Description,
            Parameters = new UpdateProductTestCaseParameters
            {
                UpdateRequest = new Client.Contracts.UpdateProductRequest
                {
                    Id = Guid.NewGuid(),
                    Category = "Children",
                    Name = "Name",
                    Price = 10000
                }
            },
            Expectations = new UpdateProductTestCaseExpectations
            {
                HttpStatusCode = HttpStatusCode.NotFound,
                Error = "ProductNotFound"
            }
        };
    }
}