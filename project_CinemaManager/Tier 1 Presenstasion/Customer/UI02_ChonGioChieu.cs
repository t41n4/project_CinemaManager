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

        private void UpdateCapacity()
        {
            int selectedrowindex = dtgvShowtime.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dtgvShowtime.Rows[selectedrowindex];
            string Capacity = Convert.ToString(selectedRow.Cells["Tình Trạng"].Value);
            string cinemaid = Convert.ToString(selectedRow.Cells["ID Phòng chiếu"].Value);
            Cinema cinema = CinemaDB.GetCinemaByID(cinemaid);
            CapacityofCinema = cinema.Row * cinema.SeatInRow;
            txStatus_ShowTimes.Text = Capacity + "/" + CapacityofCinema.ToString();
        }

        private void UI_ChonGioChieu_Load(object sender, EventArgs e)
        {
            LoadShowtime();
            HideUneseccaryColumn();
            UpdateCapacity();
        }

        private void HideUneseccaryColumn()
        {
            for (int i = 0; i < dtgvShowtime.Columns.Count; i++)
            {
                if (dtgvShowtime.Columns[i].HeaderText == "Mã lịch chiếu")
                {
                    dtgvShowtime.Columns[i].Visible = false;
                }
                else if (dtgvShowtime.Columns[i].HeaderText == "ID Phòng Chiếu")
                {
                    dtgvShowtime.Columns[i].Visible = false;
                }
            }
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
            txtMovieName_Showtime.DataBindings.Add("Text", dtgvShowtime.DataSource, "Tên phim", true, DataSourceUpdateMode.OnPropertyChanged);
            txtShowtimeDateTime.DataBindings.Add("Text", dtgvShowtime.DataSource, "Thời gian chiếu", true, DataSourceUpdateMode.OnPropertyChanged);
            txtTicketPrice_Showtime.DataBindings.Add("Text", dtgvShowtime.DataSource, "Giá vé", true, DataSourceUpdateMode.OnPropertyChanged);
            txtShowRoom_Showtime.DataBindings.Add("Text", dtgvShowtime.DataSource, "Tên Phòng chiếu", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private int CapacityofCinema = 0;

        private void btnChonGioChieu_Click(object sender, EventArgs e)
        {
            int selectedrowindex = dtgvShowtime.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dtgvShowtime.Rows[selectedrowindex];

            string IdShowTimes = Convert.ToString(selectedRow.Cells["Mã lịch chiếu"].Value);
            ShowTimes showTimes = ShowTimeDB.GetShowtimeByIDShowTimeAndIDMovie(IdShowTimes, GetMovie.ID);

            string cinemaid = Convert.ToString(selectedRow.Cells["ID Phòng chiếu"].Value);
            Cinema cinema = CinemaDB.GetCinemaByID(cinemaid);

            CapacityofCinema = cinema.Row * cinema.SeatInRow;
            if (cinema.Status == CapacityofCinema)
            {
                MessageBox.Show("Phòng Chiếu này đã đầy");
                return;
            }

            UI_ChonChoNgoi uI_ChonChoNgoi = new UI_ChonChoNgoi(showTimes, GetMovie);
            this.Hide();
            uI_ChonChoNgoi.ShowDialog();
            this.Show();
            showtimeList.DataSource = ShowTimeDB.GetShowtimeByIDMovie(GetMovie.ID);
            UpdateCapacity();


        }

        private void dtgvShowtime_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            UpdateCapacity();

        }
    }
}