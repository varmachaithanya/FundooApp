using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class UserUpdateModel
    {
        [Required(ErrorMessage = "FIRST NAME CANNOT BE EMPTY....")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LAST NAME CANNOT BE EMPTY....")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "EMAIL CANNOT BE EMPTY....")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(pattern: @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character.")]
        public string Email { get; set; }
    }
}
