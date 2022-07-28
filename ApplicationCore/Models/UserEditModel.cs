using System;
namespace ApplicationCore.Models
{
    public class UserEditModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }


        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}

