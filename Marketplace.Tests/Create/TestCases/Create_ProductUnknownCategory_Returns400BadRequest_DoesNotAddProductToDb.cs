using System.Net;
using Marketplace.Client.Contracts;
using Marketplace.Tests.Create.TestCaseEntities;
using Marketplace.Tests.TestCaseEntities;
using Marketplace.Tests.Utils;

namespace Marketplace.Tests.Create.TestCases;

/// <summary>
///     Добавление товара c несуществующей категорией возвращает 400 BadRequest и НЕ добавляет товар в БД
/// </summary>
public class Create_ProductUnknownCategory_Returns400BadRequest_DoesNotAddProductToDb
{
    private const string TestId = "Create_ProductUnknownCategory_Returns400BadRequest_DoesNotAddProductToDb";

    private const string Description = @"
Добавление товара c несуществующей категорией возвращает 400 BadRequest и НЕ добавляет товар в БД

Состояние БД:
    - нет товаров с таким же артикулом
Данные моков:
    - текущая дата UTC
Входящие параметры:
    - новый товар с категорией WRONG-CATEGORY

Действие:
    - Вызов POST api/v1/products

Ожидаемые значения:
    - Код ответа 400 BadRequest
    - Хедер location пустой
    - Ошибка - UnknownCategory
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
            Category = "WRONG-CATEGORY"
        };

        return new CreateProductTestCase
        {
            TestId = TestId,
            Description = Description,
            ExistingProduct = null,
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
                HttpStatusCode = HttpStatusCode.BadRequest,
                Product = null,
                LocationHeader = null,
                Error = "UnknownCategory"
            }
        };
    }
}