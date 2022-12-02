using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVTLHZ11.Entities
{
    public class Person //public!
    {
        public int BirthYear { get; set; }
        public Gender Gender { get; set; }
        public int NbrOfChildren { get; set; }
        public bool IsAlive { get; set; } //állapotjelzőre, azaz: az adott személy életben van-e

        public Person()
        {
            IsAlive = true;
        }

        
        //Ezeket az adatokat a nép.csv illetve a nép-teszt.csv forrásfájl beolvasásával fogjuk megkapni. (A gyermekek számát a szimulációban most nem használjuk fel, de egy továbbfejlesztett, változatban fontos lehet.)
    }
}
