using Application;
using DB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace project_CinemaManager
{
    public partial class MovieUC : UserControl
    {
        private BindingSource movieList = new BindingSource();

        public MovieUC()
        {
            InitializeComponent();
            LoadMovie();
        }

        private void LoadMovie()
        {
            dtgvMovie.DataSource = movieList;
            LoadMovieList();
            AddMovieBinding();
        }

        private void LoadMovieList()
        {
            movieList.DataSource = MovieDB.GetMovie();
        }

        private void AddMovieBinding()
        {
            txtMovieID.DataBindings.Add("Text", dtgvMovie.DataSource, "Mã phim", true, DataSourceUpdateMode.Never);
            txtMovieName.DataBindings.Add("Text", dtgvMovie.DataSource, "Tên phim", true, DataSourceUpdateMode.Never);
            txtMovieDesc.DataBindings.Add("Text", dtgvMovie.DataSource, "Mô tả", true, DataSourceUpdateMode.Never);
            txtMovieLength.DataBindings.Add("Text", dtgvMovie.DataSource, "Thời lượng", true, DataSourceUpdateMode.Never);
            dtmMovieStart.DataBindings.Add("Value", dtgvMovie.DataSource, "Ngày khởi chiếu", true, DataSourceUpdateMode.Never);
            dtmMovieEnd.DataBindings.Add("Value", dtgvMovie.DataSource, "Ngày kết thúc", true, DataSourceUpdateMode.Never);
            txtMovieProductor.DataBindings.Add("Text", dtgvMovie.DataSource, "Sản xuất", true, DataSourceUpdateMode.Never);
            txtMovieDirector.DataBindings.Add("Text", dtgvMovie.DataSource, "Đạo diễn", true, DataSourceUpdateMode.Never);
            txtMovieYear.DataBindings.Add("Text", dtgvMovie.DataSource, "Năm SX", true, DataSourceUpdateMode.Never);
            LoadGenreIntoCheckedList(clbMovieGenre);
        }

        private void LoadGenreIntoCheckedList(CheckedListBox checkedList)
        {
            try
            {
                List<Genre> genreList = GenreDB.GetListGenre();
                foreach (var item in genreList)
                {
                    checkedList.Items.Add(item);
                }
                checkedList.DisplayMember = "Name";
            }
            catch (Exception)
            {
            }
        }

        private void txtMovieID_TextChanged(object sender, EventArgs e)
        //Use to binding the CheckedListBox Genre of Movie and picture of Movie
        {
            picFilm.Image = null;
            for (int i = 0; i < clbMovieGenre.Items.Count; i++)
            {
                clbMovieGenre.SetItemChecked(i, false);
                //Uncheck all CheckBox first
            }

            List<Genre> listGenreOfMovie = MovieByGenreDB.GetListGenreByMovieID(txtMovieID.Text);
            for (int i = 0; i < clbMovieGenre.Items.Count; i++)
            {
                Genre genre = (Genre)clbMovieGenre.Items[i];
                foreach (Genre item in listGenreOfMovie)
                {
                    if (genre.ID == item.ID)
                    {
                        clbMovieGenre.SetItemChecked(i, true);
                        break;
                    }
                }
            }

            Movie movie = MovieDB.GetMovieByID(txtMovieID.Text);

            if (movie == null)
                return;

            if (movie.Poster != null)
                picFilm.Image = MovieDB.byteArrayToImage(movie.Poster);
        }

        private void InsertMovie(string id, string name, string desc, float length, DateTime startDate, DateTime endDate, string productor, string director, int year, byte[] image)
        {
            if (MovieDB.InsertMovie(id, name, desc, length, startDate, endDate, productor, director, year, image))
            {
                MessageBox.Show("Thêm phim thành công");
            }
            else
            {
                MessageBox.Show("Thêm phim thất bại");
            }
        }

        private void InsertMovie_Genre(string movieID, CheckedListBox checkedListBox)
        //Insert into table 'PhanLoaiPhim'
        {
            List<Genre> checkedGenreList = new List<Genre>();
            foreach (Genre checkedItem in checkedListBox.CheckedItems)
            {
                checkedGenreList.Add(checkedItem);
            }
            MovieByGenreDB.InsertMovie_Genre(movieID, checkedGenreList);
        }

        private void btnUpLoadPictureFilm_Click(object sender, EventArgs e)
        {
            try
            {
                string filePathImage = null;
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Filter = "Pictures files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png)|*.jpg; *.jpeg; *.jpe; *.jfif; *.png|All files (*.*)|*.*";
                openFile.FilterIndex = 1;
                openFile.RestoreDirectory = true;
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    filePathImage = openFile.FileName;
                    picFilm.Image = Image.FromFile(filePathImage.ToString());
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void btnAddMovie_Click(object sender, EventArgs e)
        {
            string movieID = txtMovieID.Text;
            string movieName = txtMovieName.Text;
            string movieDesc = txtMovieDesc.Text;
            float movieLength = float.Parse(txtMovieLength.Text);
            DateTime startDate = dtmMovieStart.Value;
            DateTime endDate = dtmMovieEnd.Value;
            string productor = txtMovieProductor.Text;
            string director = txtMovieDirector.Text;
            int year = int.Parse(txtMovieYear.Text);
            if (picFilm.Image == null)
            {
                MessageBox.Show("Mời bạn thêm hình ảnh cho phim trước");
                return;
            }
            InsertMovie(movieID, movieName, movieDesc, movieLength, startDate, endDate, productor, director, year, MovieDB.imageToByteArray(picFilm.Image));
            InsertMovie_Genre(movieID, clbMovieGenre);
            LoadMovieList();
        }

        private void UpdateMovie(string id, string name, string desc, float length, DateTime startDate, DateTime endDate, string productor, string director, int year, byte[] image)
        {
            if (MovieDB.UpdateMovie(id, name, desc, length, startDate, endDate, productor, director, year, image))
            {
                MessageBox.Show("Sửa phim thành công");
            }
            else
            {
                MessageBox.Show("Sửa phim thất bại");
            }
        }

        private void UpdateMovie_Genre(string movieID, CheckedListBox checkedListBox)
        {
            List<Genre> checkedGenreList = new List<Genre>();
            foreach (Genre checkedItem in checkedListBox.CheckedItems)
            {
                checkedGenreList.Add(checkedItem);
            }
            MovieByGenreDB.UpdateMovie_Genre(movieID, checkedGenreList);
        }

        private void btnUpdateMovie_Click(object sender, EventArgs e)
        {
            string movieID = txtMovieID.Text;
            string movieName = txtMovieName.Text;
            string movieDesc = txtMovieDesc.Text;
            float movieLength = float.Parse(txtMovieLength.Text);
            DateTime startDate = dtmMovieStart.Value;
            DateTime endDate = dtmMovieEnd.Value;
            string productor = txtMovieProductor.Text;
            string director = txtMovieDirector.Text;
            int year = int.Parse(txtMovieYear.Text);
            if (picFilm.Image == null)
            {
                MessageBox.Show("Mời bạn thêm hình ảnh cho phim trước");
                return;
            }
            UpdateMovie(movieID, movieName, movieDesc, movieLength, startDate, endDate, productor, director, year, MovieDB.imageToByteArray(picFilm.Image));
            UpdateMovie_Genre(movieID, clbMovieGenre);
            LoadMovieList();
        }

        private void DeleteMovie(string id)
        {
            if (MovieDB.DeleteMovie(id))
            {
                MessageBox.Show("Xóa phim thành công");
            }
            else
            {
                MessageBox.Show("Xóa phim thất bại");
            }
        }

        private void btnDeleteMovie_Click(object sender, EventArgs e)
        {
            string movieID = txtMovieID.Text;
            DeleteMovie(movieID);
            LoadMovieList();
        }
    }
}