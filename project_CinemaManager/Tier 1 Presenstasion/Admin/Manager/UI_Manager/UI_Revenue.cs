using DB;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace project_CinemaManager
{
    public partial class RevenueUC : UserControl
    {
        public RevenueUC()
        {
            InitializeComponent();
            LoadRevenue();
        }

        private void LoadRevenue()
        {
            LoadMovieIntoCombobox(cboSelectMovie);
           // LoadDateTimePickerRevenue();//Set "Từ ngày" & "Đến ngày ngày" về đầu tháng & cuối tháng
            //LoadRevenue(cboSelectMovie.SelectedValue.ToString(), dtmFromDate.Value, dtmToDate.Value);
        }

        private void LoadMovieIntoCombobox(ComboBox comboBox)
        {
            comboBox.DataSource = MovieDB.GetListMovie();
            comboBox.DisplayMember = "Name";
            comboBox.ValueMember = "ID";
        }

        private void LoadDateTimePickerRevenue()
        {
            dtmFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtmToDate.Value = dtmFromDate.Value.AddMonths(1).AddDays(-1);
        }

        private void LoadRevenue(string idMovie, DateTime fromDate, DateTime toDate)
        {
            CultureInfo culture = new CultureInfo("vi-VN");
            dtgvRevenue.DataSource = RevenueDB.GetRevenue(idMovie, fromDate, toDate);
            txtDoanhThu.Text = GetSumRevenue().ToString("c", culture);
        }

        private decimal GetSumRevenue()
        {
            decimal sum = 0;
            foreach (DataGridViewRow row in dtgvRevenue.Rows)
            {
                sum += Convert.ToDecimal(row.Cells["Tiền vé"].Value);
            }
            return sum;
        }

        private void btnShowRevenue_Click(object sender, EventArgs e)
        {
            LoadRevenue(cboSelectMovie.SelectedValue.ToString(), dtmFromDate.Value, dtmToDate.Value);
        }

        private void btnReportRevenue_Click(object sender, EventArgs e)
        {
            frmReport frm = new frmReport(cboSelectMovie.SelectedValue.ToString(), dtmFromDate.Value, dtmToDate.Value);
            frm.ShowDialog();
        }
    }
}