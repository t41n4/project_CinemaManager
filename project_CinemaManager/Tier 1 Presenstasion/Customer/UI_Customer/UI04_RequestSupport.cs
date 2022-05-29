using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    public partial class UI04_RequestSupport : Form
    {
        Thread refresher;
        Account account = UICustomerInfo.loginAccount;
        public UI04_RequestSupport()
        {
            InitializeComponent();
            LoadMessage();
            HideUneseccaryColumn();
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
        private void HideUneseccaryColumn()
        {
            for (int i = 0; i < dtgvMessage.Columns.Count; i++)
            {
           
                if (dtgvMessage.Columns[i].HeaderText == "ThờI gian gửi")
                {
                    dtgvMessage.Columns[i].DefaultCellStyle.Format = "MM/dd hh:mm tt";
                }
            }
        }
        private void LoadMessage()
        {
           
            dtgvMessage.DataSource = MessagesDB.GetMessagesTable(account.UserName);          
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {

            if (MessagesDB.InsertMessage(txtMessage.Text, account.UserName, "admin", DateTime.Now))
            {
                txtMessage.Text = "";
            }    
            else
            {
                MessageBox.Show("Gửi Thất Bại!!");
            }
            LoadMessage();
        }
    }
}
