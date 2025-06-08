using FluentAssertions;
using Marketplace.Api;
using Marketplace.Client;
using Marketplace.Client.Contracts;
using Marketplace.Client.Exceptions;
using Marketplace.Tests.List.TestCaseEntities;
using Marketplace.Tests.List.TestCases;
using Marketplace.Tests.TestCaseEntities;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Xunit;
using Xunit.Abstractions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;


namespace Marketplace.Tests.List
{
    /// <summary>
    /// Тесты на поиск товаров
    /// </summary>
    public class ListProductTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly ITestOutputHelper _output;
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Mock<TimeProvider> _timeProviderMock;

        public ListProductTests(ITestOutputHelper output, WebApplicationFactory<Program> factory)
        {
            _output = output;
            _factory = factory;
            _timeProviderMock = new Mock<TimeProvider>();
        }

        [Theory(DisplayName = "ListProduct_ValidParams_ReturnsExpectedProducts")]
        [ClassData(typeof(ListProductsPositiveCases))]
        public async Task ListProduct_ValidParams_ReturnsExpectedProducts(ListProductTestCase testCase)
        {
            // Arrange:
            _output.WriteLine(testCase.TestId);
            _output.WriteLine(testCase.Description);
            ArrangeMocks(testCase.MocksData);
            var apiClient = CreateClient();
            foreach (var product in testCase.StorageState.Products)
            {
                await apiClient.CreateAsync(product, CancellationToken.None);
            }

            // Act:
            var result = await apiClient.ListAsync(testCase.Parameters.Request, CancellationToken.None);

            // Assert:
            foreach (var inc in testCase.Expectations.IncludedProducts)
            {
                result.Should().ContainEquivalentOf(inc, opts => opts.Excluding(dto => dto.Id));
            }
            foreach (var exc in testCase.Expectations.ExcludedProducts)
            {
                result.Should().NotContainEquivalentOf(exc, opts => opts.Excluding(dto => dto.Id));
            }
        }

        [Theory(DisplayName = "ListProduct_InvalidParams_ThrowsError")]
        [ClassData(typeof(ListProductsNegativeCases))]
        public async Task ListProduct_InvalidParams_ThrowsError(ListProductTestCase testCase)
        {
            // Arrange:
            _output.WriteLine(testCase.TestId);
            _output.WriteLine(testCase.Description);
            var httpClient = _factory.CreateClient();
            var apiClient = new ProductsApiClient(httpClient);

            // Act:
            var exception = await Assert.ThrowsAsync<ApiException>(() =>
                apiClient.ListAsync(testCase.Parameters.Request, CancellationToken.None));

            // Assert:
            exception.Code.Should().Be(testCase.Expectations.HttpStatusCode);
            exception.Message.Should().Be(testCase.Expectations.Error);
        }

        private ProductsApiClient CreateClient()
        {
            var httpClient = _factory.WithWebHostBuilder(b => b.ConfigureTestServices(services =>
                services.AddSingleton(_timeProviderMock.Object))).CreateClient();
            return new ProductsApiClient(httpClient);
        }

        private void ArrangeMocks(MocksData mocksData)
        {
            _timeProviderMock.Setup(tp => tp.GetUtcNow()).Returns(mocksData.UtcNow);
        }
    }
}
