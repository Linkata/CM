﻿using CoffeeManagement.Bl;
using CoffeeManagement.BL;
using CoffeeManagement.DL;
using CoffeeManagement.Model;
using CoffeeManagement.TL;
using Guna.Charts.WinForms;
using Guna.UI2.WinForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CoffeeManagement.View
{
    public partial class frmStorehouseView : SampleView
    {
        private readonly MaterialBL materialBL;

        public frmStorehouseView()
        {
            InitializeComponent();
            materialBL = new MaterialBL();
        }

        private void frmStorehouseView_Load(object sender, EventArgs e)
        {
            LoadMaterials();
            customDGV(guna2DataGridView1);
        }

        private void LoadMaterials()
        {
            try
            {
                
                var materials = materialBL.GetAllMaterials();
                guna2DataGridView1.DataSource = null;
                guna2DataGridView1.DataSource = materials;
            guna2DataGridView1.AutoResizeRows();
            }
        private DataGridViewImageColumn CustomImage(string imageName)
            {
            string path = Application.StartupPath;
            var imagePath = Directory.GetParent(path);
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.Image = Image.FromFile(path + "\\Icon\\" + imageName);
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            return imageColumn;
            }

        private DataGridView customDGV(DataGridView dgv)
        {
            dgv.Columns.Clear();
            dgv.AllowUserToAddRows = false;
            dgv.AutoGenerateColumns = true;

            LoadMaterials();
            // Thêm cột hình ảnh "Update"
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.Image = Image.FromFile(Path.Combine(Application.StartupPath, "Icon", "edit_13077251.png"));
            imageColumn.Name = "Update";
            imageColumn.HeaderText = "Update";
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            dgv.Columns.Add(imageColumn);

            // Thêm cột hình ảnh "Delete"
            DataGridViewImageColumn imageColumn1 = new DataGridViewImageColumn();
            imageColumn1.Image = Image.FromFile(Path.Combine(Application.StartupPath, "Icon", "delete_12319558.png"));
            imageColumn1.Name = "Delete";
            imageColumn1.HeaderText = "Delete";
            imageColumn1.ImageLayout = DataGridViewImageCellLayout.Zoom;
            dgv.Columns.Add(imageColumn1);

            return dgv;
        }







        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var addForm = new frmStorehouseAdd())
            {
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    customDGV(guna2DataGridView1);
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var materialDL = new MaterialDL();
            var materials = materialDL.GetMaterials(txtSearch.Text);
            guna2DataGridView1.DataSource = materials;
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "Update")
            {
                frmStorehouseAdd frm = new frmStorehouseAdd();
                int materialID = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["ID"].Value);

                // Lấy thông tin của material từ DataGridView
                MaterialTL material = new MaterialTL
                {
                    ID = materialID,
                    Tên_nguyên_liệu = guna2DataGridView1.CurrentRow.Cells["Tên_nguyên_liệu"].Value.ToString(),
                    Số_lượng = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["Số_lượng"].Value),
                    Đơn_vị = guna2DataGridView1.CurrentRow.Cells["Đơn_vị"].Value.ToString(),
                    Hạn_sử_dụng = Convert.ToDateTime(guna2DataGridView1.CurrentRow.Cells["Hạn_sử_dụng"].Value),
                    SupplierID = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["SupplierID"].Value),
                    Tên_nhà_cung_cấp = guna2DataGridView1.CurrentRow.Cells["Tên_nhà_cung_cấp"].Value.ToString(),
                    SDT_nhà_cung_cấp = guna2DataGridView1.CurrentRow.Cells["SDT_nhà_cung_cấp"].Value.ToString()
                };

                // Truyền thông tin material vào form
                frm.SetMaterial(material);
                MainClass.BlurBackground(frm);

                
                customDGV(guna2DataGridView1); // Tải lại dữ liệu sau khi cập nhật
                
            }

            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "Delete")
            {
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Question;
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo;
                if (guna2MessageDialog1.Show("Bạn có chắc chắn muốn xóa sản phẩm này không?") == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["ID"].Value);
                    string qry = "Delete from Materials where MaterialID=" + id;
                    Hashtable ht = new Hashtable();
                    MainClass.SQL(qry, ht);
                    guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                    guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                    guna2MessageDialog1.Show("Delete successfully");
                    customDGV(guna2DataGridView1); // Tải lại dữ liệu sau khi xóa
                }
            }
        }

        private void btnWarehouseHistory_Click(object sender, EventArgs e)
        {
            frmStorehouseHistoryView frm = new frmStorehouseHistoryView(); 
            frm.ShowDialog();
        }
    }
}
