using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DB;
using Application;

namespace project_CinemaManager
{

    public partial class frm_Conectingdatabase : Form
    {
        public frm_Conectingdatabase()
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
            if (e.KeyCode==Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }

        private void frm_Conectingdatabase_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
