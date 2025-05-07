using FluentAssertions;
using Marketplace.Api;
using Marketplace.Client;
using Marketplace.Client.Exceptions;
using Marketplace.Tests.TestCaseEntities;
using Marketplace.Tests.Update.TestCaseEntities;
using Marketplace.Tests.Update.TestCases;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace Marketplace.Tests.Update;

/// <summary>
///     Тесты на удаление товаров
/// </summary>
public class UpdateProductTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly ITestOutputHelper _outputHelper;
    private readonly WebApplicationFactory<Program> _applicationFactory;
    private readonly Mock<TimeProvider> _timeProviderMock;

    public UpdateProductTests(ITestOutputHelper outputHelper, WebApplicationFactory<Program> applicationFactory)
    {
        _outputHelper = outputHelper;
        _applicationFactory = applicationFactory;
        _timeProviderMock = new Mock<TimeProvider>();
    }

    [Fact]
    public async Task Update_ValidParameters_UpdatesProduct()
    {
        var testCase = TestCases.Update_ValidParameters_UpdatesProduct.Get();
        _outputHelper.WriteLine(testCase.TestId);
        _outputHelper.WriteLine(testCase.Description);

        // ARRANGE 
        ArrangeMocks(testCase.MocksData);
        var productsApiClient = CreateClient();
        var product = await productsApiClient.CreateAsync(testCase.ExistingProduct, CancellationToken.None);
        testCase.Parameters.UpdateRequest.Id = product.Id;

        // ACT
        await productsApiClient.UpdateAsync(testCase.Parameters.UpdateRequest, CancellationToken.None);

        // ASSERT
        var actualProduct = await productsApiClient.GetAsync(testCase.Parameters.UpdateRequest.Id, CancellationToken.None);
        actualProduct.Should().BeEquivalentTo(testCase.Expectations.Product, options => options.Excluding(dto => dto.Id));
    }

    [Theory]
    [ClassData(typeof(UpdateProductNegativeCases))]
    public async Task Update_InvalidParameters_ThrowsAsync(UpdateProductTestCase testCase)
    {
        _outputHelper.WriteLine(testCase.TestId);
        _outputHelper.WriteLine(testCase.Description);

        // ARRANGE 
        var httpClient = _applicationFactory.CreateClient();
        var productsApiClient = new ProductsApiClient(httpClient);

        // ACT
        var error = await productsApiClient.Invoking(c => c.UpdateAsync(testCase.Parameters.UpdateRequest, CancellationToken.None))
                                           .Should()
                                           .ThrowAsync<ApiException>();

        // ASSERT
        error.Which.Code.Should().Be(testCase.Expectations.HttpStatusCode);
        error.Which.Message.Should().Be(testCase.Expectations.Error);
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