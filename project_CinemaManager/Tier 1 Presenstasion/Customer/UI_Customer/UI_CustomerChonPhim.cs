using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DB;
using Application;
namespace project_CinemaManager
{
    public partial class UI_CustomerChonPhim : Form
    {
        static public UI_ChonGioChieu chonGioChieu;
        public UI_CustomerChonPhim()
        {
            InitializeComponent();
        }

        void LoadMovie()
        {
            dtgvMovie.DataSource = MovieDB.GetMovieHaveShowTimes();
            
            // Ẩn các thuộc tính không cần thiết
            HideUneseccaryColumn();
            AddMovieBinding();
        }
        void HideUneseccaryColumn()
        {
            for (int i = 0; i < dtgvMovie.Columns.Count; i++)
            {
                if (dtgvMovie.Columns[i].HeaderText == "Mã phim")
                {
                    dtgvMovie.Columns[i].Visible = false;
                }
                else if (dtgvMovie.Columns[i].HeaderText == "Ngày khởi chiếu")
                {
                    dtgvMovie.Columns[i].Visible = false;
                }
                else if (dtgvMovie.Columns[i].HeaderText == "Ngày kết thúc")
                {
                    dtgvMovie.Columns[i].Visible = false;
                }
                else if (dtgvMovie.Columns[i].HeaderText == "Áp Phích")
                {
                    dtgvMovie.Columns[i].Visible = false;
                }
                else if (dtgvMovie.Columns[i].HeaderText == "Mô tả")
                {
                    dtgvMovie.Columns[i].Visible = false;
                }
                else if (dtgvMovie.Columns[i].HeaderText == "Sản xuất")
                {
                    dtgvMovie.Columns[i].Visible = false;
                }
                else if (dtgvMovie.Columns[i].HeaderText == "Đạo diễn")
                {
                    dtgvMovie.Columns[i].Visible = false;
                }
                else if (dtgvMovie.Columns[i].HeaderText == "Năm SX")
                {
                    dtgvMovie.Columns[i].Visible = false;
                }
                else if (dtgvMovie.Columns[i].HeaderText == "Thời lượng")
                {
                    dtgvMovie.Columns[i].Visible = false;
                }
            }
        }
  
        void AddMovieBinding()
        {
            txtMovieName.DataBindings.Add("Text", dtgvMovie.DataSource, "Tên phim", true, DataSourceUpdateMode.Never);
            txtMovieDesc.DataBindings.Add("Text", dtgvMovie.DataSource, "Mô tả", true, DataSourceUpdateMode.Never);
            txtMovieLength.DataBindings.Add("Text", dtgvMovie.DataSource, "Thời lượng", true, DataSourceUpdateMode.Never);
            txtMovieProductor.DataBindings.Add("Text", dtgvMovie.DataSource, "Sản xuất", true, DataSourceUpdateMode.Never);
            txtMovieDirector.DataBindings.Add("Text", dtgvMovie.DataSource, "Đạo diễn", true, DataSourceUpdateMode.Never);
            txtMovieYear.DataBindings.Add("Text", dtgvMovie.DataSource, "Năm SX", true, DataSourceUpdateMode.Never);         
            picFilm.DataBindings.Add("Image", dtgvMovie.DataSource, "Áp phích", true, DataSourceUpdateMode.Never);            
        }         
        private void UI_CustomerDatVe_Load(object sender, EventArgs e)
        {
            LoadMovie();
        }

        private void btnChonPhim_Click(object sender, EventArgs e)
        {
            string Idfilm;       
            int selectedrowindex = dtgvMovie.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dtgvMovie.Rows[selectedrowindex];
            Idfilm = Convert.ToString(selectedRow.Cells["Mã phim"].Value);
            
            Movie SelectedFilm = MovieDB.GetMovieByID(Idfilm);

            chonGioChieu = new UI_ChonGioChieu(SelectedFilm);
            this.Hide();
            chonGioChieu.ShowDialog();
            this.Show();
        }
    }
}
