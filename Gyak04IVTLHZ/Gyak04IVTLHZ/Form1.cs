using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Excel könyvtár hozzáadása: Projekt --> Add reference -->COM oldalon
using Excel = Microsoft.Office.Interop.Excel; //Excel alias név, az excel könyvtárban vannak osztályok, amik már részei a projektnek (pl Application) --- így tudja a kód mire hivatkozunk
//pl Excel.Appcicationnel nem az eredeti, hanem Excel alatti osztályra hivatkozunk

//technikai könyvtár, missing.value értékek miatt kell, amik lehetnek fv paraméterekben azt jelezve, hogy okés az alapértelmezett érték
using System.Reflection;
using System.Data.Entity.Migrations.Model;
using System.Diagnostics;
using System.Windows.Markup;
using Microsoft.Office.Interop.Excel;

namespace Gyak04IVTLHZ
{
    public partial class Form1 : Form
    {
        RealEstateEntities context = new RealEstateEntities(); //ORM objektum példányosítása

        List<Flat> Flats; //flat típusú elemekből álló REFERENCIA - nem new item-es

        //üres változók
        Excel.Application xlApp; //alkalmazás
        Excel.Workbook xlWB; //munkafüzet
        Excel.Worksheet xlSheet; // munkalap

        //ide kell!!!
        //Range kell, ahova írjuk az adatokat - a szám koordinátákat nem kezeli úgy, mint a cells tul. --> excel-hivtakozást kell használni
        //segéd fv, nem kellmegtanulni
        private string GetCell(int x, int y)
        {
            string ExcelCoordinate = "";
            int dividend = y;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                ExcelCoordinate = Convert.ToChar(65 + modulo).ToString() + ExcelCoordinate;
                dividend = (int)((dividend - modulo) / 26);
            }
            ExcelCoordinate += x.ToString();

            return ExcelCoordinate;
        }

        //ide is kiszervezzük a header változót a formázás miatt, nem csak a createtableben található
        string[] headers = new string[] { //string tömb tábla fejléceivel + extra oszlop fejléce
                 "Kód",
                 "Eladó",
                 "Oldal",
                 "Kerület",
                 "Lift",
                 "Szobák száma",
                 "Alapterület (m2)",
                 "Ár (mFt)",
                 "Négyzetméter ár (Ft/m2)"};




        public Form1()
        {
            InitializeComponent();

            LoadData(); //ebből void visszatérési értékkel paraméter nélküli fv (ami ténylg csak a létrehozáskori állapot)

            CreateExcel(); //hívásban később generált createTable

            CreateTable(); //munkalap beállításaihoz

            FormatTable();
        }

        private void FormatTable()
        {
            //fejléc formázása

            //cellák intervalluma változóban
            Excel.Range headerRange = xlSheet.get_Range(GetCell(1, 1), GetCell(1, headers.Length));
            //beállítások
            headerRange.Font.Bold = true;
            headerRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            headerRange.EntireColumn.AutoFit();
            headerRange.RowHeight = 40;
            headerRange.Interior.Color = Color.LightBlue;
            headerRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);

            //hasonlóan rangekben új formázások

