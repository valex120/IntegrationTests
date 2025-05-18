using FluentAssertions;
using Marketplace.Api;
using Marketplace.Client;
using Marketplace.Client.Contracts;
using Marketplace.Client.Exceptions;
using Marketplace.Tests.TestCaseEntities;
using Marketplace.Tests.Update.TestCaseEntities;
using Marketplace.Tests.Update.TestCases;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Xunit;
using Xunit.Abstractions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;


namespace Marketplace.Tests.Update
{
    /// <summary>
    /// Тесты на изменение данных товара
    /// </summary>
    public class UpdateProductTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly ITestOutputHelper _output;
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Mock<TimeProvider> _timeProviderMock;

        public UpdateProductTests(ITestOutputHelper output, WebApplicationFactory<Program> factory)
        {
            _output = output;
            _factory = factory;
            _timeProviderMock = new Mock<TimeProvider>();
        }

        [Theory(DisplayName = "UpdateProduct_ValidInput_UpdatesProduct")]
        [ClassData(typeof(UpdateProductPositiveCases))]
        public async Task UpdateProduct_ValidInput_UpdatesProduct(UpdateProductTestCase testCase)
        {
            // Arrange:
            _output.WriteLine(testCase.TestId);
            _output.WriteLine(testCase.Description);
            ArrangeMocks(testCase.MocksData);
            testCase.ExistingProduct.Article = Guid.NewGuid().ToString();
            var apiClient = CreateClient();
            var createdProduct = await apiClient.CreateAsync(testCase.ExistingProduct, CancellationToken.None);
            testCase.Parameters.UpdateRequest.Id = createdProduct.Id;
            // Act:
            await apiClient.UpdateAsync(testCase.Parameters.UpdateRequest, CancellationToken.None);
            var updatedProduct = await apiClient.GetAsync(testCase.Parameters.UpdateRequest.Id, CancellationToken.None);

            // Assert:
             updatedProduct.Should().BeEquivalentTo(testCase.Expectations.Product, options => options
                .Excluding(dto => dto.Id)
                .Excluding(dto => dto.Article)
                .Using<DateTimeOffset>(ctx =>
                {
                    // Сравниваем временные значения с допуском в 1 миллисекунду
                    ctx.Subject.Should().BeCloseTo(ctx.Expectation, TimeSpan.FromMilliseconds(1));
                })
                .WhenTypeIs<DateTimeOffset>());

        }

        [Theory(DisplayName = "UpdateProduct_InvalidInput_ReturnsError")]
        [ClassData(typeof(UpdateProductNegativeCases))]
        public async Task UpdateProduct_InvalidInput_ReturnsError(UpdateProductTestCase testCase)
        {
            // Arrange:
            _output.WriteLine(testCase.TestId);
            _output.WriteLine(testCase.Description);
            var httpClient = _factory.CreateClient();
            var apiClient = new ProductsApiClient(httpClient);

            // Act:
            var exception = await Assert.ThrowsAsync<ApiException>(() =>
                apiClient.UpdateAsync(testCase.Parameters.UpdateRequest, CancellationToken.None));

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
