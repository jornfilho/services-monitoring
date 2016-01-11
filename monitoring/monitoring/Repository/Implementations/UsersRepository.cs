namespace monitoring.Repository.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Database.MongoDb;
    using Domain.Interfaces.Repositories;
    using Domain.Models.Users;
    using Models.Users;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization;
    using MongoDB.Driver;

    public class UsersRepository : IUsersRepository
    {
        public IList<User> GetUsers(UserFilter filterData, int take, int skip)
        {
            var mongo = MongoDb.OpenConnection();

            var query = GetUserQuery(filterData) ?? new BsonDocument();

            var collection = mongo.GetCollection<BsonDocument>(CollectionsEnum.users.ToString());
            var users = collection
                .Find(query)
                .Skip(skip)
                .Limit(take)
                .ToList()
                .Select(u => BsonSerializer.Deserialize<UserMongoMap>(u).GetUserModel())
                .ToList();

            return users;
        }

        public long GetUsersCount(UserFilter filterData)
        {
            var mongo = MongoDb.OpenConnection();

            var query = GetUserQuery(filterData) ?? new BsonDocument();

            var collection = mongo.GetCollection<BsonDocument>(CollectionsEnum.users.ToString());
            var usersCount = collection
                .Count(query);

            return usersCount;
        }

        public User Save(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user", "Invalid user data");

            var mongoDb = MongoDb.OpenConnection();

            var data = new UserMongoMap().GetMongoMap(user);
            var collection = mongoDb.GetCollection<BsonDocument>(CollectionsEnum.users.ToString());

            if (data.IsNew)
            {
                collection.InsertOne(data.ToBsonDocument());
                return data.GetUserModel();
            }

            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("_id", new BsonObjectId(data.Id));
            collection.FindOneAndReplace(filter, data.ToBsonDocument());
            
            return data.GetUserModel();
        }

        public void Delete(UserFilter filterData)
        {
            if (filterData == null || !filterData.HasFilter())
                throw new ArgumentException("Invalid filter data");

            var query = GetUserQuery(filterData) ?? new BsonDocument();

            var mongoDb = MongoDb.OpenConnection();

            var collection = mongoDb.GetCollection<BsonDocument>(CollectionsEnum.users.ToString());
            var result = collection.DeleteMany(query);
            if (result.DeletedCount == 0)
                throw new Exception("There aren't any users with the criteria provided");

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

            #region email
            if (!string.IsNullOrEmpty(filterData.Email))
            {
                filter = filter == null
                    ? builder.Eq("email", new BsonString(filterData.Email))
                    : filter & builder.Eq("email", new BsonString(filterData.Email));
            }
            #endregion

            #region status
            if (filterData.StatusList != null && filterData.StatusList.Any())
            {
                var arrayStatus = filterData.StatusList.Select(i => i.GetUserStatusEnumStringValue()).ToArray();
                filter = filter == null
                    ? builder.Eq("status", new BsonDocument("$in", new BsonArray(arrayStatus)))
                    : filter & builder.Eq("status", new BsonDocument("$in", new BsonArray(arrayStatus)));
            }
            #endregion

            return filter;
        }
    }
}
