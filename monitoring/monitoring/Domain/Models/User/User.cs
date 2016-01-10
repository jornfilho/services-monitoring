namespace monitoring.Domain.Models.User
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Repositories;
    using Services;
    using Utils.Validators;

    public class User
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public IList<Service> ServicesList { get; private set; }
        public UserStatusEnum Status { get; private set; }
        public DateTime CreateTime { get; private set; }



        public User SetDataFromDatabase(string id, string name, string email, IList<Service> servicesList, UserStatusEnum status, DateTime createTime)
        {
            this.CleanModel();

            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.ServicesList = servicesList;
            this.Status = status;
            this.CreateTime = createTime;

            return this;
        }

        public User SetDataToCreate(string name, string email, IList<Service> servicesList, UserStatusEnum status)
        {
            this.CleanModel();

            this.Name = name;
            this.Email = email;
            this.ServicesList = servicesList;
            this.Status = status;
            this.CreateTime = DateTime.UtcNow;

            this.ValidateDataToCreate();

            return this;
        }

        public User SetDataToUpdate(string name = null, string email = null, IList<Service> servicesList = null, UserStatusEnum? status = null)
        {
            if (!string.IsNullOrEmpty(name))
                this.Name = name;

            if (!string.IsNullOrEmpty(email))
                this.Email = email;

            if (servicesList != null)
                this.ServicesList = servicesList;

            if (status != null && status.Value != UserStatusEnum.Undefined)
                this.Status = status.Value;

            this.ValidateDataToUpdate();

            return this;
        }



        public User Get(UserFilter filterData, IUserRepository repository)
        {
            ValidateRepositoryInstance(repository);
            this.CleanModel();

            var users = repository.Get(filterData, 1, 0);
            if (users == null || !users.Any())
                return this;

            var user = users.FirstOrDefault();
            if (user == null)
                return this;

            this.Id = user.Id;
            this.Name = user.Name;
            this.Email = user.Email;
            this.ServicesList = user.ServicesList;
            this.Status = user.Status;

            return this;
        }

        public User Save(IUserRepository repository)
        {
            ValidateRepositoryInstance(repository);

            var ready = this.IsReadyToSave();
            if (!ready)
                throw new Exception("Invalid data to save");

            var user = repository.Save(this);
            return user;
        }

        public void Delete(UserFilter filterData, IUserRepository repository)
        {
            ValidateRepositoryInstance(repository);

            if(filterData == null || !filterData.HasFilter())
                throw new ArgumentException("Invalid filter data");

            repository.Delete(filterData);
        }

        public void Delete(IUserRepository repository)
        {
            ValidateRepositoryInstance(repository);

            var filterData = new UserFilter().SetIds(new[] { this.Id });

            this.Delete(filterData, repository);
        }



        private static void ValidateRepositoryInstance(IUserRepository repository)
        {
            if (repository == null)
                throw new ArgumentNullException("repository", "Invalid repository instance");
        }

        private void ValidateDataToCreate()
        {
            if (string.IsNullOrEmpty(this.Name))
                throw new ArgumentException("Invalid name value");

            if (string.IsNullOrEmpty(this.Email))
                throw new ArgumentException("Invalid email value");

            if (this.Status == UserStatusEnum.Undefined)
                throw new ArgumentException("Invalid status value");
        }

        private void ValidateDataToUpdate()
        {
            if (string.IsNullOrEmpty(this.Id) || !this.Id.IsValidMongoObjectId())
                throw new ArgumentException("Invalid id value");

            this.ValidateDataToCreate();
        }

        private bool IsReadyToSave()
        {
            try
            {
                this.ValidateDataToCreate();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void CleanModel()
        {
            this.Id = null;
            this.Name = null;
            this.Email = null;
            this.ServicesList = null;
            this.Status = UserStatusEnum.Undefined;
        }
    }
}
