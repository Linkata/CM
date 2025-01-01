using System;
using System.Windows.Forms;
using log4net;

namespace CoffeeManagement
{
    public partial class frmPrint : Form
    {
        public frmPrint()
        {
            InitializeComponent();
        }

        private void frmPrint_Load(object sender, EventArgs e)
        {
            btnMax.PerformClick();
        }
    }
}
