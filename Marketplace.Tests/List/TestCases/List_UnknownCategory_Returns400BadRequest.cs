using System.Net;
using Marketplace.Tests.List.TestCaseEntities;

namespace Marketplace.Tests.List.TestCases;

/// <summary>
///     List. Передана неизвестная категория товара. Возвращает 400 BadRequest, "UnknownCategory".
/// </summary>
public class List_UnknownCategory_Returns400BadRequest
{
    private const string TestId = "List_UnknownCategory_Returns400BadRequest";

    private const string Description = @"
List. Передана неизвестная категория товара. Возвращает 400 BadRequest, ""UnknownCategory"".

Входящие параметры:
    - Категория 0

Действие:
    - Вызов GET api/v1/products

Ожидаемые значения:
    - Код ответа 400 BadRequest
    - Сообщение 'UnknownCategory'
";
    public static ListProductTestCase Get()
    {
        return new ListProductTestCase
        {
            TestId = TestId,
            Description = Description,
            Parameters = new ListProductTestCaseParameters
            {
                Request = new Client.Contracts.ListProductsRequest
                {
                    Category = "Unknown"
                }
            },
            Expectations = new ListProductTestCaseExpectations
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Error = "UnknownCategory"
            }
        };
    }
}
