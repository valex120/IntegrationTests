using System.Net;
using Marketplace.Tests.List.TestCaseEntities;

namespace Marketplace.Tests.List.TestCases;

/// <summary>
///     List. Передана начальная цена 0. Возвращает 400 BadRequest, "ZeroStartPrice".
/// </summary>
public class List_ZeroStartPrice_Returns400BadRequest
{
    private const string TestId = "List_ZeroStartPrice_Returns400BadRequest";

    private const string Description = @"
List. Передана начальная цена 0. Возвращает 400 BadRequest, ""ZeroStartPrice"".

Входящие параметры:
    - Нулевая начальная цена

Действие:
    - Вызов GET api/v1/products

Ожидаемые значения:
    - Код ответа 400 BadRequest
    - Сообщение 'ZeroStartPrice'
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
                    StartPrice = 0
                }
            },
            Expectations = new ListProductTestCaseExpectations
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Error = "ZeroStartPrice"
            }
        };
    }
}
