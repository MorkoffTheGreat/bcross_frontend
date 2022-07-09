using System;
using System.Collections.Generic;
using System.Text;

namespace bcross_frontend.Models
{
    class Bookrecord
    {
        public int Id { get; set; }
        public int Sender { get; set; }
        public int Book { get; set; }
        public string Location { get; set; }
        public DateTime Sendtime { get; set; }
        public int? Receiver { get; set; }
        public DateTime? Receivetime { get; set; }
        public string Commentary { get; set; }

        public Bookrecord() { }
    }
}
