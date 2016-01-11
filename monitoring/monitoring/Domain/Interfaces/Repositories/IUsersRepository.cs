namespace monitoring.Domain.Interfaces.Repositories
{
    using System.Collections.Generic;
    using Models.Users;

    public interface IUsersRepository
    {
        IList<User> GetUsers(UserFilter filterData, int take, int skip);

        long GetUsersCount(UserFilter filterData);

        User Save(User user);

        void Delete(UserFilter filterData);

        
    }
}