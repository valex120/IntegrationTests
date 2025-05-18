using System.Net.Http.Json;
using FluentAssertions;
using Marketplace.Api;
using Marketplace.Client;
using Marketplace.Client.Contracts;
using Marketplace.Client.Exceptions;
using Marketplace.Tests.Create.TestCaseEntities;
using Marketplace.Tests.Create.TestCases;
using Marketplace.Tests.TestCaseEntities; 
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Xunit;
using Xunit.Abstractions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;



namespace Marketplace.Tests.Create
{
    /// <summary>
    /// Тесты на заведение товаров
    /// </summary>
    public class CreateProductTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly ITestOutputHelper _output;
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Mock<TimeProvider> _timeProviderMock;

        public CreateProductTests(ITestOutputHelper output, WebApplicationFactory<Program> factory)
        {
            _output = output;
            _factory = factory;
            _timeProviderMock = new Mock<TimeProvider>();
        }

        [Theory(DisplayName = "CreateProduct_ValidInput_Returns201Created")]
        [MemberData(nameof(GetPositiveCases))]
        public async Task CreateProduct_ValidInput_Returns201Created(CreateProductTestCase testCase)
        {
            // Arrange:
            _output.WriteLine(testCase.TestId);
            _output.WriteLine(testCase.Description);
            ArrangeMocks(testCase.MocksData);
            var client = CreateClient();
            var apiClient = new ProductsApiClient(client);

            // Act:
            var createdProduct = await apiClient.CreateAsync(testCase.Parameters.NewProduct, CancellationToken.None);
            var fetchedProduct = await apiClient.GetAsync(createdProduct.Id, CancellationToken.None);

            // Assert:
            createdProduct.Should().NotBeNull();
            createdProduct.Name.Should().Be(testCase.Expectations.Product.Name);
            createdProduct.Price.Should().Be(testCase.Expectations.Product.Price);
            createdProduct.Article.Should().Be(testCase.Expectations.Product.Article);
            createdProduct.Category.Should().Be(testCase.Expectations.Product.Category);
            fetchedProduct.Should().BeEquivalentTo(testCase.Expectations.Product, options => options.Excluding(dto => dto.Id));
        }

        [Theory(DisplayName = "CreateProduct_InvalidInput_ReturnsError")]
        [ClassData(typeof(CreateProductNegativeCases))]
        public async Task CreateProduct_InvalidInput_ReturnsError(CreateProductTestCase testCase)
        {
            // Arrange:
            _output.WriteLine(testCase.TestId);
            _output.WriteLine(testCase.Description);
            ArrangeMocks(testCase.MocksData);
            var client = CreateClient();
            var apiClient = new ProductsApiClient(client);
            if (testCase.ExistingProduct != null)
            {
                await apiClient.CreateAsync(testCase.ExistingProduct, CancellationToken.None);
            }

            // Act:
            var exception = await Assert.ThrowsAsync<ApiException>(() =>
                apiClient.CreateAsync(testCase.Parameters.NewProduct, CancellationToken.None));

            // Assert:
            exception.Code.Should().Be(testCase.Expectations.HttpStatusCode);
            exception.Message.Should().Contain(testCase.Expectations.Error);
        }

        private HttpClient CreateClient()
        {
            return _factory.WithWebHostBuilder(b =>
            {
                b.ConfigureTestServices(services =>
                {
                    services.AddSingleton(_timeProviderMock.Object);
                });
            }).CreateClient();
        }

        private void ArrangeMocks(MocksData mocksData)
        {
            _timeProviderMock.Setup(tp => tp.GetUtcNow()).Returns(mocksData.UtcNow);
        }

        public static IEnumerable<object[]> GetPositiveCases => CreateProductPositiveCases.GetTestCases();
    }
}
