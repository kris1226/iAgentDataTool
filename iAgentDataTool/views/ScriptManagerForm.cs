using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ninject;
using iAgentDataTool.Repositories.Common;
using iAgentDataTool.Models.Remix;
using iAgentDataTool.Models.Remix.ViewModels;
using iAgentDataTool.Models.Common;
using iAgentDataTool.Helpers.Interfaces;
using iAgentDataTool.Helpers;
using iAgentDataTool.Models;
using iAgentDataTool.Repositories.AsyncRepositoires;
using iAgentDataTool.Repositories.AsyncRepositoires.RemixRepositoires;
using iAgentDataTool.Repositories;
using iAgentDataTool.AsyncRepositories.Common;
using iAgentDataTool.Repositories.RemixRepositories;
using iAgentDataTool.Repositories.AsyncRepositoires.RemixRepositoires.ViewModelRepos;
using System.Data.SqlClient;

namespace iAgentDataTool
{
    public partial class ScriptManagerForm : Form
    {
        [Inject]
        internal readonly IAsyncRepository<ScriptMaster> _scritpMasRepo;
        [Inject]
        internal readonly IDataBindingRepo _scriptMasBindRepo;
        [Inject]
        internal readonly IDbConnection _db;

        internal IEnumerable<ScriptMaster> _scripts = new List<ScriptMaster>();

        private BindingSource binding = new BindingSource();

        public ScriptManagerForm(IAsyncRepository<ScriptMaster> scriptMasRepo, 
                                 IDataBindingRepo scriptMasBindRepo, 
                                 IDbConnection db)
        {
            this._scritpMasRepo = scriptMasRepo;
            this._scriptMasBindRepo = scriptMasBindRepo;
            this._db = db;
            
            InitializeComponent();
        }

        internal IEnumerable<WebsiteMaster> Websites { get { return ClientInformationForm._websiteMasterRecords; } }

        internal void CloseScriptForm_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        internal async void ScriptManagerForm_Load(object sender, EventArgs e)
        {
            WebsiteNamesToDropDown(Websites);
            var websiteKey = Guid.Parse(txtWebsiteKey.Text);
            _scripts = await GetScripts(websiteKey);
          //  ScriptsToDataTable(_scripts);
            //var table = await GetScriptsDataTable(websiteKey);
            //ScriptsToDataGridView(table);
        }

        private async Task<DataTable> GetScriptsDataTable(Guid websiteKey)
        {
            try
            {
                return  await _scriptMasBindRepo.GetData(websiteKey);        
            }
            catch (Exception)
            {               
                throw;
            } 
        }

        private void ScriptsToDataGridView(IEnumerable<ScriptMaster> scripts)
        {
            binding.DataSource = scripts;
            ScriptMasterGridView.DataSource = binding;
        }

        private void ScriptsToDataTable(IEnumerable<ScriptMaster> scripts)
        {
            var table = new DataTable();
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;

        }
        public async Task<IEnumerable<ScriptMaster>> GetScripts(Guid websiteKey)
        {
            try
            {
                return await _scritpMasRepo.FindWithGuidAsync(websiteKey);
            }
            catch (SqlException)
            {               
                throw;
            }
        }
        internal void WebsiteNamesToDropDown(IEnumerable<WebsiteMaster> websites)
        {
            ddlWebsiteNames.DisplayMember = "WebsiteDescription";
            ddlWebsiteNames.Items.Clear();
            if (websites.Count() != 0)
            {
                ddlWebsiteNames.DataSource = websites;
                ddlWebsiteNames.SelectedIndex = 0;
            }
        }

        internal void OnChangingWebsiteName(object sender, EventArgs e)
        {
            var selectedWebsite = Websites.Where(w => w.WebsiteDescription
                                          .Equals(ddlWebsiteNames.Text))
                                          .SingleOrDefault();
            txtWebsiteKey.Text = selectedWebsite.WebsiteKey.ToString();
            ScriptMasterGridView = null;
            ScriptsToDataGridView(_scripts);
        }

        internal void WebsiteKeyToTextBox(IEnumerable<WebsiteMaster> websites)
        {
            if (!websites.Count().Equals(0))
            {
                var selectedWebsite = websites.Where(w => w.WebsiteDescription.Equals(ddlWebsiteNames.Text)).SingleOrDefault();
                txtWebsiteKey.Text = selectedWebsite.WebsiteKey.ToString();
         
            }
        }
    }
}
