using PackMaker;
using Santa.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Santa.Entities
{
    public class Child
    {
        public string Name { get; set; }
        public Behaviour Behaviour { get; set; }
        public List<Gift> Gifts { get; set; }

        public bool CheckBehaviour(int behaviour)
        {
            if (behaviour >= 1 && behaviour <= 5)
                return true;
            else
                return false;
        }
    }
}
