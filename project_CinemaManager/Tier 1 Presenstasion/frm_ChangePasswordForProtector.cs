using System;
using System.Windows.Forms;

namespace project_CinemaManager
{
    public partial class frm_ChangePasswordForProtector : Form
    {
        public frm_ChangePasswordForProtector()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (txtNewPass.Text != "" && txtOldPass.Text != "")
            {
                try
                {
                    if (txtOldPass.Text == Properties.Settings.Default.appPassword)
                    {
                        Properties.Settings.Default.appPassword = txtNewPass.Text;
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

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnConfirm.PerformClick();
            }
        }
    }
}