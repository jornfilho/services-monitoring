namespace monitoring.Repository.Models.Services
{
    using System;
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

        [BsonIgnoreIfNull]
        [BsonElement("frequency_type")]
        [BsonRepresentation(BsonType.String)]
        public string PlaymentFrequencyType { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("card_type")]
        [BsonRepresentation(BsonType.String)]
        public string CardType { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("card_number")]
        [BsonRepresentation(BsonType.String)]
        public string CardNumber { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("price")]
        [BsonRepresentation(BsonType.Double)]
        public double? Price { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("last_sync_time")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? LastSyncDate { get; set; }




        public Service GetServiceModel()
        {
            var type = this.Type.GetServiceTypeEnum();
            var service = new Service()
                .SetDataFromDatabase(type, this.Username, this.Password);

            switch (type)
            {
                case ServiceTypeEnum.Netflix:
                    var frequencyType = this.PlaymentFrequencyType.GetPaymentFrequencyTypeEnum();
                    service.SetNetflixData(frequencyType, this.CardType, this.CardNumber, this.Price, this.LastSyncDate);
                    break;
            }

            return service;
        }

        public ServiceMongoMap GetMongoMap(Service item)
        {
            if (item == null)
                return null;

            this.Type = item.Type.GetServiceTypeEnumStringValue();
            this.Username = item.Username;
            this.Password = item.Password;

            this.PlaymentFrequencyType = item.PlaymentFrequencyType.GetPaymentFrequencyTypeStringValue();
            this.CardType = item.CardType;
            this.CardNumber = item.CardNumber;
            this.Price = item.Price;
            this.LastSyncDate = item.LastSyncDate;

            return this;
        }
    }
}
