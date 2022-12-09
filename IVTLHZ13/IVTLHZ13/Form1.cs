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
using WorldsHardestGame;

namespace IVTLHZ13
{
    public partial class Form1 : Form
    {
        GameController gc = new GameController(); //játéktér vezérlői vezérli, létrehozásával meglesz a játéktér
        GameArea ga; //usercontrolból vezérlő így elhelyezhető


        int populationSize = 100;
        int nbrOfSteps = 10;
        int nbrOfStepsIncrement = 10;
        int generation = 1;

        public Form1()
        {
            InitializeComponent();

            ga = gc.ActivateDisplay(); //ezzel jelennek meg az elemek a játéktéren, ne lássa a felhasználó amíg nem aktyv
            this.Controls.Add(ga); //formon megjelenítés


            //Csak tesztelésre
            //gc.AddPlayer();
            //gc.Start(true); //játkindítás, true miatt az utoljára hozzáadott játékos vezérelhető lesz
            //wasd billentyűkkel irányítás

            gc.GameOver += Gc_GameOver;

            //ha csak ez van, akkor nem túl fejlett a populáció
            for (int i = 0; i < populationSize; i++)
            {
                gc.AddPlayer(nbrOfSteps); //a gép által vezérelt játokosnak megadjuk max hányat léphet
                //véletlenszerű indulás, 10 lépés után megállás
            }

            
            gc.Start(); //nem kell true, mert mindenkit a gép vezérel
        }

        private void Gc_GameOver(object sender)
        {
            generation++;
            label1.Text = string.Format(
                "{0}. generáció",
                generation);


            var playerList = from p in gc.GetCurrentPlayers() //sorbarendezés rátermettség alapján
                             orderby p.GetFitness() descending //getfittness double érték, attól függ milyen közel jutott a játékos a célhoz
                             select p;
            var topPerformers = playerList.Take(populationSize / 2).ToList(); //referencia típusú változó az előbbi lekérdezés, de nme ürül le a lista újraindítással ,új lista generálódik, nem baj, hogy az eredeti lista kiürül
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
