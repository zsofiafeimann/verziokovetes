﻿using System;
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

        private int _millian = (int)Math.Pow(10, 6);

        List<Flat> Flats;
        RealEstateEntities context = new RealEstateEntities();

        Excel.Application xlApp; // A Microsoft Excel alkalmazás
        Excel.Workbook xlWB; // A létrehozott munkafüzet
        Excel.Worksheet xlSheet; // Munkalap a munkafüzeten belül

        public Form1()
        {
            InitializeComponent();

            LoadData();

            CreateExel();
        }

        public void LoadData()
        {
            Flats = context.Flats.ToList();
        }

        private void CreateExel()
        {
            try
            {
                xlApp = new Excel.Application(); //elinditja az excelt és betölti az applikációt

                xlWB = xlApp.Workbooks.Add(Missing.Value); //új münkafzetet hoz létre

                xlSheet = xlWB.ActiveSheet; //új munkalapot hoz létre

                CreateTable();

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

        private void CreateTable()
        {
            string[] headers = new string[] 
            {
             "Kód",
             "Eladó",
             "Oldal",
             "Kerület",
             "Lift",
             "Szobák száma",
             "Alapterület (m2)",
             "Ár (mFt)",
             "Négyzetméter ár (Ft/m2)"
            };

            for (int i = 0; i < headers.Length; i++)
            {
                xlSheet.Cells[1, i + 1] = headers[i];
            }

            object[,] values = new object[Flats.Count, headers.Length];

            int counter = 0;

            int floorcolumn = 6;



            foreach (var f in Flats)
            {
                values[counter, 0] = f.Code;
                values[counter, 1] = f.Vendor;
                values[counter, 2] = f.Side;
                values[counter, 3] = f.District;
                if (f.Elevator) values[counter, 4] = "Van";
                else values[counter, 4] = "Nincs";
                //values[counter, 4] = f.Elevator;
                values[counter, 5] = f.NumberOfRooms;
                values[counter, 6] = f.FloorArea;
                values[counter, 7] = f.Price;
                values[counter, 8] = string.Format("={0}/{1}*{2}",
                    "H" + (counter + 2).ToString(),
                GetCell(counter + 2, floorcolumn + 1),
                    _millian.ToString());

                counter++;
            }


           
            xlSheet.get_Range(
            GetCell(2, 1),
            GetCell(1 + values.GetLength(0), values.GetLength(1))).Value2 = values;


        }

        private string GetCell(int x, int y)
        {
            string ExcelCoordinate = "";
            int dividend = y;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                ExcelCoordinate = Convert.ToChar(65 + modulo).ToString() + ExcelCoordinate;
                dividend = (int)((dividend - modulo) / 26);
            }
            ExcelCoordinate += x.ToString();

            return ExcelCoordinate;
        }
    }
}
