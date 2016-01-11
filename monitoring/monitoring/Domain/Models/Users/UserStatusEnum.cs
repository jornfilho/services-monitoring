namespace monitoring.Domain.Models.Users
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