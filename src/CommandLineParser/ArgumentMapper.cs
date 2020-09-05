using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace IntrepidProducts.CommandLineParser
{
    public static class ArgumentMapper
    {
        public static T Map<T>(IEnumerable<string> args)
            where T : IArgumentTarget, new()
        {
            var target = new T();

            var argKeyValues = new ArgumentKeyValues(args);

            InjectArgumentValues(argKeyValues.Args, target);

            target.Validate();

            return target;
        }

        private static IReadOnlyDictionary<PropertyInfo, CommandLineArgumentPropertyAttribute>
            GetTargetProperties(IArgumentTarget target)
        {
            var argumentProperties = new Dictionary<PropertyInfo, CommandLineArgumentPropertyAttribute>();

            var propertyInfos = target.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var propertyInfo in propertyInfos)
            {
                var attributes = propertyInfo.GetCustomAttributes(typeof(CommandLineArgumentPropertyAttribute), false);
                if (attributes.Length != 1)
                {
                    continue;
                }

                argumentProperties.Add(propertyInfo, (CommandLineArgumentPropertyAttribute) attributes[0]);
            }

            return argumentProperties;
        }

        private static void InjectArgumentValues(IReadOnlyDictionary<string, string> keyValues, IArgumentTarget target)
        {
            var argumentProperties = GetTargetProperties(target);

            foreach (var propertyAttr in argumentProperties)
            {
                var propertyInfo = propertyAttr.Key;
                var attribute = propertyAttr.Value;

                if (!keyValues.ContainsKey(attribute.Key))
                {
                    continue;
                }

                var argument = keyValues[attribute.Key];

                var value = GetValue(argument, propertyInfo.PropertyType, attribute.DefaultValue);
                propertyInfo.SetValue(target, value);
            }
        }

        private static object GetValue(string argument, Type propertyType, object defaultValue)
        {
            if (argument == null)
                return defaultValue;

            if (propertyType == typeof(string))
                return argument;

            if (propertyType.IsEnum)
                return Enum.Parse(propertyType, argument);

            object result;

            try
            {
                result = ((IConvertible) argument).ToType(propertyType, CultureInfo.CurrentCulture);
            }
            catch (Exception e)
            {
                var message = string.Format("Conversion to {0} is not implemented", propertyType.Name);
                throw new NotImplementedException(message, e);
            }

            return result;
        }
    }
}