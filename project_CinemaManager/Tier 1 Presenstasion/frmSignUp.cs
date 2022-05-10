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
            SignupAccount(txtUserName.Text);
            InsertCustomer(ID,txtFullName.Text, DateTime.Parse(txtBirth.Text),txtAddress.Text,txtSDT.Text,int.Parse(txtCMND.Text));
        }

        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        // source stackoverflow

        void SignupAccount(string username)
        {
           
            //MessageBox.Show(ID);
            if (AccountDAO.InsertAccount(username, 2, ID))
            {
                MessageBox.Show("Thêm tài khoản thành công, mật khẩu mặc định : 1");
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
