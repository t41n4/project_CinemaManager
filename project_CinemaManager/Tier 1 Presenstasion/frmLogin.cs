using Application;
using DB;
using System;
using System.Windows.Forms;

namespace project_CinemaManager
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            btnLogin.Enabled = false;
            string userName = txtUsername.Text;
            string passWord = txtPassword.Text;

            int result = Login(userName, passWord);
            if (result == 1)
            {
                Account loginAccount = AccountDB.GetAccountByUserName(userName);

                if (loginAccount.Type == 1)
                {
                    frmAdmin frm = new frmAdmin(loginAccount);
                    this.Hide();
                    frm.ShowDialog();
                    this.Show();
                }
                else if (loginAccount.Type == 2)
                {
                    UICustomerInfo frm = new UICustomerInfo(loginAccount);
                    this.Hide();
                    frm.ShowDialog();
                    this.Show();
                }
            }
            else if (result == 0)
            {
                MessageBox.Show("SAI TÊN TÀI KHOẢN HOẶC MẬT KHẨU!!!!", "THÔNG BÁO");
            }
            else
            {
                MessageBox.Show("KẾT NỐI THẤT BẠI", "THÔNG BÁO");
            }
            btnLogin.Enabled = true;
        }

        private int Login(string userName, string passWord)
        {
            return AccountDB.Login(userName, passWord);
        }



        private void frmLogin_Load(object sender, EventArgs e)
        {
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            frmSignUp frmSignUp = new frmSignUp();
            this.Hide();
            frmSignUp.ShowDialog();
            this.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmSettings frmSettings = new frmSettings();
            frmSettings.ShowDialog();
        }
    }
}