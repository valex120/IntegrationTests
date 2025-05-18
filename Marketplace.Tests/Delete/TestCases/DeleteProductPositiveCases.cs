using System.Collections;
using System.Collections.Generic;
using Marketplace.Tests.TestCaseEntities;


namespace Marketplace.Tests.Delete.TestCases
{
    public class DeleteProductPositiveCases : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { Delete_ExistingProduct_DeletesProduct.Get() };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}