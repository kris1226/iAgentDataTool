using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using Ninject;
using System.Threading.Tasks;
using System.Windows.Forms;
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
using Ninject.Parameters;
using iAgentDataTool.Ninject;

namespace iAgentDataTool
{
    public partial class ClientInformationForm : Form
    {
        private readonly IDbConnection _iAgentDevDb = new SqlConnection(ConfigurationManager.ConnectionStrings["AgentDevDb"].ConnectionString);
        private readonly IDbConnection _iAgentProdDb = new SqlConnection(ConfigurationManager.ConnectionStrings["AgentProdDb"].ConnectionString);
        private readonly IDbConnection _UpwProdDb = new SqlConnection(ConfigurationManager.ConnectionStrings["UPW"].ConnectionString);
        private readonly IDbConnection _ApcoWebDev = new SqlConnection(ConfigurationManager.ConnectionStrings["APCOE"].ConnectionString);

        private IPortalUserRepo _portalUserRepo;

        private IKernel _kernel;

        private IEnumerable<ClientMaster> _clientMasterRecords = new List<ClientMaster>();
        private IEnumerable<ClientLocations> _clientLocationRecords = new List<ClientLocations>();
        private IEnumerable<PortalUser> _portalUsers = new List<PortalUser>();
        public static IEnumerable<WebsiteMaster> _websiteMasterRecords = new List<WebsiteMaster>();

        public IEnumerable<WebsiteMaster> Websites { get { return _websiteMasterRecords; } }

        private DataProcesser _process;

        [Inject]
        IPayerResponseRepository _payerResponseRepo;
        [Inject]
        IDapperAsyncRepository<ExtractionMap> _extractionMapRepo;
        [Inject]
        IAsyncRepository<AgentConfiguration> _agentConfigRepo;
        [Inject]
        private readonly IAsyncRepository<ClientScript> _clientScriptsRepo;



        public int LocationId { get { return int.Parse(txtLocationId.Text); } set { } }
        public Guid ClientKey { get { return Guid.Parse(txtClientKey.Text); } set { } }

        public ClientInformationForm(IPayerResponseRepository payerResponseRepo,
                                     IDapperAsyncRepository<ExtractionMap> extractionMapRepo,
                                     IAsyncRepository<AgentConfiguration> agentConfigRepo,
                                     IAsyncRepository<ClientScript> clientScriptRepo)
        {
            this._payerResponseRepo = payerResponseRepo;
            this._extractionMapRepo = extractionMapRepo;
            this._agentConfigRepo = agentConfigRepo;
            this._clientScriptsRepo = clientScriptRepo;
            InitializeComponent();
        }

        private async void ClientInformation_Load(object sender, EventArgs e)
        {
            try
            {
                IDbConnection prodfirstConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AgentProdDb"].ConnectionString);
                IDbConnection prodsecondConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AgentProdDb"].ConnectionString);
                IDbConnection prodthirdConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AgentProdDb"].ConnectionString);
                IDbConnection devConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AgentDevDb"].ConnectionString);

                radioDevelopment.Checked = false;
                radioButtonProduction.Checked = true;

                await GetWebsiteRecords(prodfirstConnection);
                await GetClientMaster(prodsecondConnection);

                await GetPayerResponseIprKeysAsync();
            
                await GetPayerReponseRecordsWithIprKeyAsync(prodfirstConnection);
                await GetPortalUserIds(prodthirdConnection);

                //await GetAllPortalUserIds(prodthirdConnection);
            }
            catch (SqlException)
            {
                throw;
            }
        }
        private async void OnClientSelection(object sender, EventArgs e)
        {
            try
            {
                if (radioDevelopment.Checked == true)
                {
                    ddlClientLocations.Items.Clear();
                    ClientDataToForm(_clientMasterRecords);
                    IDbConnection firstConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AgentDevDb"].ConnectionString);
                    IDbConnection secondConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AgentDevDb"].ConnectionString);
                    await GetTableRecordsAsync();
                }
                if (radioButtonProduction.Checked == true)
                {
                    ClientDataToForm(_clientMasterRecords);
                    ddlClientLocations.Items.Clear();
                    
                    await GetTableRecordsAsync();
                }


            }
            catch (SqlException)
            {
                MessageBox.Show("Error getting Data: ");
            }

        }

