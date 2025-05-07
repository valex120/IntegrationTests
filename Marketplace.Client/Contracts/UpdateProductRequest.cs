namespace Marketplace.Client.Contracts;

/// <summary>
///     Запрос на изменение данных товара
/// </summary>
public class UpdateProductRequest
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
    ///     Базовая цена 
    /// </summary>
    public int Price { get; set; }

    /// <summary>
    ///     Категория
    /// </summary>
    public string Category { get; set; } = default!;
}