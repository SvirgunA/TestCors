using System;
using System.ComponentModel.DataAnnotations;

namespace TestCors.DTO.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PasswordAttribute : RegularExpressionAttribute
    {
        private const string PASSWORD_PATTERN = @"^(?=.*[0-9])(?=.*[A-Z]).{8,}$";
        private const string ERROR_MESSAGE = @"Password is not valid (one capital letter, one number and 8 characters minimal length password should be provided)";

        public PasswordAttribute() : base(PASSWORD_PATTERN)
        {
            ErrorMessage = ERROR_MESSAGE;
        }
    }
}
