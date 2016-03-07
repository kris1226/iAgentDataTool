namespace iAgentDataTool
{
    partial class ScriptManagerForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtWebsiteKey = new System.Windows.Forms.TextBox();
            this.lblWebsiteKey = new System.Windows.Forms.Label();
            this.lblWebsiteName = new System.Windows.Forms.Label();
            this.ddlWebsiteNames = new System.Windows.Forms.ComboBox();
            this.ddlPortalId = new System.Windows.Forms.ComboBox();
            this.lblPortalId = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.ScriptMasterGridView = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScriptMasterGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 142);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1196, 492);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.ControlText;
            this.tabPage1.Controls.Add(this.ScriptMasterGridView);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1188, 462);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1188, 462);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.Desktop;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnClose.Location = new System.Drawing.Point(1068, 37);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(104, 73);
            this.btnClose.TabIndex = 42;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.CloseScriptForm_Click);
            // 
            // txtWebsiteKey
            // 
            this.txtWebsiteKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWebsiteKey.Location = new System.Drawing.Point(19, 103);
            this.txtWebsiteKey.Margin = new System.Windows.Forms.Padding(4);
            this.txtWebsiteKey.Name = "txtWebsiteKey";
            this.txtWebsiteKey.Size = new System.Drawing.Size(295, 22);
            this.txtWebsiteKey.TabIndex = 56;
            // 
            // lblWebsiteKey
            // 
            this.lblWebsiteKey.AutoSize = true;
            this.lblWebsiteKey.BackColor = System.Drawing.SystemColors.ControlText;
            this.lblWebsiteKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWebsiteKey.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblWebsiteKey.Location = new System.Drawing.Point(16, 83);
            this.lblWebsiteKey.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWebsiteKey.Name = "lblWebsiteKey";
            this.lblWebsiteKey.Size = new System.Drawing.Size(95, 16);
            this.lblWebsiteKey.TabIndex = 55;
            this.lblWebsiteKey.Text = "Website Key";
            // 
            // lblWebsiteName
            // 
            this.lblWebsiteName.AutoSize = true;
            this.lblWebsiteName.BackColor = System.Drawing.SystemColors.ControlText;
            this.lblWebsiteName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWebsiteName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblWebsiteName.Location = new System.Drawing.Point(14, 27);
            this.lblWebsiteName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWebsiteName.Name = "lblWebsiteName";
            this.lblWebsiteName.Size = new System.Drawing.Size(118, 16);
            this.lblWebsiteName.TabIndex = 54;
            this.lblWebsiteName.Text = "Website Names";
            // 
            // ddlWebsiteNames
            // 
            this.ddlWebsiteNames.AutoCompleteCustomSource.AddRange(new string[] {
            "Absolute Total Care",
            "Aetna Availity Inquiry",
            "Aetna NaviNet Inquiry",
            "Aetna NaviNet NPI Inquiry",
            "Aetna Navinet Referral",
            "AIM Availity Inquiry",
            "AIM Inquiry",
            "AIM NaviNet Inquiry",
            "AIM Part II Inquiry",
            "AMbetter of Arkansas Inquiry",
            "Amerigroup Inquiry",
            "Amerihealth Navinet Inquiry",
            "AmeriHealth Navinet Referral",
            "Amerihealth PA Medical Assistance Plan Navinet Referral",
            "Anthem Availity CaseNumber Inquiry",
            "Anthem Availity NPI Inquiry",
            "Anthem BCBS CT Availity Inquiry",
            "Anthem BCBS Inquiry",
            "Anthem BCBS via AIM Inquiry",
            "Anthem Point of Care Virginia auto inquiry",
            "BCBS FL Availity Inquiry",
            "BCBS Louisiana Inquiry",
            "BCBS Louisiana via AIM Inquiry",
            "BCBS NC BlueE Inquiry",
            "BCBS NC BlueE via AIM Inquiry",
            "BCBS NEPA NaviNet Inquiry",
            "BCBS SC Inquiry",
            "BCBS Wellmark Inquiry",
            "BCBS Wellmark via AIM Inquiry",
            "Buckeye Community Health Plan Inquiry",
            "Capital Blue Cross NaviNet Inquiry",
            "CareCore National Inquiry",
            "Carefirst BC/BS",
            "CareSource Inquiry",
            "Cigna Direct Inquiry",
            "Cigna NaviNet Inquiry",
            "Connecticut Department of Social Services Inquiry",
            "Connecticut Department of Social Services Radiology Inquiry",
            "Coventry Healthcare Inquiry",
            "Emblem Health Inquiry",
            "Empire BCBS Inquiry",
            "ePaces Inquiry",
            "Floridia eQ Health Suite Inquiry",
            "Fox Valley Medicine",
            "Geisinger Inquiry",
            "Geisinger NaviNet Inquiry",
            "Geisinger Navinet Referral",
            "Health Alliance Inquiry",
            "Health Help via Presbyterian",
            "Health Spring Inquiry",
            "HealthFirst Inquiry",
            "HealthHelp Inquiry",
            "HealthHelp No Login Inquiry",
            "HighMark Blue Shield Navinet Inquiry",
            "Horizon BCBSNJ NaviNet Inquiry",
            "Horizon BCBSNJ NaviNet Referral",
            "Horizon NJ Health NaviNet Referral",
            "Humana Availity Inquiry",
            "Humana Direct Inquiry",
            "Humana Military Inquiry",
            "Husky Health Connecticut Inquiry",
            "Independence Blue Cross NaviNet Inquiry",
            "Independence Blue Cross NaviNet Referral",
            "Indiana Medicaid Inquiry",
            "Innovation Health Navinet Referral",
            "Integral Quality Care Inquiry",
            "KePro Inquiry",
            "Louisiana Medicaid Inquiry",
            "Magna Care Inquiry",
            "Med 3000 Inquiry",
            "MeDecision iEXCHANGE Inquiry",
            "Medical Mutual of Ohio Inquiry",
            "MedSolutions Inquiry",
            "Molina Healthcare Inquiry",
            "NaviNet AmeriHealth District of Columbia",
            "NaviNet Sonar",
            "NIA Inquiry",
            "NIA Tracking Number Inquiry",
            "Ohio Medicaid Inquiry",
            "OneHealthPort Aetna NaviNet Inquiry",
            "OneHealthPort CareOregon Inquiry",
            "OneHealthPort Cigna NaviNet Inquiry",
            "OneHealthPort Healthnet Inquiry",
            "OneHealthPort Pacificsource Inquiry",
            "OneHealthPort Providence Inquiry",
            "Orthonet Online Inquiry",
            "Oxford Inquiry",
            "Prestige Health Choice Auto Inquiry",
            "Qualchoice Inquiry",
            "SC Blue Cross Ble Shield Inquiry",
            "Simply Better Health",
            "Tricare Inquiry",
            "TriWest Inquiry",
            "UHC Inquiry",
            "UHC Radiology Inquiry",
            "UHC River Valley Inquiry",
            "Virginia Premier Auto Inquiry",
            "Viva Health Inquiry",
            "Wellcare Inquiry"});
            this.ddlWebsiteNames.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ddlWebsiteNames.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.ddlWebsiteNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlWebsiteNames.FormattingEnabled = true;
            this.ddlWebsiteNames.Location = new System.Drawing.Point(19, 47);
            this.ddlWebsiteNames.Margin = new System.Windows.Forms.Padding(4);
            this.ddlWebsiteNames.Name = "ddlWebsiteNames";
            this.ddlWebsiteNames.Size = new System.Drawing.Size(502, 24);
            this.ddlWebsiteNames.TabIndex = 53;
            this.ddlWebsiteNames.SelectedIndexChanged += new System.EventHandler(this.OnChangingWebsiteName);
            // 
            // ddlPortalId
            // 
            this.ddlPortalId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlPortalId.FormattingEnabled = true;
            this.ddlPortalId.Location = new System.Drawing.Point(337, 101);
            this.ddlPortalId.Margin = new System.Windows.Forms.Padding(4);
            this.ddlPortalId.Name = "ddlPortalId";
            this.ddlPortalId.Size = new System.Drawing.Size(117, 24);
            this.ddlPortalId.TabIndex = 58;
            // 
            // lblPortalId
            // 
            this.lblPortalId.AutoSize = true;
            this.lblPortalId.BackColor = System.Drawing.SystemColors.ControlText;
            this.lblPortalId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPortalId.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblPortalId.Location = new System.Drawing.Point(334, 80);
            this.lblPortalId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPortalId.Name = "lblPortalId";
            this.lblPortalId.Size = new System.Drawing.Size(66, 16);
            this.lblPortalId.TabIndex = 57;
            this.lblPortalId.Text = "Portal Id";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Desktop;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.Location = new System.Drawing.Point(793, 47);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 73);
            this.button1.TabIndex = 59;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // ScriptMasterGridView
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            this.ScriptMasterGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.ScriptMasterGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ScriptMasterGridView.Location = new System.Drawing.Point(6, 6);
            this.ScriptMasterGridView.Name = "ScriptMasterGridView";
            this.ScriptMasterGridView.Size = new System.Drawing.Size(1176, 440);
            this.ScriptMasterGridView.TabIndex = 5;
            // 
            // ScriptManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlText;
            this.ClientSize = new System.Drawing.Size(1220, 646);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ddlPortalId);
            this.Controls.Add(this.lblPortalId);
            this.Controls.Add(this.txtWebsiteKey);
            this.Controls.Add(this.lblWebsiteKey);
            this.Controls.Add(this.lblWebsiteName);
            this.Controls.Add(this.ddlWebsiteNames);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ScriptManagerForm";
            this.Text = "ScriptManagerForm";
            this.Load += new System.EventHandler(this.ScriptManagerForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ScriptMasterGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtWebsiteKey;
        private System.Windows.Forms.Label lblWebsiteKey;
        private System.Windows.Forms.Label lblWebsiteName;
        private System.Windows.Forms.ComboBox ddlWebsiteNames;
        private System.Windows.Forms.ComboBox ddlPortalId;
        private System.Windows.Forms.Label lblPortalId;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView ScriptMasterGridView;
    }
}