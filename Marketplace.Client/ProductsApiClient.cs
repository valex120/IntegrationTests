using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Encodings.Web;
using Marketplace.Client.Contracts;
using Marketplace.Client.Exceptions;

namespace Marketplace.Client;

/// <summary>
///     Клиент товаров
/// </summary>
public class ProductsApiClient
{
    /// <summary>
    ///     Клиент для отправки HTTP запросов
    /// </summary>
    private readonly HttpClient _httpClient;

    public ProductsApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    ///     Возращает страницу поиска товаров
    /// </summary>
    /// <param name="request">Запрос поиска товаров</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public async Task<ProductDto[]> ListAsync(ListProductsRequest request, CancellationToken cancellationToken)
    {
        var query = new StringBuilder();
        var encoder = UrlEncoder.Default;
        if (string.IsNullOrEmpty(request.Name) is false)
            query.Append($"name={encoder.Encode(request.Name)}&");

        if (string.IsNullOrEmpty(request.Category) is false)
            query.Append($"category={request.Category}&");

        if (request.StartPrice is not null)
            query.Append($"startprice={request.StartPrice}&");

        if (request.EndPrice is not null)
            query.Append($"endprice={request.EndPrice}");

        if (query.Length > 0)
            query.Insert(0, "?");

        var response = await _httpClient.GetAsync($"api/v1/Products{query}", cancellationToken);
        await EnsureSuccess(response, cancellationToken);
        var result = await response.Content.ReadFromJsonAsync<ProductDto[]>(cancellationToken: cancellationToken);
        return result!;
    }

    /// <summary>
    ///     Возращает товар
    /// </summary>
    /// <param name="id">Идентификатор товара</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public async Task<ProductDto?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync($"api/v1/Products/{id}", cancellationToken);
        if (response.StatusCode is HttpStatusCode.NotFound)
            return null;

        await EnsureSuccess(response, cancellationToken);
        var result = await response.Content.ReadFromJsonAsync<ProductDto>(cancellationToken: cancellationToken);
        return result!;
    }

    /// <summary>
    ///     Создаёт новый товар
    /// </summary>
    /// <param name="request">Запрос создания товара</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public async Task<ProductDto> CreateAsync(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsJsonAsync("api/v1/Products", request, cancellationToken);
        await EnsureSuccess(response, cancellationToken);
        var result = await response.Content.ReadFromJsonAsync<ProductDto>(cancellationToken: cancellationToken);
        return result!;
    }

    /// <summary>
    ///     Обновляет данные товара
    /// </summary>
    /// <param name="request">Запрос обновления данных товара</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public async Task UpdateAsync(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PatchAsJsonAsync("api/v1/Products", request, cancellationToken);
        await EnsureSuccess(response, cancellationToken);
    }

    /// <summary>
    ///     Удаляет товар
    /// </summary>
    /// <param name="id">Идентификатор товара</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var response = await _httpClient.DeleteAsync($"api/v1/Products/{id}", cancellationToken);
        await EnsureSuccess(response, cancellationToken);
    }

    /// <summary>
    ///     Проверяет код ответа сервера, выбрасывает исключение в случае неуспешного ответ
    /// </summary>
    private async ValueTask EnsureSuccess(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        if (response.IsSuccessStatusCode)
            return;

        var error = await response.Content.ReadAsStringAsync(cancellationToken);
        throw new ApiException(response.StatusCode, error);
    }
}