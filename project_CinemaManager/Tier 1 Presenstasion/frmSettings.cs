using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;

namespace project_CinemaManager
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }

        private void btn_checkconected_CheckedChanged(object sender, EventArgs e)
        {
            frm_ProtectConectingdatabase frm_Conectingdatabase = new frm_ProtectConectingdatabase();
            frm_Conectingdatabase.ShowDialog();
        }


        RegistryKey regstart = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
        //hàm khỏi động cùng windows
        private void btn_startwithwindow_CheckedChanged(object sender, EventArgs e)
        {

              
                if (btn_startwithwindow.Checked == true)
                {
                    RegistryKey regkey = Registry.CurrentUser.CreateSubKey("Software\\project_CinemaManager");
                    //mo registry khoi dong cung win
                    string keyvalue = "1";

                    try
                    {
                        //chen gia tri key
                        regkey.SetValue("Index", keyvalue);
                        regstart.SetValue("project_CinemaManager", System.Windows.Forms.Application.StartupPath + "\\project_CinemaManager.exe");
                        regkey.Close();

                    }
                    catch (Exception )
                    {
                        
                    }
                }
                else
                {
                    regstart.DeleteValue("project_CinemaManager");

                }

            
        }
    }
}
