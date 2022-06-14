﻿using Application;
using DB;
using System;
using System.Windows.Forms;

namespace project_CinemaManager
{
    public partial class ShowTimesUC : UserControl
    {
        private BindingSource showtimeList = new BindingSource();

        public ShowTimesUC()
        {
            InitializeComponent();
            LoadShowtime();
        }

        private void LoadShowtime()
        {
            dtgvShowtime.DataSource = showtimeList;
            LoadShowtimeList();
            LoadFormatMovieIntoComboBox();
            AddShowtimeBinding();
        }

        private void LoadShowtimeList()
        {
            showtimeList.DataSource = ShowTimeDB.GetListShowtime();
        }

        private void btnShowShowtime_Click(object sender, EventArgs e)
        {
            LoadShowtimeList();
        }

        //Binding
        private void AddShowtimeBinding()
        {
            txtShowtimeID.DataBindings.Add("Text", dtgvShowtime.DataSource, "Mã lịch chiếu", true, DataSourceUpdateMode.Never);
            dtmShowtimeDate.DataBindings.Add("Value", dtgvShowtime.DataSource, "Thời gian chiếu", true, DataSourceUpdateMode.Never);
            dtmShowtimeTime.DataBindings.Add("Value", dtgvShowtime.DataSource, "Thời gian chiếu", true, DataSourceUpdateMode.Never);
            txtTicketPrice_Showtime.DataBindings.Add("Text", dtgvShowtime.DataSource, "Giá vé", true, DataSourceUpdateMode.Never);
        }

        private void LoadFormatMovieIntoComboBox()
        {
            cboFormatID_Showtime.DataSource = FormatMovieDAO.GetFormatMovie();
            cboFormatID_Showtime.DisplayMember = "ID";
        }

