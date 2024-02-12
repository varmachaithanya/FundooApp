using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class ForgotPasswordModel
    {
        public long UserId { get; set; }

        [Required(ErrorMessage = "EMAIL CANNOT BE EMPTY....")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Enter a valid Email ID")]
        public string Email { get; set; }

        public string Token { get; set; }
    }
}
