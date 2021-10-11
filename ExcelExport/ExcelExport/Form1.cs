using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;


namespace ExcelExport
{
    public partial class Form1 : Form
    {

        List<Flat> Flats;
        RealEstateEntities context = new RealEstateEntities();

        Excel.Application xlApp; // A Microsoft Excel alkalmazás
        Excel.Workbook xlWB; // A létrehozott munkafüzet
        Excel.Worksheet xlSheet; // Munkalap a munkafüzeten belül

        public Form1()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            List<Flat> Flats = context.Flats.ToList();
        }

        private void CreateExel()
        {
            try
            {
                xlApp = new Excel.Application(); //elinditja az excelt és betölti az applikációt

                xlWB = xlApp.Workbooks.Add(Missing.Value); //új münkafzetet hoz létre

                xlSheet = xlWB.ActiveSheet; //új munkalapot hoz létre

                // CreateTable();

                xlApp.Visible = true;
                xlApp.UserControl = true;  //kontroll átadása a felhasználónak
            }
            catch (Exception ex)
            {
                string error = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);

                MessageBox.Show(error, "Error");

                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }

        }
    }
}
