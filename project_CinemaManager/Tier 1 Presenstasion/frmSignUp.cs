using DB;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace project_CinemaManager
{
    public partial class frmSignUp : Form
    {
        private string ID;

        public frmSignUp()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool CheckInfoCustomer()
        {
            DateTime dateTime;
            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                MessageBox.Show("Xin hãy nhập Địa Chỉ!");
                return false;
            }
            else if (string.IsNullOrEmpty(txtBirth.Text) || !DateTime.TryParse(txtBirth.Text, out dateTime))
            {
                if (string.IsNullOrEmpty(txtBirth.Text))
                    MessageBox.Show("Xin hãy nhập Ngày Sinh ");
                else
                    MessageBox.Show("Ngày Sinh Không Hợp Lệ ");

                return false;
            }
            else if (string.IsNullOrEmpty(txtCMND.Text))
            {
                MessageBox.Show("Xin hãy nhập CMND!");
                return false;
            }
            else if (string.IsNullOrEmpty(txtFullName.Text))
            {
                MessageBox.Show("Xin hãy nhập Họ Và Tên!");
                return false;
            }
            else if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Xin hãy nhập Mật Khẩu!");
                return false;
            }
            else if (string.IsNullOrEmpty(txtSDT.Text))
            {
                MessageBox.Show("Xin hãy nhập SDT!");
                return false;
            }
            else if (string.IsNullOrEmpty(txtUserName.Text))
            {
                MessageBox.Show("Xin hãy nhập UserName!");
                return false;
            }
            return true;
        }

        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // source stackoverflow

        private void SignupAccount(string username, string Pass)
        {
            string showPass = Pass;
            Pass = AccountDB.PasswordEncryption(Pass);

            if (AccountDB.InsertAccount(username, Pass, 2, ID))
            {
                MessageBox.Show("Thêm tài khoản:" + username + "thành công, mật khẩu:" + showPass);
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại");
            }
        }

        private bool InsertCustomer(string id, string hoTen, DateTime ngaySinh, string diaChi, string sdt, int cmnd)
        {
            if (CustomerDB.InsertCustomer(id, hoTen, ngaySinh, diaChi, sdt, cmnd))
            {
                MessageBox.Show("Thêm khách hàng thành công");
                return true;
            }
            else
            {
                MessageBox.Show("Thêm khách hàng thất bại");
                return false;
            }
        }

        private void btnCofirm_Click(object sender, EventArgs e)
        {
            ID = RandomString(4);

            if (!CheckInfoCustomer())
                return;

            if (InsertCustomer(ID, txtFullName.Text, DateTime.Parse(txtBirth.Text), txtAddress.Text, txtSDT.Text, int.Parse(txtCMND.Text)))
            {
                SignupAccount(txtUserName.Text, txtPassword.Text);
            }

            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}