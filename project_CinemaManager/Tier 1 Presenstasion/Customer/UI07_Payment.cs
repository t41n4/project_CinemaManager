using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QRCoder;
using System.Drawing.Imaging;
using System.IO;
using project_CinemaManager.Controllers;

namespace project_CinemaManager
{
    public partial class UI07_Payment : Form
    {
        private string Cost;
        PaymentAction paymentAction;
        public UI07_Payment()
        {
            InitializeComponent();
        }
        public UI07_Payment(string cost)
        {
            InitializeComponent();
            this.Cost = cost;
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
            paymentAction.PaymentConfirm();
        }

        private void UI07_Payment_Load(object sender, EventArgs e)
        {
             paymentAction = new PaymentAction(Cost);
            string urlpay = paymentAction.Payment();
            if (urlpay != "")
            {
                pictureQR.Image = CreateQR(urlpay);
            }

        }
    }
}
