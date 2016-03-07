namespace iAgentDataTool
{
    partial class frmTransactionManagment
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
            this.txtRerunTrans = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnReRunTransaction = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnViewAgentSched = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtReparseTransactionId = new System.Windows.Forms.TextBox();
            this.btnReparse = new System.Windows.Forms.Button();
            this.lblReparseTransaction = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.listTransactionIds = new System.Windows.Forms.ListBox();
            this.btnReparseMultiple = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtRerunTrans
            // 
            this.txtRerunTrans.Location = new System.Drawing.Point(179, 41);
            this.txtRerunTrans.Name = "txtRerunTrans";
            this.txtRerunTrans.Size = new System.Drawing.Size(447, 22);
            this.txtRerunTrans.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(307, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Insert Transaction Guid";
            // 
            // btnReRunTransaction
            // 
            this.btnReRunTransaction.Location = new System.Drawing.Point(282, 79);
            this.btnReRunTransaction.Name = "btnReRunTransaction";
            this.btnReRunTransaction.Size = new System.Drawing.Size(75, 23);
            this.btnReRunTransaction.TabIndex = 2;
            this.btnReRunTransaction.Text = "Re Run";
            this.btnReRunTransaction.UseVisualStyleBackColor = true;
            this.btnReRunTransaction.Click += new System.EventHandler(this.ReRunTransaction_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(12, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(769, 176);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnViewAgentSched);
            this.tabPage1.Controls.Add(this.txtRerunTrans);
            this.tabPage1.Controls.Add(this.btnReRunTransaction);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(761, 147);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.Blue;
            this.lblStatus.Location = new System.Drawing.Point(192, 191);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(37, 13);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Status";
            // 
            // btnViewAgentSched
            // 
            this.btnViewAgentSched.Location = new System.Drawing.Point(400, 79);
            this.btnViewAgentSched.Name = "btnViewAgentSched";
            this.btnViewAgentSched.Size = new System.Drawing.Size(75, 23);
            this.btnViewAgentSched.TabIndex = 5;
            this.btnViewAgentSched.Text = "View";
            this.btnViewAgentSched.UseVisualStyleBackColor = true;
            this.btnViewAgentSched.Click += new System.EventHandler(this.btnViewAgentSched_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnReparseMultiple);
            this.tabPage2.Controls.Add(this.listTransactionIds);
            this.tabPage2.Controls.Add(this.txtReparseTransactionId);
            this.tabPage2.Controls.Add(this.btnReparse);
            this.tabPage2.Controls.Add(this.lblReparseTransaction);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(761, 147);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtReparseTransactionId
            // 
            this.txtReparseTransactionId.Location = new System.Drawing.Point(6, 63);
            this.txtReparseTransactionId.Name = "txtReparseTransactionId";
            this.txtReparseTransactionId.Size = new System.Drawing.Size(346, 22);
            this.txtReparseTransactionId.TabIndex = 3;
            // 
            // btnReparse
            // 
            this.btnReparse.Location = new System.Drawing.Point(49, 104);
            this.btnReparse.Name = "btnReparse";
            this.btnReparse.Size = new System.Drawing.Size(75, 23);
            this.btnReparse.TabIndex = 5;
            this.btnReparse.Text = "Re Parse";
            this.btnReparse.UseVisualStyleBackColor = true;
            this.btnReparse.Click += new System.EventHandler(this.btnReparse_Click);
            // 
            // lblReparseTransaction
            // 
            this.lblReparseTransaction.AutoSize = true;
            this.lblReparseTransaction.Location = new System.Drawing.Point(113, 31);
            this.lblReparseTransaction.Name = "lblReparseTransaction";
            this.lblReparseTransaction.Size = new System.Drawing.Size(145, 16);
            this.lblReparseTransaction.TabIndex = 4;
            this.lblReparseTransaction.Text = "Insert Transaction Guid";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(5, 218);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(790, 192);
            this.dataGridView1.TabIndex = 4;
            // 
            // listTransactionIds
            // 
            this.listTransactionIds.FormattingEnabled = true;
            this.listTransactionIds.ItemHeight = 16;
            this.listTransactionIds.Location = new System.Drawing.Point(380, 6);
            this.listTransactionIds.Name = "listTransactionIds";
            this.listTransactionIds.Size = new System.Drawing.Size(277, 132);
            this.listTransactionIds.TabIndex = 6;
            // 
            // btnReparseMultiple
            // 
            this.btnReparseMultiple.Location = new System.Drawing.Point(663, 104);
            this.btnReparseMultiple.Name = "btnReparseMultiple";
            this.btnReparseMultiple.Size = new System.Drawing.Size(75, 23);
            this.btnReparseMultiple.TabIndex = 7;
            this.btnReparseMultiple.Text = "Re Parse";
            this.btnReparseMultiple.UseVisualStyleBackColor = true;
            this.btnReparseMultiple.Click += new System.EventHandler(this.btnReparseMultiple_Click);
            // 
            // frmTransactionManagment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 422);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmTransactionManagment";
            this.Text = "frmTransactionManagment";
            this.Load += new System.EventHandler(this.frmTransactionManagment_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtRerunTrans;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnReRunTransaction;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtReparseTransactionId;
        private System.Windows.Forms.Button btnReparse;
        private System.Windows.Forms.Label lblReparseTransaction;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnViewAgentSched;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ListBox listTransactionIds;
        private System.Windows.Forms.Button btnReparseMultiple;
    }
}