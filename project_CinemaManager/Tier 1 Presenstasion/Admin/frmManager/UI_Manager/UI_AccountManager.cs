using DB;
using Application;
using System;
using System.Linq;
using System.Windows.Forms;

namespace frmAdminUserControls
{
    public partial class AccountUC : UserControl
    {
        BindingSource accountList = new BindingSource();

        public AccountUC()
        {
            InitializeComponent();
            LoadAccount();
        }

        void LoadAccount()
        {
            dtgvAccount.DataSource = accountList;
            LoadAccountList();
            AddAccountBinding();
            HideUnessecsary();
        }

        private void HideUnessecsary()
        {        
                for (int i = 0; i < dtgvAccount.Columns.Count; i++)
                {
                    if (dtgvAccount.Columns[i].HeaderText == "Pass")
                    {
                        dtgvAccount.Columns[i].Visible = false;
                    }
                }           
        }

        void LoadAccountList()
        {
            accountList.DataSource = AccountDB.GetAccountList();
        }
  
        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            LoadAccountList();
        }

        void AddAccountBinding()
        {
            txtUsername.DataBindings.Add("Text", dtgvAccount.DataSource, "Username", true, DataSourceUpdateMode.Never);
            txtType_Account.DataBindings.Add("Text", dtgvAccount.DataSource, "Loại tài khoản", true, DataSourceUpdateMode.Never);
            txtID_Account.DataBindings.Add("Text", dtgvAccount.DataSource, "ID", true, DataSourceUpdateMode.Never);
            txtName_Customer.DataBindings.Add("Text", dtgvAccount.DataSource, "Họ Tên", true, DataSourceUpdateMode.Never);
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            string AccountID = (string)dtgvAccount.SelectedCells[0].OwningRow.Cells["ID"].Value;
            Account account = AccountDB.GetAccountByID(AccountID);//The Account that we're currently selecting
        }

        void InsertAccount(string username, string Pass, int accountType, string idAccount)
        {
            if (AccountDB.InsertAccount(username, Pass, accountType, idAccount))
            {
                MessageBox.Show("Thêm tài khoản thành công, mật khẩu mặc định : 1");
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại");
            }
        }
        private void btnInsertAccount_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            int accountType = int.Parse(txtType_Account.Text);
            string AccountID = txtID_Account.Text;
            InsertAccount(username,"1", accountType, AccountID);
            LoadAccountList();
        }

        void UpdateAccount(string username, int accountType)
        {
            if (AccountDB.UpdateAccount(username, accountType))
            {
                MessageBox.Show("Sửa tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Sửa tài khoản thất bại");
            }
        }
        private void btnUpdateAccount_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            int accountType = int.Parse(txtType_Account.Text);
            UpdateAccount(username, accountType);
            LoadAccountList();
        }

        void DeleteAccount(string username)
        {
            if (AccountDB.DeleteAccount(username))
            {
                MessageBox.Show("Xóa tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Xóa tài khoản thất bại");
            }
        }
        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            DeleteAccount(username);
            LoadAccountList();
        }

        void ResetPassword(string username)
        {
            if (AccountDB.ResetPassword(username))
            {
                MessageBox.Show("Reset mật khẩu thành công, mật khẩu mặc định : 1");
            }
            else
            {
                MessageBox.Show("Reset mật khẩu thất bại");
            }
        }
        private void btnResetPass_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            ResetPassword(username);
            LoadAccountList();
        }

        private void btnSearchAccount_Click(object sender, EventArgs e)
        {
            string AccountName = txtSearchAccount.Text;
            accountList.DataSource = AccountDB.SearchAccount(AccountName);
        }

		private void txtSearchAccount_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				btnSearchAccount.PerformClick();
				e.SuppressKeyPress = true;//Tắt tiếng *ting của windows
			}
		}
	}
}
