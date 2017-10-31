using NUnit.Framework;

namespace rharel.Functional.Tests
{
    [TestFixture]
    public sealed class NoneTest
    {
        private class Foo { }
        private sealed class FooDerived : Foo { }

        [Test]
        public void Test_Cast()
        {
            Assert.IsInstanceOf(typeof(None<float>), 
                                new None<int>().Cast<float>());
        }
        [Test]
        public void Test_Contains()
        {
            Assert.IsFalse(new None<int>().Contains(1));
        }
        [Test]
        public void Test_Equals()
        {
            var original = new None<int>();
            var good_copy = new None<int>();
            var other_type_copy = new None<string>();

            Assert.AreNotEqual(original, null);
            Assert.AreNotEqual(original, "incompatible type");

            Assert.AreEqual(original, original);
            Assert.AreEqual(original, good_copy);
            Assert.AreEqual(original, other_type_copy);
        }
        [Test]
        public void Test_Equals_WhenCastUp()
        {
            var source = new None<FooDerived>();
            var target = source.Cast<Foo>();

            Assert.AreEqual(source, target);
        }
        [Test]
        public void Test_Equals_WhenCastDown()
        {
            var source = new None<Foo>();
            var target = source.Cast<FooDerived>();

            Assert.AreEqual(source, target);
        }
    }
}
