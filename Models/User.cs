﻿using System.ComponentModel.DataAnnotations;

namespace UsersProject.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
