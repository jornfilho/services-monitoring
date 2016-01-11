namespace robot.Models.BillingSynchronizer
{
    using monitoring.Domain.Models.Attributes;

    public enum StateEnum
    {
        [StringValue("Undefined")]
        Undefined,
        
        [StringValue("Stop")]
        Running,

        [StringValue("Stopping")]
        Stopping,

        [StringValue("Start")]
        Stoppped,

        [StringValue("Starting")]
        Starting
    }
}