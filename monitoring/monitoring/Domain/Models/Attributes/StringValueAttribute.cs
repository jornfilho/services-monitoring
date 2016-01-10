namespace monitoring.Domain.Models.Attributes
{
    using System;
    using Interfaces.Extensions;

    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class StringValueAttribute : Attribute, IAttribute<string>
    {
        public StringValueAttribute(string value)
        {
            this.value = value;
        }

        public string Value
        {
            get { return this.value; }
        }

        private readonly string value;
    }
}
