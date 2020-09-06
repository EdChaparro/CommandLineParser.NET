using System;

namespace IntrepidProducts.CommandLineParser.Tests
{
    public enum TestArgumentEnum
    {
        Enum1 = 1,
        Enum2 = 2,
        Enum3 = 3
    }

    //Altering this class may cause tests to fail
    internal class TestTarget : IArgumentTarget
    {
        [CommandLineArgumentProperty("key1", "Foo", "First Key")]
        public string Key1 { get; set; }

        [CommandLineArgumentProperty("key2", "Bar", "Second Key")]
        public string Key2 { get; set; }

        [CommandLineArgumentProperty("key3", null, "Int Key")]
        public int Key3 { get; set; }

        [CommandLineArgumentProperty("key4", null, "Double Key")]
        public double Key4 { get; set; }

        [CommandLineArgumentProperty("key5", null, "Decimal Key")]
        public decimal Key5 { get; set; }

        [CommandLineArgumentProperty("key6", null, "Enum Key")]
        public TestArgumentEnum Key6 { get; set; }

        [CommandLineArgumentProperty("key7", null, "DateTime Key")]
        public DateTime Key7 { get; set; }

        [CommandLineArgumentProperty("key8", null, "Boolean Key")]
        public bool Key8 { get; set; }

        public bool Validate()
        {
            return true;
        }
    }
}