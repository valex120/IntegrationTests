using Marketplace.Client.Contracts;

namespace Marketplace.Tests.Update.TestCaseEntities;

/// <summary>
///     Входные параметры для тест кейса на обновление данных товара
/// </summary>
public class UpdateProductTestCaseParameters
{
    /// <summary>
    ///     Запрос на обновление данных товара
    /// </summary>
    public UpdateProductRequest UpdateRequest { get; set; } = default!;
}