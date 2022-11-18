using II.MintaZH.Mappa;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace II.MintaZH.MásikMappa
{
    internal class Food : Product
    {
        public string Description { get; set; }

        public Food()
        { Click += Food_Click; }

        protected override void Display()
        {
            
            if (Calories<750)
            {
                BackColor = Color.LightGreen;
            }
            else
            {
                if (Calories>1000)
                {
                    BackColor = Color.Salmon;
                }
                BackColor= Color.LightYellow;
            }
            
        }

        private void Food_Click(object sender, EventArgs e)
        {
            MessageBox.Show(string.Format("{0}\n{1}", Name, Description));
        }
    }
}
