using System;
using System.Collections.Generic;
using System.Text;

namespace bcross_frontend.Models
{
    class User
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public int Rating { get; set; }

        public User() { }

        public override string ToString()
        {
            return $"{Nickname} ({Rating})";
        }
    }
}
