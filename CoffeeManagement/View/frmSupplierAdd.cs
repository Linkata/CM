using CoffeeManagement.Bl;
using CoffeeManagement.TL;
using CrystalDecisions.CrystalReports.ViewerObjectModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CoffeeManagement.View
{
    public partial class frmSupplierAdd : SampleAdd
    {
        private readonly SupplierBL supplierBL;
        public frmSupplierAdd()
        {
            InitializeComponent();
        }
        public int SupplierID { get; set; }

        public void SetSupplier(SupplierTL supplier)
        {
            SupplierID = supplier.ID;
            
            txtSupplierName.Text = supplier.Tên_nhà_cung_cấp;
            txtSupplierPhone.Text = supplier.SDT_nhà_cung_cấp;
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            try
            { // Kiểm tra hoặc thêm SupplierID
                int supplierID = supplierBL.GetOrCreateSupplierID(txtSupplierName.Text, txtSupplierPhone.Text);
                var supplier = new SupplierTL
                {
                    
                    Tên_nhà_cung_cấp = txtSupplierName.Text,
                    SDT_nhà_cung_cấp = txtSupplierPhone.Text,
                    ID = supplierID
                };
                if (supplier.ID > 0)
                {
                    supplierBL.UpdSupplier(supplier);
                }// Cập nhật nếu có ID }
                else
                {
                    supplierBL.AddSupplier(supplier); // Thêm mới nếu không có ID 
                }
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while saving supplier: " + ex.Message);

            }
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