        private void PortalUserIdsToDropDownList(IEnumerable<PortalUser> portalUsers)
        {
            DDLPortalUserId.Items.Clear();
            if (portalUsers != null)
            {
                foreach (var portalUser in portalUsers)
                {
                    DDLPortalUserId.Items.Add(portalUser.PortalUser_Id);
                }
                DDLPortalUserId.SelectedIndex = 0;
            }

        }

        private void PortalUserDataToForm(IEnumerable<PortalUser> portalUsers)
        {
            try
            {
                if (portalUsers != null)
                {
                    var filter = portalUsers
                        .Where(pu => pu.PortalUser_Id.ToString()
                        .Equals(DDLPortalUserId.Text))
                        .SingleOrDefault();

                    DDLPortalUserId.Text = filter.PortalUser_Id.ToString();
                    txtUsername.Text = filter.Username.DecryptStringBase64();
                    txtPassword.Text = filter.Password.DecryptStringBase64();
                    ddlPortalId.Text = filter.Portal_Id.ToString();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ddlWebsiteKeys_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_websiteMasterRecords != null)
            {
                var websiteKey = Guid.Parse(txtWebsiteKey.Text);
                var website = _websiteMasterRecords.Where(w => w.WebsiteKey == websiteKey).SingleOrDefault();
                ddlWebsiteName.Text = website.WebsiteDescription;
                ddlPortalId.Text = website.Portal_Id;
            }
        }

        private void DDLPortalUserId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_portalUsers != null)
            {
                PortalUserDataToForm(_portalUsers);
            }
        }

        private void OnIrpKeyChange(object sender, EventArgs e)
        {
            if (DDLIPRKeys.Text != "")
            {
                var payerResponseRecords = GetPayerReponseRecordsWithIprKeyAsync(_iAgentDevDb);
            }
        }

