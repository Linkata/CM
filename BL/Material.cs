using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL;
using TL;

namespace BL
{
    public class MaterialBL
    {
        string sqlCntString = @"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\CM.mdf; Integrated Security=True;";
        private readonly MaterialDL materialDAL;

        public MaterialBL()
        {
            materialDAL = new MaterialDL();
        }

        // Lấy tất cả nguyên vật liệu (MaterialTL)
        public List<MaterialTL> GetAllMaterials()
        {
            return materialDAL.GetMaterials();
        }


        // Lấy danh sách nguyên vật liệu sắp hết hạn (MaterialTL)
        public List<MaterialTL> GetExpiringMaterials()
        {
            var allMaterials = materialDAL.GetMaterials();
            return allMaterials.Where(m => (m.Hạn_sử_dụng - DateTime.Now).TotalDays <= 3).ToList();
        }

        // Thêm nguyên vật liệu mới (MaterialTL)
        public void AddMaterial(MaterialTL material)
        {
            if (material.Số_lượng < 0)
                throw new Exception("Quantity cannot be negative.");
            materialDAL.AddMaterial(material);
        }

        public int GetOrCreateSupplierID(string supplierName, string supplierPhone)
        {
            string queryCheck = "SELECT SupplierID FROM Suppliers WHERE SupplierName = @SupplierName AND SupplierPhone = @SupplierPhone";
            string queryInsert = "INSERT INTO Suppliers (SupplierName, SupplierPhone) OUTPUT INSERTED.SupplierID VALUES (@SupplierName, @SupplierPhone)";

            using (var connection = new SqlConnection(sqlCntString))
            {
                using (var command = new SqlCommand(queryCheck, connection))
                {
                    command.Parameters.AddWithValue("@SupplierName", supplierName);
                    command.Parameters.AddWithValue("@SupplierPhone", supplierPhone);

                    connection.Open();
                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        return (int)result; // Trả về SupplierID nếu đã tồn tại
                    }
                }

                using (var command = new SqlCommand(queryInsert, connection))
                {
                    command.Parameters.AddWithValue("@SupplierName", supplierName);
                    command.Parameters.AddWithValue("@SupplierPhone", supplierPhone);

                    return (int)command.ExecuteScalar(); // Trả về SupplierID mới tạo
                }
            }
        }

    }
}
