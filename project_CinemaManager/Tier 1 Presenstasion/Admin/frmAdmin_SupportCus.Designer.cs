
namespace project_CinemaManager
{
    partial class frmAdmin_SupportCus
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAdmin_SupportCus));
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.lbname = new System.Windows.Forms.Label();
            this.dtgvMessage = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvMessage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendMessage.Location = new System.Drawing.Point(480, 360);
            this.btnSendMessage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(93, 37);
            this.btnSendMessage.TabIndex = 7;
            this.btnSendMessage.Text = "Send";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMessage.Location = new System.Drawing.Point(9, 369);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(468, 20);
            this.txtMessage.TabIndex = 6;
            this.txtMessage.Text = "your message";
            this.txtMessage.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtMessage_MouseClick);
            this.txtMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMessage_KeyDown);
            // 
            // lbname
            // 
            this.lbname.AutoSize = true;
            this.lbname.Location = new System.Drawing.Point(9, 7);
            this.lbname.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbname.Name = "lbname";
            this.lbname.Size = new System.Drawing.Size(64, 13);
            this.lbname.TabIndex = 5;
            this.lbname.Text = "Your name: ";
            // 
            // dtgvMessage
            // 
            this.dtgvMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgvMessage.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dtgvMessage.BackgroundColor = System.Drawing.SystemColors.HighlightText;
            this.dtgvMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtgvMessage.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dtgvMessage.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dtgvMessage.ColumnHeadersHeight = 29;
            this.dtgvMessage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgvMessage.DefaultCellStyle = dataGridViewCellStyle1;
            this.dtgvMessage.Location = new System.Drawing.Point(9, 32);
            this.dtgvMessage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtgvMessage.Name = "dtgvMessage";
            this.dtgvMessage.ReadOnly = true;
            this.dtgvMessage.RowHeadersVisible = false;
            this.dtgvMessage.RowHeadersWidth = 51;
            this.dtgvMessage.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dtgvMessage.RowTemplate.Height = 24;
            this.dtgvMessage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgvMessage.Size = new System.Drawing.Size(564, 323);
            this.dtgvMessage.TabIndex = 4;
            // 
            // frmAdmin_SupportCus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 416);
            this.Controls.Add(this.btnSendMessage);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.lbname);
            this.Controls.Add(this.dtgvMessage);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmAdmin_SupportCus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAdmin_GetSupport";
            this.Load += new System.EventHandler(this.frmAdmin_SupportCus_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmAdmin_SupportCus_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvMessage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSendMessage;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label lbname;
        private System.Windows.Forms.DataGridView dtgvMessage;
    }
}