using System.Net;
using Marketplace.Tests.List.TestCaseEntities;

namespace Marketplace.Tests.List.TestCases;

/// <summary>
///     List. Передана отрицательная конечная цена. Возвращает 400 BadRequest, "NegativeEndPrice".
/// </summary>
public class List_NegativeEndPrice_Returns400BadRequest
{
    private const string TestId = "List_NegativeEndPrice_Returns400BadRequest";

    private const string Description = @"
List. Передана отрицательная конечная цена. Возвращает 400 BadRequest, ""NegativeEndPrice"".

Входящие параметры:
    - Отрицательная конечная цена

Действие:
    - Вызов GET api/v1/products

Ожидаемые значения:
    - Код ответа 400 BadRequest
    - Сообщение 'NegativeEndPrice'
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
                    EndPrice = -1
                }
            },
            Expectations = new ListProductTestCaseExpectations
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Error = "NegativeEndPrice"
            }
        };
    }
}
