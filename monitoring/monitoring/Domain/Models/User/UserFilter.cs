namespace monitoring.Domain.Models.User
{
    using System.Collections.Generic;
    using System.Linq;
    using Utils.Validators;

    public class UserFilter
    {
        public IList<string> Ids { get; private set; }

        public UserFilter SetIds(IList<string> ids)
        {
            if (ids == null || !ids.Any())
                return this;

            if (this.Ids == null)
                this.Ids = new List<string>();

            foreach (var id in ids)
            {
                if(string.IsNullOrEmpty(id))
                    continue;

                if(!id.IsValidMongoObjectId())
                    continue;

                if(this.Ids.Any(i => i.Equals(id)))
                    continue;

                this.Ids.Add(id);
            }

            return this;
        }

        public bool HasFilter()
        {
            if (this.Ids != null && this.Ids.Any())
                return true;

            return false;
        }
    }
}