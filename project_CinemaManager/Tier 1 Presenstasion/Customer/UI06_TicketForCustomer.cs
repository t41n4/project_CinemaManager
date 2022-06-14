using QRCoder;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace project_CinemaManager
{
    public partial class UI06_TicketForCustomer : Form
    {
        private string[] idVe;

        public UI06_TicketForCustomer(string[] id)
        {
            InitializeComponent();
            idVe = id;
        }

        private void UI06_TicketForCustomer_Load(object sender, EventArgs e)
        {
            LoadTicket();
            this.rpViewer.LocalReport.EnableExternalImages = true;
            this.rpViewer.RefreshReport();
        }

        private Bitmap CreateQR(string content)
        {
            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
            QRCode qRCode = new QRCode(qRCodeData);
            return qRCode.GetGraphic(5);
        }

        private string GetContent(DataRow row)
        {
            string content = "";
            foreach (var item in row.ItemArray)
            {
                content += item.ToString() + " ";
            }
            return content;
        }

        private void LoadTicket()
        {
            DataTable dt = new DataTable();
            foreach (var id in idVe)
            {
                if (dt.Columns.Count == 0)
                {
                    dt = uSP_GetInfoForTicketTableAdapter.GetData(id);
                    MemoryStream ms = new MemoryStream();
                    string strToQR = GetContent(dt.Rows[0]);
                    CreateQR(strToQR).Save(ms, ImageFormat.Bmp);
                    dt.Rows[0]["QRCodeImage"] = ms.ToArray();
                }
                else
                {
                    DataRow row = uSP_GetInfoForTicketTableAdapter.GetData(id).Rows[0];
                    MemoryStream ms = new MemoryStream();
                    string strToQR = GetContent(row);
                    CreateQR(strToQR).Save(ms, ImageFormat.Bmp);
                    row["QRCodeImage"] = ms.ToArray();
                    dt.Rows.Add(row.ItemArray);
                }
            }
            USP_GetInfoForTicketBindingSource.DataSource = dt;
            qLRPDataSet.Clear();
            qLRPDataSet.Tables.Add(dt);
        }
    }
}