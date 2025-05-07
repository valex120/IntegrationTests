namespace Marketplace.Tests.Get.TestCaseEntities;

/// <summary>
///     Входные параметры для тест кейса на получение товара
/// </summary>
public class GetProductTestCaseParameters
{
    /// <summary>
    ///     Идентификатор товара
    /// </summary>
    public Guid ProductId { get; set; } = default!;
}