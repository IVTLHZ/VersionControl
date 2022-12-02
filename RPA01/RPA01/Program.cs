using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
// +
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace RPA01
{//kódszerkesztó és Solution Explorer használata (lehet fájl hozzáadása, osztálykészítés, csak vizuális megjelenítés nincs)
    internal class Program //lefutó programunk
    {
        static void Main(string[] args) //ez a fv fog lefutni
        {//ha más fv-t is akarok használni, azt itt kell meghívni

            //Console.WriteLine("Hello World");

            //int a = 5;
            //Console.WriteLine(a.ToString());



            //SELENIUM:
            //weboldalakról adatok kinyerése / beírása
            //a html szerkezetet hazsnálja ki
            //id oldalon belül jól azonosít, abból egy db van csak egy oldalon
            //neve is használható, csak az nem feltétlen egyedi

            //chromedriver: megnyit az alkalmazáson belül egy kisebb chrome ablakot, elnavigál az adott oldalra és azonosítja a megmondott objektumokat, beírja a kívánt szövegeket
            IWebDriver wd = new ChromeDriver(); //IWebDriver a chromedriver osztálya, példányosítjuk
            //weboldalra is navigáljunk
            wd.Navigate().GoToUrl("https://karosszeriajavitas.net/");
            //betöltés után fut le csk a kövi parancs
            IWebElement NevMegadas = wd.FindElement(By.Name("your-name"));
            //lekérdeztem, bele is írok
            NevMegadas.SendKeys("Gerda"); //virtuális billentyűleütés láncba szedve, tehát egyesével
            //kattintsunk
            //megint nincs id, de akkor ne is használjuk a nevet
            //azt lehet, hogy az inspect módban erre az elemre jobb klikk, copy és a full xpath-ot kell
            // elérési út alapján adjuk meg tehát
            IWebElement Referenciak = wd.FindElement(By.XPath("/html/body/div/header/div[1]/div[2]/nav/ul/li[4]")); //lehet, hogy nem egyértlműen azonosítható / az megváltozott / nem kattintható /stb
            Referenciak.Click(); //ha többször nem fog klleni, nem is kell kimenteni az elemet, vonjam egybe őket

            //várakozás x ideig pl animáció miatt vagy bármi
            Thread.Sleep(3000); //3mp

            wd.FindElement(By.XPath("/html/body/div/div/div/article/div/div/div/div[8]/div/div[2]/div/div/div/form/label[6]/span")).Click();




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
            //DBManager.AddPhone(); //így adatb-ba kerül az osztályban megadott elem

            //string name = DBManager.GetPhoneNameById(1);
            //Console.WriteLine(name);
        }



        //API-ba:

        //IWebDriver wd = new ChromeDriver();
        //wd.Navigate().GoToUrl("");
        //betöltés után fut le csk a kövi parancs
        //IWebElement NevMegadas = wd.FindElement(By.Name("your-name"));



        //DBManagerbe:

        //public static void AddPhone() //kell a static kulcsszó, hogy main-ből használható legyen
        //{
        //using (RPAEntities1 context = new RPAEntities1())
        //{
        //Phone t = new Phone();
        //t.name = "iPhone";
        //t.state = "használt";
        //t.date = DateTime.Now;
        //t.price = 200000;

        //context.Phones.Add(t);
        //context.SaveChanges();
        //}; 
        //}

        //public static string GetPhoneNameById(int id)
        //{
        //RPAEntities1 context = new RPAEntities1();

        //var query = from Phone in context.Phones
        //where Phone.id == id
        //select Phone;
        //List<Phone> phone = query.ToList(); ha lista lenne
        //var result = query.FirstOrDefault();
        //return result.name;
        //}


        //+) public static void AddFlightToDb() - kell a static kulcsszó, hogy main-ből használható legyen
    }
}
//írja, hogy code 0-val futott le, tehát sikeresen + bármely billentyűt lenyomva kilép
