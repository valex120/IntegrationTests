using System.Collections;

namespace Marketplace.Tests.List.TestCases;

/// <summary>
///     Позитивные кейсы на поиск товаров
/// </summary>
public class ListProductsPositiveCases : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new List<object[]>
    {
        new []{ List_ByStartPrice_ReturnsProduct.Get() },
        new []{ List_ByEndPrice_ReturnsProduct.Get() },
        new []{ List_ByCategory_ReturnsProduct.Get() },
        new []{ List_ByNameEntry_ReturnsProduct.Get() }
    };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}