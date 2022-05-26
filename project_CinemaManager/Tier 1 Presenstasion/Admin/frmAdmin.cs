using Application;
using System;
using System.Windows.Forms;

namespace project_CinemaManager
{
    public partial class frmAdmin : Form
    {
        private Account loginAccount;

        public frmAdmin(Account loginAccount)
        {
            InitializeComponent();
            this.LoginAccount = loginAccount;
        }

        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount.Type); }
        }

        private void ChangeAccount(int type)
        {
            if (loginAccount.Type == 2) btnAdmin.Enabled = false;
            lblAccountInfo.Text += LoginAccount.UserName;
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmAdminNewDesign frmAdminNewDesign = new frmAdminNewDesign();
            frmAdminNewDesign.ShowDialog();
            this.Show();
        }

 
        private void frmAdmin_Load(object sender, EventArgs e)
        {
        }
    }
}