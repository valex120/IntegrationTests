using System.Net;
using Marketplace.Tests.Update.TestCaseEntities;

namespace Marketplace.Tests.Update.TestCases;

/// <summary>
///     Update. Передана неизвестная категория товара. Возвращает 400 BadRequest, "UnknownCategory".
/// </summary>
public class Update_UnknownCategory_Returns400BadRequest
{
    private const string TestId = "Update_UnknownCategory_Returns400BadRequest";

    private const string Description = @"
Update. Передана неизвестная категория товара. Возвращает 400 BadRequest, ""UnknownCategory"".

Входящие параметры:
    - Категория 0

Действие:
    - Вызов PATCH api/v1/products

Ожидаемые значения:
    - Код ответа 400 BadRequest
    - Сообщение 'UnknownCategory'
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
                    Category = "Unknown",
                    Name = "Name",
                    Price = 1000
                }
            },
            Expectations = new UpdateProductTestCaseExpectations
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Error = "UnknownCategory"
            }
        };
    }
}
