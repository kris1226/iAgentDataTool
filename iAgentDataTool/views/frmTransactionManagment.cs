using iAgentDataTool.Helpers.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iAgentDataTool
{
    public partial class frmTransactionManagment : Form
    {
        [Inject]
        private ITransactionService _transService;
        [Inject]
        ITransactionRepo _transRepo;

        
        public frmTransactionManagment(ITransactionService transService, ITransactionRepo transRepo)
        {
            this._transService = transService;
            this._transRepo = transRepo;
            InitializeComponent();
        }

        private void frmTransactionManagment_Load(object sender, EventArgs e)
        {

        }

        private async void ReRunTransaction_Click(object sender, EventArgs e)
        {
            btnReRunTransaction.Enabled = false;
            lblStatus.Text = "Re running transaction...";
            var transId = Guid.Parse(txtRerunTrans.Text);
            try
            {
               await _transService.RerunTransactionAsync(transId);
            }
            catch (Exception)
            {
                lblStatus.Text = "Error re-reunning transaction..";
                throw;
            }
            finally 
            {
                btnReRunTransaction.Enabled = true;
                lblStatus.Text = "Transaction is set to run at.." + DateTime.Now;
            }
        }

        private void btnReparse_Click(object sender, EventArgs e)
        {
            btnReparse.Enabled = false;
            lblStatus.Text = "Reparsing transaction...";
            var transId = Guid.Parse(txtReparseTransactionId.Text);
            try
            {
                _transService.ReparseTransactionAsync(transId);
            }
            catch (Exception)
            {
                lblStatus.Text = "Error re-parsing transaction..";
                throw;
            }
            finally 
            {
                lblStatus.Text = "Reparsing transaction complete...";
                btnReparse.Enabled = true;
            }
        }

        private void btnViewAgentSched_Click(object sender, EventArgs e)
        {

        }

        private void btnReparseMultiple_Click(object sender, EventArgs e)
        {

        }
    }
}
