
namespace project_CinemaManager
{
    partial class frmAdmin_GetCusNeedSupport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnChooseToSupport = new System.Windows.Forms.Button();
            this.lbname = new System.Windows.Forms.Label();
            this.dtgvMessage = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvMessage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnChooseToSupport
            // 
            this.btnChooseToSupport.Location = new System.Drawing.Point(123, 284);
            this.btnChooseToSupport.Name = "btnChooseToSupport";
            this.btnChooseToSupport.Size = new System.Drawing.Size(513, 45);
            this.btnChooseToSupport.TabIndex = 7;
            this.btnChooseToSupport.Text = "Choose To Support";
            this.btnChooseToSupport.UseVisualStyleBackColor = true;
            this.btnChooseToSupport.Click += new System.EventHandler(this.btnChooseToSupport_Click);
            // 
            // lbname
            // 
            this.lbname.AutoSize = true;
            this.lbname.Location = new System.Drawing.Point(12, 20);
            this.lbname.Name = "lbname";
            this.lbname.Size = new System.Drawing.Size(85, 17);
            this.lbname.TabIndex = 5;
            this.lbname.Text = "Your name: ";
            // 
            // dtgvMessage
            // 
            this.dtgvMessage.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dtgvMessage.BackgroundColor = System.Drawing.SystemColors.Info;
            this.dtgvMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtgvMessage.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dtgvMessage.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dtgvMessage.ColumnHeadersHeight = 29;
            this.dtgvMessage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgvMessage.DefaultCellStyle = dataGridViewCellStyle3;
            this.dtgvMessage.Location = new System.Drawing.Point(12, 50);
            this.dtgvMessage.Name = "dtgvMessage";
            this.dtgvMessage.ReadOnly = true;
            this.dtgvMessage.RowHeadersVisible = false;
            this.dtgvMessage.RowHeadersWidth = 51;
            this.dtgvMessage.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dtgvMessage.RowTemplate.Height = 24;
            this.dtgvMessage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgvMessage.Size = new System.Drawing.Size(754, 228);
            this.dtgvMessage.TabIndex = 4;
            // 
            // frmAdmin_GetSupportCus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 349);
            this.Controls.Add(this.btnChooseToSupport);
            this.Controls.Add(this.lbname);
            this.Controls.Add(this.dtgvMessage);
            this.Name = "frmAdmin_GetSupportCus";
            this.Text = "frmAdmin_GetSupportCus";
            ((System.ComponentModel.ISupportInitialize)(this.dtgvMessage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnChooseToSupport;
        private System.Windows.Forms.Label lbname;
        private System.Windows.Forms.DataGridView dtgvMessage;
    }
}