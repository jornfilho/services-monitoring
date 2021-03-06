﻿namespace monitoring.Repository.Models.Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Models.Services;
    using Domain.Models.Users;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using Services;

    [BsonIgnoreExtraElements]
    internal class UserMongoMap
    {
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        [BsonRepresentation(BsonType.String)]
        public string Name { get; set; }

        [BsonElement("email")]
        [BsonRepresentation(BsonType.String)]
        public string Email { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("services")]
        public IList<ServiceMongoMap> ServicesList { get; set; }

        [BsonElement("status")]
        [BsonRepresentation(BsonType.String)]
        public string Status { get; set; }

        [BsonElement("creation_time")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreationTime { get; set; }

        [BsonIgnore]
        public bool IsNew { get; private set; }


        public User GetUserModel()
        {
            var status = this.Status.GetUserStatusEnum();

            IList<Service> servicesList = null;
            if (this.ServicesList != null && this.ServicesList.Any())
            {
                servicesList = new List<Service>();
                foreach (var item in this.ServicesList)
                {
                    var service = item.GetServiceModel();
                    if(service == null)
                        continue;

                    servicesList.Add(service);
                }
            }

            var result = new User()
                .SetDataFromDatabase(this.Id.ToString(), this.Name, this.Email, servicesList, status, this.CreationTime);

            return result;
        }

        public UserMongoMap GetMongoMap(User userData)
        {
            if (userData == null)
                throw new ArgumentNullException("userData");

            if (string.IsNullOrEmpty(userData.Id))
            {
                this.IsNew = true;
                this.Id = ObjectId.GenerateNewId();
            }
            else
                this.Id = new ObjectId(userData.Id);

            this.Name = userData.Name;
            this.Email = userData.Email;

            if (userData.ServicesList != null)
            {
                this.ServicesList = new List<ServiceMongoMap>();
                foreach (var item in userData.ServicesList)
                {
                    var service = new ServiceMongoMap().GetMongoMap(item);
                    if(service == null)
                        continue;

                    this.ServicesList.Add(service);
                }
            }

            this.Status = userData.Status.GetUserStatusEnumStringValue();
            this.CreationTime = userData.CreateTime;

            return this;
        }
    }
}
