using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace project_CinemaManager
{
    public partial class UI_DataManagernew : UserControl
    {
        private IconButton currentBtn;
        private Panel leftBorderBtn;

        public UI_DataManagernew()
        {
            InitializeComponent();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            panelMenu.Controls.Add(leftBorderBtn);


        }
        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(172, 126, 241);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(249, 88, 155);
            public static Color color6 = Color.FromArgb(24, 161, 251);
        }
        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();
                //Button
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(37, 36, 81);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;
               // Leftborder button;
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();

            }
        }
        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(31, 30, 68);
                currentBtn.ForeColor = Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.Gainsboro;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }
        private void Reset()
        {
            DisableButton();
            //leftBorderBtn.Visible = false;
        }

        private void btn_Manhinh_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            pnData.Controls.Clear();
            ScreenTypeUC screenTypeUC = new ScreenTypeUC();
            screenTypeUC.Dock = DockStyle.Fill;
            pnData.Controls.Add(screenTypeUC);
        }

        private void btn_PHongchieu_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color2);

            pnData.Controls.Clear();
            CinemaUC cinemaUc = new CinemaUC();
            cinemaUc.Dock = DockStyle.Fill;
            pnData.Controls.Add(cinemaUc);
        }

        private void btn_theLoai_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color3);

            pnData.Controls.Clear();
            GenreUC genreUc = new GenreUC();
            genreUc.Dock = DockStyle.Fill;
            pnData.Controls.Add(genreUc);
        }

        private void btn_Phim_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);

            pnData.Controls.Clear();
            MovieUC movieUc = new MovieUC();
            movieUc.Dock = DockStyle.Fill;
            pnData.Controls.Add(movieUc);
        }

        private void btn_dinhdang_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color5);

            pnData.Controls.Clear();
            FormatMovieUC formatMovieUc = new FormatMovieUC();
            formatMovieUc.Dock = DockStyle.Fill;
            pnData.Controls.Add(formatMovieUc);
        }

        private void btn_Lichchieu_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);

            pnData.Controls.Clear();
            ShowTimesUC showTimesUc = new ShowTimesUC();
            showTimesUc.Dock = DockStyle.Fill;
            pnData.Controls.Add(showTimesUc);
        }

        private void btn_ve_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color3);

            pnData.Controls.Clear();
            TicketsUC ticketsUc = new TicketsUC();
            ticketsUc.Dock = DockStyle.Fill;
            pnData.Controls.Add(ticketsUc);
        }

        private void pnData_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
