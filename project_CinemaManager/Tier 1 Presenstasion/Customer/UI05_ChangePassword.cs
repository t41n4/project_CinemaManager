using DB;
using System;
using System.Windows.Forms;

namespace project_CinemaManager
{
    public partial class UI05_ChangePassword : Form
    {
        private string usn;

        public UI05_ChangePassword()
        {
            InitializeComponent();
            usn = UICustomerInfo.loginAccount.UserName;
        }

        public UI05_ChangePassword(string usernameFromadmin)
        {
            usn = usernameFromadmin;
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (txtNewPass.Text != "" && txtOldPass.Text != "")
            {
                try
                {
                    if (AccountDB.UpdatePasswordForAccount(usn, txtOldPass.Text, txtNewPass.Text))
                    {
                        MessageBox.Show("Đổi Mật Khẩu Thành Công!!");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Đổi Mật Khẩu Thất Bại!!");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Đổi Mật Khẩu Thất Bại!!");
                }
            }
            else
            {
                MessageBox.Show("Xin Hãy Nhập Mật Khẩu!!");
            }
        }

        private void btnConfirm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            }
        }

        private void txtNewPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                btnConfirm.PerformClick();
            }
        }
    }
}