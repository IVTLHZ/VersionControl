using II.MintaZH;
using II.MintaZH.Mappa;
using II.MintaZH.MásikMappa;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace II.MintaZH
{
    public partial class Form1 : Form
    {
        List<Product> _products = new List<Product>();
   
        public Form1()
        {
            InitializeComponent();

           
            ikszemel(Lekerd("Menu.xml"));

            Fv();
        }

        private void Fv()
        {
            var linq = from p in _products orderby p.Title select p;
            var x = 0;
            foreach (var item in linq)
            {                
                Controls.Add(item);
                item.Left = 0; 
                item.Top = item.Height * x + 5;
                x++;
                
            }
        }

        private void ikszemel(string result)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(result); //karakterláncként megvan az xml
            foreach (XmlElement item in xml.DocumentElement)
            {
               
                var name = item.SelectSingleNode("name").InnerText;
                var calories = item.SelectSingleNode("calories").InnerText;
                var description = item.SelectSingleNode("description").InnerText;
                var type = item.SelectSingleNode("type").InnerText;

                if (type == "food")
                {
                    var food = new Food();
                    food.Name = name;
                    food.Description = description;
                    food.Calories = Convert.ToInt16(calories);
                    _products.Add(food);
                }
                else
                {
                    var drink = new Drink();
                    drink.Name = name;
                    drink.Calories = Convert.ToInt16(calories);
                    _products.Add(drink);
                }
                
            }
        }

        public string Lekerd(string xml_olv)
        {           
            StreamReader sr = new StreamReader(xml_olv, Encoding. Default);
            var result = sr.ReadToEnd();
            sr.Close();
                        
            return result;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
