using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntrepidProducts.CommandLineParser.Tests
{
    [TestClass]
    public class ArgumentKeyValuesTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldValidateArgumentsStartWithDoubleDash()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new ArgumentKeyValues(new [] {"InvalidKey=SomeValue"});
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldValidateArgumentHasMatchingDoubleQuotes()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new ArgumentKeyValues(new [] { "--ValidKey=\"MissingClosingQuote" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldValidateArgumentHasMatchingSingleQuotes()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new ArgumentKeyValues(new [] { "--ValidKey=MissingOpeningQuote'" });
        }

        [TestMethod]
        public void ShouldParseSingularArgument()
        {
            var argKv = new ArgumentKeyValues(new [] { "--Alpha=One" });
            Assert.AreEqual("One", argKv["Alpha"]);
        }

        [TestMethod]
        public void ShouldParseMultipleArguments()
        {
            var args = new []
            {
                "--key1=value1",
                "--key2=value2"
            };

            var argKv = new ArgumentKeyValues(args);
            Assert.AreEqual("value1", argKv["key1"]);
            Assert.AreEqual("value2", argKv["key2"]);
        }

        [TestMethod]
        public void ShouldNotIncludeValueQuotes()
        {
            var argKv = new ArgumentKeyValues(new [] { "--Key1=\"One\"" });
            Assert.AreEqual("One", argKv["Key1"]);

            argKv = new ArgumentKeyValues(new [] { "--Key1='Two'" });
            Assert.AreEqual("Two", argKv["Key1"]);
        }

        [TestMethod]
        public void ShouldSupportValuesWithEmbeddedSpaces()
        {
            var argKv = new ArgumentKeyValues(new [] { "--Key1=\"One Two Three\"" });
            Assert.AreEqual("One Two Three", argKv["Key1"]);
        }

        [TestMethod]
        public void ShouldIgnoreKeyCase()
        {
            var argKv = new ArgumentKeyValues(new [] { "--Key1=One" });
            Assert.AreEqual("One", argKv["Key1"]);
            Assert.AreEqual("One", argKv["KEY1"]);
            Assert.AreEqual("One", argKv["key1"]);
            Assert.AreEqual("One", argKv["KeY1"]);
        }

        [TestMethod]
        public void ShouldReturnNullWhenKeyIsUnknown()
        {
            var argKv = new ArgumentKeyValues(new [] { "--Key1=One" });
            Assert.AreEqual("One", argKv["Key1"]);
            Assert.IsNull(argKv["Key2"]);
        }
    }
}