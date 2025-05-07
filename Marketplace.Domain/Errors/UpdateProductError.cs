namespace Marketplace.Domain.Errors;

/// <summary>
///     Ошибки изменения данных товара
/// </summary>
public enum UpdateProductError
{
    /// <summary>
    ///     Ошибок нет
    /// </summary>
    None = 0,

    /// <summary>
    ///     Нулевая цена товара
    /// </summary>
    ZeroPrice = 2,

    /// <summary>
    ///     Отрицательная цена товара
    /// </summary>
    NegativePrice = 3,

    /// <summary>
    ///     Неизвестная категория товара
    /// </summary>
    UnknownCategory = 4,

    /// <summary>
    ///     Имя товара слишком длинное
    /// </summary>
    EmptyName = 5,

    /// <summary>
    ///     Имя товара слишком длинное
    /// </summary>
    NameTooLong = 6,

    /// <summary>
    ///     Товар не найден
    /// </summary>
    ProductNotFound = 7
}