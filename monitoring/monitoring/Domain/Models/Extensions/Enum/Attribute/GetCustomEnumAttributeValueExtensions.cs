namespace monitoring.Domain.Models.Extensions.Enum.Attribute
{
    using System;
    using System.Globalization;
    using Interfaces.Extensions;

    public static class GetCustomEnumAttributeValueExtensions
    {
        public static TR GetCustomEnumAttributeValue<T, TR>(this IConvertible @enum)
        {
            var attributeValue = default(TR);
            try
            {
                if (@enum != null)
                {
                    var fi = @enum.GetType().GetField(@enum.ToString(CultureInfo.InvariantCulture));
                    if (fi != null)
                    {
                        var attributes = fi.GetCustomAttributes(typeof(T), false) as T[];
                        if (attributes != null && attributes.Length > 0)
                        {
                            var attribute = attributes[0] as IAttribute<TR>;
                            if (attribute != null)
                                attributeValue = attribute.Value;
                        }
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return attributeValue;
        }
    }
}
