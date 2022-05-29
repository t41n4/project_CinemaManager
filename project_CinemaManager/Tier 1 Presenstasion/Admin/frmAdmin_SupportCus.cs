using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application;
using DB;

namespace project_CinemaManager
{
    public partial class frmAdmin_SupportCus : Form
    {
        string customer;
        Thread refresher;
        public frmAdmin_SupportCus(string cus)
        {
            this.customer = cus;
            InitializeComponent();
            LoadMessage();
            ConfigColumn();
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
                                LoadMessage();
                            }));
                        else
                            LoadMessage();

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

        Account account = frmAdmin.loginAccount;

        private void ConfigColumn()
        {
            for (int i = 0; i < dtgvMessage.Columns.Count; i++)
            {

                if (dtgvMessage.Columns[i].HeaderText == "Người nhận")
                {
                    dtgvMessage.Columns[i].Visible = false;
                }
                if (dtgvMessage.Columns[i].HeaderText == "ThờI gian gửi")
                {
                    dtgvMessage.Columns[i].DefaultCellStyle.Format = "MM/dd hh:mm tt";
                }
            }
        }
        private void LoadMessage()
        {

            dtgvMessage.DataSource = MessagesDB.GetMessagesTable(customer);
        }

       private void SendMessage()
        {
            if (MessagesDB.InsertMessage(txtMessage.Text, account.UserName, customer, DateTime.Now))
            {
                txtMessage.Text = "";
            }
            else
            {
                MessageBox.Show("Gửi Thất Bại!!");
            }
            LoadMessage();
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {

            SendMessage();
        }

        private void frmAdmin_SupportCus_Load(object sender, EventArgs e)
        {

        }

        private void frmAdmin_SupportCus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter )
            {
                SendMessage();
            }    
        }
    }
}
