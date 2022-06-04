using Application;
using DB;
using System;
using System.Threading;
using System.Windows.Forms;

namespace project_CinemaManager
{
    public partial class UI04_RequestSupport : Form
    {
        private Thread refresher;
        private Account account = UICustomerInfo.loginAccount;

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