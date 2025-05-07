using System.Collections;

namespace Marketplace.Tests.List.TestCases;

/// <summary>
///     Негативные кейсы на поиск товаров
/// </summary>
public class ListProductsNegativeCases : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new List<object[]>
    {
        new []{ List_UnknownCategory_Returns400BadRequest.Get() },
        new []{ List_ZeroStartPrice_Returns400BadRequest.Get() },
        new []{ List_NegativeStartPrice_Returns400BadRequest.Get() },
        new []{ List_ZeroEndPrice_Returns400BadRequest.Get() },
        new []{ List_NegativeEndPrice_Returns400BadRequest.Get() },
        new []{ List_StartPriceMoreThenEndPrice_Returns400BadRequest.Get() }
    };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}