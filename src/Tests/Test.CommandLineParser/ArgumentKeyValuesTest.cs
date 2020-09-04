using System;
using CommandLineParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.CommandLineParser
{
    [TestClass]
    public class ArgumentKeyValuesTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldValidateArgumentsStartWithDoubleDash()
        {
            var argKv = new ArgumentKeyValues(new string[] {"InvalidKey=SomeValue"});
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldValidateArgumentHasMatchingDoubleQuotes()
        {
            var argKv = new ArgumentKeyValues(new string[] { "--ValidKey=\"SomeValue" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldValidateArgumentHasMatchingSingleQuotes()
        {
            var argKv = new ArgumentKeyValues(new string[] { "--ValidKey=SomeValue'" });
        }

        [TestMethod]
        public void ShouldParseSingularArgument()
        {
            var argKv = new ArgumentKeyValues(new string[] { "--Alpha=One" });
            Assert.AreEqual("One", argKv["Alpha"]);
        }

        [TestMethod]
        public void ShouldParseMultipleArguments()
        {
            var args = new string[]
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
            var argKv = new ArgumentKeyValues(new string[] { "--Key1=\"One\"" });
            Assert.AreEqual("One", argKv["Key1"]);

            argKv = new ArgumentKeyValues(new string[] { "--Key1='Two'" });
            Assert.AreEqual("Two", argKv["Key1"]);
        }

        [TestMethod]
        public void ShouldSupportValuesWithEmbeddedSpaces()
        {
            var argKv = new ArgumentKeyValues(new string[] { "--Key1=\"One Two Three\"" });
            Assert.AreEqual("One Two Three", argKv["Key1"]);
        }

        [TestMethod]
        public void ShouldIgnoreKeyCase()
        {
            var argKv = new ArgumentKeyValues(new string[] { "--Key1=One" });
            Assert.AreEqual("One", argKv["Key1"]);
            Assert.AreEqual("One", argKv["KEY1"]);
            Assert.AreEqual("One", argKv["key1"]);
            Assert.AreEqual("One", argKv["KeY1"]);
        }

        [TestMethod]
        public void ShouldReturnNullWhenKeyIsUnknown()
        {
            var argKv = new ArgumentKeyValues(new string[] { "--Key1=One" });
            Assert.AreEqual("One", argKv["Key1"]);
            Assert.IsNull(argKv["Key2"]);
        }
    }
}
