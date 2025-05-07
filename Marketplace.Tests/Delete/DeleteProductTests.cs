using FluentAssertions;
using Marketplace.Api;
using Marketplace.Client;
using Marketplace.Client.Exceptions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;

namespace Marketplace.Tests.Delete;

/// <summary>
///     Тесты на удаление товаров
/// </summary>
public class DeleteProductTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly ITestOutputHelper _outputHelper;
    private readonly WebApplicationFactory<Program> _applicationFactory;

    public DeleteProductTests(ITestOutputHelper outputHelper, WebApplicationFactory<Program> applicationFactory)
    {
        _outputHelper = outputHelper;
        _applicationFactory = applicationFactory;
    }

    [Fact]
    public async Task Delete_ExistingProduct_DeletesProduct()
    {
        var testCase = TestCases.Delete_ExistingProduct_DeletesProduct.Get();
        _outputHelper.WriteLine(testCase.TestId);
        _outputHelper.WriteLine(testCase.Description);

        // ARRANGE 
        var httpClient = _applicationFactory.CreateClient();
        var productsApiClient = new ProductsApiClient(httpClient);
        var product = await productsApiClient.CreateAsync(testCase.ExistingProduct, CancellationToken.None);

        // ACT
        await productsApiClient.DeleteAsync(product.Id, CancellationToken.None);

        // ASSERT
        var actualProduct = await productsApiClient.GetAsync(product.Id, CancellationToken.None);
        actualProduct.Should().BeNull();
    }

    [Fact]
    public async Task Delete_NonExistingProduct_Returns404NotFound()
    {
        var testCase = TestCases.Delete_NonExistingProduct_Returns404NotFound.Get();
        _outputHelper.WriteLine(testCase.TestId);
        _outputHelper.WriteLine(testCase.Description);

        // ARRANGE 
        var httpClient = _applicationFactory.CreateClient();
        var productsApiClient = new ProductsApiClient(httpClient);

        // ACT
        var error = await productsApiClient.Invoking(c => c.DeleteAsync(testCase.Parameters.ProductId, CancellationToken.None))
                                           .Should()
                                           .ThrowAsync<ApiException>();

        // ASSERT
        error.Which.Code.Should().Be(testCase.Expectations.HttpStatusCode);
        error.Which.Message.Should().Be(testCase.Expectations.Error);
    }
}