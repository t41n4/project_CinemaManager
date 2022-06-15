namespace project_CinemaManager
{
    partial class UI06_TicketForCustomer
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UI06_TicketForCustomer));
            this.USP_GetInfoForTicketBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.qLRPDataSet = new project_CinemaManager.QLRPDataSet();
            this.uSPGetInfoForTicketBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.uSP_GetInfoForTicketTableAdapter = new project_CinemaManager.QLRPDataSetTableAdapters.USP_GetInfoForTicketTableAdapter();
            this.rpViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.USP_GetInfoForTicketBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qLRPDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uSPGetInfoForTicketBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // USP_GetInfoForTicketBindingSource
            // 
            this.USP_GetInfoForTicketBindingSource.DataMember = "USP_GetInfoForTicket";
            this.USP_GetInfoForTicketBindingSource.DataSource = this.qLRPDataSet;
            // 
            // qLRPDataSet
            // 
            this.qLRPDataSet.DataSetName = "QLRPDataSet";
            this.qLRPDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // uSPGetInfoForTicketBindingSource
            // 
            this.uSPGetInfoForTicketBindingSource.DataMember = "USP_GetInfoForTicket";
            this.uSPGetInfoForTicketBindingSource.DataSource = this.qLRPDataSet;
            // 
            // uSP_GetInfoForTicketTableAdapter
            // 
            this.uSP_GetInfoForTicketTableAdapter.ClearBeforeFill = true;
            // 
            // rpViewer
            // 
            this.rpViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet";
            reportDataSource1.Value = this.USP_GetInfoForTicketBindingSource;
            this.rpViewer.LocalReport.DataSources.Add(reportDataSource1);
            this.rpViewer.LocalReport.ReportEmbeddedResource = "project_CinemaManager.ReportTicket.rdlc";
            this.rpViewer.Location = new System.Drawing.Point(0, 0);
            this.rpViewer.Name = "rpViewer";
            this.rpViewer.ServerReport.BearerToken = null;
            this.rpViewer.Size = new System.Drawing.Size(1145, 637);
            this.rpViewer.TabIndex = 0;
            this.rpViewer.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            // 
            // UI06_TicketForCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1145, 637);
            this.Controls.Add(this.rpViewer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UI06_TicketForCustomer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UI06_TicketForCustomer";
            this.Load += new System.EventHandler(this.UI06_TicketForCustomer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.USP_GetInfoForTicketBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qLRPDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uSPGetInfoForTicketBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource uSPGetInfoForTicketBindingSource;
        private QLRPDataSet qLRPDataSet;
        private QLRPDataSetTableAdapters.USP_GetInfoForTicketTableAdapter uSP_GetInfoForTicketTableAdapter;
        private Microsoft.Reporting.WinForms.ReportViewer rpViewer;
        private System.Windows.Forms.BindingSource USP_GetInfoForTicketBindingSource;
    }
}