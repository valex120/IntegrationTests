using Marketplace.Tests.TestCaseEntities;

namespace Marketplace.Tests.List.TestCaseEntities;

/// <summary>
///     Тест-кейс на поиск товаров
/// </summary>
public class ListProductTestCase : TestCaseBase
{
    /// <summary>
    ///     Состояние хранилища
    /// </summary>
    public ProductsStorageState StorageState { get; set; } = default!;

    /// <summary>
    ///     Данные для настройки моков
    /// </summary>
    public MocksData MocksData { get; set; } = default!;

    /// <summary>
    ///     Входные параметры
    /// </summary>
    public ListProductTestCaseParameters Parameters { get; set; } = default!;

    /// <summary>
    ///     Ожидаемые значения
    /// </summary>
    public ListProductTestCaseExpectations Expectations { get; set; } = default!;
}