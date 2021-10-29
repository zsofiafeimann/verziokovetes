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
using WebService.MnbServiceReference;

namespace WebService
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            GetExchangeRates();
        }

        private void GetExchangeRates()
        {
            MNBArfolyamServiceSoapClient mnbService = new MNBArfolyamServiceSoapClient();

            GetExchangeRatesRequestBody request = new GetExchangeRatesRequestBody();

            request.currencyNames = "EUR";
            request.startDate = "2020-01-01";
            request.endDate = "2020-06-30";

            mnbService.GetExchangeRates(request);

            var response = mnbService.GetExchangeRates(request);
            string result = response.GetExchangeRatesResult;
            File.WriteAllText("export.xml", result);
        }


    }
}
