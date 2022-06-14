
namespace project_CinemaManager
{
    partial class frmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.lbstartwithWD = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_checkconected = new project_CinemaManager.RJToggleButton();
            this.btn_startwithwindow = new project_CinemaManager.RJToggleButton();
            this.SuspendLayout();
            // 
            // lbstartwithWD
            // 
            this.lbstartwithWD.AutoSize = true;
            this.lbstartwithWD.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbstartwithWD.Location = new System.Drawing.Point(51, 164);
            this.lbstartwithWD.Name = "lbstartwithWD";
            this.lbstartwithWD.Size = new System.Drawing.Size(308, 29);
            this.lbstartwithWD.TabIndex = 11;
            this.lbstartwithWD.Text = "Khởi động cùng Windows";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(51, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 29);
            this.label1.TabIndex = 13;
            this.label1.Text = "Kiểm tra kết nối";
            // 
            // btn_checkconected
            // 
            this.btn_checkconected.AutoSize = true;
            this.btn_checkconected.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_checkconected.Location = new System.Drawing.Point(385, 103);
            this.btn_checkconected.MinimumSize = new System.Drawing.Size(45, 22);
            this.btn_checkconected.Name = "btn_checkconected";
            this.btn_checkconected.OffBackColor = System.Drawing.Color.Gray;
            this.btn_checkconected.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.btn_checkconected.OnBackColor = System.Drawing.Color.MediumSlateBlue;
            this.btn_checkconected.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.btn_checkconected.Size = new System.Drawing.Size(45, 22);
            this.btn_checkconected.TabIndex = 14;
            this.btn_checkconected.UseVisualStyleBackColor = true;
            this.btn_checkconected.CheckedChanged += new System.EventHandler(this.btn_checkconected_CheckedChanged);
            // 
            // btn_startwithwindow
            // 
            this.btn_startwithwindow.AutoSize = true;
            this.btn_startwithwindow.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_startwithwindow.Location = new System.Drawing.Point(385, 166);
            this.btn_startwithwindow.MinimumSize = new System.Drawing.Size(45, 22);
            this.btn_startwithwindow.Name = "btn_startwithwindow";
            this.btn_startwithwindow.OffBackColor = System.Drawing.Color.Gray;
            this.btn_startwithwindow.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.btn_startwithwindow.OnBackColor = System.Drawing.Color.MediumSlateBlue;
            this.btn_startwithwindow.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.btn_startwithwindow.Size = new System.Drawing.Size(45, 22);
            this.btn_startwithwindow.TabIndex = 12;
            this.btn_startwithwindow.UseVisualStyleBackColor = true;
            this.btn_startwithwindow.CheckedChanged += new System.EventHandler(this.btn_startwithwindow_CheckedChanged);
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(500, 322);
            this.Controls.Add(this.btn_checkconected);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_startwithwindow);
            this.Controls.Add(this.lbstartwithWD);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RJToggleButton btn_startwithwindow;
        private System.Windows.Forms.Label lbstartwithWD;
        private System.Windows.Forms.Label label1;
        private RJToggleButton btn_checkconected;
    }
}