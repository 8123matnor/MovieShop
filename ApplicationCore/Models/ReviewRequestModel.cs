﻿using System;
using ApplicationCore.Entities;

namespace ApplicationCore.Models
{
    public class ReviewRequestModel
    {
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public decimal Rating { get; set; }
        public string ReviewText { get; set; }
        public DateTime CreatedDate { get; set; }
        public User User { get; set; }
        public Movie Movie { get; set; }
    }
}

