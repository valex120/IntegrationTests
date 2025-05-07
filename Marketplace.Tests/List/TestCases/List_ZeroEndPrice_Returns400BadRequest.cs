using System.Net;
using Marketplace.Tests.List.TestCaseEntities;

namespace Marketplace.Tests.List.TestCases;

/// <summary>
///     List. Передана нулевая конечная цена. Возвращает 400 BadRequest, "ZeroEndPrice".
/// </summary>
public class List_ZeroEndPrice_Returns400BadRequest
{
    private const string TestId = "List_ZeroPrice_Returns400BadRequest";

    private const string Description = @"
List. Передана нулевая конечная цена. Возвращает 400 BadRequest, ""ZeroEndPrice"".

Входящие параметры:
    - Нулевая конечная цена

Действие:
    - Вызов GET api/v1/products

Ожидаемые значения:
    - Код ответа 400 BadRequest
    - Сообщение 'ZeroPrice'
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
                    EndPrice = 0
                }
            },
            Expectations = new ListProductTestCaseExpectations
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Error = "ZeroEndPrice"
            }
        };
    }
}
