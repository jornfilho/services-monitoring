namespace monitoring.Domain.Models.User
{
    using Attributes;

    public enum UserStatusEnum
    {
        [StringValue("")]
        Undefined,

        [StringValue("active")]
        Active,

        [StringValue("inactive")]
        Inactive
    }
}