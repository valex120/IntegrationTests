using System.Collections;
using System.Collections.Generic;
using Marketplace.Tests.Get.TestCaseEntities;

namespace Marketplace.Tests.Get.TestCases
{
    public class GetProductPositiveCases : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { Get_ExistingProduct_ReturnsProduct.Get() };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}