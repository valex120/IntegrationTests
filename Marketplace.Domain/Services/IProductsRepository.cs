using Marketplace.Domain.Entities;

namespace Marketplace.Domain.Services;

/// <summary>
///     Репозиторий товаров
/// </summary>
public interface IProductsRepository
{
    /// <summary>
    ///     Возвращает товар по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор товара</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task<Product?> GetAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Возвращает true, если товар с артикулом есть в БД
    /// </summary>
    /// <param name="article">Артикул товара</param>
    /// <param name="cancellationToken">Токен отмены операции</param>

    Task<bool> ExistsAsync(string article, CancellationToken cancellationToken);

    /// <summary>
    ///     Возвращает страницу поиска товаров
    /// </summary>
    /// <param name="name">Вхождение в наименование товара</param>
    /// <param name="startPrice">Начальная цена товара</param>
    /// <param name="endPrice">Конечная цена товара</param>
    /// <param name="productCategory">Категория товара</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task<Product[]> SearchAsync(string? name,
        int? startPrice,
        int? endPrice,
        ProductCategory? productCategory,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Добавляет новый товар в БД
    /// </summary>
    /// <param name="product">Данные товара</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task CreateAsync(Product product, CancellationToken cancellationToken);

    /// <summary>
    ///     Обновляет данные товара в БД
    /// </summary>
    /// <param name="product">Данные товара</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task UpdateAsync(Product product, CancellationToken cancellationToken);

    /// <summary>
    ///     Удаляет товар из БД
    /// </summary>
    /// <param name="id">Идентификатор товара</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}