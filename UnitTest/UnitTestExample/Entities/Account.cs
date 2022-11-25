using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestExample.Entities
{
    public class Account
    {
        public Guid ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
