namespace Marketplace.Domain.Errors;

/// <summary>
///     Ошибки в параметрах поиска товаров
/// </summary>
public enum ListParametersError
{
    /// <summary>
    ///     Ошибок нет
    /// </summary>
    None = 0,

    /// <summary>
    ///     Нулевая начальная цена товара
    /// </summary>
    ZeroStartPrice = 2,

    /// <summary>
    ///     Отрицательная начальная цена товара
    /// </summary>
    NegativeStartPrice = 3,

    /// <summary>
    ///     Нулевая конечная цена товара
    /// </summary>
    ZeroEndPrice = 4,

    /// <summary>
    ///     Отрицательная конечная цена товара
    /// </summary>
    NegativeEndPrice = 5,

    /// <summary>
    ///     Неизвестная категория товара
    /// </summary>
    UnknownCategory = 6,

    /// <summary>
    ///     Начальная цена больше конечной
    /// </summary>
    StartPriceMoreThenEndPrice = 7
}