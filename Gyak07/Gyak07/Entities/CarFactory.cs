using Gyak07.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gyak07.Entities
{
    internal class CarFactory : IToyFactory //interfész őssel meg is jelenik az azt tartalmazó fv
    {
        public Toy CreateNew()
        {
            return new Car();
        }
    }
}
