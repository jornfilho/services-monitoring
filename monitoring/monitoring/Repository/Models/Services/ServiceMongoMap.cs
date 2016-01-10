namespace monitoring.Repository.Models.Services
{
    using Domain.Models.Services;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    [BsonIgnoreExtraElements]
    internal class ServiceMongoMap
    {
        [BsonElement("type")]
        [BsonRepresentation(BsonType.String)]
        public string Type { get; set; }

        [BsonElement("username")]
        [BsonRepresentation(BsonType.String)]
        public string Username { get; set; }

        [BsonElement("password")]
        [BsonRepresentation(BsonType.String)]
        public string Password { get; set; }

        public Service GetServiceModel()
        {
            var type = this.Type.GetServiceTypeEnum();
            var service = new Service().SetDataFromDatabase(type, this.Username, this.Password);
            return service;
        }

        public ServiceMongoMap GetMongoMap(Service item)
        {
            if (item == null)
                return null;

            this.Type = item.Type.GetServiceTypeEnumStringValue();
            this.Username = item.Username;
            this.Password = item.Password;

            return this;
        }
    }
}
