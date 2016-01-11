namespace monitoring.Domain.Models.Users
{
    using System;
    using System.Collections.Generic;
    using Interfaces.Repositories;
    using Pagination;

    public class Users
    {
        public IList<User> UsersList { get; private set; }
        public Pagination Pagination { get; private set; }
        

        public Users GetUsersPaginated(UserFilter filterData, Pagination pagination, IUsersRepository repository)
        {
            ValidateRepositoryInstance(repository);

            if(pagination == null)
                pagination = new Pagination().SetRequestData(int.MaxValue, 0);

            this.Pagination = pagination;

            var usersCount = repository.GetUsersCount(filterData);
            this.Pagination.SetResponseData(0);
            if (usersCount == 0)
            {
                this.Pagination.SetResponseData(0);
                return this;
            }

            var users = repository.GetUsers(filterData, pagination.PageSize, this.Pagination.CurrentPage * this.Pagination.PageSize);
            this.UsersList = users;
            
            return this;
        }

        public IList<User> GetUsersAll(UserFilter filterData, IUsersRepository repository)
        {
            ValidateRepositoryInstance(repository);

            var users = repository.GetUsers(filterData, int.MaxValue, 0);
            return users;
        }


        private static void ValidateRepositoryInstance(IUsersRepository repository)
        {
            if (repository == null)
                throw new ArgumentNullException("repository", "Invalid repository instance");
        }
    }
}
