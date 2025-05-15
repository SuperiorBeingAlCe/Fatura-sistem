using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fatura_sistem.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string Age { get; set; }

        public string Gender { get; set; }

       
       
        public User(string username, string email, string password, string Age, string Gender)
        {
            Username = username;
            Email = email;
            Password = password;
            this.Age = Age;
            this.Gender = Gender;
        }

        public User()
        {
        }
    }
}
