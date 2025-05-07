using System.Net;
using Marketplace.Client.Contracts;
using Marketplace.Tests.Create.TestCaseEntities;
using Marketplace.Tests.TestCaseEntities;
using Marketplace.Tests.Utils;

namespace Marketplace.Tests.Create.TestCases;

/// <summary>
///     Добавление товара с дублирующимся артикулом возвращает 409 Conflict и НЕ добавляет товар в БД
/// </summary>
public class Create_ProductWithDuplicatedArticle_Returns409Conflict_DoesNotAddProductToDb
{
    private const string TestId = "Create_ProductWithDuplicatedArticle_Returns409Conflict_DoesNotAddProductToDb";

    private const string Description = @"
Добавление товара с дублирующимся артикулом возвращает 409 Conflict и НЕ добавляет товар в БД

Состояние БД:
    - есть товар с данным артикулом
Данные моков:
    - текущая дата UTC
Входящие параметры:
    - новый товар с дублирующимся артикулом

Действие:
    - Вызов POST api/v1/products

Ожидаемые значения:
    - Код ответа 409 Conflict
    - Хедер location пустой
    - Ошибка - AlreadExists
    - В БД не добавлен новый товар
";

    public static CreateProductTestCase Get()
    {
        var utcNow = DateTimeOffset.UtcNow.Truncate();
        var request = new CreateProductRequest
        {
            Article = Guid.NewGuid().ToString(),
            Name = "Плюшевый мишка",
            Price = 10000,
            Category = "Children"
        };

        return new CreateProductTestCase
        {
            TestId = TestId,
            Description = Description,
            ExistingProduct = request,
            MocksData = new MocksData
            {
                UtcNow = utcNow
            },
            Parameters = new CreateProductTestCaseParameters
            {
                NewProduct = request
            },
            Expectations = new CreateProductTestCaseExpectations
            {
                HttpStatusCode = HttpStatusCode.Conflict,
                Product = null,
                LocationHeader = null,
                Error = "AlreadyExists"
            }
        };
    }
}