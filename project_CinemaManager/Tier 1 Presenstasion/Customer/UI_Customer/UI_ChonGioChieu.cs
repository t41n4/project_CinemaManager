using Application;
using DB;
using System;
using System.Windows.Forms;

namespace project_CinemaManager
{
    public partial class UI_ChonGioChieu : Form
    {
        private Movie GetMovie;
        private BindingSource showtimeList = new BindingSource();

        public UI_ChonGioChieu(Movie SelectedFilmFromUser)
        {
            InitializeComponent();
            this.Movie = SelectedFilmFromUser;
        }

        public Movie Movie
        {
            get { return GetMovie; }
            set { GetMovie = value; }
        }

        public void LoadShowtime()
        {
            dtgvShowtime.DataSource = showtimeList;
            showtimeList.DataSource = ShowTimeDB.GetShowtimeByIDMovie(GetMovie.ID);
            if (showtimeList.Count > 0)
            {
                AddShowtimeBinding();
            }
            else
            {
                MessageBox.Show("Phim này không có lịch chiếu!!");
                UI_CustomerChonPhim.chonGioChieu.Close();
            }
        }

        private void AddShowtimeBinding()
        {
            txtMovieName_Showtime.DataBindings.Add("Text", dtgvShowtime.DataSource, "Tên phim", true, DataSourceUpdateMode.Never);
            txtShowtimeDateTime.DataBindings.Add("Text", dtgvShowtime.DataSource, "Thời gian chiếu", true, DataSourceUpdateMode.Never);
            txtTicketPrice_Showtime.DataBindings.Add("Text", dtgvShowtime.DataSource, "Giá vé", true, DataSourceUpdateMode.Never);
            txtShowRoom_Showtime.DataBindings.Add("Text", dtgvShowtime.DataSource, "Phòng chiếu", true, DataSourceUpdateMode.Never);
        }

        private void btnChonGioChieu_Click(object sender, EventArgs e)
        {
            string IdShowTimes;
            int selectedrowindex = dtgvShowtime.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dtgvShowtime.Rows[selectedrowindex];
            IdShowTimes = Convert.ToString(selectedRow.Cells["Mã lịch chiếu"].Value);

            ShowTimes showTimes = ShowTimeDB.GetShowtimeByIDShowTimeAndIDMovie(IdShowTimes, GetMovie.ID);

            UI_ChonChoNgoi uI_ChonChoNgoi = new UI_ChonChoNgoi(showTimes, GetMovie);
            this.Hide();
            uI_ChonChoNgoi.ShowDialog();
            this.Show();
        }

        private void UI_ChonGioChieu_Load(object sender, EventArgs e)
        {
            LoadShowtime();
        }
    }
}