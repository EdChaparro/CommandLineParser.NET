using System;
using System.Collections.Generic;

namespace IntrepidProducts.CommandLineParser
{
    public class ArgumentKeyValues
    {
        public ArgumentKeyValues(IEnumerable<string> args)
        {
            Parse(args);
        }

        private readonly Dictionary<string, string> _args =
            new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase);

        private void Parse(IEnumerable<string> args)
        {
            foreach (var arg in args)
            {
                var kvArray = arg.Split('=');

                if (kvArray.Length != 2)
                {
                    throw new ArgumentException($"Unable to parse into key-value, arg: {arg}");
                }

                var kvTuple = Clean(kvArray);

                _args.Add(kvTuple.key, kvTuple.value);
            }
        }

        private (string key, string value) Clean(string[] kvArray)
        {
            var key = CleanKey(kvArray[0]);
            var value = CleanValue(kvArray[1]);

            return (key, value);
        }

        private string CleanKey(string key)
        {
            if (key.StartsWith("--") && (key.Length > 2))
            {
                return key.Substring(2);
            }

            throw new ArgumentException($"Key must begin with -- and contain at least one char, arg: {key}");
        }

        private string CleanValue(string value)
        {
            if (value.StartsWith("\"") && value.EndsWith("\""))
            {
                return value.Trim('"');
            }

            if (value.StartsWith("'") && value.EndsWith("'"))
            {
                return value.Trim('\'');
            }

            if (value.StartsWith("\"") || value.EndsWith("\"") ||
                value.StartsWith("'") || value.EndsWith("'"))
            {
                    throw new ArgumentException($"Missing quote, arg: {value}");
            }

            return value;
        }

        public IReadOnlyDictionary<string, string> Args => _args;

        public string this[string key] => (_args.ContainsKey(key)) ? _args[key] : null;
    }
}