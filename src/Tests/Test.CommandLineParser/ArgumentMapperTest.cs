using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntrepidProducts.CommandLineParser.Tests
{
    [TestClass]
    public class ArgumentMapperTest
    {
        [TestMethod]
        public void ShouldMapArgumentsToTarget()
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
    }
}