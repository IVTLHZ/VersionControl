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

namespace Gyak04IVTLHZ
{
    public partial class Form1 : Form
    {
        RealEstateEntities context; //ORM objektum példányosítása

        List<Flat> Flats; //flat típusú elemekből álló REFERENCIA - nem new item-es

        public Form1()
        {
            InitializeComponent();

            LoadData(); //ebből void visszatérési értékkel paraméter nélküli fv (ami ténylg csak a létrehozáskori állapot)
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
