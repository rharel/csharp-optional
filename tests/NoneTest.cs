using NUnit.Framework;
using static rharel.Functional.Option;

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
            Assert.DoesNotThrow(() => None<int>().Cast<float>()); 
        }
        [Test]
        public void Test_Contains()
        {
            Assert.IsFalse(None<int>().Contains(1));
        }
        [Test]
        public void Test_Equals()
        {
            var original = None<int>();
            var good_copy = None<int>();
            var other_type_copy = None<string>();

            Assert.AreNotEqual(original, null);
            Assert.AreNotEqual(original, "incompatible type");

            Assert.AreEqual(original, original);
            Assert.AreEqual(original, good_copy);
            Assert.AreEqual(original, other_type_copy);
        }
        [Test]
        public void Test_Equals_WhenCastUp()
        {
            var source = None<FooDerived>();
            var target = source.Cast<Foo>();

            Assert.AreEqual(source, target);
        }
        [Test]
        public void Test_Equals_WhenCastDown()
        {
            var source = None<Foo>();
            var target = source.Cast<FooDerived>();

            Assert.AreEqual(source, target);
        }
    }
}
