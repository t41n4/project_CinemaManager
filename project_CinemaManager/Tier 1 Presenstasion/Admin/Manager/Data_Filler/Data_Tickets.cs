﻿using Application;
using DB;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace project_CinemaManager
{
    public partial class TicketsUC : UserControl
    {
        public TicketsUC()
        {
            InitializeComponent();
            LoadAllListShowTimes();
        }

        private void LoadAllListShowTimes()
        {
            lsvAllListShowTimes.Items.Clear();

            List<ShowTimes> allListShowTime = ShowTimeDB.GetAllListShowTimes();
            foreach (ShowTimes showTimes in allListShowTime)
            {
                ListViewItem lvi = new ListViewItem(showTimes.CinemaID);
                lvi.SubItems.Add(showTimes.MovieName);
                lvi.SubItems.Add(showTimes.Time.ToString("HH:mm:ss dd/MM/yyyy"));
                lvi.Tag = showTimes;

                if (showTimes.Status == 1)
                {
                    lvi.SubItems.Add("Đã tạo");
                }
                else
                {
                    lvi.SubItems.Add("Chưa Tạo");
                }
                lsvAllListShowTimes.Items.Add(lvi);
            }
        }

        private void LoadTicketsByShowTimes(string showTimesID)
        {
            List<Ticket> listTicket = TicketDB.GetListTicketsByShowTimes(showTimesID);
            dtgvTicket.DataSource = listTicket;
        }

        private void LoadTicketsBoughtByShowTimes(string showTimesID)
        {
            List<Ticket> listTicket = TicketDB.GetListTicketsBoughtByShowTimes(showTimesID);
            dtgvTicket.DataSource = listTicket;
        }

        private void AutoCreateTicketsByShowTimes(ShowTimes showTimes)
        {
            int result = 0;
            Cinema cinema = CinemaDB.GetCinemaByName(showTimes.CinemaID);
            int Row = cinema.Row;
            int Column = cinema.SeatInRow;
            for (int i = 0; i < Row; i++)
            {
                int temp = i + 65;
                char nameRow = (char)(temp);
                for (int j = 1; j <= Column; j++)
                {
                    string seatName = nameRow.ToString() + j;
                    result += TicketDB.InsertTicketByShowTimes(showTimes.ID, seatName);
                }
            }
            if (result == Row * Column)
            {
                int ret = ShowTimeDB.UpdateStatusShowTimes(showTimes.ID, 1);
                if (ret > 0)
                    MessageBox.Show("TẠO VÉ TỰ ĐỘNG THÀNH CÔNG!", "THÔNG BÁO");
            }
            else
                MessageBox.Show("TẠO VÉ TỰ ĐỘNG THẤT BẠI!", "THÔNG BÁO");
        }

        private void btnAddTicketsByShowTime_Click(object sender, EventArgs e)
        {
            if (lsvAllListShowTimes.SelectedItems.Count > 0)
            {
                ShowTimes showTimes = lsvAllListShowTimes.SelectedItems[0].Tag as ShowTimes;
                if (showTimes.Status == 1)
                {
                    MessageBox.Show("LỊCH CHIẾU NÀY ĐÃ ĐƯỢC TẠO VÉ!!!", "THÔNG BÁO");
                    return;
                }
                AutoCreateTicketsByShowTimes(showTimes);
                LoadAllListShowTimes();
                LoadTicketsByShowTimes(showTimes.ID);
            }
            else
            {
                MessageBox.Show("BẠN CHƯA CHỌN LỊCH CHIẾU ĐỂ TẠO!!!", "THÔNG BÁO");
            }
        }

        private void lsvAllListShowTimes_Click(object sender, EventArgs e)
        {
            if (lsvAllListShowTimes.SelectedItems.Count > 0)
            {
                ShowTimes showTimes = lsvAllListShowTimes.SelectedItems[0].Tag as ShowTimes;
                LoadTicketsByShowTimes(showTimes.ID);
            }
        }

        private void btnDeleteTicketsByShowTime_Click(object sender, EventArgs e)
        {
            if (lsvAllListShowTimes.SelectedItems.Count > 0)
            {
                ShowTimes showTimes = lsvAllListShowTimes.SelectedItems[0].Tag as ShowTimes;
                if (showTimes.Status == 0)
                {
                    MessageBox.Show("LỊCH CHIẾU NÀY CHƯA ĐƯỢC TẠO VÉ!!!", "THÔNG BÁO");
                    return;
                }
                DeleteTicketsByShowTimes(showTimes);
                LoadAllListShowTimes();
                LoadTicketsByShowTimes(showTimes.ID);
            }
            else
            {
                MessageBox.Show("BẠN CHƯA CHỌN LỊCH CHIẾU ĐỂ XÓA!!!", "THÔNG BÁO");
            }
        }

        private void DeleteTicketsByShowTimes(ShowTimes showTimes)
        {
            //Cinema cinema = CinemaDB.GetCinemaByName(showTimes.CinemaID);
            //int Row = cinema.Row;
            //int Column = cinema.SeatInRow;
            int result = TicketDB.DeleteTicketsByShowTimes(showTimes.ID);
            if (result >= 0)
            {
                int ret = ShowTimeDB.UpdateStatusShowTimes(showTimes.ID, 0);
                if (ret > 0)
                    MessageBox.Show("XÓA TẤT CẢ CÁC VÉ CỦA LỊCH CHIẾU ID=" + showTimes.ID + " THÀNH CÔNG!", "THÔNG BÁO");
            }
            else
                MessageBox.Show("XÓA TẤT CẢ CÁC VÉ CỦA LỊCH CHIẾU ID=" + showTimes.ID + " THẤT BẠI!", "THÔNG BÁO");
        }

        private void btnAllListShowTimes_Click(object sender, EventArgs e)
        {
            LoadAllListShowTimes();
        }

        private void btnShowShowTimeNotCreateTickets_Click(object sender, EventArgs e)
        {
            LoadListShowTimesNotCreateTickets();
        }

        private void LoadListShowTimesNotCreateTickets()
        {
            lsvAllListShowTimes.Items.Clear();

            List<ShowTimes> allListShowTime = ShowTimeDB.GetListShowTimesNotCreateTickets();
            foreach (ShowTimes showTimes in allListShowTime)
            {
                ListViewItem lvi = new ListViewItem(showTimes.CinemaID);
                lvi.SubItems.Add(showTimes.MovieName);
                lvi.SubItems.Add(showTimes.Time.ToString("HH:mm:ss dd/MM/yyyy"));
                lvi.Tag = showTimes;

                if (showTimes.Status == 1)
                {
                    lvi.SubItems.Add("Đã tạo");
                }
                else
                {
                    lvi.SubItems.Add("Chưa Tạo");
                }
                lsvAllListShowTimes.Items.Add(lvi);
            }
        }

        private void btnShowAllTicketsBoughtByShowTime_Click(object sender, EventArgs e)
        {
            if (lsvAllListShowTimes.SelectedItems.Count > 0)
            {
                ShowTimes showTimes = lsvAllListShowTimes.SelectedItems[0].Tag as ShowTimes;
                LoadTicketsBoughtByShowTimes(showTimes.ID);
            }
            else
            {
                MessageBox.Show("BẠN CHƯA CHỌN LỊCH CHIẾU ĐỂ XEM!!!", "THÔNG BÁO");
            }
        }

        private void btnShowAllTicketsByShowTime_Click(object sender, EventArgs e)
        {
            if (lsvAllListShowTimes.SelectedItems.Count > 0)
            {
                ShowTimes showTimes = lsvAllListShowTimes.SelectedItems[0].Tag as ShowTimes;
                LoadTicketsByShowTimes(showTimes.ID);
            }
            else
            {
                MessageBox.Show("BẠN CHƯA CHỌN LỊCH CHIẾU ĐỂ XEM!!!", "THÔNG BÁO");
            }
        }
    }
}