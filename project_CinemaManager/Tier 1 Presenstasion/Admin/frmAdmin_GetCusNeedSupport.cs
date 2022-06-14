using Application;
using DB;
using System;
using System.Threading;
using System.Windows.Forms;

namespace project_CinemaManager
{
    public partial class frmAdmin_GetCusNeedSupport : Form
    {
        private Thread refresher;

        public frmAdmin_GetCusNeedSupport()
        {
            InitializeComponent();
            LoadCusNeedSP();
            ConfiDataGV();
            lbname.Text += account.UserName;
            refresher = new Thread(ThreadRefreshData);
            refresher.IsBackground = true;
            refresher.Start();
        }

        private void ThreadRefreshData()
        {
            try
            {
                while (true)
                {
                    if (IsHandleCreated)
                    {
                        if (InvokeRequired)
                            dtgvMessage.Invoke(new Action(() =>
                            {
                                LoadCusNeedSP();
                            }));
                        else
                            LoadCusNeedSP();
                    }
                    else
                    {
                    }
                    Thread.Sleep(Properties.Settings.Default.RefreshTimeOut);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Account account = frmAdmin.loginAccount;

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

        private string selectedRow(string attr)
        {
            try
            {
                string value;
                int selectedrowindex = dtgvMessage.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dtgvMessage.Rows[selectedrowindex];
                value = Convert.ToString(selectedRow.Cells[attr].Value);
                return value;
            }
            catch (Exception)
            {
                return "";
            }
        }

        private void btnChooseToSupport_Click(object sender, EventArgs e)
        {
            try
            {
                frmAdmin_SupportCus supportCus = new frmAdmin_SupportCus(selectedRow("Khách Hàng Cần Hỗ Trợ"));
                this.Hide();
                supportCus.ShowDialog();
                this.Show();
                LoadCusNeedSP();
            }
            catch (Exception)
            {
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            MessagesDB.DeleteMessage(selectedRow("Khách Hàng Cần Hỗ Trợ"));
            LoadCusNeedSP();
        }
    }
}