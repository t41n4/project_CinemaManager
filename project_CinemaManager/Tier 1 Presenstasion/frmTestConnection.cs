﻿using DB;
using System;
using System.Windows.Forms;

namespace project_CinemaManager
{
    public partial class frmTestConnection : Form
    {
        public frmTestConnection()
        {
            InitializeComponent();

            if (Properties.Settings.Default.DataSource != string.Empty && Properties.Settings.Default.InitialCatalog != string.Empty)
            {
                txtServerName.Text = Properties.Settings.Default.DataSource;
                txtDatabaseName.Text = Properties.Settings.Default.InitialCatalog;
            }
        }

        public static string DataSource;
        public static string InitialCatalog;
        public static string UserID;
        public static string pwd;
        public static string connectionSTR;

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            btnConnect.Enabled = false;
            btnExit.Enabled = false;
            if (txtPassword.Text != "")
            {
                connectionSTR = "Data Source=" + txtServerName.Text
                    + ";Initial Catalog=" + txtDatabaseName.Text
                    + ";User ID=" + txtUserName.Text
                    + ";pwd=" + txtPassword.Text;
            }
            else
            {
                connectionSTR = "Data Source=" + txtServerName.Text
                    + ";Initial Catalog=" + txtDatabaseName.Text
                    + ";Integrated Security=True";
            }

            Properties.Settings.Default.DataSource = txtServerName.Text;
            Properties.Settings.Default.InitialCatalog = txtDatabaseName.Text;
            Properties.Settings.Default.UserID = txtUserName.Text;
            Properties.Settings.Default.pwd = txtPassword.Text;
            Properties.Settings.Default.connectionSTR = connectionSTR;
            Properties.Settings.Default.Save();

            bool result = DataProvider.TestConnectionSQL(connectionSTR);
            
            if (result)
                MessageBox.Show("KẾT NỐI THÀNH CÔNG", "THÔNG BÁO");
            else
                MessageBox.Show("KẾT NỐI THẤT BẠI", "THÔNG BÁO");

            btnConnect.Enabled = true;
            btnExit.Enabled = true;
        }
    }

}