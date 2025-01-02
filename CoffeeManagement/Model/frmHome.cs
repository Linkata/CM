using CoffeeManagement.TL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeManagement
{
    public partial class frmHome : Form
    {
        private readonly RevenueTransaction _revenueTransaction = new RevenueTransaction(MainClass.GetConnectionString());
        public frmHome()
        {
            InitializeComponent();
            DataTable productSalesData = _revenueTransaction.GetProductSalesData();
            DataTable revenueData = _revenueTransaction.GetProcessedRevenueData(DateTime.Parse("10/1/2024"), DateTime.Now);
            DataTable profitData = _revenueTransaction.GetProfitData(DateTime.Parse("10/1/2024"), DateTime.Now); // Fetch profit data
            DataTable customerData = _revenueTransaction.GetCustomerData(DateTime.Parse("10/1/2024"), DateTime.Now); // Fetch customer data

            try
            {
                DisplayGunaChart(revenueData);
                DisplayPieChart(productSalesData);
                DisplayProfitChart(profitData); // Display profit chart
                DisplayCustomerChart(customerData); // Display customer chart
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hiển thị doanh thu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                Label = "Doanh số sản phẩm"
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

        // New method to display profit chart
        private void DisplayProfitChart(DataTable dt)
        {
            guna2ChartProfit.Datasets.Clear();

            var columnSeries = new Guna.Charts.WinForms.GunaBarDataset
            {
                Label = "Lợi nhuận hàng ngày"
            };

            foreach (DataRow row in dt.Rows)
            {
                string date = Convert.ToDateTime(row["aDate"]).ToString("dd/MM/yyyy");
                double profit = Convert.ToDouble(row["Tổng lợi nhuận"]);
                columnSeries.DataPoints.Add(date, profit);
            }

            guna2ChartProfit.Datasets.Add(columnSeries);
            guna2ChartProfit.Update();
        }

        // New method to display customer chart
        private void DisplayCustomerChart(DataTable dt)
        {
            guna2ChartCustomer.Datasets.Clear();

            var columnSeries = new Guna.Charts.WinForms.GunaBarDataset
            {
                Label = "Số lượng khách hàng"
            };

            foreach (DataRow row in dt.Rows)
            {
                string date = Convert.ToDateTime(row["aDate"]).ToString("dd/MM/yyyy");
                int customerCount = Convert.ToInt32(row["Tổng khách hàng"]);
                columnSeries.DataPoints.Add(date, customerCount);
            }

            guna2ChartCustomer.Datasets.Add(columnSeries);
            guna2ChartCustomer.Update();
        }

        private void frmHome_Load(object sender, EventArgs e)
        {

        }

        private void guna2ChartRevenue_Load(object sender, EventArgs e)
        {

        }

        private void guna2ChartSale_Load(object sender, EventArgs e)
        {

        }
    }
}
