using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DL
{
    public class DataProvider
    {
        private SqlConnection cn;

        public DataProvider()
        {
            string cnStr = @"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\CM.mdf; Integrated Security=True;";
            cn = new SqlConnection(cnStr);
        }

        public static string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            IConfiguration config = builder.Build();
            return config.GetConnectionString("DefaultConnection");
        }

        public void Connect()
        {
            try
            {
                if (cn != null && cn.State != System.Data.ConnectionState.Open)
                {
                    cn.Open();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public void Disconnect()
        {
            if (cn != null && cn.State == System.Data.ConnectionState.Open)
            {
                cn.Close();
            }
        }

        public object MyExecuteScalar(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.CommandType = System.Data.CommandType.Text;

            Connect();
            try
            {
                return (cmd.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                Disconnect();
            }
        }
        public SqlDataReader MyExecuteReader(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.CommandType = System.Data.CommandType.Text;
            Connect();
            try
            {
                return (cmd.ExecuteReader());
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public int MyExecuteNonQuery(string sql, CommandType type)
        {
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.CommandType = type;

            Connect();

            try
            {
                return cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                Disconnect();
            }
        }
    }
}
