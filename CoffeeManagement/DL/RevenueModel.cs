using System;
using System.Data;
using System.Data.SqlClient;

namespace CoffeeManagement.DL
{
    public class RevenueModel
    {
        private readonly string _connectionString;

        public RevenueModel(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable GetRevenueData(DateTime startDate, DateTime endDate)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT aDate, SUM(amount) AS [Tổng doanh thu]
                    FROM tblMain m
                    JOIN tblDetails d ON m.MainID = d.MainID
                    WHERE aDate BETWEEN @startDate AND @endDate
                    GROUP BY aDate
                    ORDER BY aDate";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);

                    DataTable dt = new DataTable();
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    con.Close();

                    return dt;
                }
            }
        }

        public DataTable GetProductSalesData()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT pName, SUM(qty) AS [Tổng doanh số]
                    FROM tblDetails d
                    JOIN products p ON p.pID = d.proID
                    GROUP BY pName
                    ORDER BY pName";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    DataTable dt = new DataTable();
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    con.Close();

                    return dt;
                }
            }
        }
    }
}
