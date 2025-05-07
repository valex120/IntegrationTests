using Marketplace.Client.Contracts;
using Marketplace.Tests.TestCaseEntities;

namespace Marketplace.Tests.Update.TestCaseEntities;

/// <summary>
///     Тест-кейс на обновление данных товара
/// </summary>
public class UpdateProductTestCase : TestCaseBase
{
    /// <summary>
    ///     Состояние хранилища
    /// </summary>
    public CreateProductRequest ExistingProduct { get; set; } = default!;

    /// <summary>
    ///     Данные для настройки моков
    /// </summary>
    public MocksData MocksData { get; set; } = default!;

    /// <summary>
    ///     Входные параметры
    /// </summary>
    public UpdateProductTestCaseParameters Parameters { get; set; } = default!;

    /// <summary>
    ///     Ожидаемые значения
    /// </summary>
    public UpdateProductTestCaseExpectations Expectations { get; set; } = default!;
}