namespace Tests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using AnyReference;

    public class TestType
    {
        public int F;
        public int P { get; set; }
        public int G => _g;
        public int _g;
        public int[] A { get; } = new int[5];
        public List<int> L { get; } = new List<int>();
    }

    [TestClass]
    public class RandomTests
    {
        [TestMethod]
        public void Field()
        {
            var obj = new TestType();
            var r = Ref.Of(() => obj.F);            

            var rand = new Random();
            for (var i = 0; i < 100; i++)
            {
                var n1 = rand.Next();
                r.Value = n1;
                Assert.AreEqual(n1, obj.F);

                var n2 = rand.Next();
                obj.F = n2;
                Assert.AreEqual(n2, r.Value);
            }
        }

        [TestMethod]
        public void GetSetProperty()
        {
            var obj = new TestType();
            var r = Ref.Of(() => obj.P);

            var rand = new Random();
            for (var i = 0; i < 100; i++)
            {
                var n1 = rand.Next();
                r.Value = n1;
                Assert.AreEqual(n1, obj.P);

                var n2 = rand.Next();
                obj.P = n2;
                Assert.AreEqual(n2, r.Value);
            }
        }

        [TestMethod]
        public void GetProperty()
        {
            var obj = new TestType();
            var r = Ref.Of(() => obj.G);

            var rand = new Random();
            for (var i = 0; i < 100; i++)
            {
                var n2 = rand.Next();
                obj._g = n2;
                Assert.AreEqual(n2, r.Value);
            }
        }

        [TestMethod]
        public void Local()
        {
            var obj = 5;
            var r = Ref.Of(() => obj);

            var rand = new Random();
            for (var i = 0; i < 100; i++)
            {
                var n1 = rand.Next();
                r.Value = n1;
                Assert.AreEqual(n1, obj);
            }
        }

        [TestMethod]
        public void LocalArray()
        {
            var obj = new int[5];
            var r = Ref.Of(() => obj[2]);

            var rand = new Random();
            for (var i = 0; i < 100; i++)
            {
                var n1 = rand.Next();
                r.Value = n1;
                Assert.AreEqual(n1, obj[2]);
            }
        }

        [TestMethod]
        public void MemberArray()
        {
            var obj = new TestType();
            var r = Ref.Of(() => obj.A[2]);

            var rand = new Random();
            for (var i = 0; i < 100; i++)
            {
                var n1 = rand.Next();
                r.Value = n1;
                Assert.AreEqual(n1, obj.A[2]);
            }
        }

        [TestMethod]
        public void List()
        {
            var obj = new List<int> { 0, 0, 0, 0, 0 };
            var r = Ref.Of(() => obj[2]);

            var rand = new Random();
            for (var i = 0; i < 100; i++)
            {
                var n1 = rand.Next();
                r.Value = n1;
                Assert.AreEqual(n1, obj[2]);
            }
        }
    }
}
