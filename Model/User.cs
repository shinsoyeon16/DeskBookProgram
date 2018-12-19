using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Model
{
    class User
    {
        public string ID { get; set; }
        public string Password { get; set; }
        public bool isBooked { get; set; }
        public int DeskNumber { get; set; }
        public string Print { get; set; }
    }
}
