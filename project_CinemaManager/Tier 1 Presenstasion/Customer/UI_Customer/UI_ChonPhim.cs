using Application;
using DB;
using System;
using System.Windows.Forms;

namespace project_CinemaManager
{
    public partial class UI_CustomerChonPhim : Form
    {
        public static UI_ChonGioChieu chonGioChieu;

        public UI_CustomerChonPhim()
        {
            InitializeComponent();
        }

        private void LoadMovie()
        {
            dtgvMovie.DataSource = MovieDB.GetMovieHaveShowTimes();

            // Ẩn các thuộc tính không cần thiết
            HideUneseccaryColumn();
            AddMovieBinding();          
        }

        private void HideUneseccaryColumn()
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

        private void AddMovieBinding()
        {
            
           // txt_TheLoai.DataBindings.Add("Text", )
            txtMovieName.DataBindings.Add("Text", dtgvMovie.DataSource, "Tên phim", true, DataSourceUpdateMode.Never);
            txtMovieLength.DataBindings.Add("Text", dtgvMovie.DataSource, "Thời lượng", true, DataSourceUpdateMode.Never);
            txtMovieProductor.DataBindings.Add("Text", dtgvMovie.DataSource, "Sản xuất", true, DataSourceUpdateMode.Never);
            txtMovieDirector.DataBindings.Add("Text", dtgvMovie.DataSource, "Đạo diễn", true, DataSourceUpdateMode.Never);
            txtMovieYear.DataBindings.Add("Text", dtgvMovie.DataSource, "Năm SX", true, DataSourceUpdateMode.Never);
            picFilm.DataBindings.Add("Image", dtgvMovie.DataSource, "Áp phích", true, DataSourceUpdateMode.Never);

            string Idfilm;
            int selectedrowindex = dtgvMovie.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dtgvMovie.Rows[selectedrowindex];
            Idfilm = Convert.ToString(selectedRow.Cells["Mã phim"].Value);
            txt_TheLoai.Text = "";
            foreach (var item in MovieByGenreDB.GetListGenreByMovieID(Idfilm))
            {
                txt_TheLoai.Text += item.Name + ", ";
            }
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

        private void dtgvMovie_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string Idfilm;
            int selectedrowindex = dtgvMovie.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dtgvMovie.Rows[selectedrowindex];
            Idfilm = Convert.ToString(selectedRow.Cells["Mã phim"].Value);
            txt_TheLoai.Text = "";
            foreach (var item in MovieByGenreDB.GetListGenreByMovieID(Idfilm))
            {
                txt_TheLoai.Text += item.Name + ", ";
            }
        }


    }
}