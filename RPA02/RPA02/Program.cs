using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using Actions = OpenQA.Selenium.Interactions.Actions;
using System.IO;

namespace RPA02
{
    class Program
    {
        static void Main(string[] args)
        {
            //https://www.expedia.com/Flights-Search?flight-type=on&mode=search&trip=oneway&leg1=from%3Abudapest%2Cto%3Abarcelona%2Cdeparture%3A12%2F9%2F2022TANYT&options=cabinclass%3Aeconomy&passengers=children%3A0%2Cadults%3A1%2Cseniors%3A0%2Cinfantinlap%3AY&fromDate=12%2F9%2F2022&d1=2022-12-09
            string url= "https://www.expedia.com/?pwaLob=wizard-flight-pwa";
            string dep_place ="budapest";
            string arr_place= "barcelona";
            string dep_date= "Dec162022"; //csak ebben a formátumban működik (hónap nevének első 3 betűje, nap(2 karakter), év(2 karakter))
            List<string[]> data = new List<string[]>();

            char[] dep_date_chars = dep_date.ToCharArray();
            dep_date = dep_date_chars[0].ToString() + dep_date_chars[1].ToString() + dep_date_chars[2].ToString() + " " + dep_date_chars[3] + dep_date_chars[4] + ", " + dep_date_chars[5] + dep_date_chars[6] + dep_date_chars[7] + dep_date_chars[8];

            IWebDriver driver = new ChromeDriver();
            driver.Url = url;

            driver.FindElement(By.XPath("//*[@id=\"uitk-tabs-button-container\"]/div[1]/li[2]/a")).Click();

            driver.FindElement(By.ClassName("uitk-form-field-trigger")).Click();
            driver.FindElement(By.Id("location-field-leg1-origin")).SendKeys(dep_place);
            driver.FindElement(By.Id("location-field-leg1-origin")).SendKeys(Keys.Enter);

            driver.FindElement(By.CssSelector("[aria-label='Going to']")).Click();
            driver.FindElement(By.Id("location-field-leg1-destination")).SendKeys(arr_place);
            driver.FindElement(By.Id("location-field-leg1-destination")).SendKeys(Keys.Enter);

            driver.FindElement(By.Id("d1-btn")).Click();
            driver.FindElement(By.CssSelector("[aria-label='" + dep_date + "']")).Click();

            driver.FindElement(By.CssSelector("[data-stid='apply-date-picker']")).Click();

            driver.FindElement(By.CssSelector("[data-testid='submit-button']")).Click();

            while (true)
            {
                try
                {
                    string a = driver.FindElement(By.XPath("/html/body/div[2]/div[1]/div/div[2]/div[3]/div/section/main/ul/li[2]/div/div/div[1]/button/span")).Text;
                    data.Add(textToData(a));
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Thread.Sleep(500);
                }
            }

            IWebElement b;
            for (int i = 3; i < 12; i++)
            {
                b = driver.FindElement(By.XPath("/html/body/div[2]/div[1]/div/div[2]/div[3]/div/section/main/ul/li[" + i + "]/div/div/div[1]/button/span"));

                Actions actions = new Actions(driver);
                actions.MoveToElement(b);
                actions.Perform();

                data.Add(textToData(b.Text));
            }

            exportToExcel(data);

            foreach (string[] i in data){
                 foreach (string j in i) {
                     Console.WriteLine(j);
                 }
             }
        }

        static string[] textToData(string text)
        {
            string[] data = text.Split(',');
            //rep tarsasag: 0
            //dep time: 1
            //arr time: 2
            //price: 3
            //remaining: 4
            //Layover: 5
            data[0] = data[0].Substring(37, (data[0].Length - 44));
            data[1] = data[1].Substring(14, 7);
            data[2] = data[2].Substring(13, 7);
            if (data[data.Length - 1] == " Nonstop.")
            {
                if (data.Length == 6)
                {
                    data[3] = data[3].Substring(11, 5);
                    data[4] = data[4].Substring(1, 1);
                    data[5] = "Nonstop";
                }
                else
                {
                    data[3] = data[3].Substring(11, 5);
                    data[4] = "Na";
                    data = data.Append("Nonstop").ToArray();
                }
            }
            else
            {
                if (data.Length == 7)
                {
                    data[3] = data[3].Substring(11, 5);
                    data[4] = data[4].Substring(1, 1);
                    data[5] = data[5].Substring(1, (data[5].Length - 6));
                }
                else
                {
                    data[3] = data[3].Substring(11, 5);
                    data[5] = data[4].Substring(1, (data[4].Length - 6));
                    data[4] = "Na";
                }
            }

            return data;
        }

        static void exportToExcel(List<string[]> data)
        {
           
            try
            {
                
                Excel.Application xlApp = new Excel.Application();
                
                
                if (xlApp!=null)
                {
                    Excel.Workbook xlWB = xlApp.Workbooks.Add();
                    Excel.Worksheet xlWS = (Excel.Worksheet)xlWB.Sheets.Add();
                    
                    //headers
                    xlWS.Cells[1, 1] = "Repülő társaság";
                    xlWS.Cells[1, 2] = "Indulási idő";
                    xlWS.Cells[1, 3] = "Érkezési idő";
                    xlWS.Cells[1, 4] = "Ár";
                    xlWS.Cells[1, 5] = "Elérhető jegyek";
                    xlWS.Cells[1, 6] = "Átszállások száma";
                    int line = 2;
                    foreach (var item in data)
                    {
                        xlWS.Cells[line, 1] = item[0];
                        xlWS.Cells[line, 2] = item[1];
                        xlWS.Cells[line, 3] = item[2];
                        xlWS.Cells[line, 4] = item[3];
                        xlWS.Cells[line, 5] = item[4];
                        xlWS.Cells[line, 6] = item[5];
                        
                        line++;
                    }
                    
                    xlWS.get_Range("A1", "F1").Font.Bold = true;
                    xlWS.get_Range("A1", "F1").VerticalAlignment =Excel.XlVAlign.xlVAlignCenter;
                    
                    xlWS.get_Range("A1", "F1").BorderAround2(Excel.XlLineStyle.xlContinuous, XlBorderWeight.xlThick);
                    var workingDirectory = AppContext.BaseDirectory;
                   
                    xlApp.ActiveWorkbook.SaveAs(String.Concat(workingDirectory, "available_flights.xls"), Excel.XlFileFormat.xlWorkbookNormal);
                    xlWB.Close();
                    xlApp.Quit();
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlWS);
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlWB);
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlApp);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                }
            }
            catch (Exception ex)
            {
                string errMsg = string.Format("Error:{0}\nLine:{1}", ex.Message, ex.Source);
                Console.WriteLine(errMsg);
            }
            
        }
    }
}
