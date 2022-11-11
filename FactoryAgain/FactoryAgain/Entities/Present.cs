using FactoryAgain.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryAgain.Entities
{
    public class Present : Toy
    {
        //2 bemenő paraméter állításával kitöltőszínes
       
        public SolidBrush BoxColor { get; private set; }
        public SolidBrush RibbonColor { get; private set; }

        public Present(Color box, Color ribbon)
        {
            BoxColor = new SolidBrush(box);
            RibbonColor = new SolidBrush(ribbon);
        }

        protected override void DrawImage(Graphics g)
        {
            g.FillRectangle(BoxColor, 0, 0, 50, 50);
            g.FillRectangle(RibbonColor, 0, Width/2 -5, 50, 50/5);
            g.FillRectangle(RibbonColor, Width/2 -5, 0, 50/5, 50);
        }
    }
}
