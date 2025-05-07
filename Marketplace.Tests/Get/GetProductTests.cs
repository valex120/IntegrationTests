using FluentAssertions;
using Marketplace.Api;
using Marketplace.Client;
using Marketplace.Client.Exceptions;
using Marketplace.Tests.TestCaseEntities;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace Marketplace.Tests.Get;

/// <summary>
///     Тесты на получение товаров
/// </summary>
public class GetProductTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly ITestOutputHelper _outputHelper;
    private readonly WebApplicationFactory<Program> _applicationFactory;
    private readonly Mock<TimeProvider> _timeProviderMock;

    public GetProductTests(ITestOutputHelper outputHelper, WebApplicationFactory<Program> applicationFactory)
    {
        _outputHelper = outputHelper;
        _applicationFactory = applicationFactory;
        _timeProviderMock = new Mock<TimeProvider>();
    }

    [Fact]
    public async Task Get_ExistingProduct_ReturnsProduct()
    {
        var testCase = TestCases.Get_ExistingProduct_ReturnsProduct.Get();
        _outputHelper.WriteLine(testCase.TestId);
        _outputHelper.WriteLine(testCase.Description);

        // ARRANGE 
        ArrangeMocks(testCase.MocksData);
        var productsApiClient = CreateClient();
        var product = await productsApiClient.CreateAsync(testCase.ExistingProduct, CancellationToken.None);

        // ACT
        var actualProduct = await productsApiClient.GetAsync(product.Id, CancellationToken.None);

        // ASSERT
        actualProduct.Should().BeEquivalentTo(testCase.Expectations.Product, options => options.Excluding(dto => dto.Id));
    }

    [Fact]
    public async Task Get_NonExistingProduct_Returns404NotFound()
    {
        var testCase = TestCases.Get_NonExistingProduct_Returns404NotFound.Get();
        _outputHelper.WriteLine(testCase.TestId);
        _outputHelper.WriteLine(testCase.Description);

        // ARRANGE 
        var productsApiClient = CreateClient();

        // ACT
        var product = await productsApiClient.GetAsync(testCase.Parameters.ProductId, CancellationToken.None);

        // ASSERT
        product.Should().BeNull();
    }

    /// <summary>
    ///     Настраивает состояние БД перед прогоном тестов
    /// </summary>
    private async Task ArrangeStorageState(ProductsStorageState storageState, ProductsApiClient productsApiClient)
    {
        foreach (var product in storageState.Products)
        {
            await productsApiClient.CreateAsync(product, CancellationToken.None);
        }
    }

    /// <summary>
    ///     Поднимает контект приложения, возвращает сконфигурированный клиент
    /// </summary>
    private ProductsApiClient CreateClient()
    {
        var httpClient = _applicationFactory.WithWebHostBuilder(b => b.ConfigureTestServices(s => s.AddSingleton(_timeProviderMock.Object)))
                                  .CreateClient();
        return new ProductsApiClient(httpClient);
    }

    /// <summary>
    ///     Настраивает моки
    /// </summary>
    private void ArrangeMocks(MocksData mocksData)
    {
        _timeProviderMock.Setup(provider => provider.GetUtcNow()).Returns(mocksData.UtcNow);
    }
}