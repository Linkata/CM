using System;
using System.Data;

namespace CoffeeManagement.BL
{
    public class RevenueLogic
    {
        public decimal CalculateTotalRevenue(DataTable revenueData)
        {
            decimal total = 0;
            foreach (DataRow row in revenueData.Rows)
            {
                total += Convert.ToDecimal(row["Tổng doanh thu"]);
            }
            return total;
        }
    }
}
