using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Advisor;
using NUnit.Framework;

namespace Advisor.Tests.DataTests
{
    [TestFixture]
    public class CategoryDataTests
    {
        [Test]
        public void GetCategories_Test()
        {
            var categories = CategoryData.GetCategories();

            Assert.IsTrue(categories.Any());
        }

    }
}
