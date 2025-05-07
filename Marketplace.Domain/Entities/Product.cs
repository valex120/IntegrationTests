namespace Marketplace.Domain.Entities
{
    /// <summary>
    ///     Товар
    /// </summary>
    public class Product
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
        public ProductCategory Category { get; set; }

        /// <summary>
        ///     Дата заведения
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        ///     Дата изменения
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
