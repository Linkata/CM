using System;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace DL
{
    public static class DatabaseHelper
    {
        // Lấy chuỗi kết nối từ appsettings.json
        public static string GetConnectionString()
        {
            string cntstr = Environment.GetEnvironmentVariable("ConnectionString");

            IConfiguration config = builder.Build();
            return config.GetConnectionString("DefaultConnection");
        }

        // Thực hiện câu lệnh SQL với tham số
        public static int ExecuteSQL(string qry, Hashtable parameters)
        {
            int result = 0;
            string conString = GetConnectionString();
            using (SqlConnection con = new SqlConnection(conString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(qry, con);
                    foreach (DictionaryEntry param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.Key.ToString(), param.Value);
                    }
                    con.Open();
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error: {ex.Message}");
                }
            }
            return result;
        }

        // Kiểm tra nếu bảng category rỗng
        public static bool IsCategoryTableEmpty()
        {
            bool isEmpty = true;
            string qry = "SELECT COUNT(*) FROM category";
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(qry, con);
                con.Open();
                int count = (int)cmd.ExecuteScalar();
                if (count > 0) isEmpty = false;
            }
            return isEmpty;
        }
    }
}