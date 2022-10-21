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

namespace Gyak06
{
    public partial class Form1 : Form
    {
        MNBArfolyamServiceSoapClient mnbService = new MNBArfolyamServiceSoapClient();

        BindingList<RateDate> Rates = new BindingList<RateDate>();
        public Form1()
        {
            InitializeComponent();

            GetExchangeRatesRequestBody request = new GetExchangeRatesRequestBody();
            request.currencyNames = "EUR";
            request.startDate = "2020-01-01";
            request.endDate = "2020-06-30";

            var response = mnbService.GetExchangeRates(request);

            var result = response.GetExchangeRatesResult;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Rates;
        }
    }
}
