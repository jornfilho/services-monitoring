namespace robot.Models.Users
{
    using monitoring.Domain.Models.Services;

    public class UserServices
    {
        public string Id { get; private set; }
        public Service Service { get; private set; }

        public UserServices(string id, Service service)
        {
            this.Id = id;
            this.Service = service;
        }
    }
}
