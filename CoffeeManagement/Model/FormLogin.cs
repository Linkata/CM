using BL;
using CoffeeManagement.BL;
using System;
using System.Windows.Forms;

namespace CoffeeManagement
{
    public partial class FormLogin : Form
    {
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
            if (!isValid)
            {
                guna2MessageDialog1.Show("Thông tin tài khoản hoặc mật khẩu sai! Vui lòng nhập lại");
                return;
            }
            // Nếu hợp lệ, chuyển sang form chính
            this.Hide();
            formMain mainForm = new formMain();
            mainForm.Show();     
        }

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }
    }
}
