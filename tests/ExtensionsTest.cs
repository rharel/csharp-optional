using NUnit.Framework;
using System;
using static rharel.Functional.Option;

namespace rharel.Functional.Tests
{
    [TestFixture]
    public sealed class ExtensionsTest
    {
        private static readonly int VALUE = 1;

        private static readonly Optional<int> NONE = None<int>();
        private static readonly Optional<int> SOME = Some(VALUE);

        [Test]
        public void Test_IsSome()
        {
            Assert.IsFalse(NONE.IsSome);
            Assert.IsTrue(SOME.IsSome);
        }
        [Test]
        public void Test_IsNone()
        {
            Assert.IsTrue(NONE.IsNone);
            Assert.IsFalse(SOME.IsNone);
        }

        [Test]
        public void Test_ForSome_WithNullAction()
        {
            Assert.Throws<ArgumentNullException>(() => SOME.ForSome(null));
        }
        [Test]
        public void Test_ForSome()
        {
            NONE.ForSome(_ => Assert.Fail());
            SOME.ForSome(x =>
            {
                Assert.AreEqual(VALUE, x);
                Assert.Pass();
            });
            Assert.Fail();
        }

        [Test]
        public void Test_ForSomeOrElse_WithNullAction()
        {
            Assert.Throws<ArgumentNullException>(
                () => SOME.ForSomeOrElse(null, () => { })
            );
            Assert.Throws<ArgumentNullException>(
                () => SOME.ForSomeOrElse(_ => { }, null)
            );
        }
        [Test]
        public void Test_ForSomeOrElse()
        {
            NONE.ForSomeOrElse(_ => Assert.Fail(), () => Assert.Pass());
            SOME.ForSomeOrElse(
                x =>
                {
                    Assert.AreEqual(VALUE, x);
                    Assert.Pass();
                },
                () => Assert.Fail()
            );
            Assert.Fail();
        }

        [Test]
        public void Test_MapSomeOr_WithNullExpression()
        {
            Assert.Throws<ArgumentNullException>(
                () => SOME.MapSomeOr(null, 0)
            );
        }
        [Test]
        public void Test_MapSomeOr()
        {
            Assert.AreEqual(0, NONE.MapSomeOr(_ => 1, 0));
            Assert.AreEqual(VALUE, SOME.MapSomeOr(x => x, 0));
        }

        [Test]
        public void Test_MapSomOrElse_WithNullExpression()
        {
            Assert.Throws<ArgumentNullException>(
                () => SOME.MapSomeOrElse(null, () => 0)
            );
            Assert.Throws<ArgumentNullException>(
                () => SOME.MapSomeOrElse(_ => 0, null)
            );
        }
        [Test]
        public void Test_MapSomeOrElse()
        {
            Assert.AreEqual(
                0,
                NONE.MapSomeOrElse(_ => 1, () => 0)
            );
            Assert.AreEqual(
                VALUE,
                SOME.MapSomeOrElse(x => x, () => 0)
            );
        }

        [Test]
        public void Test_Unwrap()
        {
            Assert.Throws<InvalidOperationException>(() => NONE.Unwrap());
            Assert.AreEqual(VALUE, SOME.Unwrap());
        }
        [Test]
        public void Test_UnwrapOr()
        {
            Assert.AreEqual(0, NONE.UnwrapOr(0));
            Assert.AreEqual(VALUE, SOME.UnwrapOr(0));
        }
    }
}
