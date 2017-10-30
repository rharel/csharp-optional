using NUnit.Framework;

namespace rharel.Functional.Tests
{
    [TestFixture]
    public sealed class NoneTest
    {
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
            var flawed_type_copy = new None<string>();

            Assert.AreNotEqual(original, null);
            Assert.AreNotEqual(original, "incompatible type");
            Assert.AreNotEqual(original, flawed_type_copy);

            Assert.AreEqual(original, original);
            Assert.AreEqual(original, good_copy);
        }
    }
}
