﻿namespace frmAdminUserControls
{
    partial class CustomerUC
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnDeleteCustomer = new System.Windows.Forms.Button();
            this.btnUpdateCustomer = new System.Windows.Forms.Button();
            this.btnAddCustomer = new System.Windows.Forms.Button();
            this.grpCustomer = new System.Windows.Forms.GroupBox();
            this.lblCusID = new System.Windows.Forms.Label();
            this.txtCusID = new System.Windows.Forms.TextBox();
            this.txtCusAddress = new System.Windows.Forms.TextBox();
            this.lblCusINumber = new System.Windows.Forms.Label();
            this.txtCusName = new System.Windows.Forms.TextBox();
            this.lblCusBirth = new System.Windows.Forms.Label();
            this.txtCusPhone = new System.Windows.Forms.TextBox();
            this.lblCusPhone = new System.Windows.Forms.Label();
            this.txtCusBirth = new System.Windows.Forms.TextBox();
            this.lblCusAddress = new System.Windows.Forms.Label();
            this.txtCusINumber = new System.Windows.Forms.TextBox();
            this.lblCusName = new System.Windows.Forms.Label();
            this.dtgvCustomer = new System.Windows.Forms.DataGridView();
            this.btnSearchCus = new System.Windows.Forms.Button();
            this.txtSearchCus = new System.Windows.Forms.TextBox();
            this.grpCustomer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvCustomer)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDeleteCustomer
            // 
            this.btnDeleteCustomer.BackColor = System.Drawing.Color.RosyBrown;
            this.btnDeleteCustomer.Font = new System.Drawing.Font("Consolas", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteCustomer.Location = new System.Drawing.Point(693, 116);
            this.btnDeleteCustomer.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeleteCustomer.Name = "btnDeleteCustomer";
            this.btnDeleteCustomer.Size = new System.Drawing.Size(68, 26);
            this.btnDeleteCustomer.TabIndex = 17;
            this.btnDeleteCustomer.Text = "Xóa";
            this.btnDeleteCustomer.UseVisualStyleBackColor = false;
            this.btnDeleteCustomer.Click += new System.EventHandler(this.btnDeleteCustomer_Click);
            // 
            // btnUpdateCustomer
            // 
            this.btnUpdateCustomer.Font = new System.Drawing.Font("Consolas", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateCustomer.Location = new System.Drawing.Point(694, 77);
            this.btnUpdateCustomer.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpdateCustomer.Name = "btnUpdateCustomer";
            this.btnUpdateCustomer.Size = new System.Drawing.Size(68, 26);
            this.btnUpdateCustomer.TabIndex = 18;
            this.btnUpdateCustomer.Text = "Sửa";
            this.btnUpdateCustomer.UseVisualStyleBackColor = true;
            this.btnUpdateCustomer.Click += new System.EventHandler(this.btnUpdateCustomer_Click);
            // 
            // btnAddCustomer
            // 
            this.btnAddCustomer.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnAddCustomer.Font = new System.Drawing.Font("Consolas", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddCustomer.Location = new System.Drawing.Point(694, 33);
            this.btnAddCustomer.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddCustomer.Name = "btnAddCustomer";
            this.btnAddCustomer.Size = new System.Drawing.Size(68, 26);
            this.btnAddCustomer.TabIndex = 19;
            this.btnAddCustomer.Text = "Thêm";
            this.btnAddCustomer.UseVisualStyleBackColor = false;
            this.btnAddCustomer.Click += new System.EventHandler(this.btnAddCustomer_Click);
            // 
            // grpCustomer
            // 
            this.grpCustomer.BackColor = System.Drawing.Color.Transparent;
            this.grpCustomer.Controls.Add(this.lblCusID);
            this.grpCustomer.Controls.Add(this.txtCusID);
            this.grpCustomer.Controls.Add(this.txtCusAddress);
            this.grpCustomer.Controls.Add(this.lblCusINumber);
            this.grpCustomer.Controls.Add(this.txtCusName);
            this.grpCustomer.Controls.Add(this.lblCusBirth);
            this.grpCustomer.Controls.Add(this.txtCusPhone);
            this.grpCustomer.Controls.Add(this.lblCusPhone);
            this.grpCustomer.Controls.Add(this.txtCusBirth);
            this.grpCustomer.Controls.Add(this.lblCusAddress);
            this.grpCustomer.Controls.Add(this.txtCusINumber);
            this.grpCustomer.Controls.Add(this.lblCusName);
            this.grpCustomer.Font = new System.Drawing.Font("Consolas", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpCustomer.Location = new System.Drawing.Point(156, 24);
            this.grpCustomer.Margin = new System.Windows.Forms.Padding(2);
            this.grpCustomer.Name = "grpCustomer";
            this.grpCustomer.Padding = new System.Windows.Forms.Padding(2);
            this.grpCustomer.Size = new System.Drawing.Size(533, 118);
            this.grpCustomer.TabIndex = 16;
            this.grpCustomer.TabStop = false;
            this.grpCustomer.Text = "Thông tin khách hàng";
            // 
            // lblCusID
            // 
            this.lblCusID.AutoSize = true;
            this.lblCusID.Font = new System.Drawing.Font("Consolas", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCusID.Location = new System.Drawing.Point(18, 32);
            this.lblCusID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCusID.Name = "lblCusID";
            this.lblCusID.Size = new System.Drawing.Size(56, 18);
            this.lblCusID.TabIndex = 4;
            this.lblCusID.Text = "Mã KH:";
            // 
            // txtCusID
            // 
            this.txtCusID.Font = new System.Drawing.Font("Consolas", 12F);
            this.txtCusID.Location = new System.Drawing.Point(119, 28);
            this.txtCusID.Margin = new System.Windows.Forms.Padding(2);
            this.txtCusID.Name = "txtCusID";
            this.txtCusID.Size = new System.Drawing.Size(144, 26);
            this.txtCusID.TabIndex = 2;
            // 
            // txtCusAddress
            // 
            this.txtCusAddress.Font = new System.Drawing.Font("Consolas", 12F);
            this.txtCusAddress.Location = new System.Drawing.Point(377, 29);
            this.txtCusAddress.Margin = new System.Windows.Forms.Padding(2);
            this.txtCusAddress.Name = "txtCusAddress";
            this.txtCusAddress.Size = new System.Drawing.Size(144, 26);
            this.txtCusAddress.TabIndex = 2;
            // 
            // lblCusINumber
            // 
            this.lblCusINumber.AutoSize = true;
            this.lblCusINumber.Font = new System.Drawing.Font("Consolas", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCusINumber.Location = new System.Drawing.Point(307, 88);
            this.lblCusINumber.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCusINumber.Name = "lblCusINumber";
            this.lblCusINumber.Size = new System.Drawing.Size(48, 18);
            this.lblCusINumber.TabIndex = 4;
            this.lblCusINumber.Text = "CMND:";
            // 
            // txtCusName
            // 
            this.txtCusName.Font = new System.Drawing.Font("Consolas", 12F);
            this.txtCusName.Location = new System.Drawing.Point(119, 56);
            this.txtCusName.Margin = new System.Windows.Forms.Padding(2);
            this.txtCusName.Name = "txtCusName";
            this.txtCusName.Size = new System.Drawing.Size(144, 26);
            this.txtCusName.TabIndex = 2;
            // 
            // lblCusBirth
            // 
            this.lblCusBirth.AutoSize = true;
            this.lblCusBirth.Font = new System.Drawing.Font("Consolas", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCusBirth.Location = new System.Drawing.Point(18, 88);
            this.lblCusBirth.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCusBirth.Name = "lblCusBirth";
            this.lblCusBirth.Size = new System.Drawing.Size(88, 18);
            this.lblCusBirth.TabIndex = 4;
            this.lblCusBirth.Text = "Ngày sinh:";
            // 
            // txtCusPhone
            // 
            this.txtCusPhone.Font = new System.Drawing.Font("Consolas", 12F);
            this.txtCusPhone.Location = new System.Drawing.Point(377, 58);
            this.txtCusPhone.Margin = new System.Windows.Forms.Padding(2);
            this.txtCusPhone.Name = "txtCusPhone";
            this.txtCusPhone.Size = new System.Drawing.Size(144, 26);
            this.txtCusPhone.TabIndex = 2;
            // 
            // lblCusPhone
            // 
            this.lblCusPhone.AutoSize = true;
            this.lblCusPhone.Font = new System.Drawing.Font("Consolas", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCusPhone.Location = new System.Drawing.Point(307, 61);
            this.lblCusPhone.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCusPhone.Name = "lblCusPhone";
            this.lblCusPhone.Size = new System.Drawing.Size(56, 18);
            this.lblCusPhone.TabIndex = 4;
            this.lblCusPhone.Text = "Số ĐT:";
            // 
            // txtCusBirth
            // 
            this.txtCusBirth.Font = new System.Drawing.Font("Consolas", 12F);
            this.txtCusBirth.Location = new System.Drawing.Point(119, 86);
            this.txtCusBirth.Margin = new System.Windows.Forms.Padding(2);
            this.txtCusBirth.Name = "txtCusBirth";
            this.txtCusBirth.Size = new System.Drawing.Size(144, 26);
            this.txtCusBirth.TabIndex = 2;
            // 
            // lblCusAddress
            // 
            this.lblCusAddress.AutoSize = true;
            this.lblCusAddress.Font = new System.Drawing.Font("Consolas", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCusAddress.Location = new System.Drawing.Point(307, 32);
            this.lblCusAddress.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCusAddress.Name = "lblCusAddress";
            this.lblCusAddress.Size = new System.Drawing.Size(72, 18);
            this.lblCusAddress.TabIndex = 4;
            this.lblCusAddress.Text = "Địa chỉ:";
            // 
            // txtCusINumber
            // 
            this.txtCusINumber.Font = new System.Drawing.Font("Consolas", 12F);
            this.txtCusINumber.Location = new System.Drawing.Point(377, 88);
            this.txtCusINumber.Margin = new System.Windows.Forms.Padding(2);
            this.txtCusINumber.Name = "txtCusINumber";
            this.txtCusINumber.Size = new System.Drawing.Size(144, 26);
            this.txtCusINumber.TabIndex = 2;
            // 
            // lblCusName
            // 
            this.lblCusName.AutoSize = true;
            this.lblCusName.Font = new System.Drawing.Font("Consolas", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCusName.Location = new System.Drawing.Point(18, 61);
            this.lblCusName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCusName.Name = "lblCusName";
            this.lblCusName.Size = new System.Drawing.Size(64, 18);
            this.lblCusName.TabIndex = 4;
            this.lblCusName.Text = "Họ tên:";
            // 
            // dtgvCustomer
            // 
            this.dtgvCustomer.AllowUserToAddRows = false;
            this.dtgvCustomer.AllowUserToDeleteRows = false;
            this.dtgvCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgvCustomer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgvCustomer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtgvCustomer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvCustomer.Location = new System.Drawing.Point(2, 180);
            this.dtgvCustomer.Margin = new System.Windows.Forms.Padding(2);
            this.dtgvCustomer.Name = "dtgvCustomer";
            this.dtgvCustomer.ReadOnly = true;
            this.dtgvCustomer.RowHeadersWidth = 51;
            this.dtgvCustomer.RowTemplate.Height = 24;
            this.dtgvCustomer.Size = new System.Drawing.Size(1046, 348);
            this.dtgvCustomer.TabIndex = 14;
            // 
            // btnSearchCus
            // 
            this.btnSearchCus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearchCus.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchCus.Location = new System.Drawing.Point(871, 110);
            this.btnSearchCus.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearchCus.Name = "btnSearchCus";
            this.btnSearchCus.Size = new System.Drawing.Size(20, 21);
            this.btnSearchCus.TabIndex = 21;
            this.btnSearchCus.UseVisualStyleBackColor = true;
            this.btnSearchCus.Click += new System.EventHandler(this.btnSearchCus_Click);
            // 
            // txtSearchCus
            // 
            this.txtSearchCus.Font = new System.Drawing.Font("Consolas", 12F);
            this.txtSearchCus.Location = new System.Drawing.Point(766, 114);
            this.txtSearchCus.Margin = new System.Windows.Forms.Padding(2);
            this.txtSearchCus.Name = "txtSearchCus";
            this.txtSearchCus.Size = new System.Drawing.Size(100, 26);
            this.txtSearchCus.TabIndex = 20;
            this.txtSearchCus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchCus_KeyDown);
            // 
            // CustomerUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSearchCus);
            this.Controls.Add(this.txtSearchCus);
            this.Controls.Add(this.btnDeleteCustomer);
            this.Controls.Add(this.btnUpdateCustomer);
            this.Controls.Add(this.btnAddCustomer);
            this.Controls.Add(this.grpCustomer);
            this.Controls.Add(this.dtgvCustomer);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CustomerUC";
            this.Size = new System.Drawing.Size(1050, 528);
            this.grpCustomer.ResumeLayout(false);
            this.grpCustomer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvCustomer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnDeleteCustomer;
        private System.Windows.Forms.Button btnUpdateCustomer;
        private System.Windows.Forms.Button btnAddCustomer;
        private System.Windows.Forms.GroupBox grpCustomer;
        private System.Windows.Forms.Label lblCusID;
        private System.Windows.Forms.TextBox txtCusID;
        private System.Windows.Forms.TextBox txtCusAddress;
        private System.Windows.Forms.Label lblCusINumber;
        private System.Windows.Forms.TextBox txtCusName;
        private System.Windows.Forms.Label lblCusBirth;
        private System.Windows.Forms.TextBox txtCusPhone;
        private System.Windows.Forms.Label lblCusPhone;
        private System.Windows.Forms.TextBox txtCusBirth;
        private System.Windows.Forms.Label lblCusAddress;
        private System.Windows.Forms.TextBox txtCusINumber;
        private System.Windows.Forms.Label lblCusName;
        private System.Windows.Forms.DataGridView dtgvCustomer;
        private System.Windows.Forms.Button btnSearchCus;
        private System.Windows.Forms.TextBox txtSearchCus;
    }
}
