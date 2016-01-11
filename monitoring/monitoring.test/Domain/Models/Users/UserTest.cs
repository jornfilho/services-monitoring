namespace monitoring.test.Domain.Models.Users
{
    using System.Collections.Generic;
    using monitoring.Domain.Interfaces.Repositories;
    using monitoring.Domain.Models.Services;
    using monitoring.Domain.Models.Users;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Repository.Implementations;

    [TestClass]
    public class UserTest
    {
        private const string Name = "name - user test";
        private const string Email = "email@usertest.com.br";
        private readonly IList<Service> services = new List<Service>
                           {
                               new Service().SetDataToCreate(ServiceTypeEnum.Netflix, "jornfilho", "010203")
                           };
        private readonly IUsersRepository repository = new UsersRepository();

        [TestMethod]
        public void CanCreateNewUser()
        {
            var user = new User()
                .SetDataToCreate(Name, Email, this.services, UserStatusEnum.Active)
                .Save(this.repository);

            Assert.IsFalse(string.IsNullOrEmpty(user.Id));
        }

        [TestMethod]
        public void CanGetAnUser()
        {
            new User()
                .SetDataToCreate(Name, Email, this.services, UserStatusEnum.Active)
                .Save(this.repository);

            var user = new User()
                .Get(new UserFilter().SetEmail(Email), this.repository);

            Assert.IsNotNull(user);
            Assert.IsFalse(string.IsNullOrEmpty(user.Id));
        }

        [TestMethod]
        public void CanUpdateAnUser()
        {
            var user = new User()
                .SetDataToCreate(Name, Email, this.services, UserStatusEnum.Active)
                .Save(this.repository);

            user.SetDataToUpdate(status: UserStatusEnum.Inactive)
                .Save(this.repository);

            Assert.IsFalse(string.IsNullOrEmpty(user.Id));
        }

        [TestCleanup]
        public void DeleteUser()
        {
            this.repository.Delete(new UserFilter().SetEmail(Email));
        }
    }
}
