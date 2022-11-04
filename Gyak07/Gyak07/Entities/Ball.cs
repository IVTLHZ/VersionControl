using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gyak07.Entities
{
    internal class Ball : Label
    {
        public Ball() //hogy tulajdonságot állíthassak rá
        {
            AutoSize = false;
            Width = 50;
            Height = Width;
            Paint += Ball_Paint; //paint += tab tabbal eseménykezelő
        }

        private void Ball_Paint(object sender, PaintEventArgs e)
        {
            DrawImage(e.Graphics); //adott osztályban létrehozott grafikapéldánnyal rajz
            //bármi amit itt a vászonra rajzolunk, megjelenik afelhasználó grafikus felületén
        }

        //private: elem csak osztályon belül kezelhető
        //a protected void-ban a fv kívülről nem férhető hozzá, de a Ball osztály minden leszármazottja használhatja
        protected void DrawImage(Graphics g) 
        {
            //vezérlőbe illeszkedő kitöltött kék kör
            //Graphics osztály segít ebben
            g.FillEllipse(new SolidBrush(Color.Blue), 0, 0, Width, Height); //width és height az ellipszis köré rakott téglalapé, ami most a balltól jön(?)
        }

        //ball arrébb csuszi 1-gyel
        public void MoveBall()
        {
            Left += 1;
        }
    }
}
