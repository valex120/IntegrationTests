using System.Net;
using Marketplace.Tests.List.TestCaseEntities;

namespace Marketplace.Tests.List.TestCases;

/// <summary>
///     List. Передана начальная цена 10, конечная цена 5. Возвращает 400 BadRequest, "StartPriceMoreThenEndPrice".
/// </summary>
public class List_StartPriceMoreThenEndPrice_Returns400BadRequest
{
    private const string TestId = "List_StartPriceMoreThenEndPrice_Returns400BadRequest";

    private const string Description = @"
List. Передана начальная цена 10, конечная цена 5. Возвращает 400 BadRequest, ""StartPriceMoreThenEndPrice"".

Входящие параметры:
    - Начальная цена 10
    - Конечная цена 5

Действие:
    - Вызов GET api/v1/products

Ожидаемые значения:
    - Код ответа 400 BadRequest
    - Сообщение 'StartPriceMoreThenEndPrice'
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
                    StartPrice = 10,
                    EndPrice = 5
                }
            },
            Expectations = new ListProductTestCaseExpectations
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Error = "StartPriceMoreThenEndPrice"
            }
        };
    }
}
