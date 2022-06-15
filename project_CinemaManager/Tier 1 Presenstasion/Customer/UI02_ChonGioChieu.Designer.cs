
namespace project_CinemaManager
{
    partial class UI_ChonGioChieu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UI_ChonGioChieu));
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMovieName_Showtime = new System.Windows.Forms.TextBox();
            this.txtTicketPrice_Showtime = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnChonGioChieu = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtShowtimeDateTime = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtShowRoom_Showtime = new System.Windows.Forms.TextBox();
            this.txStatus_ShowTimes = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtgvShowtime = new System.Windows.Forms.DataGridView();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvShowtime)).BeginInit();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(11, 223);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 29);
            this.label7.TabIndex = 22;
            this.label7.Text = "Giá vé:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 29);
            this.label4.TabIndex = 24;
            this.label4.Text = "Phim:";
            // 
            // txtMovieName_Showtime
            // 
            this.txtMovieName_Showtime.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold);
            this.txtMovieName_Showtime.Location = new System.Drawing.Point(191, 110);
            this.txtMovieName_Showtime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMovieName_Showtime.Multiline = true;
            this.txtMovieName_Showtime.Name = "txtMovieName_Showtime";
            this.txtMovieName_Showtime.ReadOnly = true;
            this.txtMovieName_Showtime.Size = new System.Drawing.Size(286, 71);
            this.txtMovieName_Showtime.TabIndex = 17;
            // 
            // txtTicketPrice_Showtime
            // 
            this.txtTicketPrice_Showtime.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold);
            this.txtTicketPrice_Showtime.Location = new System.Drawing.Point(191, 226);
            this.txtTicketPrice_Showtime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTicketPrice_Showtime.Name = "txtTicketPrice_Showtime";
            this.txtTicketPrice_Showtime.ReadOnly = true;
            this.txtTicketPrice_Showtime.Size = new System.Drawing.Size(286, 34);
            this.txtTicketPrice_Showtime.TabIndex = 18;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnChonGioChieu);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtShowRoom_Showtime);
            this.panel2.Controls.Add(this.txtMovieName_Showtime);
            this.panel2.Controls.Add(this.txStatus_ShowTimes);
            this.panel2.Controls.Add(this.txtTicketPrice_Showtime);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(544, 404);
            this.panel2.TabIndex = 0;
            // 
            // btnChonGioChieu
            // 
            this.btnChonGioChieu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(234)))), ((int)(((byte)(216)))));
            this.btnChonGioChieu.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChonGioChieu.Location = new System.Drawing.Point(149, 354);
            this.btnChonGioChieu.Name = "btnChonGioChieu";
            this.btnChonGioChieu.Size = new System.Drawing.Size(216, 37);
            this.btnChonGioChieu.TabIndex = 29;
            this.btnChonGioChieu.Text = "Chọn Giờ Chiếu";
            this.btnChonGioChieu.UseVisualStyleBackColor = false;
            this.btnChonGioChieu.Click += new System.EventHandler(this.btnChonGioChieu_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtShowtimeDateTime);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 11);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(485, 89);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thời gian chiếu";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 29);
            this.label5.TabIndex = 12;
            this.label5.Text = "Ngày,Giờ:";
            // 
            // txtShowtimeDateTime
            // 
            this.txtShowtimeDateTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold);
            this.txtShowtimeDateTime.Location = new System.Drawing.Point(181, 45);
            this.txtShowtimeDateTime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtShowtimeDateTime.Name = "txtShowtimeDateTime";
            this.txtShowtimeDateTime.ReadOnly = true;
            this.txtShowtimeDateTime.Size = new System.Drawing.Size(286, 34);
            this.txtShowtimeDateTime.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 182);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 29);
            this.label2.TabIndex = 21;
            this.label2.Text = "Phòng chiếu:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 263);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 29);
            this.label1.TabIndex = 22;
            this.label1.Text = "Tình Trạng:";
            // 
            // txtShowRoom_Showtime
            // 
            this.txtShowRoom_Showtime.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold);
            this.txtShowRoom_Showtime.Location = new System.Drawing.Point(191, 186);
            this.txtShowRoom_Showtime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtShowRoom_Showtime.Name = "txtShowRoom_Showtime";
            this.txtShowRoom_Showtime.ReadOnly = true;
            this.txtShowRoom_Showtime.Size = new System.Drawing.Size(286, 34);
            this.txtShowRoom_Showtime.TabIndex = 17;
            // 
            // txStatus_ShowTimes
            // 
            this.txStatus_ShowTimes.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold);
            this.txStatus_ShowTimes.Location = new System.Drawing.Point(191, 267);
            this.txStatus_ShowTimes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txStatus_ShowTimes.Name = "txStatus_ShowTimes";
            this.txStatus_ShowTimes.ReadOnly = true;
            this.txStatus_ShowTimes.Size = new System.Drawing.Size(286, 34);
            this.txStatus_ShowTimes.TabIndex = 18;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(234)))), ((int)(((byte)(216)))));
            this.panel1.Controls.Add(this.dtgvShowtime);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1452, 404);
            this.panel1.TabIndex = 13;
            // 
            // dtgvShowtime
            // 
            this.dtgvShowtime.AllowUserToAddRows = false;
            this.dtgvShowtime.AllowUserToDeleteRows = false;
            this.dtgvShowtime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgvShowtime.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgvShowtime.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvShowtime.Location = new System.Drawing.Point(551, 0);
            this.dtgvShowtime.Margin = new System.Windows.Forms.Padding(4);
            this.dtgvShowtime.Name = "dtgvShowtime";
            this.dtgvShowtime.ReadOnly = true;
            this.dtgvShowtime.RowHeadersWidth = 51;
            this.dtgvShowtime.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgvShowtime.Size = new System.Drawing.Size(901, 391);
            this.dtgvShowtime.TabIndex = 1;
            this.dtgvShowtime.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvShowtime_CellContentClick);
            // 
            // UI_ChonGioChieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1452, 404);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UI_ChonGioChieu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UI_ChonGioChieu";
            this.Load += new System.EventHandler(this.UI_ChonGioChieu_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvShowtime)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMovieName_Showtime;
        private System.Windows.Forms.TextBox txtTicketPrice_Showtime;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dtgvShowtime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnChonGioChieu;
        private System.Windows.Forms.TextBox txtShowRoom_Showtime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txStatus_ShowTimes;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtShowtimeDateTime;
    }
}