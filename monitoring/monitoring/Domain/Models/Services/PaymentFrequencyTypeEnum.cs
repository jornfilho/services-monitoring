namespace monitoring.Domain.Models.Services
{
    using Attributes;

    public enum PaymentFrequencyTypeEnum
    {
        [StringValue("")]
        Undefined,

        [StringValue("year")]
        Year,

        [StringValue("three-months")]
        TriMonths,

        [StringValue("month")]
        Month
    }
}