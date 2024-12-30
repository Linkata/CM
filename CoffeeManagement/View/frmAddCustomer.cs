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
    public partial class frmAddCustomer : Form
    {
        public frmAddCustomer()
        {
            InitializeComponent();
        }
        public string orderType = "";
        public int driverID = 0;
        public string cusName = "";

        public int mainID = 0;

        private void frmAddCustomer_Load(object sender, EventArgs e)
        {
            if(orderType =="Mang đi")
            {
                lblDriver.Visible = false;
                cbDriver.Visible = false;
            }
            string qry = "Select staffID 'id', sName 'name' from staff where sRole like 'Tài xế'";
            MainClass.CBFill(qry, cbDriver);
            if (mainID > 0)
            {
                cbDriver.SelectedValue = driverID;
            }
        }

        private void cbDriver_SelectedIndexChanged(object sender, EventArgs e)
        {
            driverID = Convert.ToInt32(cbDriver.SelectedValue);
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Show the label as an alert on screen
            label2.Text = "Lưu thành công!";  // Set the alert text
            label2.Visible = true;            // Make the label visible

            // Optionally, hide the label after a few seconds
            Timer timer = new Timer();
            timer.Interval = 3000; // 3 seconds
            timer.Tick += (s, args) =>
            {
                label2.Visible = false; // Hide the label after 3 seconds
                timer.Stop();             // Stop the timer
            };
            timer.Start(); // Start the timer
        }


    }
}
