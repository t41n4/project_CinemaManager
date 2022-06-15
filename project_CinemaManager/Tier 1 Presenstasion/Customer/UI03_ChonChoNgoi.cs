using Application;
using DB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace project_CinemaManager
{
    public partial class UI_ChonChoNgoi : Form
    {
        public Account loginAccout = UICustomerInfo.loginAccount;

        public static int cost = 0;

        public UI_ChonChoNgoi(ShowTimes showTimes, Movie movie)
        {
            InitializeComponent();
            ShowTimes = showTimes;
            Movie = movie;
            frmTheatre_Load();
        }

        private string apiUrl = "https://cinema-manager-nhom09.herokuapp.com/payment";

        private int SIZE = 30;//Size của ghế
        private int GAP = 7;//Khoảng cách giữa các ghế

        private int Row;
        private int Column;

        private List<Ticket> listSeatonDB = new List<Ticket>();

        //dùng lưu vết các Ghế đang chọn
        private List<Button> listSeatSelected = new List<Button>();

        private List<Button> listSeatofCinema = new List<Button>();

        private float displayPrice = 0;//Hiện thị giá vé
        private float ticketPrice = 0;//Lưu giá vé gốc
        private float total = 0;//Tổng giá tiền
        private float payment = 0;//Tiền phải trả
        private int SeatBought = 0;

        private ShowTimes ShowTimes;
        private Movie Movie;
        private Cinema cinema;

        private void frmTheatre_Load()
        {
            cinema = CinemaDB.GetCinemaByID(ShowTimes.CinemaID);
            ticketPrice = ShowTimes.TicketPrice;

            lblInformation.Text = "CGV ABC | " + cinema.Name + " | " + ShowTimes.MovieName;
            lblTime.Text = ShowTimes.Time.ToShortDateString() + " | "
                + ShowTimes.Time.ToShortTimeString() + " - "
                + ShowTimes.Time.AddMinutes(Movie.Time).ToShortTimeString();
            if (Movie.Poster != null)
                picFilm.Image = MovieDB.byteArrayToImage(Movie.Poster);
            LoadDataCinema();
            listSeatonDB = TicketDB.GetListTicketsByShowTimes(ShowTimes.ID);
            LoadSeats(listSeatonDB);
            CinemaDB.UpdateCinema(cinema.ID, cinema.Name, cinema.ScreenTypeID, cinema.Seats, SeatBought, cinema.Row, cinema.SeatInRow);
        }

        private void LoadDataCinema()
        {
            Row = cinema.Row;
            Column = cinema.SeatInRow;
            this.Size = new Size(this.Size.Width + 100, this.Size.Height);
            flpSeat.Size = new Size((SIZE + 20 + GAP) * Column, (SIZE + GAP + 5) * Row);
        }

        private void ReLoadInfo()
        {
            CultureInfo culture = new CultureInfo("vi-VN");
            lblTotal.Text = total.ToString("c", culture);
            lblPayment.Text = payment.ToString("c", culture);
        }

        private void LoadSeats(List<Ticket> listTicket)
        {
            SeatBought = 0;
            flpSeat.Controls.Clear();
            listSeatofCinema = new List<Button>();
            for (int i = 0; i < listTicket.Count; i++)
            {
                Button btnSeat = new Button() { Width = SIZE + 20, Height = SIZE };
                btnSeat.Text = listTicket[i].SeatName;
                if (listTicket[i].Status == 1)
                {
                    btnSeat.BackColor = Color.Red;
                    SeatBought++;
                }
                else
                    btnSeat.BackColor = Color.White;
                btnSeat.Click += BtnSeat_Click;
                flpSeat.Controls.Add(btnSeat);

                btnSeat.Tag = listTicket[i];
                listSeatofCinema.Add(btnSeat);
            }
        }

        private void BtnSeat_Click(object sender, EventArgs e)
        {
            
            Button btnSeat = sender as Button;
            if (btnSeat.BackColor == Color.White)
            {
                if (listSeatSelected.Count >= 8)
                {
                    MessageBox.Show("Bạn chỉ được mua tối đa 8 ghế!");
                    return;
                }
                btnSeat.BackColor = Color.Yellow;
                Ticket ticket = btnSeat.Tag as Ticket;
                listSeatofCinema.Find(x => x.Text == btnSeat.Text).BackColor = Color.Yellow;

                ticket.Price = ticketPrice;
                displayPrice = ticket.Price;
                total += ticketPrice;
                payment = total;
                ticket.Type = 1;

                listSeatSelected.Add(btnSeat);
            }
            else if (btnSeat.BackColor == Color.Yellow)
            {
                btnSeat.BackColor = Color.White;
                Ticket ticket = btnSeat.Tag as Ticket;

                listSeatofCinema.Find(x => x.Text == btnSeat.Text).BackColor = Color.White;
                total -= ticket.Price;
                payment = total;
                ticket.Price = 0;
                displayPrice = ticket.Price;
                ticket.Type = 0;

                listSeatSelected.Remove(btnSeat);
            }
            else if (btnSeat.BackColor == Color.Red)
            {
                MessageBox.Show("Ghế số [" + btnSeat.Text + "] đã có người mua");
            }

            ReLoadInfo();
        }

        private bool CheckLeftRightOfRow(List<Button> listRowofSeat)
        {
            bool res = true;
            if (listRowofSeat[0].BackColor == Color.White)
            {
                if (listRowofSeat[1].BackColor != Color.White) return false;
            }
            if (listRowofSeat[listRowofSeat.Count() - 1].BackColor == Color.White)
            {
                if (listRowofSeat[listRowofSeat.Count() - 2].BackColor != Color.White) return false;
            }
            return res;
        }

        private bool CheckValidSelection(List<Button> listSeatofCinema)
        {
            int y = 0;
            for (int x = 1; x < Row + 1; x++)
            {
                List<Button> listRowofSeat = new List<Button>();
                if (x == 1)
                {
                    y = 0;
                    for (; y < ((Column + 1) * x) - 1; y++)
                    {
                        listRowofSeat.Add(listSeatofCinema[y]);
                    }
                }
                else
                {
                    for (; y < ((Column + 1) * x) - x; y++)
                    {
                        listRowofSeat.Add(listSeatofCinema[y]);
                    }
                }

                if (!CheckLeftRightOfRow(listRowofSeat))
                {
                    return false;
                }
                int countSpace = 0;
                for (int i = 0; i < listRowofSeat.Count() - 1; i++)
                {
                    if (listRowofSeat[i].BackColor == Color.White)
                    {
                        countSpace++;
                    }
                    else if (listRowofSeat[i].BackColor != Color.White)
                    {
                        if (countSpace == 1)
                        {
                            if (i == 2 || i == listRowofSeat.Count() - 3)
                            {
                                countSpace = 0;
                                continue;
                            }
                            return false;
                        }
                        else
                        {
                            countSpace = 0;
                        }
                    }
                }
            }
            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn hủy tất cả những vé đã chọn ko?",
                "Hủy Mua Vé", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;
            foreach (Button btn in listSeatSelected)
            {
                btn.BackColor = Color.White;
            }
            RestoreDefault();
            this.OnLoad(new EventArgs());
        }

        private void RestoreDefault()
        {
            listSeatSelected.Clear();
            total = 0;
            displayPrice = 0;
            payment = 0;
            ReLoadInfo();
        }

        private string Post(string url)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                string idlist = "";
                foreach (Button btn in listSeatSelected)
                {
                    Ticket ticket = btn.Tag as Ticket;
                    idlist += "[" + ticket.ID + "]";
                }

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        id = "Ticket PayMent",
                        payment = payment.ToString(),
                        detail = Movie.Name + " - " + ShowTimes.Time.ToString() + " - " + cinema.Name + " - " + idlist
                    });

                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    return streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            if (listSeatSelected.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn vé trước khi thanh toán!");
                return;
            }
            if (!CheckValidSelection(listSeatofCinema))
            {
                MessageBox.Show("Vui lòng không chừa 1 ghế trống bên trái hoặc bên phải của các ghế bạn đã chọn.");
                return;
            }

            string message = "Bạn có chắc chắn mua những vé: \n";
            foreach (Button btn in listSeatSelected)
            {
                message += "[" + btn.Text + "] ";
            }
            message += "\nKhông?";
            DialogResult result = MessageBox.Show(message, "Hỏi Mua",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                int ret = 0;
                {
                    ///////////////// Đặt code Thanh Toán  ở đây, không sử dụng VNPAY nữa nhaa//////////////////////
                    string url = Post(apiUrl).Split('"')[1];
                    UI07_Payment uI07_Payment = new UI07_Payment(url);
                    this.Hide();
                    uI07_Payment.ShowDialog();
                    this.Show();
                    ////////////////////////////////////////////////////////////////////////////////////////////////
                    List<string> listidVe = new List<string>();

                    if (uI07_Payment.status == "0")
                    {
                        foreach (Button btn in listSeatSelected)
                        {
                            Ticket ticket = btn.Tag as Ticket;
                            ret += TicketDB.BuyTicket(ticket.ID, ticket.Type, ticket.Price, loginAccout.ID);
                            listidVe.Add(ticket.ID);
                        }

                        UI06_TicketForCustomer Bill = new UI06_TicketForCustomer(listidVe.ToArray());
                        this.Hide();
                        Bill.ShowDialog();
                        this.Show();
                    }
                }
                if (ret == listSeatSelected.Count)
                {
                    MessageBox.Show("Bạn đã mua vé thành công!");
                }
                else
                {
                    MessageBox.Show("Mua vé Thất Bại!");
                }
            }
            RestoreDefault();

            listSeatonDB = TicketDB.GetListTicketsByShowTimes(ShowTimes.ID);
            LoadSeats(listSeatonDB);
            CinemaDB.UpdateCinema(cinema.ID, cinema.Name, cinema.ScreenTypeID, cinema.Seats, SeatBought, cinema.Row, cinema.SeatInRow);
            this.OnLoad(new EventArgs());
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}