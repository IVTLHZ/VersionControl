using NUnit.Framework;
using Santa.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaTest
{
    public class SantaTestFixture
    {
        [
            Test,
            TestCase(0, false),
            TestCase(100, false),
            TestCase(1, true),
            TestCase(2, true),
            TestCase(3, true),
            TestCase(4, true),
            TestCase(5, true)            
        ]
        public void TestCheckBehaviour(int behaviour, bool expectedResult)
        {
            // Arrange
            var child = new Child();

            // Act
            var actualResult = child.CheckBehaviour(behaviour);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
