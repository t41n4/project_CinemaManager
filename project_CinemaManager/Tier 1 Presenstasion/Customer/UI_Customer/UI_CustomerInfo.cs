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
    public partial class UICustomerInfo : Form
    {
        private Account loginAccount;
        BindingSource customerList = new BindingSource();
 
        public UICustomerInfo(Account loginAccount)
        {
            InitializeComponent();
            this.LoginAccount = loginAccount;
        }

        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; //ChangeAccount(loginAccount.Type);
            }
        }

        void LoadCustomer()
        {
            customerList.DataSource = CustomerDB.SearchCustomerByID(loginAccount.ID);        
            txtCusID.DataBindings.Add("Text", customerList.DataSource, "Mã Khách Hàng");
            txtCusBirth.DataBindings.Add("Text", customerList.DataSource, "Ngày sinh");
            txtCusINumber.DataBindings.Add("Text", customerList.DataSource, "CMND");
            txtCusName.DataBindings.Add("Text", customerList.DataSource, "Họ tên");
            txtCusPhone.DataBindings.Add("Text", customerList.DataSource, "SĐT");
            txtCusAddress.DataBindings.Add("Text", customerList.DataSource, "Địa chỉ");
        }

        private void btnShowCustomer_Click(object sender, EventArgs e)
        {
            LoadCustomer();
        }
        private void SearchCus()
        {
            string cusName = "";
            customerList.DataSource = CustomerDB.SearchCustomerByID(cusName);
        }
        
          


        private void btnDatVe_Click(object sender, EventArgs e)
        {

        }

        private void UICustomerInfo_Load(object sender, EventArgs e)
        {     
            grpCustomer.Text = "Hello,"+loginAccount.UserName;
            LoadCustomer();
        }

        bool UpdateCustomer(string id, string hoTen, DateTime ngaySinh, string diaChi, string sdt, int cmnd)
        {
            if (CustomerDB.UpdateCustomer(id, hoTen, ngaySinh, diaChi, sdt, cmnd))
            {
                MessageBox.Show("Sửa khách hàng thành công");
                return true;
            }
            else
            {
                MessageBox.Show("Sửa khách hàng thất bại");
                return false;
            }
        }
        private void btnConfigInfo_Click(object sender, EventArgs e)
        {
            if (btnConfigInfo.Text == "Chỉnh Sửa Thông Tin")
            {
                MessageBox.Show("Bạn đang chỉnh sửa thông tin của tài khoản: "+loginAccount.UserName);
                btnConfigInfo.Text = "Xác nhận";
                txtCusAddress.ReadOnly = false;
                txtCusBirth.ReadOnly = false;            
                txtCusINumber.ReadOnly = false;
                txtCusName.ReadOnly = false;
                txtCusPhone.ReadOnly = false;
            }
            else 
            {                                   
                // Get Data
                string cusID = txtCusID.Text;
                string cusName = txtCusName.Text;
                DateTime cusBirth = DateTime.Parse(txtCusBirth.Text);
                string cusAddress = txtCusAddress.Text;
                string cusPhone = txtCusPhone.Text;
                int cusINumber = Int32.Parse(txtCusINumber.Text);
                if (UpdateCustomer(cusID, cusName, cusBirth, cusAddress, cusPhone, cusINumber))
                {
                    btnConfigInfo.Text = "Chỉnh Sửa Thông Tin";
                    txtCusAddress.ReadOnly = true;
                    txtCusBirth.ReadOnly = true;
                    txtCusID.ReadOnly = true;
                    txtCusINumber.ReadOnly = true;
                    txtCusName.ReadOnly = true;
                    txtCusPhone.ReadOnly = true;
                } 

              
            }






        }

    }
}
