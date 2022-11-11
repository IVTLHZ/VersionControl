using FactoryAgain.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryAgain.Entities
{
    public class Car : Toy //összes implementálandó fv váza létrejön új toy behivatlkozáskor névtér hozzáadásával
    {
        protected override void DrawImage(Graphics g)
        {
            Image imageFile = Image.FromFile("Images/car.png");
            g.DrawImage(imageFile, new Rectangle(0, 0, Width, Height));
        }
    }
}
