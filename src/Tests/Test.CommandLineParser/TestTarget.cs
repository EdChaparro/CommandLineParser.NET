namespace IntrepidProducts.CommandLineParser.Tests
{
    internal class TestTarget : IArgumentTarget
    {
        [CommandLineArgumentProperty("key1", null, "First Key")]
        public string Key1 { get; set; }

        [CommandLineArgumentProperty("key2", null, "First Key")]
        public string Key2 { get; set; }

        public bool Validate()
        {
            return true;
        }
    }
}