using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FileCloud.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "Please provide username", AllowEmptyStrings = false)]
        public string username { get; set; }
        [Required(ErrorMessage = "Please provide Password", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Password must be 4 char long.")]
        public string password { get; set; }
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$",
       ErrorMessage = "Please provide valid email id")]
        public string email { get; set; }

    }
}