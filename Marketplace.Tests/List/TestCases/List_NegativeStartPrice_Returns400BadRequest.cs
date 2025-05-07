using System.Net;
using Marketplace.Tests.List.TestCaseEntities;

namespace Marketplace.Tests.List.TestCases;

/// <summary>
///     List. Передана отрицательная цена. Возвращает 400 BadRequest, "NegativeStartPrice".
/// </summary>
public class List_NegativeStartPrice_Returns400BadRequest
{
    private const string TestId = "List_NegativeStartPrice_Returns400BadRequest";

    private const string Description = @"
List. Передана отрицательная цена. Возвращает 400 BadRequest, ""NegativeStartPrice"".

Входящие параметры:
    - Отрицательная начальная цена

Действие:
    - Вызов GET api/v1/products

Ожидаемые значения:
    - Код ответа 400 BadRequest
    - Сообщение 'NegativeStartPrice'
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
                    StartPrice = -1
                }
            },
            Expectations = new ListProductTestCaseExpectations
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Error = "NegativeStartPrice"
            }
        };
    }
}
