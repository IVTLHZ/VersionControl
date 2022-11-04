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
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using System.Xml.Linq;

namespace Gyak06
{
    public partial class Form1 : Form
    {
        MNBArfolyamServiceSoapClient mnbService = new MNBArfolyamServiceSoapClient();

        BindingList<RateDate> Rates = new BindingList<RateDate>();
        BindingList<string> Currencies = new BindingList<string>();

        public Form1()
        {
            InitializeComponent();
            IkszEmEl(Lekerdezes());
            comboBox1.DataSource = Currencies;

            RefreshData();
        }

        private void IkszEmEl2(string result)
        {
            XmlDocument xml = new XmlDocument();

            xml.LoadXml(result);

            //Childnodes 0 fontos!!!
            foreach (XmlElement item in xml.DocumentElement.ChildNodes[0]) 
            {
                string currency;
                if (item.ChildNodes[0] == null)
                    continue;
                currency = item.ChildNodes[0].InnerText;
                Currencies.Add(currency);
            }

            
        }

        private string Lekerdezes2()
        {
            //elérhető valuták listája
            GetCurrenciesRequestBody request = new GetCurrenciesRequestBody();

            var response = mnbService.GetCurrencies(request);

            //Currencies lista feltöltéséhez
            var result = response.GetCurrenciesResult;
            return result;
        }

        private void RefreshData()
        {
            Rates.Clear();
         
            IkszEmEl(Lekerdezes());
            
            Diagram();
        }

        private void Diagram()
        {
            chartRateDate.DataSource = Rates;
            // chart adatsorokból álló tömb első eleme
            var elsőElem = chartRateDate.Series[0];
            elsőElem.ChartType = SeriesChartType.Line;
            //ezek az elemek már ratedate-ek, adott tulajdonság átadása sima ""-el
            elsőElem.XValueMember = "Date";
            elsőElem.YValueMembers ="Value";
            //kétszeres adatsorvastagság
            elsőElem.BorderWidth = 2;
            //oldasló legend címke ne látszódjon
            chartRateDate.Legends[0].Enabled = false;
            //fő grid vonalak ne látszanak
            chartRateDate.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartRateDate.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            //y tengely ne 0-tól induljon
            chartRateDate.ChartAreas[0].AxisY.IsStartedFromZero = false;
        }

        public string Lekerdezes()
        {
            GetExchangeRatesRequestBody request = new GetExchangeRatesRequestBody();
            //request.currencyNames = "EUR";

            //ITT NEM JÓ VALAMI
            request.currencyNames = comboBox1.SelectedItem.ToString(); //de próbából valuemember és text EUR lett
            
            //request.startDate = "2020-01-01";
            //request.endDate = "2020-06-30";
            request.startDate = dateTimePicker1.Value.ToString();
            request.endDate = dateTimePicker2.Value.ToString();

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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
