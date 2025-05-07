using Marketplace.Client.Contracts;

namespace Marketplace.Tests.TestCaseEntities;

/// <summary>
///     Состояние хранилища для тестов
/// </summary>
public class ProductsStorageState
{
    /// <summary>
    ///     Запросы на создание товаров для заполнения БД
    /// </summary>
    public CreateProductRequest[] Products { get; set; }
}