using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class LoginDL:DataProvider
    {
        public bool Login(string username, string password)
        {
            string cnStr = @"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\CM.mdf; Integrated Security=True;";

            using (SqlConnection conn = new SqlConnection(cnStr))
            {
                string sql = "Select COUNT(1) FROM users WHERE username = @username AND upass = @password";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
    }
}
