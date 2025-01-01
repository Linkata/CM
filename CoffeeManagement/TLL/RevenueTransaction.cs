using CoffeeManagement.DL;
using System;
using System.Data;

namespace CoffeeManagement.TL
{
    public class RevenueTransaction
    {
        private readonly RevenueModel _revenueModel;

        public RevenueTransaction(string connectionString)
        {
            _revenueModel = new RevenueModel(connectionString);
        }

        public DataTable GetProcessedRevenueData(DateTime startDate, DateTime endDate)
        {
            // Lấy dữ liệu doanh thu từ DL
            return _revenueModel.GetRevenueData(startDate, endDate);
        }

        public DataTable GetProductSalesData()
        {
            // Lấy dữ liệu doanh số sản phẩm từ DL
            return _revenueModel.GetProductSalesData();
        }
    }
}
