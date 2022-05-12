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
    public partial class UI_ChonGioChieu : Form
    {
        private Movie GetMovie;
        BindingSource showtimeList = new BindingSource();
        public UI_ChonGioChieu(Movie SelectedFilmFromUser)
        {
            InitializeComponent();
            this.Movie = SelectedFilmFromUser;
            LoadShowtime();
        }
        public Movie Movie
        {
            get { return GetMovie; }
            set { GetMovie = value; }
        }
        void LoadShowtime()
        {      
            dtgvShowtime.DataSource = showtimeList;
            showtimeList.DataSource = ShowTimeDB.GetShowtimeByIDMovie(GetMovie.ID);
            AddShowtimeBinding();
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

        }
    }
}
