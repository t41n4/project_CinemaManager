﻿using DB;
using System;
using System.Windows.Forms;

namespace frmAdminUserControls
{
    public partial class CustomerUC : UserControl
    {
        

        public CustomerUC()
        {
            InitializeComponent();
            LoadCustomer();
        }

        private void LoadCustomer()
        {
            LoadCustomerList();

           
            AddCustomerBinding();

        }

        private void LoadCustomerList()
        {
            dtgvCustomer.DataSource = CustomerDB.GetListCustomer();
            HideUnessecsary();
        }

        private void btnShowCustomer_Click(object sender, EventArgs e)
        {
            LoadCustomerList();
        }
        private void HideUnessecsary()
        {
            //if (dtgvCustomer.Is)
            //{

            //}
            //foreach (DataGridViewRow item in dtgvCustomer.Rows)
            //{
            //    if (item.Cells[0].Value.ToString() == "ADMIN")
            //    {
            //        item.Visible = false;
            //    }
            //}
              
        }

        private void AddCustomerBinding()
        {
            txtCusID.DataBindings.Add("Text", dtgvCustomer.DataSource, "Mã khách hàng", true, DataSourceUpdateMode.Never);
            txtCusName.DataBindings.Add("Text", dtgvCustomer.DataSource, "Họ tên", true, DataSourceUpdateMode.Never);
            txtCusBirth.DataBindings.Add("Text", dtgvCustomer.DataSource, "Ngày sinh", true, DataSourceUpdateMode.Never);
            txtCusAddress.DataBindings.Add("Text", dtgvCustomer.DataSource, "Địa chỉ", true, DataSourceUpdateMode.Never);
            txtCusPhone.DataBindings.Add("Text", dtgvCustomer.DataSource, "SĐT", true, DataSourceUpdateMode.Never);
            txtCusINumber.DataBindings.Add("Text", dtgvCustomer.DataSource, "CMND", true, DataSourceUpdateMode.Never);
        }

        private void InsertCustomer(string id, string hoTen, DateTime ngaySinh, string diaChi, string sdt, int cmnd)
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

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            string cusID = txtCusID.Text;
            string cusName = txtCusName.Text;
            DateTime cusBirth = DateTime.Parse(txtCusBirth.Text);
            string cusAddress = txtCusAddress.Text;
            string cusPhone = txtCusPhone.Text;
            int cusINumber = Int32.Parse(txtCusINumber.Text);
            InsertCustomer(cusID, cusName, cusBirth, cusAddress, cusPhone, cusINumber);
            LoadCustomerList();
        }

        private void UpdateCustomer(string id, string hoTen, DateTime ngaySinh, string diaChi, string sdt, int cmnd)
        {
            if (CustomerDB.UpdateCustomer(id, hoTen, ngaySinh, diaChi, sdt, cmnd))
            {
                MessageBox.Show("Sửa khách hàng thành công");
            }
            else
            {
                MessageBox.Show("Sửa khách hàng thất bại");
            }
        }

        private void btnUpdateCustomer_Click(object sender, EventArgs e)
        {
            string cusID = txtCusID.Text;
            string cusName = txtCusName.Text;
            DateTime cusBirth = DateTime.Parse(txtCusBirth.Text);
            string cusAddress = txtCusAddress.Text;
            string cusPhone = txtCusPhone.Text;
            int cusINumber = Int32.Parse(txtCusINumber.Text);
            UpdateCustomer(cusID, cusName, cusBirth, cusAddress, cusPhone, cusINumber);
            LoadCustomerList();
        }

        private void DeleteCustomer(string id)
        {
            if (CustomerDB.DeleteCustomer(id))
            {
                MessageBox.Show("Xóa khách hàng thành công");
            }
            else
            {
                MessageBox.Show("Xóa khách hàng thất bại");
            }
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            string cusID = txtCusID.Text;
            DeleteCustomer(cusID);
            LoadCustomerList();
        }

        private void btnSearchCus_Click(object sender, EventArgs e)
        {
            string cusName = txtSearchCus.Text;
            dtgvCustomer.DataSource = CustomerDB.SearchCustomerByID(cusName);
        }

        private void txtSearchCus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearchCus.PerformClick();
                e.SuppressKeyPress = true;//Tắt tiếng *ting của windows
            }
        }
    }
}