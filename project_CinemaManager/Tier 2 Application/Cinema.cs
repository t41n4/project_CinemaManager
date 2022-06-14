using System.Data;

namespace Application
{
    public class Cinema
    {
        public Cinema(string iD, string name, string screenType, int seats, int status, int row, int seatInRow)

        {
            this.ID = iD;
            this.Name = name;
            this.ScreenTypeID = screenType;
            this.Seats = seats;
            this.Status = status;
            this.Row = row;
            this.SeatInRow = seatInRow;
        }

        public Cinema(DataRow row)
        {
            this.ID = row["id"].ToString();
            this.Name = row["TenPhong"].ToString();
            this.ScreenTypeID = row["idManHinh"].ToString();
            this.Seats = (int)row["SoChoNgoi"];
            this.Status = (int)row["TinhTrang"];
            this.Row = (int)row["SoHangGhe"];
            this.SeatInRow = (int)row["SoGheMotHang"];
        }

        public string ID { get; set; }

        public string Name { get; set; }

        public string ScreenTypeID { get; set; }

        public int Seats { get; set; }

        public int Status { get; set; }

        public int Row { get; set; }

        public int SeatInRow { get; set; }
    }
}