using System;
using System.Windows.Forms;

namespace project_CinemaManager
{
    public partial class frm_ProtectConectingdatabase : Form
    {
        public frm_ProtectConectingdatabase()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == Properties.Settings.Default.appAccount && txtPassword.Text == Properties.Settings.Default.appPassword)
            {
                frmTestConnection frmTestConnection = new frmTestConnection();
                frmTestConnection.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Sai Tài Khoản Vui Lòng Nhập Lại");
            }
            //btnLogin.Enabled = false;
            //string userName = txtUsername.Text;
            //string passWord = txtPassword.Text;

            //int result = Login(userName, passWord);
            //if (result == 1)
            //{
            //    Account loginAccount = AccountDB.GetAccountByUserName(userName);

            //    if (loginAccount.Type == 1)
            //    {
            //        frmTestConnection frmTestConnection = new frmTestConnection();
            //        frmTestConnection.ShowDialog();
            //        this.Close();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Bạn Không thể truy cập bằng loại tài khoản này!!!!", "THÔNG BÁO");
            //    }
            //}
            //else if (result == 0)
            //{
            //    MessageBox.Show("SAI TÊN TÀI KHOẢN HOẶC MẬT KHẨU!!!!", "THÔNG BÁO");
            //}
            //else
            //{
            //    MessageBox.Show("KẾT NỐI THẤT BẠI", "THÔNG BÁO");
            //}
            //btnLogin.Enabled = true;
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}