            //táblára körbeszegély
            Excel.Range tableRange = xlSheet.get_Range(GetCell(1, 1), GetCell(xlSheet.Rows.Count, headers.Length));
            tableRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);
            //első oszlop félkövér és háttérszín
            Excel.Range firstColumnRange = xlSheet.get_Range(GetCell(2, 1), GetCell(xlSheet.Rows.Count, 1));
            firstColumnRange.Font.Bold = true;
            firstColumnRange.Interior.Color = Color.LightYellow;
            //utolsó oszlop halványzöld hátterű
            Excel.Range lastColumnRange = xlSheet.get_Range(GetCell(2, headers.Length), GetCell(xlSheet.Rows.Count, headers.Length));
            lastColumnRange.Interior.Color = Color.LightGreen;
        }

        private void CreateTable()
        {

            //az excel intervallumokon működik jól, nagyobb adathalmaznál egyszerre exportálás jobb, mert a cellahivatkozás is intervallumbetöltés
            //tehát nagy erőforráshazsnálat ha az adatokat nem rakjuk tömbbe kiíratás előtt
            string[] headers = new string[] { //string tömb tábla fejléceivel + extra oszlop fejléce
                 "Kód",
                 "Eladó",
                 "Oldal",
                 "Kerület",
                 "Lift",
                 "Szobák száma",
                 "Alapterület (m2)",
                 "Ár (mFt)",
                 "Négyzetméter ár (Ft/m2)"};


            //tömb elemeinek kiírássa munkalap 1.sorába
            for (int i = 0; i < headers.Length; i++)
            {
                //tömb 0-tól számozódik, de az excel mezők 1-től!!
                xlSheet.Cells[1, i + 1] = headers[i]; //A1 mező pl 1,1
            }


            //adattárolásra kétdimenziós tömb
            //object típus, mert abba mindne típus betehető, nem kell felesleges típuskonverzió, amiért az excel adattípust detektál maga
            object[,] values = new object[Flats.Count, headers.Length];


            int counter = 0;
            foreach (var item in Flats)
            {
                //tömb feltölrése flats lista soraival
                values[counter, 0] = item.Code;
                values[counter, 1] = item.Vendor;
                values[counter, 2] = item.Side;
                values[counter, 3] = item.District;
                if (item.Elevator==true)
                {
                    values[counter, 4] = "Van";
                }
                else
                {
                    values[counter, 4] = "Nincs";
                }
                values[counter, 5] = item.NumberOfRooms;
                values[counter, 6] = item.FloorArea;
                values[counter, 7] = item.Price;
                values[counter, 8] = "=" + GetCell(counter+2, 8) + "/" + GetCell(counter+2, 7); //kiszámolás --- 0 indexkezdés miatt egyel lejjebb és jobbrább kell számozni és header miatt a sor még eggyel lejjebb

                counter++;
            }

            //KELL ZH-ban
            //values tömb tartalmának kiírása excel fájlba
            xlSheet.get_Range(
                GetCell(2, 1), //adatok 2.sor 1.cellájában kezdődnek
                GetCell(1 + values.GetLength(0), values.GetLength(1))).Value2 = values; //oszlopok száma a vízsz.irány+1, a sorok száma a kezdőértéktől nézve a függyőleges irányú hossz


            //Excel tábla utolsó oszlopába értkek
            //string formában írható, = jellel kezdődik
            //GetCell fv-el referencia

        }

        private void CreateExcel()
        {
            try
            {
                // Excel elindítása és az applikáció objektum betöltése
                xlApp = new Excel.Application();

                // Új munkafüzet
                xlWB = xlApp.Workbooks.Add(Missing.Value); //tehát alapértelmezett értékkel

                // Új munkalap
                xlSheet = xlWB.ActiveSheet; //a wb active sheet-je

                // Tábla létrehozása
                CreateTable(); // Ennek megírása a következő feladatrészben következik

                // Control átadása a felhasználónak
                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex) // Hibakezelés a beépített hibaüzenettel
            {
                string errMsg = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(errMsg, "Error");

                // Hiba esetén az Excel applikáció bezárása automatikusan
                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }
        }

        private void LoadData() //cél: programstrukturálás --- érdemes feladatrészeket függvénybe rendezni --- konstruktorban csak függvényhívások lesznek, átláthatóbb
        {
            //adattábla másolása memóriába
            Flats = context.Flats.ToList(); //context.Flats alapján a linq sql mondatokat generál, sql szerverrel végrehajtatja a lekérdezést --> nagy szerverterhelés lenne
            //a context. Flats tábla lemásolása Flats nevű, Flat típusú elemekből álló listába a memóriába
            //linq műveletek így lokálisan szerverterhelés nélkül
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
