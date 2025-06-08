using System.Collections;
using System.Collections.Generic;
using Marketplace.Tests.Update.TestCaseEntities;

namespace Marketplace.Tests.Update.TestCases
{
    public class UpdateProductPositiveCases : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { GetTestCasesInternal() };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public static IEnumerable<object[]> GetTestCases()
        {
            return new UpdateProductPositiveCases();
        }

        private static UpdateProductTestCase GetTestCasesInternal()
        {
            return new UpdateProductTestCase
            {
                TestId = "Update_ValidParameters_ReturnsProduct",
                Description = "Stub описание для Update_ValidParameters_ReturnsProduct",
                ExistingProduct = new Marketplace.Client.Contracts.CreateProductRequest
                {
                    Name = "Old Name",
                    Article = "ABCDEFGHIJ",
                    Price = 100,
                    Category = "Children"
                },
                MocksData = new Marketplace.Tests.TestCaseEntities.MocksData
                {
                    UtcNow = DateTimeOffset.UtcNow
                },
                Parameters = new Marketplace.Tests.Update.TestCaseEntities.UpdateProductTestCaseParameters
                {
                    UpdateRequest = new Marketplace.Client.Contracts.UpdateProductRequest
                    {
                        Name = "New Name",
                        Price = 200,
                        Category = "Women"
                        
                    }
                },
                Expectations = new Marketplace.Tests.Update.TestCaseEntities.UpdateProductTestCaseExpectations
                {
                     Product = new Marketplace.Client.Contracts.ProductDto
                    {
                        Name = "New Name",
                        Price = 200,
                        Article = "ABCDEFGHIJ",
                        Category = "Women",
                        CreatedAt = DateTimeOffset.UtcNow, 
                        UpdatedAt = DateTimeOffset.UtcNow
                    }
                }
            };
        }
    }
}
