using System.Net;
using Marketplace.Tests.Update.TestCaseEntities;

namespace Marketplace.Tests.Update.TestCases;

/// <summary>
///     Update. Передана нулевая цена. Возвращает 400 BadRequest, "ZeroPrice".
/// </summary>
public class Update_ZeroPrice_Returns400BadRequest
{
    private const string TestId = "Update_ZeroPrice_Returns400BadRequest";

    private const string Description = @"
Update. Передана нулевая цена.  Возвращает 400 BadRequest, ""ZeroPrice"".

Входящие параметры:
    - Нулевая цена

Действие:
    - Вызов PATCH api/v1/products

Ожидаемые значения:
    - Код ответа 400 BadRequest
    - Сообщение 'ZeroPrice'
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
                    Price = 0
                }
            },
            Expectations = new UpdateProductTestCaseExpectations
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Error = "ZeroPrice"
            }
        };
    }
}
