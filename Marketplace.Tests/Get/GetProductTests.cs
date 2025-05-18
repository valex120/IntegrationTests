using FluentAssertions;
using Marketplace.Api;
using Marketplace.Client;
using Marketplace.Client.Exceptions;
using Marketplace.Tests.Get.TestCaseEntities;
using Marketplace.Tests.Get.TestCases;
using Marketplace.Tests.TestCaseEntities;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Xunit;
using Xunit.Abstractions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;


namespace Marketplace.Tests.Get
{
    /// <summary>
    /// Тесты на получение товаров
    /// </summary>
    public class GetProductTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly ITestOutputHelper _output;
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Mock<TimeProvider> _timeProviderMock;

        public GetProductTests(ITestOutputHelper output, WebApplicationFactory<Program> factory)
        {
            _output = output;
            _factory = factory;
            _timeProviderMock = new Mock<TimeProvider>();
        }

        [Fact(DisplayName = "GetProduct_Existing_ReturnsProduct")]
        public async Task GetProduct_Existing_ReturnsProduct()
        {
            // Arrange:
            var testCase = Get_ExistingProduct_ReturnsProduct.Get();
            _output.WriteLine(testCase.TestId);
            _output.WriteLine(testCase.Description);
            ArrangeMocks(testCase.MocksData);
            var apiClient = CreateClient();

            // Act:
            var createdProduct = await apiClient.CreateAsync(testCase.ExistingProduct, CancellationToken.None);
            var fetchedProduct = await apiClient.GetAsync(createdProduct.Id, CancellationToken.None);

            // Assert:
            fetchedProduct.Should().BeEquivalentTo(testCase.Expectations.Product,
                options => options.Excluding(dto => dto.Id));
        }

        [Fact(DisplayName = "GetProduct_NonExisting_Returns404")]
        public async Task GetProduct_NonExisting_Returns404()
        {
            // Arrange:
            var testCase = Get_NonExistingProduct_Returns404NotFound.Get();
            _output.WriteLine(testCase.TestId);
            _output.WriteLine(testCase.Description);
            var apiClient = CreateClient();

            // Act:
            var fetchedProduct = await apiClient.GetAsync(testCase.Parameters.ProductId, CancellationToken.None);

            // Assert:
            fetchedProduct.Should().BeNull();
        }

        private ProductsApiClient CreateClient()
        {
            var httpClient = _factory.WithWebHostBuilder(b => b.ConfigureTestServices(services =>
                services.AddSingleton(_timeProviderMock.Object)))
                .CreateClient();
            return new ProductsApiClient(httpClient);
        }

        private void ArrangeMocks(MocksData mocksData)
        {
            _timeProviderMock.Setup(tp => tp.GetUtcNow()).Returns(mocksData.UtcNow);
        }
    }
}
