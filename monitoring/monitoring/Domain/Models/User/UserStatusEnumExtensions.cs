namespace monitoring.Domain.Models.User
{
    using System;
    using Attributes;
    using Extensions.Enum.Attribute;

    public static class UserStatusEnumExtensions
    {
        public static UserStatusEnum GetUserStatusEnum(this string strValue)
        {
            foreach (UserStatusEnum item in Enum.GetValues(typeof(UserStatusEnum)))
            {
                if (item.GetCustomEnumAttributeValue<StringValueAttribute, string>() == strValue)
                    return item;

                if (item.ToString() == strValue)
                    return item;
            }

            return UserStatusEnum.Undefined;
        }

        public static string GetUserStatusEnumStringValue(this UserStatusEnum enumValue)
        {
            return enumValue.GetCustomEnumAttributeValue<StringValueAttribute, string>();
        }
    }
}
