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

        }
    }
}
//írja, hogy code 0-val futott le, tehát sikeresen + bármely billentyűt lenyomva kilép
