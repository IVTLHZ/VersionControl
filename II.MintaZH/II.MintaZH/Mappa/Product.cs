using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace II.MintaZH.Mappa
{
    public abstract class Product : Button
    {
        private string title;

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                Text = Title;
            }
        }

        private int cal;

        public int Calories
        {
            get { return cal; }
            set
            {
                cal = value;
                Display();
            }
        }

        protected abstract void Display();

        public Product()
        {
            Width = 150;
            Height = 50;
        }
    }
}
