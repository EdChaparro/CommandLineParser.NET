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

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowArgumentExceptionWhenEnumValueIsInvalid()
        {
            var args = new[]
            {
                "--key6=Foo"
            };

            var target = ArgumentMapper.Map<TestTarget>(args);
        }

        [TestMethod]
        public void ShouldMapArgumentsToDateTimeProperties()
        {
            var args = new[]
            {
                "--key7=7/4/1776"
            };

            var target = ArgumentMapper.Map<TestTarget>(args);

            Assert.AreEqual(new DateTime(1776, 07, 4), target.Key7);
        }

        [TestMethod]
        public void ShouldMapArgumentsToBooleanProperties()
        {
            var args = new[]
            {
                "--key8=True"
            };

            var target = ArgumentMapper.Map<TestTarget>(args);

            Assert.IsTrue(target.Key8);
        }

        [TestMethod]
        public void ShouldSetDefaultWhenArgumentsNotProvided()
        {
            var args1 = new[] {"--key2=value2"};
            var target1 = ArgumentMapper.Map<TestTarget>(args1);
            Assert.AreEqual("Foo", target1.Key1);

            var args2 = new[] { "--key1=value1" };
            var target2 = ArgumentMapper.Map<TestTarget>(args2);
            Assert.AreEqual("Bar", target2.Key2);
        }
    }
}