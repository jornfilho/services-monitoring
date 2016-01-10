namespace monitoring.Domain.Models.User
{
    using System;
    using System.Collections.Generic;
    using Interfaces.Repositories;
    using Pagination;

    public class Users
    {
        public IList<User> UsersList { get; private set; }
        public Pagination Pagination { get; private set; }
        

        public Users GetUsersPaginated(UserFilter filterData, Pagination pagination, IUserRepository repository)
        {
            ValidateRepositoryInstance(repository);

            if(pagination == null)
                pagination = new Pagination().SetRequestData(int.MaxValue, 0);

            this.Pagination = pagination;

            var usersCount = repository.GetCount(filterData);
            this.Pagination.SetResponseData(0);
            if (usersCount == 0)
            {
                this.Pagination.SetResponseData(0);
                return this;
            }

            var users = repository.Get(filterData, pagination.PageSize, this.Pagination.CurrentPage * this.Pagination.PageSize);
            this.UsersList = users;
            
            return this;
        }

        public IList<User> GetUsersAll(UserFilter filterData, IUserRepository repository)
        {
            ValidateRepositoryInstance(repository);

            var users = repository.Get(filterData, int.MaxValue, 0);
            return users;
        }


        private static void ValidateRepositoryInstance(IUserRepository repository)
        {
            if (repository == null)
                throw new ArgumentNullException("repository", "Invalid repository instance");
        }
    }
}
