using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gyak03IVTLHZ.Entities //a mappán belüli névtérbe hoztam létre a class-t, de ez átírható, ha elrontottam
{//ez a névtér DLL fájlként kezelendő
 //public tul.-ok fontosak, hogy más névtérből is látszódjon
 //egy fájlban csak a szükséges névterek legyenek
    internal class User
    {
        //egyedi azonosító generálása sorszámozás helyett, nem lehet bennük ismérlődés
        public Guid ID { get; set; } = Guid.NewGuid();

        //tasks/csv miatt kiszedem a külön bekérést, felesleges
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        public string FullName //a két névrészből fűz össze
        {
            get;
            //csv miatt nem kellő részek{
            //return string.Format("{0} {1}",
            //LastName,
            //FirstName);
            set;
        }
    }
}

