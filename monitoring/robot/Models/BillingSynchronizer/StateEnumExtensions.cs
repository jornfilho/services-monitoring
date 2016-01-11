namespace robot.Models.BillingSynchronizer
{
    using System;
    using monitoring.Domain.Models.Attributes;
    using monitoring.Domain.Models.Extensions.Enum.Attribute;

    public static class StateEnumExtensions
    {
        public static StateEnum GetStateEnum(this string strValue)
        {
            foreach (StateEnum item in Enum.GetValues(typeof(StateEnum)))
            {
                if (item.GetCustomEnumAttributeValue<StringValueAttribute, string>() == strValue)
                    return item;

                if (item.ToString() == strValue)
                    return item;
            }

            return StateEnum.Undefined;
        }

        public static string GetStateEnumStringValue(this StateEnum enumValue)
        {
            return enumValue.GetCustomEnumAttributeValue<StringValueAttribute, string>();
        }
    }
}
