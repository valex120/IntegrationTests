using Marketplace.Domain.Entities;
using Marketplace.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.DataAccess
{
    /// <summary>
    ///     Репозиторий товаров
    /// </summary>
    public sealed class ProductsRepository : IProductsRepository
    {
        private readonly MarketplaceContext _marketplaceContext;

        public ProductsRepository(MarketplaceContext marketplaceContext)
        {
            _marketplaceContext = marketplaceContext;
        }

        /// <summary>
        ///     Возвращает товар по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор товара</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        public async Task<Product?> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _marketplaceContext.Products.FindAsync(new object[] { id }, cancellationToken);
        }

        /// <summary>
        ///     Возвращает true, если товар с артикулом есть в БД
        /// </summary>
        /// <param name="article">Артикул товара</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        public async Task<bool> ExistsAsync(string article, CancellationToken cancellationToken)
        {
            return await _marketplaceContext.Products.AnyAsync(p => p.Article == article, cancellationToken);
        }

        /// <summary>
        ///     Возвращает страницу поиска товаров
        /// </summary>
        /// <param name="name">Вхождение в наименование товара</param>
        /// <param name="startPrice">Начальная цена товара</param>
        /// <param name="endPrice">Конечная цена товара</param>
        /// <param name="productCategory">Категория товара</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        public async Task<Product[]> SearchAsync(string? name, int? startPrice, int? endPrice, ProductCategory? productCategory,
            CancellationToken cancellationToken)
        {
            var query = _marketplaceContext.Products.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(p => EF.Functions.Like(p.Name, $"%{name}%"));

            if (startPrice is not null)
                query = query.Where(p => p.Price >= startPrice);

            if (endPrice is not null)
                query = query.Where(p => p.Price <= endPrice);

            if (productCategory is not null)
                query = query.Where(p => p.Category == productCategory);

            return await query.ToArrayAsync(cancellationToken);
        }

        /// <summary>
        ///     Добавляет новый товар в БД
        /// </summary>
        /// <param name="product">Данные товара</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        public async Task CreateAsync(Product product, CancellationToken cancellationToken)
        {
            await _marketplaceContext.Products.AddAsync(product, cancellationToken);
            await _marketplaceContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        ///     Обновляет данные товара в БД
        /// </summary>
        /// <param name="product">Данные товара</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        public async Task UpdateAsync(Product product, CancellationToken cancellationToken)
        {
            // Используем Entry у контекста (исправлено)
            _marketplaceContext.Entry(product).State = EntityState.Modified;
            await _marketplaceContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        ///     Удаляет товар из БД
        /// </summary>
        /// <param name="id">Идентификатор товара</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = await _marketplaceContext.Products.FindAsync(new object[] { id }, cancellationToken);
            if (product != null)
            {
                _marketplaceContext.Products.Remove(product);
                await _marketplaceContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