        private void cboFormatID_Showtime_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFormatID_Showtime.SelectedIndex != -1)
            {
                FormatMovie formatMovieSelecting = (FormatMovie)cboFormatID_Showtime.SelectedItem;
                txtMovieName_Showtime.Text = formatMovieSelecting.MovieName;
                txtScreenTypeName_Showtime.Text = formatMovieSelecting.ScreenTypeName;

                cboCinemaID_Showtime.DataSource = null;
                ScreenType screenType = ScreenTypeDB.GetScreenTypeByName(formatMovieSelecting.ScreenTypeName);
                cboCinemaID_Showtime.DataSource = CinemaDB.GetCinemaByScreenTypeID(screenType.ID);
                cboCinemaID_Showtime.DisplayMember = "Name";
            }
        }

        private void txtShowtimeID_TextChanged(object sender, EventArgs e)
        {
            #region Change selected index of ComboBox FormatMovie

            string movieName = (string)dtgvShowtime.SelectedCells[0].OwningRow.Cells["Tên phim"].Value;
            string screenTypeName = (string)dtgvShowtime.SelectedCells[0].OwningRow.Cells["Màn hình"].Value;
            FormatMovie formatMovieSelecting = FormatMovieDAO.GetFormatMovieByName(movieName, screenTypeName);
            if (formatMovieSelecting == null)
                return;
            int indexFormatMovie = -1;
            for (int i = 0; i < cboFormatID_Showtime.Items.Count; i++)
            {
                FormatMovie item = cboFormatID_Showtime.Items[i] as FormatMovie;
                if (item.ID == formatMovieSelecting.ID)
                {
                    indexFormatMovie = i;
                    break;
                }
            }
            cboFormatID_Showtime.SelectedIndex = indexFormatMovie;

            #endregion Change selected index of ComboBox FormatMovie

            #region Change selected index of ComboBox Cinema

            string cinemaID = (string)dtgvShowtime.SelectedCells[0].OwningRow.Cells["Mã phòng"].Value;
            Cinema cinemaSelecting = CinemaDB.GetCinemaByID(cinemaID);
            //This is the Cinema that we're currently selecting in dtgv

            if (cinemaSelecting == null)
                return;

            int indexCinema = -1;
            int iCinema = 0;
            foreach (Cinema item in cboCinemaID_Showtime.Items)
            {
                if (item.ID == cinemaSelecting.ID)
                {
                    indexCinema = iCinema;
                    break;
                }
                iCinema++;
            }
            cboCinemaID_Showtime.SelectedIndex = indexCinema;

            #endregion Change selected index of ComboBox Cinema

            toolTipCinema.SetToolTip(cboCinemaID_Showtime, "Danh sách phòng chiếu hỗ trợ loại màn hình trên");
        }

        //Insert
        private void InsertShowtime(string id, string cinemaID, string formatMovieID, DateTime time, float ticketPrice)
        {
            if (ShowTimeDB.InsertShowtime(id, cinemaID, formatMovieID, time, ticketPrice))
            {
                MessageBox.Show("Thêm lịch chiếu thành công");
            }
            else
            {
                MessageBox.Show("Thêm lịch chiếu thất bại");
            }
        }

        private void btnInsertShowtime_Click(object sender, EventArgs e)
        {
            string showtimeID = txtShowtimeID.Text;
            string cinemaID = ((Cinema)cboCinemaID_Showtime.SelectedItem).ID;
            string formatMovieID = ((FormatMovie)cboFormatID_Showtime.SelectedItem).ID;
            DateTime time = new DateTime(dtmShowtimeDate.Value.Year, dtmShowtimeDate.Value.Month, dtmShowtimeDate.Value.Day, dtmShowtimeTime.Value.Hour, dtmShowtimeTime.Value.Minute, dtmShowtimeTime.Value.Second);
            //Bind dtmShowtimeDate to "time.date" and dtmShowtimeTime to "time.time" ... TODO : Look for a better way to do this
            float ticketPrice = float.Parse(txtTicketPrice_Showtime.Text);
            InsertShowtime(showtimeID, cinemaID, formatMovieID, time, ticketPrice);
            LoadShowtimeList();
        }

        //Update
        private void UpdateShowtime(string id, string cinemaID, string formatMovieID, DateTime time, float ticketPrice)
        {
            if (ShowTimeDB.UpdateShowtime(id, cinemaID, formatMovieID, time, ticketPrice))
            {
                MessageBox.Show("Sửa lịch chiếu thành công");
            }
            else
            {
                MessageBox.Show("Sửa lịch chiếu thất bại");
            }
        }

        private void btnUpdateShowtime_Click(object sender, EventArgs e)
        {
            string showtimeID = txtShowtimeID.Text;
            string cinemaID = ((Cinema)cboCinemaID_Showtime.SelectedItem).ID;
            string formatMovieID = ((FormatMovie)cboFormatID_Showtime.SelectedItem).ID;
            DateTime time = new DateTime(dtmShowtimeDate.Value.Year, dtmShowtimeDate.Value.Month, dtmShowtimeDate.Value.Day, dtmShowtimeTime.Value.Hour, dtmShowtimeTime.Value.Minute, dtmShowtimeTime.Value.Second);
            //Bind dtmShowtimeDate to "time.date" and dtmShowtimeTime to "time.time" ... TODO : Look for a better way to do this
            float ticketPrice = float.Parse(txtTicketPrice_Showtime.Text);
            UpdateShowtime(showtimeID, cinemaID, formatMovieID, time, ticketPrice);
            LoadShowtimeList();
        }

        //Delete
        private void DeleteShowtime(string id)
        {
            if (ShowTimeDB.DeleteShowtime(id))
            {
                MessageBox.Show("Xóa lịch chiếu thành công");
            }
            else
            {
                MessageBox.Show("Xóa lịch chiếu thất bại");
            }
        }

        private void btnDeleteShowtime_Click(object sender, EventArgs e)
        {
            string showtimeID = txtShowtimeID.Text;
            DeleteShowtime(showtimeID);
            LoadShowtimeList();
        }

        //Search
        private void btnSearchShowtime_Click(object sender, EventArgs e)
        {
            string movieName = txtSearchShowtime.Text;
            showtimeList.DataSource = ShowTimeDB.SearchShowtimeByMovieName(movieName);
        }

        private void txtSearchShowtime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearchShowtime.PerformClick();
                e.SuppressKeyPress = true;//Tắt tiếng *ting của windows
            }
        }
    }
}