using Marketplace.Api.Mappers;
using Marketplace.Client.Contracts;
using Marketplace.Domain.Errors;
using Marketplace.Domain.Services;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Api.Controllers;

/// <summary>
///     Контроллер товаров
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductsService _productsService;
    private readonly TimeProvider _timeProvider;

    public ProductsController(ProductsService productsService, TimeProvider timeProvider)
    {
        _productsService = productsService;
        _timeProvider = timeProvider;
    }

    /// <summary>
    ///     Возвращает страницу поиска товаров
    /// </summary>
    /// <param name="request">Условия поиска товаров</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    [HttpGet]
    public async Task<ActionResult<ProductDto[]>> List(
        [FromQuery] ListProductsRequest request,
        CancellationToken cancellationToken)
    {
        var (products, error) = await _productsService.ListAsync(
            request.Name,
            request.StartPrice,
            request.EndPrice,
            ProductCategoriesMapper.Map(request.Category),
            cancellationToken);

        if (error is not ListParametersError.None)
            return BadRequest(error.ToString());

        var result = products!.Select(p => p.MapToDto()).ToArray();

        return Ok(result);
    }

    /// <summary>
    ///     Возвращает товар по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор товара</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto?>> Get(
        Guid id,
        CancellationToken cancellationToken)
    {
        var product = await _productsService.GetAsync(id, cancellationToken);
        if (product is null)
            return NotFound("ProductNotFound");

        return Ok(product.MapToDto());
    }

    /// <summary>
    ///     Добавляет новый товар
    /// </summary>
    /// <param name="request">Данные товара</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    [HttpPost]
    public async Task<ActionResult> Create(
        [FromBody] CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        var product = request.MapToEntity(_timeProvider.GetUtcNow());
        var error = await _productsService.CreateAsync(product, cancellationToken);

        return error switch
        {
            CreateProductError.AlreadyExists => Conflict(error.ToString()),
            CreateProductError.None => Created(new Uri($"{Request.GetEncodedUrl()}/{product.Id}"), product.MapToDto()),
            _ => BadRequest(error.ToString())
        };
    }

    /// <summary>
    ///     Изменяет данные товара
    /// </summary>
    /// <param name="request">Данные товара</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    [HttpPatch]
    public async Task<ActionResult> Update(
        [FromBody] UpdateProductRequest request,
        CancellationToken cancellationToken)
    {
        var product = request.MapToEntity();
        var error = await _productsService.UpdateAsync(product, cancellationToken);
        return error switch
        {
            UpdateProductError.ProductNotFound => NotFound(error.ToString()),
            UpdateProductError.None => NoContent(),
            _ => BadRequest(error.ToString())
        };
    }

    /// <summary>
    ///     Удаляет товар
    /// </summary>
    /// <param name="id">Идентификатор товара</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken)
    {
        var error = await _productsService.DeleteAsync(id, cancellationToken);
        if (error is DeleteProductError.ProductNotFound)
            return NotFound(error.ToString());

        return NoContent();
    }
}