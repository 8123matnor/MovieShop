﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? IsLocked { get; set; }
        public string? ProfilePictureUrl { get; set; }

        public ICollection<UserRole> RolesOfUser { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Purchase> Purchases { get; set; }
    }
}
