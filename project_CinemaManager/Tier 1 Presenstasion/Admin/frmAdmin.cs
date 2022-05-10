using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using DAO;

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

        void ChangeAccount(int type)
        {
            if (loginAccount.Type == 2) btnAdmin.Enabled = false;
            lblAccountInfo.Text += LoginAccount.UserName;
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {

        }

        private void btnSeller_Click(object sender, EventArgs e)
        {

        }
    }
}
