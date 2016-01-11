namespace monitoring.Domain.Models.Services
{
    using System;
    using Attributes;
    using Extensions.Enum.Attribute;

    public static class PaymentFrequencyTypeExtensions
    {
        public static PaymentFrequencyTypeEnum GetPaymentFrequencyTypeEnum(this string strValue)
        {
            foreach (PaymentFrequencyTypeEnum item in Enum.GetValues(typeof(PaymentFrequencyTypeEnum)))
            {
                if (item.GetCustomEnumAttributeValue<StringValueAttribute, string>() == strValue)
                    return item;

                if (item.ToString() == strValue)
                    return item;
            }

            return PaymentFrequencyTypeEnum.Undefined;
        }

        public static string GetPaymentFrequencyTypeStringValue(this PaymentFrequencyTypeEnum enumValue)
        {
            return enumValue.GetCustomEnumAttributeValue<StringValueAttribute, string>();
        }
    }
}
