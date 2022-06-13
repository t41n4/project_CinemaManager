using QRCoder;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace project_CinemaManager
{
    public partial class UI07_Payment : Form
    {
        private string PayUrl;
        public string status = "default";

        public UI07_Payment()
        {
            InitializeComponent();
        }

        public UI07_Payment(string payUrl)
        {
            InitializeComponent();
            this.PayUrl = payUrl;
        }

        private Bitmap CreateQR(string content)
        {
            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
            QRCode qRCode = new QRCode(qRCodeData);
            return qRCode.GetGraphic(5);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
        }

        private void UI07_Payment_Load(object sender, EventArgs e)
        {
            try
            {
                webView1.Url = PayUrl;
                webView1.Create(pictureQR.Handle);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void webView1_UrlChanged(object sender, EventArgs e)
        {
            if (status == "default")
            {
                if (webView1.Url.Contains("Successful"))
                {
                    status = "0";
                    webView1.Close(true);
                    MessageBox.Show("Bạn Đã Thanh Toán Thành Công!!");

                    this.Close();
                }
                else if (webView1.Url.Contains("pay?"))
                {
                    return;
                }
                else
                {
                    status = "1";
                    webView1.Close(true);
                    MessageBox.Show("Bạn Đã Thanh Toán Thất Bại!!");
                    this.Close();
                }
            }
        }
    }
}