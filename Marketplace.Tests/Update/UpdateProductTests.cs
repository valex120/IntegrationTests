using Marketplace.Client.Contracts;
using Marketplace.Tests.TestCaseEntities;
using Marketplace.Tests.Update.TestCaseEntities;
using Marketplace.Api.Mappers;
using Marketplace.Tests.Utils;
using System;

namespace Marketplace.Tests.Update.TestCases
{
    /// <summary>
    /// Update. Переданы корректные данные. Обновляет данные товара.
    /// </summary>
    public class Update_ValidParameters_ReturnsProduct
    {
        private const string TestId = "Update_ValidParameters_ReturnsProduct";
        private const string Description = @"
Update. Переданы корректные данные. Обновляет данные товара.

Состояние БД:
    - создан товар с фиксированным артикулом ""ABCDEFGHIJ""
Входящие параметры:
    - обновляются только Name, Price, Category (Article не изменяется)
Ожидаемые значения:
    - После обновления Article равен ""ABCDEFGHIJ""
";

        public static UpdateProductTestCase Get()
        {
            var now = DateTimeOffset.UtcNow.Truncate();
            const string fixedArticle = "ABCDEFGHIJ";  
            var existingProduct = new CreateProductRequest
            {
                Article = fixedArticle,
                Name = "Old Product Name",
                Price = 10000,
                Category = "Children"
            };

            var updateRequest = new UpdateProductRequest
            {
                Name = "Updated Product Name",
                Price = 20000,
                Category = "Women"
            };

            var expectedProduct = updateRequest.MapToEntity();
            expectedProduct.Article = fixedArticle;
            expectedProduct.CreatedAt = now;
            expectedProduct.UpdatedAt = now;

            return new UpdateProductTestCase
            {
                TestId = TestId,
                Description = Description,
                ExistingProduct = existingProduct,
                MocksData = new MocksData { UtcNow = now },
                Parameters = new UpdateProductTestCaseParameters { UpdateRequest = updateRequest },
                Expectations = new UpdateProductTestCaseExpectations { Product = expectedProduct.MapToDto() }
            };
        }
    }
}
