using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gyak07.Entities
{
    internal class BallFactory
    {
        public Ball CreateNew() //ball visszatérési érték
        {
            return new Ball(); //és létre is kell hozni az új labdát
        }
            }
}
