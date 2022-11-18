using FactoryAgain.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryAgain.Entities
{
    //public kell hogy legyen!
    public class Ball : Toy 
    {
        //labda színe kell, ez alapján rajzolá
        public SolidBrush BallColor { get; private set; } //private set, hogy kívülről csak lekérdezni lehessen a property értéket

        public Ball(Color color)
        {
            BallColor = new SolidBrush(color);
        }


        //nem elég fv írása amiért ennyi az őstől az eltérés, felülírás kell

        //private: elem csak osztályon belül kezelhető
        //a protected void-ban a fv kívülről nem férhető hozzá, de a Ball osztály minden leszármazottja használhatja

        //absztrakt fv-ek és implementációjuk nem lehet private, protected oké, hogy kívülről ne legyen látható
        //virtual metódus felülírására is override kell

        protected override void DrawImage(Graphics g)
        {
            //vezérlőbe illeszkedő kitöltött kék kör
            //Graphics osztály segít ebben
            //g.FillEllipse(new SolidBrush(Color.Blue), 0, 0, Width, Height); //width és height az ellipszis köré rakott téglalapé, ami most a balltól jön(?)
            g.FillEllipse(BallColor, 0, 0, Width, Height);
        }

    }
}
