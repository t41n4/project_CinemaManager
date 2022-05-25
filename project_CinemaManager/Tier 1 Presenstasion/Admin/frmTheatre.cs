
using project_CinemaManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DB;
using Application;

namespace project_CinemaManager
{
    public partial class frmTheatre : Form
    {
        int SIZE = 30;//Size của ghế
        int GAP = 7;//Khoảng cách giữa các ghế

        List<Ticket> listSeat = new List<Ticket>();

        //dùng lưu vết các Ghế đang chọn
        List<Button> listSeatSelected = new List<Button>();

        float displayPrice = 0;//Hiện thị giá vé
        float ticketPrice = 0;//Lưu giá vé gốc
        float total = 0;//Tổng giá tiền     
        float payment = 0;//Tiền phải trả


        Customer customer;//lưu lại khách hàng thành viên
        ShowTimes Times;
        Movie Movie;

        public frmTheatre(ShowTimes showTimes, Movie movie)
        {
            InitializeComponent();

            Times = showTimes;
            Movie = movie;
        }

        private void frmTheatre_Load(object sender, EventArgs e)
        {
            ticketPrice = Times.TicketPrice;

            lblInformation.Text = "CGV Hung Vuong | " + Times.CinemaName + " | " + Times.MovieName;
            lblTime.Text = Times.Time.ToShortDateString() + " | "
                + Times.Time.ToShortTimeString() + " - "
                + Times.Time.AddMinutes(Movie.Time).ToShortTimeString();
            if (Movie.Poster != null)
                picFilm.Image = MovieDB.byteArrayToImage(Movie.Poster);

            rdoAdult.Checked = true;
            chkCustomer.Enabled = false;
            grpLoaiVe.Enabled = false;

            LoadDataCinema(Times.CinemaName);

            ShowOrHideLablePoint();

            listSeat = TicketDAO.GetListTicketsByShowTimes(Times.ID);

            LoadSeats(listSeat);
        }

        private void LoadDataCinema(string cinemaName)
        {
            Cinema cinema = CinemaDAO.GetCinemaByName(cinemaName);
            int Row = cinema.Row;
            int Column = cinema.SeatInRow;
            flpSeat.Size = new Size((SIZE + 20 + GAP) * Column, (SIZE + GAP) * Row);
        }

        private void LoadBill()
        {
            CultureInfo culture = new CultureInfo("vi-VN");
            //Đổi culture vùng quốc gia để đổi đơn vị tiền tệ 

            //Thread.CurrentThread.CurrentCulture = culture;
            //dùng thread để chuyển cả luồng đang chạy về vùng quốc gia đó

            lblTicketPrice.Text = displayPrice.ToString("c", culture);
            lblTotal.Text = total.ToString("c", culture);
            lblPayment.Text = payment.ToString("c", culture);

            //Đổi đơn vị tiền tệ
            //gán culture chỗ này thì chỉ có chỗ này sd culture này còn
            //lại sài mặc định
        }

        private void LoadSeats(List<Ticket> list)
        {
            flpSeat.Controls.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                Button btnSeat = new Button() { Width = SIZE + 20, Height = SIZE };
                btnSeat.Text = list[i].SeatName;
                if (list[i].Status == 1)
                    btnSeat.BackColor = Color.Red;
                else
                    btnSeat.BackColor = Color.White;
                btnSeat.Click += BtnSeat_Click;
                flpSeat.Controls.Add(btnSeat);
                btnSeat.Tag = list[i];
            }
        }

        private void BtnSeat_Click(object sender, EventArgs e)
        {
            if (listSeatSelected.Count > 8)
            {
                MessageBox.Show("BẠN CHỈ ĐỔI ĐƯỢC TỐT ĐA [8] VÉ", "THÔNG BÁO");
                return;
            }
            Button btnSeat = sender as Button;
            if (btnSeat.BackColor == Color.White)
            {
                grpLoaiVe.Enabled = true;
                rdoAdult.Checked = true;

                btnSeat.BackColor = Color.Yellow;
                Ticket ticket = btnSeat.Tag as Ticket;

                ticket.Price = ticketPrice;
                displayPrice = ticket.Price;
                total += ticketPrice;
                payment = total;
                ticket.Type = 1;
            }
            else if (btnSeat.BackColor == Color.Yellow)
            {
                btnSeat.BackColor = Color.White;
                Ticket ticket = btnSeat.Tag as Ticket;

                total -= ticket.Price;
                payment = total;
                ticket.Price = 0;
                displayPrice = ticket.Price;
                ticket.Type = 0;

                listSeatSelected.Remove(btnSeat);       
                grpLoaiVe.Enabled = false;
            }
            else if (btnSeat.BackColor == Color.Red)
            {
                MessageBox.Show("Ghế số [" + btnSeat.Text + "] đã có người mua");
            }
            LoadBill();
            if (listSeatSelected.Count > 0)
            {
                chkCustomer.Enabled = true;
            }
            else
            {
                chkCustomer.Enabled = false;
            }
        }
   
        void CheckLeftRight()
        {
            //foreach (var item in listSeatSelected)
            //{
            //    if(item.Text.First)
            //}
        }

        //dùng để ẩn hiện lable điểm tích lũy của khách hàng thành viên
        private void ShowOrHideLablePoint()
        {
            if (chkCustomer.Checked == true)
            {
                pnCustomer.Visible = true;
            }
            else
            {
                pnCustomer.Visible = false;
            }
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

            rdoAdult.Checked = true;
            grpLoaiVe.Enabled = false;
            chkCustomer.Checked = false;
            chkCustomer.Enabled = false;

            ShowOrHideLablePoint();

            total = 0;
            displayPrice = 0;

            payment = 0;


            LoadBill();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            if (listSeatSelected.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn vé trước khi thanh toán!");
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
                if (chkCustomer.Checked == true)
                {
                    foreach (Button btn in listSeatSelected)
                    {
                        Ticket ticket = btn.Tag as Ticket;

                        ret += TicketDAO.BuyTicket(ticket.ID, ticket.Type, customer.ID, ticket.Price);
                    }
                    CustomerDB.UpdatePointCustomer(customer.ID, customer.Point);
                }
                else
                {
                    foreach (Button btn in listSeatSelected)
                    {
                        Ticket ticket = btn.Tag as Ticket;

                        ret += TicketDAO.BuyTicket(ticket.ID, ticket.Type, ticket.Price);
                    }
                }
                if (ret == listSeatSelected.Count)
                {
                    MessageBox.Show("Bạn đã mua vé thành công!");
                }
            }
            RestoreDefault();
            this.OnLoad(new EventArgs());
        }

       


        


    }
}
