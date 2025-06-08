using System.Text.RegularExpressions;
using Marketplace.Domain.Entities;
using Marketplace.Domain.Errors;

namespace Marketplace.Domain.Services
{
    /// <summary>
    ///     Служба товаров
    /// </summary>
    public sealed class ProductsService
    {
        private readonly IProductsRepository _productsRepository;
        private readonly TimeProvider _timeProvider;

        public ProductsService(IProductsRepository productsRepository, TimeProvider timeProvider)
        {
            _productsRepository = productsRepository;
            _timeProvider = timeProvider;
        }

        /// <summary>
        ///     Возвращает товар
        /// </summary>
        /// <param name="id">Идентификатор товара</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        public async Task<Product?> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = await _productsRepository.GetAsync(id, cancellationToken);
            return product;
        }

        /// <summary>
        ///     Возвращает страницу поиска товаров
        /// </summary>
        /// <param name="name">Вхождение в наименование товара</param>
        /// <param name="startPrice">Начальная цена товара</param>
        /// <param name="endPrice">Конечная цена товара</param>
        /// <param name="productCategory">Категория товара</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>
        ///     Возвращает ошибку, если переданы некорректные параметры поиска
        /// </returns>
        public async Task<(Product[]?, ListParametersError)> ListAsync(
            string? name,
            int? startPrice,
            int? endPrice,
            ProductCategory? productCategory,
            CancellationToken cancellationToken)
        {
            // Arrange
            if (startPrice == 0)
                return (null, ListParametersError.ZeroStartPrice);
            if (startPrice < 0)
                return (null, ListParametersError.NegativeStartPrice);
            if (endPrice == 0)
                return (null, ListParametersError.ZeroEndPrice);
            if (endPrice < 0)
                return (null, ListParametersError.NegativeEndPrice);
            if (startPrice > endPrice)
                return (null, ListParametersError.StartPriceMoreThenEndPrice);
            if (productCategory is ProductCategory.Unknown)
                return (null, ListParametersError.UnknownCategory);

            // Act
            var products = await _productsRepository.SearchAsync(name, startPrice, endPrice, productCategory, cancellationToken);
            // Assert
            return (products, ListParametersError.None);
        }

        /// <summary>
        ///     Добавляет новый товар
        /// </summary>
        /// <param name="product">Данные товара</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>
        ///     Возвращает ошибку, если переданы некорректные данные товара
        /// </returns>
        public async Task<CreateProductError> CreateAsync(Product product, CancellationToken cancellationToken)
        {
            // Arrange
            if (string.IsNullOrEmpty(product.Name))
                return CreateProductError.EmptyName;
            if (product.Name.Length > 1000)
                return CreateProductError.NameTooLong;
            if (product.Article.Length > 255)
                return CreateProductError.ArticleTooLong;
            if (Regex.IsMatch(product.Article, "^[A-Za-z0-9-]{10,40}$") is false)
                return CreateProductError.IncorrectArticle;
            if (product.Category is ProductCategory.Unknown)
                return CreateProductError.UnknownCategory;
            if (product.Price == 0)
                return CreateProductError.ZeroPrice;
            if (product.Price < 0)
                return CreateProductError.NegativePrice;
            if (await _productsRepository.ExistsAsync(product.Article, cancellationToken))
                return CreateProductError.AlreadyExists;

            // Act
            await _productsRepository.CreateAsync(product, cancellationToken);
            // Assert
            return CreateProductError.None;
        }

        /// <summary>
        ///     Изменяет данные товара
        /// </summary>
        /// <param name="product">Данные товара</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>
        ///     Возвращает ошибку, если переданы некорректные данные товара
        /// </returns>
        public async Task<UpdateProductError> UpdateAsync(Product product, CancellationToken cancellationToken)
        {
            // Arrange
            if (string.IsNullOrEmpty(product.Name))
                return UpdateProductError.EmptyName;
            if (product.Name.Length > 1000)
                return UpdateProductError.NameTooLong;
            if (product.Price == 0)
                return UpdateProductError.ZeroPrice;
            if (product.Price < 0)
                return UpdateProductError.NegativePrice;
            if (product.Category is ProductCategory.Unknown)
                return UpdateProductError.UnknownCategory;

            var existingProduct = await _productsRepository.GetAsync(product.Id, cancellationToken);
            if (existingProduct is null)
                return UpdateProductError.ProductNotFound;

            // Act
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Category = product.Category;
            // Обновляем время модификации
            existingProduct.UpdatedAt = _timeProvider.GetUtcNow();

            await _productsRepository.UpdateAsync(existingProduct, cancellationToken);
            // Assert
            return UpdateProductError.None;
        }

        /// <summary>
        ///     Удаляет товар
        /// </summary>
        /// <param name="id">Идентификатор товара</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>
        ///     Возвращает ошибку, если товар не был найден
        /// </returns>
        public async Task<DeleteProductError> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            // Arrange
            if (await _productsRepository.GetAsync(id, cancellationToken) is null)
                return DeleteProductError.ProductNotFound;
            // Act
            await _productsRepository.DeleteAsync(id, cancellationToken);
            // Assert
            return DeleteProductError.None;
        }
    }
}
