namespace monitoring.Domain.Models.Services
{
    using Attributes;

    public enum ServiceTypeEnum
    {
        [StringValue("")]
        Undefined,

        [StringValue("netflix")]
        Netflix
    }
}