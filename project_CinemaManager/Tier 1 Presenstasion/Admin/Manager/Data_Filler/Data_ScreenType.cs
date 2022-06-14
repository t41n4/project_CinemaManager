using DB;
using System;
using System.Windows.Forms;

namespace project_CinemaManager
{
    public partial class ScreenTypeUC : UserControl
    {
        private BindingSource screenTypeList = new BindingSource();

        public ScreenTypeUC()
        {
            InitializeComponent();
            LoadScreenType();
        }

        private void LoadScreenType()
        {
            dtgvScreenType.DataSource = screenTypeList;
            LoadScreenTypeList();
            AddScreenTypeBinding();
        }

        private void LoadScreenTypeList()
        {
            screenTypeList.DataSource = ScreenTypeDB.GetScreenType();
        }

        private void AddScreenTypeBinding()
        {
            txtScreenTypeID.DataBindings.Add("Text", dtgvScreenType.DataSource, "Mã loại màn hình", true, DataSourceUpdateMode.Never);
            txtScreenTypeName.DataBindings.Add("Text", dtgvScreenType.DataSource, "Tên màn hình", true, DataSourceUpdateMode.Never);
        }

        private void btnShowScreenType_Click(object sender, EventArgs e)
        {
            LoadScreenTypeList();
        }

        private void InsertScreenType(string id, string name)
        {
            if (ScreenTypeDB.InsertScreenType(id, name))
            {
                MessageBox.Show("Thêm loại màn hình thành công");
            }
            else
            {
                MessageBox.Show("Thêm loại màn hình thất bại");
            }
        }

        private void btnInsertScreenType_Click(object sender, EventArgs e)
        {
            string screenTypeID = txtScreenTypeID.Text;
            string screenTypeName = txtScreenTypeName.Text;
            InsertScreenType(screenTypeID, screenTypeName);
            LoadScreenTypeList();
        }

        private void UpdateScreenType(string id, string name)
        {
            if (ScreenTypeDB.UpdateScreenType(id, name))
            {
                MessageBox.Show("Sửa loại màn hình thành công");
            }
            else
            {
                MessageBox.Show("Sửa loại màn hình thất bại");
            }
        }

        private void btnUpdateScreenType_Click(object sender, EventArgs e)
        {
            string screenTypeID = txtScreenTypeID.Text;
            string screenTypeName = txtScreenTypeName.Text;
            UpdateScreenType(screenTypeID, screenTypeName);
            LoadScreenTypeList();
        }

        private void DeleteScreenType(string id)
        {
            if (ScreenTypeDB.DeleteScreenType(id))
            {
                MessageBox.Show("Xóa loại màn hình thành công");
            }
            else
            {
                MessageBox.Show("Xóa loại màn hình thất bại");
            }
        }

        private void btnDeleteScreenType_Click(object sender, EventArgs e)
        {
            string screenTypeID = txtScreenTypeID.Text;
            DeleteScreenType(screenTypeID);
            LoadScreenTypeList();
        }
    }
}