        private void DDLPayerResponseIds_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLPayerResponseIds.Text != "")
            {
                var extractionMapRecords = GetExtractionMapRecordsAsync();
            }
        }

        private async void ddlWebsiteName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_websiteMasterRecords == null)
            {
                lblStatus.Text = "No website Records Found";
            }
            else
            {
                try
                {
                    var websiteReocrd = _websiteMasterRecords
                                             .Where(w => w.WebsiteDescription
                                             .Equals(ddlWebsiteName.Text))
                                             .OrderBy(w => w.WebsiteKey)
                                             .SingleOrDefault();

                    if (websiteReocrd == null)
                    {
                        lblStatus.Text = "Error getting a website";
                    }
                    else
                    {
                        txtWebsiteKey.Text = websiteReocrd.WebsiteKey.ToString();
                        txtWebsiteUrl.Text = websiteReocrd.WebsiteDomain;
                        ddlPortalId.Text = websiteReocrd.Portal_Id.ToString();
                        txtPortalId.Text = websiteReocrd.Portal_Id.ToString();
                    }
                    await GetPortals(_iAgentDevDb);
                }
                catch (Exception)
                {
                    lblStatus.Text = "Error getting a website";
                    //throw;
                }
            }
        }


        private void WebsiteRecordDataToForm()
        {
            try
            {

                var websiteReocrd = _websiteMasterRecords
                                         .Where(w => w.WebsiteDescription
                                         .Equals(ddlWebsiteName.Text))
                                         .OrderBy(w => w.WebsiteKey)
                                         .SingleOrDefault();

                if (websiteReocrd == null)
                {
                    lblStatus.Text = "Error getting a website";
                }
                else
                {
                    txtWebsiteKey.Text = websiteReocrd.WebsiteKey.ToString();
                    txtWebsiteUrl.Text = websiteReocrd.WebsiteDomain;
                    ddlPortalId.Text = websiteReocrd.Portal_Id.ToString();
                    txtPortalId.Text = websiteReocrd.Portal_Id.ToString();
                }
            }
            catch (Exception)
            {
                lblStatus.Text = "Error getting a website";
                throw;
            }
        }

        private async void OnWebsiteNameChange_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (_websiteMasterRecords.Any().Equals(false))
                {
                    lblStatus.Text = "No website Records Found";
                }
                else
                {
                    WebsiteRecordDataToForm();
                    if (radioDevelopment.Checked.Equals(true))
                    {
                        IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["AgentDevDb"].ConnectionString);
                        await GetPortals(db);
                    }
                    if(radioButtonProduction.Checked.Equals(true))
                    {
                        IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["AgentProdDb"].ConnectionString);
                        await GetPortals(db);
                        
                    }
                }
            }
            catch (Exception)
            {
                lblStatus.Text = @"Error Getting Portals";
            }
        }

        private async void GetData_Click(object sender, EventArgs e)
        {
            //IAsyncRepository<AgentConfiguration> agentConfigRepo = null;
            btnGetData.Enabled = false;
            if (radioDevelopment.Checked)
            {
                await GetClientLocations(_iAgentDevDb);
                await GetFacilityMaster(_iAgentDevDb);
                await GetAgentConfiguration();

                lblStatus.Text = "Done";

            }
            if (radioButtonProduction.Checked)
            {
                IDbConnection firstConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AgentProdDb"].ConnectionString);
                IDbConnection secondConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AgentProdDb"].ConnectionString);
                IDbConnection thirdConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AgentProdDb"].ConnectionString);

                ddlClients.Text = "";
                try
                {
                    await GetWebsiteRecords(firstConnection)
                        .ContinueWith(t => GetClientMaster(secondConnection));
                    await GetPortalUserIds(thirdConnection);
                    await GetPayerReponseRecordsWithIprKeyAsync(thirdConnection);
                    await GetPayerResponseIprKeysAsync();
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    btnGetData.Enabled = true;
                }
            }
        }


        private PortalUserAndLocationId BuildPortalUserData()
        {
            var portalUserData = new PortalUserAndLocationId()
            {
                Portal_Id = int.Parse(ddlPortalId.Text),
                Username = txtUsername.Text.EncryptStringBase64(),
                Password = txtPassword.Text.EncryptStringBase64(),
                DateExpired = DateTime.MinValue,
                IsEnabled = true,
                IsExpired = false,
                Location_Id = int.Parse(txtLocationId.Text)
            };
            if (int.Parse(ddlPortalId.Text).Equals(3) || int.Parse(ddlPortalId.Text).Equals(12))
            {
                portalUserData.AllowsConcurrent = false;
            }
            else
            {
                portalUserData.AllowsConcurrent = true;
            }
            return portalUserData;
        }

        private async void ApcoSearch_Click(object sender, EventArgs e)
        {
            btnApcoSearch.Enabled = false;
            lblStatus.Text = "Searching For Apco Web Dev records.";
            var apcoRecords = await FindApcoWebDevRecords();
            ApcoRecordsToGridView(apcoRecords);
            lblStatus.Text = "Done.";
            btnApcoSearch.Enabled = true;
            TabControl.SelectTab(ApcoWebTab);
        }

        private async void SearchUpw_Click(object sender, EventArgs e)
        {
            btnSearchUpw.Enabled = false;
            lblStatus.Text = "Searching For Upw records.";
            try
            {
                var upwRecords = await FindUpwRecords();
                UpwRecordsToGridView(upwRecords);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            lblStatus.Text = "Done.";
            btnSearchUpw.Enabled = true;
            TabControl.SelectTab(UpwTab);
        }

        private async void GetCredential_Click(object sender, EventArgs e)
        {
            try
            {
                btnGetCredential.Enabled = false;
                if (radioButtonProduction.Checked.Equals(true))
                {
                    var userlogins = await FinUserLogins();

                    if (MessageBox.Show("Are you sure you want to Add Credentials?", "Confirm Add Credential", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        await AddUserLoginsListToDb(userlogins);
                    }
                }
                btnGetCredential.Enabled = true;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }




        private async Task<IEnumerable<ClientLocations>> GetClientLocationRecords(IDbConnection db) {
            var processer = new DataProcesser(db);
            lblStatus.Text = "Getting Client Location Records";
            var clientKey = Guid.Parse(txtClientKey.Text);

            if (!clientKey.Equals(null)) {
                return await processer.GetClientLocations_With_ClientKey(clientKey);
            }
            else {
                lblStatus.Text = "Client key is null. please provide one.";
            }
            return null;
        }


        private async Task GetAutoAgentId(IDbConnection db)
        {
            try
            {
                lblStatus.Text = "Getting Auto Agent Id";
                var autoAgentId = await GetAutoAgentIdRecord(db);
                if (autoAgentId.Count() != 0)
                {
                    var agentUser = autoAgentId.SingleOrDefault();
                    lblAutoAgentName.Text = agentUser.UserName;
                }
                lblStatus.Text = "Done";
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error getting AutoAgentId " + ex.ToString());
            }


        }


        private void FacilityDataToForm(IEnumerable<FacilityMaster> facilities)
        {
            try
            {
                if (facilities.Any())
                {
                    var facility = facilities.SingleOrDefault();
                    txtFacilityKey.Text = facility.FacilityKey.ToString();
                    txtOrderMap.Text = facility.Ordermap;
                }
                else
                {
                    lblStatus.Text = "No facilites found to add to gridview...";
                }
            }
            catch (Exception)
            {               
                throw;
            }      
        }

        private void ClientLocationToForm(IEnumerable<ClientLocations> clientLocations)
        {
            if (clientLocations != null)
            {
                var location = clientLocations.Where(l => l.ClientLocationName.Equals(ddlClientLocations.Text)).SingleOrDefault();
                txtClientLocationKey.Text = location.ClientLocationKey.ToString();
                txtFacilityId.Text = location.FacilityId;
                txtTradingPartnerId.Text = location.TpId;
                txtParentId.Text = location.Parent_Id.ToString();
                txtClientId.Text = location.ClientId;
                txtLocationId.Text = location.Id.ToString();

            }
        }

        private void ClientLocations_To_Drop_Down(IEnumerable<ClientLocations> clientlocations)
        {
            ddlClientLocations.Items.Clear();
            if (clientlocations != null)
            {
                foreach (var location in clientlocations)
                {
                    ddlClientLocations.Items.Add(location.ClientLocationName);
                }
                ddlClientLocations.SelectedIndex = 0;
            }
        }

        private void ClientMasterRecords_ToDropDownList(IEnumerable<ClientMaster> clients)
        {
            try
            {
                ddlClients.Items.Clear();
                ddlClients.DisplayMember = "ClientName";
                if (clients.Any())
                {
                    ddlClients.DataSource = clients;
                    ddlClients.SelectedIndex = 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void WebsiteMasterRecords_ToDropDownList(IEnumerable<WebsiteMaster> websiteMasterRecords)
        {
            ddlWebsiteName.Items.Clear();
            try
            {
                ddlWebsiteName.DisplayMember = "WebsiteDescription";
                if (websiteMasterRecords == null)
                {
                    lblStatus.Text = "No websites to add to drop down list";
                }
                else
                {
                    ddlWebsiteName.DataSource = websiteMasterRecords;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                ddlWebsiteName.SelectedIndex = 0;
            }
        }
        private void PortalIds_To_DropDownList(IEnumerable<Portal> portalRecords)
        {
            ddlPortalId.Items.Clear();
            if (!portalRecords.Equals(null))
            {
                foreach (var item in portalRecords)
                {

                }
            }
        }
        private void PortalsToDropDownList(IEnumerable<Portal> portals)
        {
            ddlPortalId.Items.Clear();
            if (!portals.Equals(null))
            {
                foreach (var portal in portals)
                {
                    ddlPortalId.Items.Add(portal.Id);
                }
            }
        }

        private void ClientMappingValuesToGridView(IEnumerable<ClientMappingValue> clientMappingValues)
        {
            ClientMappingValuesGridView.DataSource = null;
            if (clientMappingValues != null)
            {
                ClientMappingValuesGridView.DataSource = clientMappingValues;
            }
            else
            {
                lblStatus.Text = "No client mapping values records to add to Data Grid View";
            }
        }
        private void ClientMappingMasterRecordsToGridView(IEnumerable<ClientMappingMaster> clientMappingMasterRecords)
        {
            ClientMappingMasterGridView.DataSource = null;
            if (clientMappingMasterRecords != null)
            {
                ClientMappingMasterGridView.DataSource = clientMappingMasterRecords;
            }
            else
            {
                lblStatus.Text = "No client mapping records to add to Data Grid View";
            }
        }
        private void ClientScriptsToGridView(IEnumerable<ClientScript> clientScripts)
        {
            DataGridViewClientScripts.DataSource = null;
            if (clientScripts == null)
            {
                lblStatus.Text = "No client scripts to add to Data Grid View";
            }
            else
            {
                try
                {
                    DataGridViewClientScripts.DataSource = clientScripts;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        private void FacilityDetialsToGridView(IEnumerable<FacilityDetail> facilityDetials)
        {
            FacilityDetailsDataGridView.DataSource = null;
            if (facilityDetials.Any())
            {
                FacilityDetailsDataGridView.DataSource = facilityDetials;
            }
            else
            {
                lblStatus.Text = "No facility details to add to Gird View";
            }
        }
        private void ApcoRecordsToGridView(IEnumerable<ApcoWebDev> apcoRecords)
        {
            ApcoDataGridView.DataSource = null;
            if (apcoRecords.IsNull())
            {
                lblStatus.Text = "No apco Records to add to grid view";
            }
            else
            {
                ApcoDataGridView.DataSource = apcoRecords;
            }
        }
        private void UpwRecordsToGridView(IEnumerable<Upw> upwRecords)
        {
            UpwDataGridView.DataSource = null;
            if (upwRecords != null)
            {
                UpwDataGridView.DataSource = upwRecords;
            }
            else
            {
                lblStatus.Text = "No upw records to add to grid view";
            }
        }
        private void AgentConfigsToGridView(IEnumerable<AgentConfiguration> agentConfigs)
        {
            AgentConfigDataGridView.DataSource = null;
            if (agentConfigs != null)
            {
                AgentConfigDataGridView.DataSource = agentConfigs;
            }
            else
            {
                lblStatus.Text = "No agent configurations to add to grid view";
            }
        }
        private void PayerWebsiteMappingValuesToGridView(IEnumerable<PayerWebsiteMappingValue> payerWebsiteMappingValues)
        {
            PayerWebsiteMappingValuesGridView.DataSource = null;
            if (payerWebsiteMappingValues != null)
            {
                PayerWebsiteMappingValuesGridView.DataSource = payerWebsiteMappingValues;
            }
            else
            {
                lblStatus.Text = "No Payer Website Mapping Values to add to grid view";
            }
        }
        private void UserLoginsToGridView(IEnumerable<UserLogin> userLogins)
        {
            UserLoginsGridView.DataSource = null;
            if (userLogins != null)
            {
                UserLoginsGridView.DataSource = userLogins;
            }
            else
            {
                lblStatus.Text = "No user logins to add to grid view";
            }
        }

        private UserLogin BuildUserLoginRecord()
        {
            var userLogin = new UserLogin()
            {
                DateAdded = DateTime.Now,
                DateChanged = DateTime.Now,
                LastUserID = lblAutoAgentName.Text,
                DeviceID = "AUT0UPDAT8R",
                ClientKey = Guid.Parse(txtClientKey.Text),
                ClientLocationKey = Guid.Parse(txtClientLocationKey.Text),
                WebsiteUserName = txtUsername.Text.EncryptStringBase64(),
                WebsitePassword = txtPassword.Text.EncryptStringBase64(),
                UserID = lblAutoAgentName.Text,
                WebsiteKey = Guid.Parse(txtWebsiteKey.Text)
            };
            return userLogin;
        }

        private Upw BuildUpwObj()
        {
            if (string.IsNullOrWhiteSpace(txtClientKey.Text))
            {
                var upwRecord = new Upw()
                {
                    ClientKey = txtClientKey.Text,
                    ClientLocationKey = txtClientLocationKey.Text
                };
                return upwRecord;
            }
            return null;
        }



        private void txtWebsiteUrl_TextChanged(object sender, EventArgs e)
        {

        }

        private async Task CreateClientMasterRecord(IDbConnection db, ClientMaster client)
        {
            IAsyncRepository<ClientMaster> clientMasterRepo = new ClientMasterRepositoryAsync(db);
            try
            {
                await clientMasterRepo.AddAsync(client);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private ClientMaster BuildClient()
        {
            var client = new ClientMaster()
            {
                ClientName = txtEnterClientName.Text,
                HowToDeliver = txtHowToDeliver.Text
            };
            return client;
        }

        private void AddPayerWebsiteMappingValuesRecords_Click(object sender, EventArgs e)
        {
            btnAddPayerWebsiteMappingValuesRecords.Enabled = false;
            try
            {
                if (MessageBox.Show("Do you want to create payer website mapping value record for this client?", "Confirm create record",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    lblStatus.Text = "Creating payer website mapping values records...";
                    var payerWebsiteMappingValuesRecord = BuildPayerWebsiteMappingValueEntity();
                    CreatePayerWebsiteMappingValuesRecord(payerWebsiteMappingValuesRecord, _iAgentDevDb);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                btnAddPayerWebsiteMappingValuesRecords.Enabled = true;
            }
        }

        private void CreatePayerWebsiteMappingValuesRecord(PayerWebsiteMappingValue payerWebsiteMappingValuesRecord, IDbConnection db)
        {
            try
            {
                IAsyncRepository<PayerWebsiteMappingValue> payerWebsiteRepo = new PayerWebsiteMappingValuesAsyncRepository(db);
                payerWebsiteRepo.AddAsync(payerWebsiteMappingValuesRecord);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private PayerWebsiteMappingValue BuildPayerWebsiteMappingValueEntity()
        {
            var record = new PayerWebsiteMappingValue()
            {
                ClientKey = Guid.Parse(txtClientKey.Text),
                ClientLocationKey = Guid.Parse(txtClientLocationKey.Text)
            };
            return record;
        }

        private async void AddPortalUserAndLocationId_Click(object sender, EventArgs e)
        {
            BTNAddPortalUserAndLocationId.Enabled = false;
            try
            {
                if (MessageBox.Show("Do you want to add this record to Portal Users?", "Confirm Add Credentials", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    lblStatus.Text = "Adding Portal User record...";
                    var portalUser = BuildPortalUserData();
                    if (radioDevelopment.Checked == true)
                    {
                        await AddPortalUserWithLocationId(portalUser, _iAgentDevDb);
                    }
                    if (radioButtonProduction.Checked == true)
                    {
                        await AddPortalUserWithLocationId(portalUser, _iAgentProdDb);
                    }
                }
            }
            catch (Exception)
            {
                lblStatus.Text = "Error adding portal user with location record...";
                throw;
            }
            finally
            {
                BTNAddPortalUserAndLocationId.Enabled = true;
                lblStatus.Text = "Added Portal User with location record Successfully!";
            }
        }

        private Task AddPortalUserWithLocationId(PortalUserAndLocationId portalUser, IDbConnection _iAgentDevDb)
        {
            IAsyncRepository<PortalUserAndLocationId> portalUserRepo = new PortalUserDataRepo(_iAgentDevDb);
            try
            {
                return portalUserRepo.AddAsync(portalUser);
            }
            catch (SqlException)
            {
                throw;
            }
        }

        private async void AddUserLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioDevelopment.Checked.Equals(true))
                {

                    await CreateUserLogin(_iAgentDevDb);
                    if (MessageBox.Show("Do you want to add this record to Portal Users and portal user locations?",
                        "Confirm Add Credentials", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        var portalUser = BuildPortalUserData();
                        await AddPortalUserWithLocationId(portalUser, _iAgentDevDb);
                    }

                }
                else
                {
                    await CreateUserLogin(_iAgentProdDb);
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Uh Oh..STB on adding user logins !!";
                throw ex;
            }
            btnAddUserLogin.Enabled = true;
        }

        private async Task CreateUserLogin(IDbConnection db)
        {
            try
            {
                if (MessageBox.Show("Do you want to add this record to Portal Users and portal user locations?",
               "Confirm Add Credentials", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var userLogin = BuildUserLoginRecord();
                    await AddUserLogin(userLogin, db);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                lblStatus.Text = "Added User Login Successfully!";
            }
        }

        private async void AddPortalUserRecord_Click(object sender, EventArgs e)
        {
            BtnAddPortalUserRecord.Enabled = false;
            try
            {
                if (MessageBox.Show("Do you want to add this record to Portal Users?", "Confirm Add Credentials", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var portalUser = BuildPortalUserRecord();
                    await AddPortalUserRecord(portalUser, _iAgentDevDb);
                }
                lblStatus.Text = "Added Portal User record Successfully!";
            }
            catch (SqlException)
            {
                lblStatus.Text = "Uh OH!, error adding Portal User record";
                throw;
            }
            BtnAddPortalUserRecord.Enabled = true;
        }

        private async void AddClient_Click(object sender, EventArgs e)
        {
            BtnAddClient.Enabled = false;
            if (MessageBox.Show("Create Client?", "Confirm Add Credentials", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                lblStatus.Text = "Adding client in development...";
                if (radioDevelopment.Checked == true)
                {
                    var newClient = BuildClient();
                    try
                    {
                        await CreateClientMasterRecord(_iAgentDevDb, newClient);
                    }
                    catch (Exception)
                    {
                        lblStatus.Text = "Error Adding client in development...";
                        throw;
                    }
                }
                lblStatus.Text = "Adding client in development done...";
            }
        }

        async void CreateUserLoginRecords_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Create user login records for this client?", "Confirm create records",
                MessageBoxButtons.YesNo) == DialogResult.Yes) {
                lblStatus.Text = "Creating user Login records in development db...";
                var userlogins = BuildUserLoginRecord();
                if (radioDevelopment.Checked == true)
                {
                    try
                    {
                        await AddUserLogin(userlogins, _iAgentDevDb);
                    }
                    catch (SqlException)
                    {
                        throw;
                    }
                    finally
                    {
                        lblStatus.Text = "Done adding user logins to development...";
                    }
                }
                else
                {
                    try
                    {
                        await AddUserLogin(userlogins, _iAgentProdDb);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        lblStatus.Text = "Done adding user logins Production...";
                    }
                }

            }
        }

        private void ClientDataToForm(IEnumerable<ClientMaster> clientRecords)
        {
            if (clientRecords.Count() != 0)
            {
                var selectedClient = clientRecords.Where(c => c.ClientName
                                                  .Equals(ddlClients.Text))
                                                  .SingleOrDefault();

                txtClientKey.Text = selectedClient.ClientKey.ToString();
                txtHowToDeliver.Text = selectedClient.HowToDeliver.ToString();
            }
            else
            {
                lblStatus.Text = "No clients available";
            }
        }

        private async void OnClientLocations_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClientLocationToForm(_clientLocationRecords);
            try
            {
                if (radioButtonProduction.Checked.Equals(true))
                {
                    await GetFacilityRecords(_iAgentProdDb);
                    await GetPortalUsers(_iAgentProdDb);
                    await GetClientScripts(_iAgentProdDb);
                    await GetUserLogins(_iAgentProdDb);
                }
                else
                {
                    await GetFacilityRecords(_iAgentDevDb);
                    await GetPortalUsers(_iAgentDevDb);
                    await GetClientScripts(_iAgentDevDb);
                    await GetUserLogins(_iAgentDevDb);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task GetFacilityRecords(IDbConnection db)
        {
            var facility = await GetFacility(db);
            if (facility.Any())
            {
                FacilityDataToForm(facility);
                var facilityDetails = await GetFacilityDetails(db);
                if (facilityDetails.Any())
                {
                    FacilityDetialsToGridView(facilityDetails);
                }
            }
        }

        private void ddlClients_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void OpenScriptsForm_Click(object sender, EventArgs e)
        {
            _kernel = new StandardKernel(new ApplcationModule());
            var scriptsForm = _kernel.Get<ScriptManagerForm>();
            scriptsForm.Show();
        }

        private async void UpdatePortalUser_Click(object sender, EventArgs e)
        {
            btnUpdatePortalUser.Enabled = false;
            try
            {
                lblStatus.Text = "Updating Portal User in development...";
                if (MessageBox.Show("Do you want to add this record to Portal Users?", "Confirm Add Credentials", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (radioDevelopment.Checked == true)
                    {
                        await UpdatePortalUserRecord(_iAgentDevDb);
                        lblStatus.Text = "Updating development Portal user Complete...";
                    }
                    if (radioButtonProduction.Checked == true)
                    {
                        lblStatus.Text = "Updating Production Portal User in ...";
                        await UpdatePortalUserRecord(_iAgentProdDb);
                        lblStatus.Text = "Updating Production Portal user Complete...";
                    }
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Error Updating Portal Users from repository.";
                throw ex;
            }
            finally
            {
                btnUpdatePortalUser.Enabled = true;
            }
        }

        private async void UpdatePortalId_Click(object sender, EventArgs e)
        {
            try
            {
                btnUpdatePortalId.Enabled = false;
                if (MessageBox.Show("Do you want to Update portal Id for portalUser Record? " + txtPortalUsers.Text, "Confirm update portal username", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (radioDevelopment.Checked.Equals(true))
                    {
                        await UpdatePortalId(_iAgentDevDb);
                        lblStatus.Text = "portal Id updated dev";
                    }
                    else
                    {
                        await UpdatePortalId(_iAgentProdDb);
                        lblStatus.Text = "portal Id updated prod";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                btnUpdatePortalId.Enabled = true;
            }
        }




        private async void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to Update portal password?", "Confirm update portal password " + txtPortalUsers.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    btnUpdatePassword.Enabled.Equals(false);
                    if (radioDevelopment.Enabled.Equals(true))
                    {
                        await UpdatePortalUserPassword(_iAgentDevDb);
                        lblStatus.Text = "portal user password updated dev";
                    }
                    else
                    {
                        await UpdatePortalUserPassword(_iAgentProdDb);
                        lblStatus.Text = "portal user password updated prod";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                btnUpdatePassword.Enabled.Equals(true);
            }

        }
        private async void btnUpdateUsername_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to Update portal username?", "Confirm update portal username", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    btnUpdateUsername.Enabled.Equals(false);
                    if (radioDevelopment.Enabled.Equals(true))
                    {
                        await UpdatePortalUsername(_iAgentDevDb);
                        lblStatus.Text = "portal username updated dev";
                    }
                    else
                    {
                        await UpdatePortalUsername(_iAgentProdDb);
                        lblStatus.Text = "portal username updated prod";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                btnUpdateUsername.Enabled.Equals(true);
            }
        }

        private void OpenTransactionManagmentForm_Click(object sender, EventArgs e)
        {
            _kernel = new StandardKernel(new TransactionModule());
            var transManagmentForm = _kernel.Get<frmTransactionManagment>();
            transManagmentForm.Show();
        }

        private async void btnMoveClientScriptsToProd_Click(object sender, EventArgs e) {
            btnClientScriptsToProd.Enabled = false;
            if (!DataGridViewClientScripts.DataSource.Equals(0)) {
                try {
                    var clientScripts = await GetClientScriptsRecords(_iAgentDevDb);
                    if (!clientScripts.Equals(0)) {
                        await _clientScriptsRepo.AddMultipleToProd(clientScripts);
                    }
                }
                catch (Exception) {
                    throw;
                }
                finally
                {
                    btnClientScriptsToProd.Enabled = true;
                }
            }
        }

        private void ddlPortalId_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
