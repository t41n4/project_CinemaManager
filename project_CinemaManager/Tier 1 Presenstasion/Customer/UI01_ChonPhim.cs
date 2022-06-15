using Application;
using DB;
using System;
using System.Collections.Generic;
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
            if (dtgvMovie.RowCount == 0)
            {
                MessageBox.Show("Admin hôm nay bị lười khum up phim cho các bạn xem được, quay lại sauu nha :3");
                this.Close();
            }
            // Ẩn các thuộc tính không cần thiết
            HideUneseccaryColumn();
            AddMovieBinding();
            GetGenre();
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
        }

        private List<string> GenresFilm = new List<string> { };

        private List<string> GetGenre()
        {
            List<string> Idfilm = new List<string> { };

            for (int i = 0; i < dtgvMovie.RowCount; i++)
            {
                DataGridViewRow selectedRow = dtgvMovie.Rows[i];
                Idfilm.Add(Convert.ToString(selectedRow.Cells["Mã phim"].Value));
            }

            for (int i = 0; i < Idfilm.Count; i++)
            {
                GenresFilm.Add(Idfilm[i] + " ");
                foreach (var genre in MovieByGenreDB.GetListGenreByMovieID(Idfilm[i]))
                {
                    GenresFilm[i] += genre.Name + " ";
                }
            }
            if (GenresFilm.Count != 0)
            {
                int startIndex = GenresFilm[0].LastIndexOf(Idfilm[0]) + Idfilm[0].Length;
                txt_TheLoai.Text = GenresFilm[0].Substring(startIndex);
            }
            return GenresFilm;
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

        private void dtgvMovie_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string Idfilm;
            int selectedrowindex = dtgvMovie.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dtgvMovie.Rows[selectedrowindex];
            Idfilm = Convert.ToString(selectedRow.Cells["Mã phim"].Value);

            for (int i = 0; i < dtgvMovie.RowCount; i++)
            {
                if (GenresFilm[i].Contains(Idfilm))
                {
                    int startIndex = GenresFilm[i].LastIndexOf(Idfilm) + Idfilm.Length;
                    txt_TheLoai.Text = GenresFilm[i].Substring(startIndex);
                }
            }
        }
    }
}