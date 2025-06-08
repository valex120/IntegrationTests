using FluentAssertions;
using Marketplace.Domain.Entities;
using Marketplace.Domain.Errors;
using Marketplace.Domain.Services;
using Moq;
using Xunit;

namespace Marketplace.Tests.Domain
{
    /// <summary>
    /// Юнит-тесты для метода CreateAsync сервиса товаров.
    /// Тесты покрывают все ветви валидации в методе.
    /// </summary>
    public class ProductServiceCreateTests
    {
        private readonly Mock<IProductsRepository> _repositoryMock;
        private readonly TimeProvider _fixedTime;
        private readonly ProductsService _productService;

        public ProductServiceCreateTests()
        {
            _repositoryMock = new Mock<IProductsRepository>();
            _fixedTime = TimeProvider.System;
            _productService = new ProductsService(_repositoryMock.Object, _fixedTime);
        }

        [Fact(DisplayName = "CreateAsync_EmptyName_ReturnsEmptyName")]
        public async Task CreateAsync_EmptyName_ReturnsEmptyName()
        {
            // Arrange:
            var product = new Product
            {
                Name = "",
                Article = "ABCDEFGHIJ",
                Price = 100,
                Category = ProductCategory.Children
            };

            // Act:
            var error = await _productService.CreateAsync(product, CancellationToken.None);

            // Assert:
            error.Should().Be(CreateProductError.EmptyName);
        }

        [Fact(DisplayName = "CreateAsync_NameTooLong_ReturnsNameTooLong")]
        public async Task CreateAsync_NameTooLong_ReturnsNameTooLong()
        {
            // Arrange:
            var longName = new string('A', 1001);
            var product = new Product
            {
                Name = longName,
                Article = "ABCDEFGHIJ",
                Price = 100,
                Category = ProductCategory.Children
            };

            // Act:
            var error = await _productService.CreateAsync(product, CancellationToken.None);

            // Assert:
            error.Should().Be(CreateProductError.NameTooLong);
        }

        [Fact(DisplayName = "CreateAsync_ArticleTooLong_ReturnsArticleTooLong")]
        public async Task CreateAsync_ArticleTooLong_ReturnsArticleTooLong()
        {
            // Arrange:
            string longArticle = new string('B', 256);
            var product = new Product
            {
                Name = "Valid Name",
                Article = longArticle,
                Price = 100,
                Category = ProductCategory.Children
            };

            // Act:
            var error = await _productService.CreateAsync(product, CancellationToken.None);

            // Assert:
            error.Should().Be(CreateProductError.ArticleTooLong);
        }

        [Fact(DisplayName = "CreateAsync_IncorrectArticle_ReturnsIncorrectArticle")]
        public async Task CreateAsync_IncorrectArticle_ReturnsIncorrectArticle()
        {
            // Arrange:
            var product = new Product
            {
                Name = "Valid Name",
                Article = "ABC", // неверный формат
                Price = 100,
                Category = ProductCategory.Children
            };

            // Act:
            var error = await _productService.CreateAsync(product, CancellationToken.None);

            // Assert:
            error.Should().Be(CreateProductError.IncorrectArticle);
        }

        [Fact(DisplayName = "CreateAsync_UnknownCategory_ReturnsUnknownCategory")]
        public async Task CreateAsync_UnknownCategory_ReturnsUnknownCategory()
        {
            // Arrange:
            var product = new Product
            {
                Name = "Valid Name",
                Article = "ABCDEFGHIJ",
                Price = 100,
                Category = ProductCategory.Unknown
            };

            // Act:
            var error = await _productService.CreateAsync(product, CancellationToken.None);

            // Assert:
            error.Should().Be(CreateProductError.UnknownCategory);
        }

        [Fact(DisplayName = "CreateAsync_ZeroPrice_ReturnsZeroPrice")]
        public async Task CreateAsync_ZeroPrice_ReturnsZeroPrice()
        {
            // Arrange:
            var product = new Product
            {
                Name = "Valid Name",
                Article = "ABCDEFGHIJ",
                Price = 0,
                Category = ProductCategory.Children
            };

            // Act:
            var error = await _productService.CreateAsync(product, CancellationToken.None);

            // Assert:
            error.Should().Be(CreateProductError.ZeroPrice);
        }

        [Fact(DisplayName = "CreateAsync_NegativePrice_ReturnsNegativePrice")]
        public async Task CreateAsync_NegativePrice_ReturnsNegativePrice()
        {
            // Arrange:
            var product = new Product
            {
                Name = "Valid Name",
                Article = "ABCDEFGHIJ",
                Price = -50,
                Category = ProductCategory.Children
            };

            // Act:
            var error = await _productService.CreateAsync(product, CancellationToken.None);

            // Assert:
            error.Should().Be(CreateProductError.NegativePrice);
        }

        [Fact(DisplayName = "CreateAsync_AlreadyExists_ReturnsAlreadyExists")]
        public async Task CreateAsync_AlreadyExists_ReturnsAlreadyExists()
        {
            // Arrange:
            var article = "ABCDEFGHIJ";
            var product = new Product
            {
                Name = "Valid Name",
                Article = article,
                Price = 100,
                Category = ProductCategory.Children
            };

            _repositoryMock.Setup(r => r.ExistsAsync(article, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(true);

            // Act:
            var error = await _productService.CreateAsync(product, CancellationToken.None);

            // Assert:
            error.Should().Be(CreateProductError.AlreadyExists);
        }

        [Fact(DisplayName = "CreateAsync_ValidInput_ReturnsNone")]
        public async Task CreateAsync_ValidInput_ReturnsNone()
        {
            // Arrange:
            var article = "ABCDEFGHIJ";
            var product = new Product
            {
                Name = "Valid Name",
                Article = article,
                Price = 100,
                Category = ProductCategory.Children,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

            _repositoryMock.Setup(r => r.ExistsAsync(article, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(false);
            _repositoryMock.Setup(r => r.CreateAsync(product, It.IsAny<CancellationToken>()))
                           .Returns(Task.CompletedTask);

            // Act:
            var error = await _productService.CreateAsync(product, CancellationToken.None);

            // Assert:
            error.Should().Be(CreateProductError.None);
        }

        [Fact(DisplayName = "CreateAsync_RepositoryThrowsException_ThrowsException")]
        public async Task CreateAsync_RepositoryThrowsException_ThrowsException()
        {
            // Arrange:
            var article = "ABCDEFGHIJ";
            var product = new Product
            {
                Name = "Valid Name",
                Article = article,
                Price = 100,
                Category = ProductCategory.Children,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

            _repositoryMock.Setup(r => r.ExistsAsync(article, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(false);
            _repositoryMock.Setup(r => r.CreateAsync(product, It.IsAny<CancellationToken>()))
                           .ThrowsAsync(new InvalidOperationException("Test exception"));

            // Act & Assert:
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _productService.CreateAsync(product, CancellationToken.None));
        }
    }
}
