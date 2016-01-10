namespace monitoring.Repository.Database.MongoDb
{
    using System;
    using System.Configuration;
    using MongoDB.Driver;

    public class MongoDb
    {
        private static string defaultDbName;


        private static string GetConnectionString(string dbName)
        {
            try
            {
                //http://docs.mongodb.org/ecosystem/tutorial/use-csharp-driver/
                //mongodb://[username:password@]hostname[:port][/[database][?options]]
                const string connectionString = "mongodb://{0}{1}{2}{3}{4}{5}";

                string mongoUser = ConfigurationManager.AppSettings["mongoUser"];
                string mongoPassword = ConfigurationManager.AppSettings["mongoPassword"];
                string mongoServers = ConfigurationManager.AppSettings["mongoServers"];
                string mongoPort = ConfigurationManager.AppSettings["mongoPort"];
                string mongoDefaultDb = ConfigurationManager.AppSettings["mongoDefaultDb"];
                string mongoOptions = ConfigurationManager.AppSettings["mongoOptions"];

                if (!string.IsNullOrEmpty(mongoUser) && !string.IsNullOrEmpty(mongoPassword))
                {
                    mongoUser = mongoUser + ":";
                    mongoPassword = mongoPassword + "@";
                }
                else
                {
                    mongoUser = "";
                    mongoPassword = "";
                }

                if (string.IsNullOrEmpty(mongoServers))
                {
                    throw new ConfigurationErrorsException("Error getting server from configuration;");
                }

                if (string.IsNullOrEmpty(mongoPort))
                {
                    throw new ConfigurationErrorsException("Error getting server port from configuration;");
                }

                mongoPort = ":" + mongoPort;

                if (!string.IsNullOrEmpty(mongoDefaultDb) && string.IsNullOrEmpty(dbName))
                {
                    defaultDbName = mongoDefaultDb;
                    mongoDefaultDb = "/" + mongoDefaultDb;
                    mongoOptions = string.IsNullOrEmpty(mongoOptions) ? "" : "?" + mongoOptions;
                }
                else if (!string.IsNullOrEmpty(dbName))
                {
                    mongoDefaultDb = "/" + dbName;
                    mongoOptions = string.IsNullOrEmpty(mongoOptions) ? "" : "?" + mongoOptions;
                }
                else
                {
                    throw new ConfigurationErrorsException("Error getting mongo db name from configuration;");
                }

                return string.Format(connectionString, mongoUser, mongoPassword, mongoServers, mongoPort, mongoDefaultDb, mongoOptions);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static IMongoDatabase OpenConnection(string dbName = null)
        {
            try
            {
                string connectionString = GetConnectionString(dbName);
                if (string.IsNullOrEmpty(connectionString))
                    throw new ConfigurationErrorsException("Error getting connection string");

                IMongoClient client = new MongoClient(connectionString);
                IMongoDatabase database = client.GetDatabase(defaultDbName);

                return database;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
