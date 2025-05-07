using System.Collections;

namespace Marketplace.Tests.Create.TestCases;

/// <summary>
///     Негативные кейсы на добавление товаров
/// </summary>
public class CreateProductNegativeCases : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new List<object[]>
    {
        new []{ Create_ProductWithDuplicatedArticle_Returns409Conflict_DoesNotAddProductToDb.Get() },
        new []{ Create_ProductUnknownCategory_Returns400BadRequest_DoesNotAddProductToDb.Get() }
    };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}