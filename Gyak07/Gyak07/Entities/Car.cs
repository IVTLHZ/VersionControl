using Gyak07.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gyak07.Entities
{
    internal class Car : Toy //összes implementálandó fv váza létrejön új toy behivatlkozáskor névtér hozzáadásával
    {
        protected override void DrawImage(Graphics g)
        {
            Image imageFile = Image.FromFile("Images/car.png"); //oké akkor is ha nem tudom itt nyitogatni az images mappát
            g.DrawImage(imageFile, new Rectangle(0, 0, Width, Height));
        }
    }
}
