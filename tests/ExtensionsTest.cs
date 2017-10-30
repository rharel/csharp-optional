using Moq;
using NUnit.Framework;
using System;

namespace rharel.Functional.Optional.Tests
{
    [TestFixture]
    public sealed class ExtensionsTest
    {
        private static readonly int VALUE = 1;

        private static readonly Optional<int> NONE = new None<int>();
        private static readonly Optional<int> SOME = new Some<int>(VALUE);

        [Test]
        public void Test_ForSome_WithNullAction()
        {
            var option = new Mock<Optional<int>>().Object;

            Assert.Throws<ArgumentNullException>(() => option.ForSome(null));
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
            var option = new Mock<Optional<int>>().Object;

            Assert.Throws<ArgumentNullException>(
                () => option.ForSomeOrElse(null, () => { })
            );
            Assert.Throws<ArgumentNullException>(
                () => option.ForSomeOrElse(_ => { }, null)
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
            var option = new Mock<Optional<int>>().Object;

            Assert.Throws<ArgumentNullException>(
                () => option.MapSomeOr(null, 0)
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
            var option = new Mock<Optional<int>>().Object;

            Assert.Throws<ArgumentNullException>(
                () => option.MapSomeOrElse(null, () => 0)
            );
            Assert.Throws<ArgumentNullException>(
                () => option.MapSomeOrElse(_ => 0, null)
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
