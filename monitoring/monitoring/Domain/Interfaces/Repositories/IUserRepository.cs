namespace monitoring.Domain.Interfaces.Repositories
{
    using System.Collections.Generic;
    using Models.User;

    public interface IUserRepository
    {
        IList<User> Get(UserFilter filterData, int take, int skip);

        long GetCount(UserFilter filterData);

        User Save(User user);

        void Delete(UserFilter filterData);

        
    }
}