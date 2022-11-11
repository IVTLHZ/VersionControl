using FactoryAgain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryAgain.Entities
{
    public class CarFactory : IToyFactory //interfész őssel meg is jelenik az azt tartalmazó fv
    {
        public Toy CreateNew()
        {
            return new Car();
        }
    }
}
