namespace Marketplace.Domain.Entities;

/// <summary>
///     Категория товара
/// </summary>
public enum ProductCategory
{
    /// <summary>
    ///     Неизвестная категория
    /// </summary>
    Unknown = 0,

    /// <summary>
    ///     Товары для детей
    /// </summary>
    Children = 1,

    /// <summary>
    ///     Товары для женщин
    /// </summary>
    Women = 2,

    /// <summary>
    ///     Товары для мужчин
    /// </summary>
    Men = 3
}