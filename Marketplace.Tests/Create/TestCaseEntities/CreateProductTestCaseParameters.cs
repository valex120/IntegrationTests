using Marketplace.Client.Contracts;

namespace Marketplace.Tests.Create.TestCaseEntities;

/// <summary>
///     ¬ходные параметры дл€ тест кейса на создание товара
/// </summary>
public class CreateProductTestCaseParameters
{
    /// <summary>
    ///     «апрос на создание товара
    /// </summary>
    public CreateProductRequest NewProduct { get; set; } = default!;
}