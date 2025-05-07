using Marketplace.Client.Contracts;
using Marketplace.Tests.TestCaseEntities;

namespace Marketplace.Tests.Create.TestCaseEntities;

/// <summary>
///     Тест-кейс на создание товара
/// </summary>
public class CreateProductTestCase : TestCaseBase
{
    /// <summary>
    ///     Состояние хранилища
    /// </summary>
    public CreateProductRequest? ExistingProduct { get; set; } = default!;

    /// <summary>
    ///     Данные для настройки моков
    /// </summary>
    public MocksData MocksData { get; set; } = default!;

    /// <summary>
    ///     Входные параметры
    /// </summary>
    public CreateProductTestCaseParameters Parameters { get; set; } = default!;

    /// <summary>
    ///     Ожидаемые значения
    /// </summary>
    public CreateProductTestCaseExpectations Expectations { get; set; } = default!;
}