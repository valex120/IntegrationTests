using System.Net;
using Marketplace.Tests.Update.TestCaseEntities;

namespace Marketplace.Tests.Update.TestCases;

/// <summary>
///     Update. Передана отрицательная цена. Возвращает 400 BadRequest, "NegativePrice".
/// </summary>
public class Update_NegativePrice_Returns400BadRequest
{
    private const string TestId = "Update_NegativePrice_Returns400BadRequest";

    private const string Description = @"
Update. Передана отрицательная цена. Возвращает 400 BadRequest, ""NegativePrice"".

Входящие параметры:
    - Отрицательная цена

Действие:
    - Вызов PATCH api/v1/products

Ожидаемые значения:
    - Код ответа 400 BadRequest
    - Сообщение 'NegativePrice'
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
                    Category = "Children",
                    Name = "Name",
                    Price = -1
                }
            },
            Expectations = new UpdateProductTestCaseExpectations
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Error = "NegativePrice"
            }
        };
    }
}
