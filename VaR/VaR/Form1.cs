﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VaR.Entities;

namespace VaR
{
    public partial class Form1 : Form
    {
        PortfolioEntities context = new PortfolioEntities();

        List<Tick> Ticks;

        List<PortfolioItem> Portfolio = new List<PortfolioItem>();


        public Form1()
        {
            InitializeComponent();

            Ticks = context.Ticks.ToList();

            dataGridView1.DataSource = Ticks;

            CreatePortfolio();



            //int elemszám = Portfolio.Count();

            //decimal részvényekSzáma = (from x in Portfolio
            //                           select x.Volume).Sum();

            //var otp = from x in Ticks
            //          where x.Index.Equals("OTP")
            //          select new
            //          {
            //              x.Index,
            //              x.Price
            //          };

            //Console.WriteLine("OTP darabszám:" + otp.Count().ToString());
            //var top = from o in otp
            //          where o.Price > 5000
            //          select o;
            //Console.WriteLine("OTP darabszám:" + otp.Count().ToString());

            //var topsum = (from t in top
            //              select t.Price).Sum();

            //DateTime minDátum = (from x in Ticks
            //                     select x.TradingDay).Min();

            //DateTime maxDátum = (from x in Ticks
            //                     select x.TradingDay).Max();

            //int elteltNapokSzáma = (maxDátum - minDátum).Days;

            //Console.WriteLine((maxDátum - minDátum).ToString());

            //var kapcsolt =
            //                from
            //                x in Ticks
            //                join
            //                y in Portfolio
            //                on x.Index equals y.Index
            //                select new
            //                {
            //                    Index = x.Index,
            //                    Date = x.TradingDay,
            //                    Value = x.Price,
            //                    Volume = y.Volume
            //                };
            //    dataGridView1.DataSource = kapcsolt.ToList();


                                }

        private void CreatePortfolio()
        {
            Portfolio.Add(new PortfolioItem() { Index = "OTP", Volume = 10 });
            Portfolio.Add(new PortfolioItem() { Index = "ZWACK", Volume = 10 });
            Portfolio.Add(new PortfolioItem() { Index = "ELMU", Volume = 10 });

            dataGridView2.DataSource = Portfolio;
        }
        private decimal GetPortfolioValue(DateTime date)
        {
            decimal value = 0;
            foreach (var item in Portfolio)
            {
                var last = (from x in Ticks
                            where item.Index == x.Index.Trim()
                               && date <= x.TradingDay
                            select x)
                            .First();
                value += (decimal)last.Price * item.Volume;
            }
            return value;
        }
    }
}
