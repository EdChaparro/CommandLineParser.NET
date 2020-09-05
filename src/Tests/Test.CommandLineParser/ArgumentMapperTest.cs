using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntrepidProducts.CommandLineParser.Tests
{
    [TestClass]
    public class ArgumentMapperTest
    {
        [TestMethod]
        public void ShouldMapArgumentsToStringProperties()
        {
            var args = new []
            {
                "--key1=value1",
                "--key2=value2"
            };

            var target = ArgumentMapper.Map<TestTarget>(args);

            Assert.AreEqual("value1", target.Key1);
            Assert.AreEqual("value2", target.Key2);
        }

        [TestMethod]
        public void ShouldMapArgumentsToIntegerProperties()
        {
            var args = new[]
            {
                "--key3=99"
            };

            var target = ArgumentMapper.Map<TestTarget>(args);

            Assert.AreEqual(99, target.Key3);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ShouldThrowFormatExceptionOnArgumentTypeMismatch()
        {
            var args = new[]
            {
                "--key3=3.14"   //Decimal value won't map to Integer property
            };

            var target = ArgumentMapper.Map<TestTarget>(args);
        }

        [TestMethod]
        public void ShouldMapArgumentsToDoubleProperties()
        {
            var args = new[]
            {
                "--key4=3.14"
            };

            var target = ArgumentMapper.Map<TestTarget>(args);

            Assert.AreEqual(3.14D, target.Key4);
        }

        [TestMethod]
        public void ShouldMapArgumentsToDecimalProperties()
        {
            var args = new[]
            {
                "--key5=3.14"
            };

            var target = ArgumentMapper.Map<TestTarget>(args);

            Assert.AreEqual(3.14M, target.Key5);
        }

        [TestMethod]
        public void ShouldMapArgumentsToEnumProperties()
        {
            var args = new[]
            {
                "--key6=Enum3"
            };

            var target = ArgumentMapper.Map<TestTarget>(args);

            Assert.AreEqual(TestArgumentEnum.Enum3, target.Key6);
        }
    }
}