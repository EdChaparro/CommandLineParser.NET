using System;

namespace IntrepidProducts.CommandLineParser.Tests
{
    public enum TestArgumentEnum
    {
        Enum1 = 1,
        Enum2 = 2,
        Enum3 = 3
    }

    internal class TestTarget : IArgumentTarget
    {
        [CommandLineArgumentProperty("key1", null, "First Key")]
        public string Key1 { get; set; }

        [CommandLineArgumentProperty("key2", null, "Second Key")]
        public string Key2 { get; set; }

        [CommandLineArgumentProperty("key3", null, "Int Key")]
        public int Key3 { get; set; }

        [CommandLineArgumentProperty("key4", null, "Double Key")]
        public double Key4 { get; set; }

        [CommandLineArgumentProperty("key5", null, "Decimal Key")]
        public decimal Key5 { get; set; }

        [CommandLineArgumentProperty("key6", null, "Enum Key")]
        public TestArgumentEnum Key6 { get; set; }

        [CommandLineArgumentProperty("key7", null, "Enum Key")]
        public DateTime Key7 { get; set; }

        public bool Validate()
        {
            return true;
        }
    }
}