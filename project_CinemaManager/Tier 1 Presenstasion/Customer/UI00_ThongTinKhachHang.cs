using Application;
using DB;
using System;
using System.Windows.Forms;

namespace project_CinemaManager
{
    public partial class UICustomerInfo : Form
    {
        public static Account loginAccount;
        private BindingSource customerList = new BindingSource();

        public UICustomerInfo(Account loginAccount)
        {
            InitializeComponent();
            this.LoginAccount = loginAccount;
        }

        public Account LoginAccount
        {
            get { return loginAccount; }
            set
            {
                loginAccount = value; //ChangeAccount(loginAccount.Type);
            }
        }

        private void LoadCustomer()
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

        private void btnDatVe_Click(object sender, EventArgs e)
        {
            UI_CustomerChonPhim uI_CustomerChonPhim = new UI_CustomerChonPhim();
            this.Hide();
            uI_CustomerChonPhim.ShowDialog();
            this.Show();
        }

        private void UICustomerInfo_Load(object sender, EventArgs e)
        {
            grpCustomer.Text = "Hello," + loginAccount.UserName;
            LoadCustomer();
        }

        private bool UpdateCustomer(string id, string hoTen, DateTime ngaySinh, string diaChi, string sdt, int cmnd)
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
                MessageBox.Show("Bạn đang chỉnh sửa thông tin của tài khoản: " + loginAccount.UserName);
                btnConfigInfo.Text = "Xác nhận";
                txtCusAddress.ReadOnly = false;
                txtCusBirth.ReadOnly = false;
                txtCusINumber.ReadOnly = false;
                txtCusName.ReadOnly = false;
                txtCusPhone.ReadOnly = false;

                txtCusAddress.BackColor = System.Drawing.Color.White;
                txtCusBirth.BackColor = System.Drawing.Color.White;
                txtCusINumber.BackColor = System.Drawing.Color.White;
                txtCusName.BackColor = System.Drawing.Color.White;
                txtCusPhone.BackColor = System.Drawing.Color.White;
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

                    txtCusAddress.BackColor = System.Drawing.Color.Gainsboro;
                    txtCusBirth.BackColor = System.Drawing.Color.Gainsboro;
                    txtCusINumber.BackColor = System.Drawing.Color.Gainsboro;
                    txtCusName.BackColor = System.Drawing.Color.Gainsboro;
                    txtCusPhone.BackColor = System.Drawing.Color.Gainsboro;
                }
            }
        }

        private void btnRqSupport_Click(object sender, EventArgs e)
        {
            UI04_RequestSupport requestSupport = new UI04_RequestSupport();
            this.Hide();
            requestSupport.ShowDialog();
            this.Show();
        }

        private void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            UI05_ChangePassword updatePassWord = new UI05_ChangePassword();
            this.Hide();
            updatePassWord.ShowDialog();
            this.Show();
        }
    }
}