namespace Marketplace.Tests.Create.TestCaseEntities;

/// <summary>
///     Входные параметры для тест кейса на удаление товара
/// </summary>
public class DeleteProductTestCaseParameters
{
    /// <summary>
    ///     Идентификатор товара
    /// </summary>
    public Guid ProductId { get; set; } = default!;
}