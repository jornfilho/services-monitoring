namespace monitoring.Domain.Models.Services
{
    using System;

    public class Service
    {
        public ServiceTypeEnum Type { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        
        public PaymentFrequencyTypeEnum PlaymentFrequencyType { get; private set; }
        public string CardType { get; private set; }
        public string CardNumber { get; private set; }
        public double? Price { get; private set; }
        public DateTime? LastSyncDate { get; private set; }
        

        public Service SetDataFromDatabase(ServiceTypeEnum type, string username, string password)
        {
            this.Type = type;
            this.Username = username;
            this.Password = password;

            return this;
        }

        public Service SetDataToCreate(ServiceTypeEnum type, string username, string password)
        {
            ValidateDataToCreate(type, username, password);

            this.Type = type;
            this.Username = username;
            this.Password = password;

            return this;
        }

        public Service SetNetflixData(PaymentFrequencyTypeEnum frequency, string cardType, string cardNumber, double? price, DateTime? lastSyncTime)
        {
            this.PlaymentFrequencyType = frequency;
            this.CardType = cardType;
            this.CardNumber = cardNumber;
            this.Price = price;
            this.LastSyncDate = lastSyncTime;

            return this;
        }

        private static void ValidateDataToCreate(ServiceTypeEnum type, string username, string password)
        {
            if(type == ServiceTypeEnum.Undefined)
                throw new ArgumentException("Invalid service type");

            if(string.IsNullOrEmpty(username))
                throw new ArgumentException("Invalid username");

            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Invalid password");
        }
    }
}
