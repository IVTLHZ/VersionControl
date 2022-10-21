using I.MintaZH.Eredmeny;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

//Excel könyvtár hozzáadása: Projekt --> Add reference -->COM oldalon
using Excel = Microsoft.Office.Interop.Excel; //Excel alias név, az excel könyvtárban vannak osztályok, amik már részei a projektnek (pl Application) --- így tudja a kód mire hivatkozunk
//pl Excel.Appcicationnel nem az eredeti, hanem Excel alatti osztályra hivatkozunk

namespace I.MintaZH
{
    public partial class Form1 : Form
    {
        List<OlympicResult> results = new List<OlympicResult>();

        Excel.Application xlApp; //alkalmazás
        Excel.Workbook xlWB; //munkafüzet
        Excel.Worksheet xlSheet; // munkalap

        
        public Form1()
        {
            InitializeComponent();
            //fájlbeolvasás
            Fv("Summer_olympic_Medals.csv");
            //évválasztás
            Fv2();
            //sorrendmeghatározás
            //Fv3();
            //position feltöltése minden results elemre
            Fv4();
        }

        private void Fv4()
        {
            foreach (var r in results)
                r.Position = Fv3(r);
        }

        //kimenet olympicresult helyezése
        private int Fv3(OlympicResult or)
        {
            //bemeneti országnál jobban teljesítő országok száma
            int counter = 0;
            

            var linq2 = from r in results where r.Year==Convert.ToInt32(or.Year) && or.Country!=r.Country select r;

            //szűrt listán végigmegyünk
            for (int i = 0; i < linq2.ToList().Count; i++)
            {
                if (or.Medals[0] < results[i].Medals[0])
                {
                    counter++;
                }
                else
                {
                    if (or.Medals[0] == results[i].Medals[0])
                    {
                        if (or.Medals[1] < results[i].Medals[1])
                        {
                            counter++;
                        }
                    }

                    if (or.Medals[2] < results[i].Medals[2])
                    {
                        if (or.Medals[1] == results[i].Medals[1])
                        {
                            if (or.Medals[0] == results[i].Medals[0])
                            {
                                counter++;
                            }
                        }
                    }
                }
            }
            //a számlálónál eggyel nagyobb szám a helyezés
            int orr = counter + 1;
            return orr;
        }

        private void Fv2()
        {
            //sorbarendezés + duplikátumszűrés
            var linq = (from r in results orderby r.Year select r.Year).Distinct();
            comboBox1.DataSource = linq.ToList();
        }

        private void Fv(string filename)
        {
            //OpenFileDialog ofd = new OpenFileDialog();
            //if (ofd.ShowDialog() != DialogResult.OK) return;

            StreamReader sr = new StreamReader(filename, Encoding.UTF8);
            //fejléc nem kell
            sr.ReadLine();
            while (!sr.EndOfStream)
            {
                string[] sor = sr.ReadLine().Split(',');
                OlympicResult result = new OlympicResult();
                result.Year = Convert.ToInt32(sor[0]);
                result.Country = sor[3];
                //medalshoz tömb írása
                int[] medals = new int[] {
                Convert.ToInt32(sor[5]), Convert.ToInt32(sor[6]), Convert.ToInt32(sor[7])};
                result.Medals = medals;
            
                results.Add(result);
            }
            sr.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //előkészületek után tényleges excel munkalap nyitása
                xlApp = new Excel.Application();
                xlWB = xlApp.Workbooks.Add(Missing.Value);
                xlSheet = xlWB.ActiveSheet;

                CreateExcel();

                //felhasználónak kontrolátadás
                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }
        }

        private void CreateExcel()
        {
            //fejlécek
            var headers = new string[]
            {
                "Helyezés",
                "Ország",
                "Arany",
                "Ezüst",
                "Bronz"
            };

            //tömb elemeinek kiírássa munkalap 1.sorába
            for (int i = 0; i < headers.Length; i++)
            {
                //tömb 0-tól számozódik, de az excel mezők 1-től!!
                xlSheet.Cells[1, i + 1] = headers[i]; //A1 mező pl 1,1
            }


           //adattárolásra kétdimenziós tömbbel cellafeltöltés másik módszerrel
            
            var linq3 = from r in results where r.Year==Convert.ToInt32(comboBox1.SelectedItem) orderby r.Position select r;

            int counter = 2; //2.sortól kezdek írni!!!
            //menj végig = foreach
            foreach (var r in linq3) //másik megoldás a 4.gyakos, tömb feltöltése, majd tömb tartalmának kiírása
            {
                //excel megfelelő celláiba kiírás (pozi, ország, medáloK)
                xlSheet.Cells[counter, 1] = r.Position;
                xlSheet.Cells[counter, 2] = r.Country;
                //medál egy tömb, szét kell szedni részekre
                for (int i = 0; i <= 2; i++)
                    xlSheet.Cells[counter, i + 3] = r.Medals[i];
                counter++;
            }
        }
    }
}
