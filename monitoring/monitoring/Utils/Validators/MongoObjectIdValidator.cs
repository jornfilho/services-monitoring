namespace monitoring.Utils.Validators
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class MongoObjectIdValidator
    {
        public static bool IsValidMongoObjectId(this string mongoObjectId)
        {
            if (string.IsNullOrEmpty(mongoObjectId))
                return false;

            Regex regex = new Regex("^[0-9a-fA-F]{24}$");
            return regex.Match(mongoObjectId).Success;
        }

        public static IList<string> GetValidMongoObjectIds(this IList<string> ids, IList<string> existentIds = null)
        {
            if (ids == null && existentIds == null)
                return null;

            if (ids != null && existentIds != null)
                return ids.Concat(existentIds).ToList();

            if (ids == null)
                ids = existentIds;

            if (!ids.Any())
                return ids;

            IList<string> result = new List<string>();
            foreach (var id in ids)
            {
                if (id == null)
                    continue;

                if (!id.IsValidMongoObjectId())
                    continue;

                result.Add(id);
            }

            return result;
        }
    }
}
