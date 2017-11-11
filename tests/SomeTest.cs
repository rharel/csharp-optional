using NUnit.Framework;
using System;
using static rharel.Functional.Option;

namespace rharel.Functional.Tests
{
    [TestFixture]
    public sealed class SomeTest
    {
        private class Foo { }
        private sealed class FooDerived: Foo { }
        private sealed class Bar { }

        private static readonly int VALUE = 1;

        [Test]
        public void Test_Contructor_WithNull()
        {
            Assert.Throws<ArgumentNullException>(() => Some<Foo>(null));
        }
        [Test]
        public void Test_Constructor()
        {
            var option = Some<int>(VALUE);

            Assert.AreEqual(VALUE, option.Unwrap());
        }

        [Test]
        public void Test_Cast_ToInvalidType()
        {
            Assert.Throws<InvalidCastException>(
                () => Some<Foo>(new Foo()).Cast<Bar>()
            );
        }
        [Test]
        public void Test_Cast_Up()
        {
            var source = Some<FooDerived>(new FooDerived());
            var target = source.Cast<Foo>();

            Assert.IsTrue(target.Contains(source.Unwrap()));
        }
        [Test]
        public void Test_Cast_Down()
        {
            var source = Some<Foo>(new FooDerived());
            var target = source.Cast<FooDerived>();

            Assert.IsTrue(target.Contains((FooDerived)source.Unwrap()));
        }

        [Test]
        public void Test_Contains()
        {
            var option = Some<int>(VALUE);

            Assert.IsFalse(option.Contains(VALUE + 1));
            Assert.IsTrue(option.Contains(VALUE));
        }

        [Test]
        public void Test_Equals()
        {
            var original = Some<int>(VALUE);
            var good_copy = Some<int>(original.Unwrap());
            var flawed_value_copy = Some<int>(original.Unwrap() + 1);

            Assert.AreNotEqual(original, null);
            Assert.AreNotEqual(original, "incompatible type");
            Assert.AreNotEqual(original, flawed_value_copy);

            Assert.AreEqual(original, original);
            Assert.AreEqual(original, good_copy);
        }
        [Test]
        public void Test_Equals_WhenCastUp()
        {
            var source = Some<FooDerived>(new FooDerived());
            var target = source.Cast<Foo>();

            Assert.AreEqual(source, target);
        }
        [Test]
        public void Test_Equals_WhenCastDown()
        {
            var source = Some<Foo>(new FooDerived());
            var target = source.Cast<FooDerived>();

            Assert.AreEqual(source, target);
        }
    }
}
