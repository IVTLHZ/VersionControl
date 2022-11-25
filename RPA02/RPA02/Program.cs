using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPA02
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //code-first megközelítés

            //connection string, ez látszik, hogy létrehozott egy local hostot, ebben master db-t
            //Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;
            //csak a sqlexpress nevet kell megadni majd ssms-ben

            //adatbázist a robot elvárt funkcionalitásai alapján tervezünk
            //futás során kell kívülről jövő paraméter, amit belerakunk
            //pl összes létező hirdetést töltse le és lehet tovább nézni, hogy lett-e x idő óta új, változozz-e valami

            //kézzel ssms-ben adatbázis és tábla létrehozása
            //csatlakozás
            //new database
            //new table

            //telefonok lekérdezésére
            //id-nál set primary key és is identity yes
            //new item ADO.NET
            //queryket kezelő osztály kell
            DBManager.AddPhone(); //így adatb-ba kerül az osztályban megadott elem

            string name = DBManager.GetPhoneNameById(1);
            Console.WriteLine(name);
        }
    }
}
