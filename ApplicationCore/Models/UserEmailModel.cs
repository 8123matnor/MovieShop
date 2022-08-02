using System;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models
{
    public class UserEmailModel
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}

