using NUnit.Framework; //kell az attribútumhoz
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestExample.Test
{
    public class AccountControllerTestFixture
    {   
        [Test]//fv attribútuma
        public void TestValidateEmail(string email, bool expectedResult) //bemenő paraméteres fv., public kell!
        {
            
        }
    }
}
