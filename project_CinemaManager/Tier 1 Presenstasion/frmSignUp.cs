using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAO;
using DTO;
namespace project_CinemaManager
{
    public partial class frmSignUp : Form
    {
        string ID;
        public frmSignUp()
        {
            InitializeComponent();
        }
        private void btnCofirm_Click(object sender, EventArgs e)
        {
            ID = RandomString(4);

            if (!CheckInfoCustomer())
                return;

            SignupAccount(txtUserName.Text,txtPassword.Text);
            InsertCustomer(ID,txtFullName.Text, DateTime.Parse(txtBirth.Text),txtAddress.Text,txtSDT.Text,int.Parse(txtCMND.Text));
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

        void SignupAccount(string username,string Pass)
        {
            string showPass = Pass;
            Pass =AccountDAO.PasswordEncryption(Pass);

            if (AccountDAO.InsertAccount(username, Pass, 2 , ID))
            {
                MessageBox.Show("Thêm tài khoản:"  + username+  "thành công, mật khẩu:" + showPass);
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại");
            }
        }
        void InsertCustomer(string id, string hoTen, DateTime ngaySinh, string diaChi, string sdt, int cmnd)
        {
           
            if (CustomerDB.InsertCustomer(id, hoTen, ngaySinh, diaChi, sdt, cmnd))
            {
                MessageBox.Show("Thêm khách hàng thành công");
            }
            else
            {
                MessageBox.Show("Thêm khách hàng thất bại");
            }
        }



    }
}
