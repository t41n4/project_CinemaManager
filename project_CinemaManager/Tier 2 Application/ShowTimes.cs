﻿using System;
using System.Data;

namespace Application
{
    public class ShowTimes
    {
        public ShowTimes(string iD, DateTime time, string cinemaID,
            string formatMovieID, string movieName, float ticketPrice, int status)
        {
            this.ID = iD;
            this.CinemaID = cinemaID;
            this.MovieName = movieName;
            this.Time = time;
            this.FormatMovieID = formatMovieID;
            this.TicketPrice = ticketPrice;
            this.Status = status;
        }

        public ShowTimes(DataRow row)
        {
            this.ID = row["id"].ToString();
            this.CinemaID = row["TenPhong"].ToString();
            this.MovieName = row["TenPhim"].ToString();
            this.Time = (DateTime)row["ThoiGianChieu"];
            this.FormatMovieID = row["idDinhDang"].ToString();
            if (row["GiaVe"].ToString() == "")
                this.TicketPrice = 0;
            else
                this.TicketPrice = float.Parse(row["GiaVe"].ToString());
            this.Status = (int)row["TrangThai"];
        }

        public string ID { get; set; }

        public DateTime Time { get; set; }

        public string CinemaID { get; set; }

        public string FormatMovieID { get; set; }

        public string MovieName { get; set; }

        public float TicketPrice { get; set; }

        public int Status { get; set; }
    }
}