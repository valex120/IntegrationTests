using Marketplace.Client.Contracts;
using Marketplace.Tests.TestCaseEntities;

namespace Marketplace.Tests.Get.TestCaseEntities;

/// <summary>
///     Тест-кейс на получение товара
/// </summary>
public class GetProductTestCase : TestCaseBase
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
    public GetProductTestCaseParameters Parameters { get; set; } = default!;

    /// <summary>
    ///     Ожидаемые значения
    /// </summary>
    public GetProductTestCaseExpectations Expectations { get; set; } = default!;
}