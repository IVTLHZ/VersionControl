using FactoryAgain.Abstractions;
using FactoryAgain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FactoryAgain
{
    public partial class Form1 : Form
    {
        List<Toy> _toys = new List<Toy>();

        //osztályszintű változó, amire fv
        private Toy _nxtToy;


        //kifejtett prop
        private IToyFactory _factory;

        public IToyFactory Factory
        {
            get { return _factory; }
            set
            {
                _factory = value;
                DisplayNext(); //factory változása esetén más megjelenített kép
            }
        }

        private void DisplayNext()
        {
            if (_nxtToy != null)
            {
                Controls.Remove(_nxtToy); //form1 vezérlői közül eltávolítás
            }
            _nxtToy = Factory.CreateNew();
            //elhelyezés a coming next címkéhez képest
            _nxtToy.Top = label1.Top + label1.Height + 10;
            _nxtToy.Left = label1.Left;
            Controls.Add(_nxtToy);
        }

        public Form1()
        {
            InitializeComponent();
            //példány feltöltése konstruktorból
            Factory = new BallFactory(); //interfész nem hivatkozható, ezen a ponton kell döntnei, hogy labda vagy car, de lett választógomb
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
       
        private void button1_Click_1(object sender, EventArgs e)
        {
            Factory = new CarFactory();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
           var button = (Button)sender;
            var cd = new ColorDialog();
            cd.Color = button3.BackColor; //UGYANOLYAN SZÍN, MINT GOMBNAK
            if (cd.ShowDialog() != DialogResult.OK)
                return;
            button.BackColor = cd.Color; //color dialogban button színének állítása a választott alapján

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        //timerekkel futószalag az új elemeknek
        //a beállított 3mp után megjelenik az adott játék és az 1000es pozicíóban az megsemmisül
        private void createTimer_Tick_1(object sender, EventArgs e)
        {
            var új = Factory.CreateNew();
            _toys.Add(új);
            //panel vezérlőihez hozzáadás
            mainPanel.Controls.Add(új);
            új.Left = -új.Width; //képernyőn kívülről beúszás
                      
        }

        private void Új_Click(object sender, EventArgs e)
        {
        }

        private void conveyorTimer_Tick_1(object sender, EventArgs e)
        {
            var maxPosition = 0;

            foreach (var item in _toys)
            {
                item.MoveToy();
                //kell a legjobbrább levő pozíciója
                if (item.Left > maxPosition)
                    maxPosition = item.Left;
                //ha 1000nél nagyobb a legnagyobb pozi, akkor ne legyen a listában és vezérlők között már
                
            }
            if (maxPosition >= 1000)
            {
                var oldestToy = _toys[0];
                _toys.Remove(oldestToy);
                mainPanel.Controls.Remove(oldestToy);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Factory = new PresentFactory()
            {
                BoxColor = button5.BackColor, //a színválasztás megvalósítása után így bűvitjük a fv-t
                RibbonColor = button6.BackColor
            };
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Factory = new BallFactory()
            {
                BallColor = button3.BackColor //a színválasztás megvalósítása után így bűvitjük a fv-t
                
            };
        }
    }
}
