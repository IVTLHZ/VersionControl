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

namespace Gyak04IVTLHZ
{
    public partial class Form1 : Form
    {
        RealEstateEntities context; //ORM objektum példányosítása

        List<Flat> Flats; //flat típusú elemekből álló REFERENCIA - nem new item-es

        //üres változók
        Excel.Application xlApp; //alkalmazás
        Excel.Workbook xlWB; //munkafüzet
        Excel.Worksheet xlSheet; // munkalap

        public Form1()
        {
            InitializeComponent();

            LoadData(); //ebből void visszatérési értékkel paraméter nélküli fv (ami ténylg csak a létrehozáskori állapot)

            CreateExcel(); //hívásban később generált createTable
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
            List<Flat> Flats = context.Flats.ToList(); //context.Flats alapján a linq sql mondatokat generál, sql szerverrel végrehajtatja a lekérdezést --> nagy szerverterhelés lenne
            //a context. Flats tábla lemásolása Flats nevű, Flat típusú elemekből álló listába a memóriába
            //linq műveletek így lokálisan szerverterhelés nélkül
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
