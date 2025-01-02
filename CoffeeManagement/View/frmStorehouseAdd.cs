using BL;
using TL;
using CoffeeManagement.TL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeManagement.Model
{
    public partial class frmStorehouseAdd : SampleAdd
    {
        private readonly MaterialBL materialBL;

        public frmStorehouseAdd()
        {
            InitializeComponent();
            materialBL = new MaterialBL();
        }

        public override void  btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra hoặc thêm SupplierID
                int supplierID = materialBL.GetOrCreateSupplierID(txtSupplier.Text, txtSupplierPhone.Text);
                var material = new MaterialTL
                {
                    Tên_nguyên_liệu = txtName.Text,
                    Số_lượng = int.Parse(txtQuantity.Text),
                    Đơn_vị = txtUnit.Text,
                    Hạn_sử_dụng = DateTime.ParseExact(txtExDate.Text, "dd/MM/yyyy", null),
                    Tên_nhà_cung_cấp = txtSupplier.Text,
                    SDT_nhà_cung_cấp = txtSupplierPhone.Text,
                    SupplierID = supplierID
                };

                materialBL.AddMaterial(material);
                MessageBox.Show("Material added successfully with Supplier ID: " + supplierID);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while adding material: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void frmStorehouseAdd_Load(object sender, EventArgs e)
        {

        }
    }
}

