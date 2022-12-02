using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace RPA02
{
    public class Flight_API
    {
        IWebDriver wd;
        string url;
        string dep_place;
        string arr_place;
        string dep_date;

        public void StartSession(string url, string dep_place, string arr_place, string dep_date)
        {
            this.url = url;
            this.dep_place = dep_place;
            this.arr_place = arr_place;
            this.dep_date = dep_date;

            wd = new ChromeDriver();

            try
            {
                wd.Navigate().GoToUrl(url);
            }
            catch (Exception ex)
            {
                throw new Exception("A megadott url nem elérhető");
            }
        }



        
    }
}
