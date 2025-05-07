using System.Net;
using Marketplace.Tests.Update.TestCaseEntities;

namespace Marketplace.Tests.Update.TestCases;

/// <summary>
///     Update. Передано имя товара длиной 1001 символ. Возвращает 400 BadRequest, "NameTooLong".
/// </summary>
public class Update_TooLongName_Returns400BadRequest
{
    private const string TestId = "Update_TooLongName_Returns400BadRequest";

    private const string Description = @"
Update. Передано имя товара длиной 1001 символ. Возвращает 400 BadRequest, ""NameTooLong"".

Входящие параметры:
    - Имя товара длиной 1001 символ

Действие:
    - Вызов PATCH api/v1/products

Ожидаемые значения:
    - Код ответа 400 BadRequest
    - Сообщение 'NameTooLong'
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
                    Name = new string('N', 1001),
                    Price = 10000
                }
            },
            Expectations = new UpdateProductTestCaseExpectations
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Error = "NameTooLong"
            }
        };
    }
}
