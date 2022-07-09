using System;
using System.Collections.Generic;
using System.Text;

namespace bcross_frontend.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Year { get; set; }
        public Guid Uuid { get; set; }

        public Book() { }

        public override string ToString()
        {
            return $"{Author} - {Name} ({Year})";
        }
    }
}
