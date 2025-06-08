using System.Collections;
using System.Collections.Generic;
using Marketplace.Tests.Create.TestCaseEntities;

namespace Marketplace.Tests.Create.TestCases
{
    public class CreateProductPositiveCases : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator() => GetTestCases().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public static IEnumerable<object[]> GetTestCases()
        {

            yield return new object[] { Create_Product_Returns201Created_AddsProductToDb.Get() };
        }
    }
}