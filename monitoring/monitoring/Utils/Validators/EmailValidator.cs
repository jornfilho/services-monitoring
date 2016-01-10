namespace monitoring.Utils.Validators
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public static class EmailValidator
    {
        public static bool IsValidEmail(this string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            try
            {
                return new EmailAddressAttribute().IsValid(email);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
