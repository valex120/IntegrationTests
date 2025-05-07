namespace Marketplace.Tests.TestCaseEntities;

/// <summary>
///     Данные для настройки моков для тестов
/// </summary>
public class MocksData
{
    /// <summary>
    ///     Зафиксированная дата-время для мока <see cref="TimeProvider"/>
    /// </summary>
    public DateTimeOffset UtcNow { get; set; }
}