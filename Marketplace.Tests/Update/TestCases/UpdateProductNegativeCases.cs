using System.Collections;

namespace Marketplace.Tests.Update.TestCases;

/// <summary>
///     Негативные кейсы на обновление данных товаров
/// </summary>
public class UpdateProductNegativeCases : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new List<object[]>
    {
        new []{ Update_NonExistingProduct_Returns404NotFound.Get() },
        new []{ Update_EmptyName_Returns400BadRequest.Get() },
        new []{ Update_TooLongName_Returns400BadRequest.Get() },
        new []{ Update_ZeroPrice_Returns400BadRequest.Get() },
        new []{ Update_NegativePrice_Returns400BadRequest.Get() },
        new []{ Update_UnknownCategory_Returns400BadRequest.Get() }
    };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}