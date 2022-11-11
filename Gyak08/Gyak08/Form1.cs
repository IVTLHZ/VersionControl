using Gyak08.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Gyak08
{
    public partial class Form1 : Form
    {
        List<Tick> ticks = new List<Tick>();
        //ORM objektum példányosítása
        PortfolioEntities context = new PortfolioEntities();
        
        List<PortfolioItem> pl = new List<PortfolioItem>();

        public Form1()
        {
            InitializeComponent();
            //adatábla (context.ticks) memóriába (tick típusú elemű listába)másolása
            ticks = context.Ticks.ToList(); //tolist nélkül sql mondatok generálása
            //DG feltöltés
            dataGridView1.DataSource = ticks;

            CreatePortfolio();
        }

        private void CreatePortfolio()
        {
            // PortfolioItem p = new PortfolioItem();
            //p.Index = "OTP";
            //p.Volume = 10;
            //Portfolio.Add(p);
            // ==   és olvashatóbb, példányfeltöltés egyből
            pl.Add(new PortfolioItem() { Index = "OTP", Volume = 10 });
            pl.Add(new PortfolioItem() { Index = "ZWACK", Volume = 10 });
            pl.Add(new PortfolioItem() { Index = "ELMU", Volume = 10 });

            dataGridView2.DataSource = pl;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
        //készen kapott fv
        //portfólióérték számítása
        private decimal GetPortfolioValue(DateTime date)
        {
            decimal value = 0;
            foreach (var item in pl)
            {
                var last = (from x in ticks
                            where item.Index == x.Index.Trim() //tickek szűrése a keresett index-xel megegyezőre
                                                               //trim, mert az index nchar(15)
                                                               //tárol unicode karaktereket , de fix 15 karakter hosszú mező
                                                               //le kell vágni a fel nem hazsnált karaktereket
                               && date <= x.TradingDay //ha nincs a kérdezett napon rekord az aznap kereskedésről
                            select x)
                            .First();
                value += (decimal)last.Price * item.Volume;
            }
            return value;
        }
    }
}
