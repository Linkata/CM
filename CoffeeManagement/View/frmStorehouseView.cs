using BL;
using CoffeeManagement.BL;
using CoffeeManagement.Model;
using CoffeeManagement.TL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
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
        }

        private void LoadMaterials()
        {
            try
            {
                
                var materials = materialBL.GetAllMaterials();
                guna2DataGridView1.DataSource = null;
                guna2DataGridView1.DataSource = materials;
                guna2DataGridView1.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading materials: " + ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var addForm = new frmStorehouseAdd())
            {
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    LoadMaterials();
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadMaterials();
        }
    }
}
