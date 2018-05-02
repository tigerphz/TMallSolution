using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TMall.Infrastructure.SearchModel.Test
{
    /// <summary>
    ///这是 SearchModelTest 的测试类，旨在
    ///包含所有 SearchModelTest 单元测试
    ///</summary>
    [TestFixture]
    public class SearchModelTest
    {
        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext { get; set; }
        [Test]
        public void TupleTest()
        {
            var list1 = new List<Tuple<string, string>>
                            {
                                new Tuple<string, string>("1", "2"),
                                new Tuple<string, string>("3", "4"),
                            };
            var list2 = new List<Tuple<string, string>>
                            {
                                new Tuple<string, string>("5", "6"),
                                new Tuple<string, string>("3", "4"),
                            };
            var list3 = list1.Intersect(list2);
            Assert.AreEqual(Tuple.Create("3","4"),list3.FirstOrDefault());
            Assert.AreEqual(1,list3.Count());
        }
    }
}
