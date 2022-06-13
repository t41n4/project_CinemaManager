using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            if (txtUsername.Text=="admin" && txtPassword.Text=="admin")
            {
                frmTestConnection frmTestConnection = new frmTestConnection();
                frmTestConnection.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Sai Tài Khoản Vui Lòng Nhập Lại");
            }
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
