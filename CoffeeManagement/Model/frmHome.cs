using CoffeeManagement.TL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeManagement
{
    public partial class frmHome : Form
    {
        private readonly RevenueTransaction _revenueTransaction;
        public frmHome()
        {
            InitializeComponent();
            _revenueTransaction = new RevenueTransaction(MainClass.GetConnectionString());
        }

        private void btnShowRevenue_Click(object sender, EventArgs e)
        {
            DateTime startDate = dateTimePickerStart.Value;
            DateTime endDate = dateTimePickerEnd.Value;

            try
            {
                // Lấy dữ liệu doanh thu thông qua tầng TL
                DataTable revenueData = _revenueTransaction.GetProcessedRevenueData(startDate, endDate);
                DisplayGunaChart(revenueData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hiển thị doanh thu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnShowProductSales_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu doanh số sản phẩm thông qua tầng TL
                DataTable productSalesData = _revenueTransaction.GetProductSalesData();
                DisplayPieChart(productSalesData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hiển thị doanh số: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DisplayGunaChart(DataTable dt)
        {
            guna2ChartRevenue.Datasets.Clear();

            var columnSeries = new Guna.Charts.WinForms.GunaBarDataset
            {
                Label = "Doanh thu hàng ngày"
            };

            foreach (DataRow row in dt.Rows)
            {
                string date = Convert.ToDateTime(row["aDate"]).ToString("dd/MM/yyyy");
                double revenue = Convert.ToDouble(row["Tổng doanh thu"]);
                columnSeries.DataPoints.Add(date, revenue);
            }

            guna2ChartRevenue.Datasets.Add(columnSeries);
            guna2ChartRevenue.Update();
        }
        private void DisplayPieChart(DataTable dt)
        {
            guna2ChartSale.Datasets.Clear();
            guna2ChartSale.Legend.Position = Guna.Charts.WinForms.LegendPosition.Right;
            guna2ChartSale.XAxes.Display = false;
            guna2ChartSale.YAxes.Display = false;

            var pieSeries = new Guna.Charts.WinForms.GunaPieDataset
            {
                Label = "Doanh số"
            };

            foreach (DataRow row in dt.Rows)
            {
                string productName = row["pName"].ToString();
                double sales = Convert.ToDouble(row["Tổng doanh số"]);
                pieSeries.DataPoints.Add(productName, sales);
            }

            guna2ChartSale.Datasets.Add(pieSeries);
            guna2ChartSale.Update();
        }
    }
}