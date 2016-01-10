namespace monitoring.Domain.Models.Services
{
    using System;
    using Attributes;
    using Extensions.Enum.Attribute;

    public static class ServiceTypeEnumExtensions
    {
        public static ServiceTypeEnum GetServiceTypeEnum(this string strValue)
        {
            foreach (ServiceTypeEnum item in Enum.GetValues(typeof(ServiceTypeEnum)))
            {
                if (item.GetCustomEnumAttributeValue<StringValueAttribute, string>() == strValue)
                    return item;

                if (item.ToString() == strValue)
                    return item;
            }

            return ServiceTypeEnum.Undefined;
        }

        public static string GetServiceTypeEnumStringValue(this ServiceTypeEnum enumValue)
        {
            return enumValue.GetCustomEnumAttributeValue<StringValueAttribute, string>();
        }
    }
}
