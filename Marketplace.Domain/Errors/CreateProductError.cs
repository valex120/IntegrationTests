namespace Marketplace.Domain.Errors;

/// <summary>
///     Ошибки заведения товара
/// </summary>
public enum CreateProductError
{
    /// <summary>
    ///     Ошибок нет
    /// </summary>
    None = 0,

    /// <summary>
    ///     Товар с таким артикулом уже существует
    /// </summary>
    AlreadyExists = 1,

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
    ///     Артикул слишком длинный
    /// </summary>
    ArticleTooLong = 7,

    /// <summary>
    ///     Некорректный артикул
    /// </summary>
    IncorrectArticle = 8
}