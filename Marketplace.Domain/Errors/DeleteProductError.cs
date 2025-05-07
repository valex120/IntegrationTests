namespace Marketplace.Domain.Errors;

/// <summary>
///     Ошибки удаления товара
/// </summary>
public enum DeleteProductError
{
    /// <summary>
    ///     Ошибок нет
    /// </summary>
    None = 0,

    /// <summary>
    ///     Товар не найден
    /// </summary>
    ProductNotFound
}