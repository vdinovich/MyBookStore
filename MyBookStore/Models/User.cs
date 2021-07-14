using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBookStore.Models
{
    public class User
    {
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string User_Name { get; set; }
        [Display(Name = "Ваш логин: ")] //there is an overwrite for this line in SignIn view
        [Required(ErrorMessage = "Пожалуйста введити свой логин")]
        public string Login { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Пожалуйста введити свой пароль")]
        public string Password { get; set; }
        public string Role { get; set; }


    }
}