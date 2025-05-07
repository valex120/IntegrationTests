using FluentAssertions;
using Marketplace.Api;
using Marketplace.Client;
using Marketplace.Client.Contracts;
using Marketplace.Client.Exceptions;
using Marketplace.Tests.List.TestCaseEntities;
using Marketplace.Tests.List.TestCases;
using Marketplace.Tests.TestCaseEntities;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace Marketplace.Tests.List;

/// <summary>
///     Тесты на поиск товаров
/// </summary>
public class ListProductTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly ITestOutputHelper _outputHelper;
    private readonly WebApplicationFactory<Program> _applicationFactory;
    private readonly Mock<TimeProvider> _timeProviderMock;

    public ListProductTests(ITestOutputHelper outputHelper, WebApplicationFactory<Program> applicationFactory)
    {
        _outputHelper = outputHelper;
        _applicationFactory = applicationFactory;
        _timeProviderMock = new Mock<TimeProvider>();
    }

    [Theory]
    [ClassData(typeof(ListProductsPositiveCases))]
    public async Task List_ValidParamaters_ReturnsProducts(ListProductTestCase testCase)
    {
        _outputHelper.WriteLine(testCase.TestId);
        _outputHelper.WriteLine(testCase.Description);

        // ARRANGE 
        ArrangeMocks(testCase.MocksData);
        var productsApiClient = CreateClient();
        var products = await ArrangeStorageState(testCase.StorageState, productsApiClient);

        // ACT
        var actualProducts = await productsApiClient.ListAsync(testCase.Parameters.Request, CancellationToken.None);

        // ASSERT
        foreach ( var product in testCase.Expectations.IncludedProducts)
        {
            actualProducts.Should().ContainEquivalentOf(product, options => options.Excluding(dto => dto.Id));
        }

        foreach (var product in testCase.Expectations.ExcludedProducts)
        {
            actualProducts.Should().NotContainEquivalentOf(product, options => options.Excluding(dto => dto.Id));
        }
    }

    [Theory]
    [ClassData(typeof(ListProductsNegativeCases))]
    public async Task List_InvalidParamaters_ThrowsException(ListProductTestCase testCase)
    {
        _outputHelper.WriteLine(testCase.TestId);
        _outputHelper.WriteLine(testCase.Description);

        // ARRANGE 
        var httpClient = _applicationFactory.CreateClient();
        var productsApiClient = new ProductsApiClient(httpClient);

        // ACT
        var error = await productsApiClient.Invoking(c => c.ListAsync(testCase.Parameters.Request, CancellationToken.None))
                                           .Should()
                                           .ThrowAsync<ApiException>();

        // ASSERT
        error.Which.Code.Should().Be(testCase.Expectations.HttpStatusCode);
        error.Which.Message.Should().Be(testCase.Expectations.Error);
    }

    /// <summary>
    ///     Настраивает состояние БД перед прогоном тестов
    /// </summary>
    private async Task<ProductDto[]> ArrangeStorageState(ProductsStorageState storageState, ProductsApiClient productsApiClient)
    {
        var result = new List<ProductDto>();
        foreach (var product in storageState.Products)
        {
            var productDto = await productsApiClient.CreateAsync(product, CancellationToken.None);
            result.Add(productDto);
        }

        return result.ToArray();
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