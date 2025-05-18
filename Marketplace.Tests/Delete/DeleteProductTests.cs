using FluentAssertions;
using Marketplace.Api;
using Marketplace.Client;
using Marketplace.Client.Exceptions;
using Marketplace.Tests.TestCaseEntities;
using Marketplace.Tests.Delete.TestCases;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;

namespace Marketplace.Tests.Delete
{
    /// <summary>
    /// Тесты на удаление товаров
    /// </summary>
    public class DeleteProductTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly ITestOutputHelper _output;
        private readonly WebApplicationFactory<Program> _factory;

        public DeleteProductTests(ITestOutputHelper output, WebApplicationFactory<Program> factory)
        {
            _output = output;
            _factory = factory;
        }

        [Fact(DisplayName = "DeleteProduct_Existing_DeletesProduct")]
        public async Task DeleteProduct_Existing_DeletesProduct()
        {
            // Arrange:
            var testCase = Delete_ExistingProduct_DeletesProduct.Get();
            _output.WriteLine(testCase.TestId);
            _output.WriteLine(testCase.Description);
            var apiClient = CreateClient();
            var createdProduct = await apiClient.CreateAsync(testCase.ExistingProduct, CancellationToken.None);

            // Act:
            await apiClient.DeleteAsync(createdProduct.Id, CancellationToken.None);

            // Assert:
            var fetchedProduct = await apiClient.GetAsync(createdProduct.Id, CancellationToken.None);
            fetchedProduct.Should().BeNull();
        }

        [Fact(DisplayName = "DeleteProduct_NonExisting_Returns404")]
        public async Task DeleteProduct_NonExisting_Returns404()
        {
            // Arrange
            var testCase = Delete_NonExistingProduct_Returns404NotFound.Get();
            _output.WriteLine(testCase.TestId);
            _output.WriteLine(testCase.Description);
            var apiClient = CreateClient();

            // Act
            var exception = await Assert.ThrowsAsync<ApiException>(() =>
                apiClient.DeleteAsync(testCase.Parameters.ProductId, CancellationToken.None));
            // Assert
            exception.Code.Should().Be(testCase.Expectations.HttpStatusCode);
            exception.Message.Should().Be(testCase.Expectations.Error);
        }

        private ProductsApiClient CreateClient()
        {
            var httpClient = _factory.CreateClient();
            return new ProductsApiClient(httpClient);
        }
    }
}
