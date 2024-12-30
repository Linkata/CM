using BL;
using CoffeeManagement.BL;
using System;
using System.Windows.Forms;

namespace CoffeeManagement
{
    public partial class FormLogin : Form
    {
        public string currentUsername;
        public string CurrentUsername { get; set; }
        public FormLogin()
        {
            InitializeComponent();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text.Trim();
            string password = txtPass.Text.Trim();

            UserBL userBL = new UserBL();
            bool isValid = userBL.Login(username, password);
            //Login success
            if (isValid)
            {
                // Login success
                currentUsername = username;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult result = MessageBox.Show("Username or password is incorrect", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (result == DialogResult.Cancel)
                {
                    Application.Exit();
                } 
                else
                {
                    txtUser.Clear();
                    txtPass.Clear();
                    txtUser.Focus();
                }
                
            }
 
        }

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
