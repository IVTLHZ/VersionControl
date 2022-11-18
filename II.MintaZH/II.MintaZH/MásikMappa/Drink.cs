using II.MintaZH.Mappa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace II.MintaZH.MásikMappa
{
    public class Drink : Product
    {
        protected override void Display()
        {
           BackColor = System.Drawing.Color.LightBlue;
        }
    }
}
