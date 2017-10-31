using NUnit.Framework;
using System;

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
            Assert.Throws<ArgumentNullException>(() => new Some<Foo>(null));
        }
        [Test]
        public void Test_Constructor()
        {
            var option = new Some<int>(VALUE);

            Assert.AreEqual(VALUE, option.Value);
        }

        [Test]
        public void Test_Cast_ToInvalidType()
        {
            Assert.Throws<InvalidCastException>(
                () => new Some<Foo>(new Foo()).Cast<Bar>()
            );
        }
        [Test]
        public void Test_Cast_Up()
        {
            var source = new Some<FooDerived>(new FooDerived());
            var target = source.Cast<Foo>();

            Assert.IsTrue(target.Contains(source.Value));
        }
        [Test]
        public void Test_Cast_Down()
        {
            var source = new Some<Foo>(new FooDerived());
            var target = source.Cast<FooDerived>();

            Assert.IsTrue(target.Contains((FooDerived)source.Value));
        }

        [Test]
        public void Test_Contains()
        {
            var option = new Some<int>(VALUE);

            Assert.IsFalse(option.Contains(VALUE + 1));
            Assert.IsTrue(option.Contains(VALUE));
        }

        [Test]
        public void Test_Equals()
        {
            var original = new Some<int>(VALUE);
            var good_copy = new Some<int>(original.Value);
            var flawed_value_copy = new Some<int>(original.Value + 1);

            Assert.AreNotEqual(original, null);
            Assert.AreNotEqual(original, "incompatible type");
            Assert.AreNotEqual(original, flawed_value_copy);

            Assert.AreEqual(original, original);
            Assert.AreEqual(original, good_copy);
        }
    }
}
