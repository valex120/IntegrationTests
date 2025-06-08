using System.Collections;
using System.Collections.Generic;
using Marketplace.Tests.Get.TestCaseEntities;

namespace Marketplace.Tests.Get.TestCases
{
    public class GetProductNegativeCases : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { Get_NonExistingProduct_Returns404NotFound.Get() };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}