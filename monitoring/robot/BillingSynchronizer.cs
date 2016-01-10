namespace robot
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using monitoring.Domain.Models.Services;
    using monitoring.Domain.Models.User;

    public partial class BillingSynchronizer : Form
    {
        public BillingSynchronizer()
        {
            this.InitializeComponent();
            this.ManagerSynchronizer();
        }

        private void ManagerSynchronizer()
        {
            var users = new Users().GetUsersAll(new UserFilter(), null);
            this.ProcessUsers(users);
        }

        private void ProcessUsers(IList<User> users)
        {
            if (users == null || !users.Any())
                return;

            foreach (var user in users)
                this.ProcessUser(user);
        }

        private void ProcessUser(User user)
        {
            if (user == null)
                return;

            if(user.ServicesList == null || !user.ServicesList.Any())
                return;

            foreach (var service in user.ServicesList)
                this.ProcessService(service);
        }

        private void ProcessService(Service service)
        {
            if(service == null)
                return;

            switch (service.Type)
            {
                case ServiceTypeEnum.Netflix:
                    this.Netflix();
                    break;
                default:
                    break;
            }
        }

        public void Netflix()
        {
            
        }
    }
}
