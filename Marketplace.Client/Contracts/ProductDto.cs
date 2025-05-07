namespace Marketplace.Client.Contracts;

/// <summary>
///     Данные товара
/// </summary>
public class ProductDto
{
    /// <summary>
    ///     Идентификатор
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Наименование
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    ///     Артикул
    /// </summary>
    public string Article { get; set; } = default!;

    /// <summary>
    ///     Базовая цена 
    /// </summary>
    public int Price { get; set; }

    /// <summary>
    ///     Категория
    /// </summary>
    public string Category { get; set; } = default!;

    /// <summary>
    ///     Дата заведения
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    ///     Дата изменения
    /// </summary>
    public DateTimeOffset UpdatedAt { get; set; }
}