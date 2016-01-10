namespace monitoring.Repository.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Database.MongoDb;
    using Domain.Interfaces.Repositories;
    using Domain.Models.User;
    using MongoDB.Bson;
    using MongoDB.Driver;

    public class UserRepository : IUserRepository
    {
        public IList<User> Get(UserFilter filterData, int take, int skip)
        {
            var mongo = MongoDb.OpenConnection();

            var query = GetUserQuery(filterData);

            var collection = mongo.GetCollection<BsonDocument>(CollectionsEnum.users.ToString());
            var users = collection
                .Find(query)
                .Skip(skip)
                .Limit(take)
                .ToList();

            return null;
        }

        

        public long GetCount(UserFilter filterData)
        {
            throw new NotImplementedException();
        }

        public User Save(User user)
        {
            throw new NotImplementedException();
        }

        public void Delete(UserFilter filterData)
        {
            throw new NotImplementedException();
        }

        private static FilterDefinition<BsonDocument> GetUserQuery(UserFilter filterData)
        {
            if (filterData == null)
                return null;

            //https://docs.mongodb.org/getting-started/csharp/query/

            var builder = Builders<BsonDocument>.Filter;
            FilterDefinition<BsonDocument> filter = null;

            #region ids
            if (filterData.Ids != null && filterData.Ids.Any())
            {
                var arrayIds = filterData.Ids.Select(i => new BsonObjectId(new ObjectId(i))).ToArray();
                filter = builder.Eq("_id", new BsonDocument("$in", new BsonArray(arrayIds)));
            }
            #endregion

            return filter;
        }
    }
}
