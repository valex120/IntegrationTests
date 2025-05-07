using System.Net;
using Marketplace.Tests.Update.TestCaseEntities;

namespace Marketplace.Tests.Update.TestCases;

/// <summary>
///     Update. Передано пустое имя товара. Возвращает 400 BadRequest, "EmptyName".
/// </summary>
public class Update_EmptyName_Returns400BadRequest
{
    private const string TestId = "Update_EmptyName_Returns400BadRequest";

    private const string Description = @"
Update. Передано пустое имя товара. Возвращает 400 BadRequest, ""EmptyName"".

Входящие параметры:
    - Пустое имя товара

Действие:
    - Вызов PATCH api/v1/products

Ожидаемые значения:
    - Код ответа 400 BadRequest
    - Сообщение 'EmptyName'
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
                    Name = string.Empty,
                    Price = 10000
                }
            },
            Expectations = new UpdateProductTestCaseExpectations
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Error = "EmptyName"
            }
        };
    }
}
