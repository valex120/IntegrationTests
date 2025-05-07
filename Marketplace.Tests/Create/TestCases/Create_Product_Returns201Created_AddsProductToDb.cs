using System.Net;
using Marketplace.Client.Contracts;
using Marketplace.Tests.Create.TestCaseEntities;
using Marketplace.Tests.TestCaseEntities;
using Marketplace.Tests.Utils;

namespace Marketplace.Tests.Create.TestCases;

/// <summary>
///     Добавление нового товара с валидными данными возвращает 201 Created и добавляет товар в БД
/// </summary>
public class Create_Product_Returns201Created_AddsProductToDb
{
    private const string TestId = "Create_Product_Returns201Created_AddsProductToDb";

    private const string Description = @"
Добавление нового товара с валидными данными возвращает 201 Created и добавляет товар в БД

Состояние БД:
    - нет товаров с таким же артикулом
Данные моков:
    - текущая дата UTC
Входящие параметры:
    - новый товар с уникальным артикулом

Действие:
    - Вызов POST api/v1/products

Ожидаемые значения:
    - Код ответа 201 Created
    - Хедер location - URL для получения добавленного товара
    - В БД добавлен новый товар
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
                HttpStatusCode = HttpStatusCode.Created,
                Product = new ProductDto
                {
                    Name = request.Name,
                    Article = request.Article,
                    Category = request.Category,
                    Price = request.Price,
                    CreatedAt = utcNow,
                    UpdatedAt = utcNow
                },
                LocationHeader = "http://localhost/api/v1/Products/{0}",
                Error = null
            }
        };
    }
}