using System.Collections;
using System.Collections.Generic;
using Marketplace.Tests.TestCaseEntities;


namespace Marketplace.Tests.Delete.TestCases
{
    public class DeleteProductNegativeCases : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { Delete_NonExistingProduct_Returns404NotFound.Get() };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}