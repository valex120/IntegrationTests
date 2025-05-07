namespace Marketplace.Client.Contracts;

/// <summary>
///     Запрос на заведение нового товара
/// </summary>
public class CreateProductRequest
{
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
}