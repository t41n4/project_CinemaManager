using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application;
using DB;


namespace project_CinemaManager
{
    public partial class frmAdmin_GetCusNeedSupport : Form
    {
        public frmAdmin_GetCusNeedSupport()
        {
            InitializeComponent();
            LoadCusNeedSP();
            ConfiDataGV();
            lbname.Text += account.UserName;      
        }


  
        Account account = frmAdmin.loginAccount;

        private void ConfiDataGV()
        {
            for (int i = 0; i < dtgvMessage.Columns.Count; i++)
            {
             
                if (dtgvMessage.Columns[i].HeaderText == "Thời gian gửi")
                {
                    dtgvMessage.Columns[i].DefaultCellStyle.Format = "MM/dd hh:mm tt";
                }
            }
        }
        private void LoadCusNeedSP()
        {
            dtgvMessage.DataSource = MessagesDB.GetCustomNeedSupport();
            
        }
        string selectedRow(string attr)
        {
            string value;
            int selectedrowindex = dtgvMessage.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dtgvMessage.Rows[selectedrowindex];
            value = Convert.ToString(selectedRow.Cells[attr].Value);
            return value;
        }
        private void btnChooseToSupport_Click(object sender, EventArgs e)
        {
            frmAdmin_SupportCus supportCus = new frmAdmin_SupportCus(selectedRow("Khách Hàng Cần Hỗ Trợ"));
            this.Hide();
            supportCus.ShowDialog();
            this.Show();
            LoadCusNeedSP();
        }
    }
}
