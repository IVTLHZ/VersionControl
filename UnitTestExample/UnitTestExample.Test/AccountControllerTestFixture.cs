using NUnit.Framework; //kell az attribútumhoz
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestExample.Controllers;

namespace UnitTestExample.Test
{
    public class AccountControllerTestFixture //ha nem fut le jól. akkor lehet újra kell a nunit, nunit3, moq
    {   //fv attribútumai
        [Test,
         TestCase("abcd1234", false), //véletlen jelszó megy az e-mailhez
         TestCase("irf@uni-corvinus", false), //e-mailből kimarad a domain
         TestCase("irf.uni-corvinus.hu", false), //kimarad a @
         TestCase("irf@uni-corvinus.hu", true)] //oké az e-mail
        public void TestValidateEmail(string email, bool expectedResult) //bemenő paraméteres fv., public kell!
        {
            //ARRANGE
            //kell add reference, project, és a unittestexample-s kell
            //hogy ez sikerüljön:
            var aC = new AccountController();
            //ACT: bemenő email paraméter értékével meghívás, változóba tárolás
            var actualResult = aC.ValidateEmail(email);
            //ASSERT: azAssert osztály statikus AreEqual függvényével hasonlítsd össze a tényleges eredményt a bemeneti paraméterként kapott elvárt eredménnyel (expectedResult)
            Assert.AreEqual(expectedResult, actualResult);
            //több assert hívásnál akkor lesz sikeres a teszt, ha minden egyes hívás eredménye sikeres 

        }
    }
}
