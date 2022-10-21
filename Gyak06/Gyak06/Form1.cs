using Gyak06.Entites;
using Gyak06.MnbServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Gyak06
{
    public partial class Form1 : Form
    {
        MNBArfolyamServiceSoapClient mnbService = new MNBArfolyamServiceSoapClient();

        BindingList<RateDate> Rates = new BindingList<RateDate>();
        public Form1()
        {
            InitializeComponent();

            IkszEmEl(Lekerdezes());
        }

        public string Lekerdezes()
        {
            GetExchangeRatesRequestBody request = new GetExchangeRatesRequestBody();
            request.currencyNames = "EUR";
            request.startDate = "2020-01-01";
            request.endDate = "2020-06-30";

            var response = mnbService.GetExchangeRates(request);

            var result = response.GetExchangeRatesResult;
            return result;
        }

        private void IkszEmEl(string result) //majd a lekérdezés által visszaadott értéket hívjuk be a konstruktorból
        {
            XmlDocument xml = new XmlDocument();

            xml.LoadXml(result); //ez az az érték, lekérdezés visszaadott értéke

            //mnb exchange rates fő eleméből(lekérdezett napok) documentelement lekérdezése
            foreach (XmlElement item in xml.DocumentElement) //speciális listából kérdezünk le, var nem elég ide
            {
                RateDate rateDate = new RateDate();
                rateDate.Date =Convert.ToDateTime(item.GetAttribute("date"));
                rateDate.Currency = ((XmlElement)item.ChildNodes[0]).GetAttribute("curr");
                //1 vagy 100 egységnyi valuta értékét nézzük meg Ft-ban
                var unit = Convert.ToInt16(((XmlElement)item.ChildNodes[0]).GetAttribute("unit"));
                var value = Decimal.Parse(((XmlElement)item.ChildNodes[0]).InnerText);
                if (unit != 0) rateDate.Value = value / unit;
                Rates.Add(rateDate);
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Rates;
        }
    }
}
