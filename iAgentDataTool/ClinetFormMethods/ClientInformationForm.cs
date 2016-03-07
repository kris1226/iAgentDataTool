using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ninject;
using iAgentDataTool.Repositories.Common;
using iAgentDataTool.Models.Remix;
using iAgentDataTool.Models.Common;
using iAgentDataTool.Helpers.Interfaces;
using iAgentDataTool.Helpers;
using iAgentDataTool.Helpers.StringHelpers;
using iAgentDataTool.Repositories.AsyncRepositoires.RemixRepositoires;
using iAgentDataTool.AsyncRepositories.Common;
using iAgentDataTool.Repositories.AsyncRepositoires;
using iAgentDataTool.Repositories.RemixRepositories;
using Ninject.Parameters;

namespace iAgentDataTool
{
    public partial class ClientInformationForm : Form
    {
        private async Task UpdatePortalUserPassword(IDbConnection db)
        {
            var portalUserId = int.Parse(DDLPortalUserId.Text);
            var password = txtPassword.Text.EncryptStringBase64();

            _portalUserRepo = _kernel.Get<PortalUserRepo>(new ConstructorArgument("db", db));
            await _portalUserRepo.UpdatePassword(password, portalUserId);
        }

        private async Task UpdatePortalUsername(IDbConnection db)
        {
            var portalUserId = int.Parse(DDLPortalUserId.Text);
            var username = txtUsername.Text.EncryptStringBase64();

            _portalUserRepo = _kernel.Get<PortalUserRepo>(new ConstructorArgument("db", db));
            await _portalUserRepo.UpdateUsername(username, portalUserId);
        }
        private async Task UpdatePortalId(IDbConnection db)
        {
            var portalId = int.Parse(txtPortalId.Text);
            var portalUserId = int.Parse(DDLPortalUserId.Text);

            _portalUserRepo = _kernel.Get<PortalUserRepo>(new ConstructorArgument("db", db));
            await _portalUserRepo.UpdatePortalId(portalId, portalUserId);
        }

        public int ParentId { get { return txtParentId.Text.ToInt(); } set { } }

        private async Task<IEnumerable<Upw>> FindUpwRecords()
        {
            IDbConnection upwDatabase = new SqlConnection(ConfigurationManager.ConnectionStrings["UPW"].ConnectionString);
            IUpwAsyncRepository upwRepo = new UpwAsyncRepository(upwDatabase);
            return await upwRepo.FindAutoAgentId(txtClientKey.Text, txtClientLocationKey.Text);
        }

        private async Task<IEnumerable<ApcoWebDev>> FindApcoWebDevRecords()
        {
            _process = new DataProcesser(_ApcoWebDev);
            return await _process.FindApcoRecordsByName(txtApcoSearch.Text);
        }

        private IEnumerable<PortalUser> DecryptBase64PortalUsers(IEnumerable<PortalUser> portalUsers)
        {
            var decryptedList = new List<PortalUser>();
            foreach (var portalUser in portalUsers)
            {
                var decryptedPortalUser = new PortalUser()
                {
                    PortalUser_Id = portalUser.PortalUser_Id,
                    Portal_Id = portalUser.Portal_Id,
                    Username = portalUser.Username.DecryptStringBase64(),
                    Password = portalUser.Password.DecryptStringBase64(),
                    IsExpired = false,
                    IsEnabled = true,
                    AllowsConcurrent = true
                };
                decryptedList.Add(decryptedPortalUser);
            }
            return decryptedList;
        }

        private async Task<IEnumerable<Portal>> GetPortalRecords(IDbConnection db)
        {
            IEnumerable<Portal> portals = new List<Portal>();
            try
            {
                IAsyncRepository<Portal> portalRepo = new PortalsAsyncRepository(db);
                return await portalRepo.GetAllAsync();
            }
            catch (Exception)
            {
                lblStatus.Text = "Error getting Portals";
            }
            var portal = new Portal()
            {
                Description = "NOT FOUND"
            };
            portals.ToList().Add(portal);
            return portals;
        }

        private async Task<IEnumerable<UserLogin>> GetUserLoginRecords(IDbConnection db)
        {
            var clientKey = Guid.Parse(txtClientKey.Text);
            var clientLocationKey = Guid.Parse(txtClientLocationKey.Text);
            try
            {
                IAsyncRepository<UserLogin> userLoginsRepo = new UserLoginsRepositoryAsync(db);
                return await userLoginsRepo.FindWith2GuidsAsync(clientKey, clientLocationKey);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<IEnumerable<PortalUser>> GetPortalUserRecords(IDbConnection db)
        {
            var processer = new DataProcesser(db);
            var locationId = int.Parse(txtLocationId.Text);
            try
            {
                return await processer.GetPortalUsersWithLocationd(locationId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task<IEnumerable<FacilityDetail>> GetFacilityDetails(IDbConnection db)
        {
            lblStatus.Text = "Getting facility detials..";
            var facilityKey = Guid.Parse(txtFacilityKey.Text);
            var kernel = new StandardKernel();
            IAsyncRepository<FacilityDetail> repo = kernel.Get<FacilityDetialsAsyncRepository>(new ConstructorArgument("db", db));
            repo = new FacilityDetialsAsyncRepository(db);
            try
            {
                return await repo.FindWithGuidAsync(facilityKey);
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                lblStatus.Text = "Getting facility detials complete..";
            }
        }

        private async Task<IEnumerable<FacilityMaster>> GetFacility(IDbConnection db)
        {
            var processer = new DataProcesser(db);
            lblStatus.Text = "Getting Facilities";
            var clientKey = new Guid(txtClientKey.Text);
            var locationKey = new Guid(txtClientLocationKey.Text);
            try
            {
                return await processer.GetFacility(clientKey, locationKey);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        private async Task<IEnumerable<ClientScript>> GetClientScriptsRecords(IDbConnection db)
        {
            var clientKey = new Guid(txtClientKey.Text);
            var clientLocationKey = new Guid(txtClientLocationKey.Text);

            var kernel = new StandardKernel();
            IAsyncRepository<ClientScript> repo = kernel.Get<ClientScriptsAsyncRepostiory>(new ConstructorArgument("db", db));
            try
            {
                return await repo.FindWith2GuidsAsync(clientKey, clientLocationKey);
            }
            catch (Exception)
            {
                lblStatus.Text = "Error getting clients Scripts";
                throw;
            }
        }

        private async Task<IEnumerable<ClientMaster>> GetClientRecords(IDbConnection db)
        {
            var processer = new DataProcesser(db);
            try
            {
                IAsyncRepository<ClientMaster> clientRepo = null;
                return await clientRepo.GetAllAsync();
            }
            catch (SqlException ex)
            {
                lblStatus.Text = "Error getting clients";
                throw ex;
            }
        }

        private async Task<IEnumerable<Upw>> GetAutoAgentIdRecord(IDbConnection db)
        {
            IUpwAsyncRepository upwRepo = new UpwAsyncRepository(db);
            try
            {
                var clientKey = txtClientKey.Text;
                var clientLocationKey = txtClientLocationKey.Text;
                return await upwRepo.FindAutoAgentId(clientKey, clientLocationKey);
            }
            catch (SqlException ex)
            {
                throw ex;
            }

        }

        private async Task AddUserLoginsListToDb(IEnumerable<UserLogin> userlogins)
        {
            try
            {
                var prodProcesser = new DataProcesser(_iAgentProdDb);
                await prodProcesser.AddListOfUserLogins(userlogins);
                lblStatus.Text = "Added all user logins to production";
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<IEnumerable<UserLogin>> FinUserLogins()
        {
            try
            {
                var clientKey = Guid.Parse(txtClientKey.Text);
                var clientLocationKey = Guid.Parse(txtClientLocationKey.Text);
                var devProcesser = new DataProcesser(_iAgentDevDb);
                var userlogins = await devProcesser.FindUserLogins_With_ClientKey_ClientLocationKey(clientKey, clientLocationKey);
                return userlogins;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task AddUserLogin(UserLogin builtUserLogin, IDbConnection db)
        {
            try
            {
                IAsyncRepository<UserLogin> userLoginsRepo = new UserLoginsRepositoryAsync(db);
                await userLoginsRepo.AddAsync(builtUserLogin);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        private async Task<IEnumerable<PayerWebsiteMappingValue>> GetPayerWebsiteMappingValuesRecords(IDbConnection db)
        {
            var processer = new DataProcesser(db);
            var clientKey = Guid.Parse(txtClientKey.Text);
            var clientLocationKey = Guid.Parse(txtClientLocationKey.Text);
            try
            {
                return await processer.GetPayerWebsiteMappingValues_With_client_and_LocationKeys(clientKey, clientLocationKey);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task GetPayerResponseIprKeysAsync()
        {
            try
            {
                var iprkeys = await _payerResponseRepo.GetAllResponseMapIPRKeys();
                IPRKeysToDropDownMenu(iprkeys);
            }
            catch (SqlException)
            {
                throw;
            }
        }

        private async Task GetPayerReponseRecordsWithIprKeyAsync(IDbConnection db)
        {
            try
            {
                var iprKey = DDLIPRKeys.Text;
                lblStatus.Text = "Getting payer response records";
                //var repo = _kernel.Get<PayerResponseRepositoryAsync>()
                IKernel kerenl = new StandardKernel();
                var repo = kerenl.Get<PayerResponseRepositoryAsync>(new ConstructorArgument("db", db));
                var payerResponseRecords = await repo.GetByIPRKeyAsync(iprKey);
                PayerReposeRecordsToDataGridView(payerResponseRecords);
                PayerReponseIdsToDDL(payerResponseRecords);
                lblStatus.Text = "done...";
            }
            catch (SqlException)
            {
                throw;
            }
        }

        private async Task GetExtractionMapRecordsAsync()
        {
            var payerResponseId = long.Parse(DDLPayerResponseIds.Text);
            if (payerResponseId > 0)
            {
                try
                {
                    lblStatus.Text = "Getting Extraction records...";
                    var extractionMapRecords = await _extractionMapRepo.GetById(payerResponseId);
                    ExtractionMapToDataGridVew(extractionMapRecords);
                }
                catch (SqlException)
                {
                    throw;
                }
            }
        }

        private void ExtractionMapToDataGridVew(IEnumerable<ExtractionMap> extractionMapRecords)
        {
            if (extractionMapRecords != null)
            {
                DataGridViewExtractionMap.DataSource = extractionMapRecords;
            }
        }

        private void PayerReposeRecordsToDataGridView(IEnumerable<PayerResponseMap> payerReponseRecords)
        {
            if (payerReponseRecords != null)
            {
                DataGridViewPayerReponseMap.DataSource = payerReponseRecords;
            }
        }
        private void PayerReponseIdsToDDL(IEnumerable<PayerResponseMap> payerReponseRecords)
        {
            if (payerReponseRecords.Any())
            {
                DDLPayerResponseIds.Items.Clear();
                //var filter = payerReponseRecords.Select(r => r.IPRKey);
                //DDLPayerResponseIds.DataSource = filter.ToList();

                foreach (var item in payerReponseRecords)
                {
                    DDLPayerResponseIds.Items.Add(item.PayerResponseId);
                }
            }
        }
        private void IPRKeysToDropDownMenu(IEnumerable<PayerResponseMap> iprkeys)
        {
            try
            {
                DDLIPRKeys.DataSource = null;
                DDLIPRKeys.DisplayMember = "IPRKey";
                if (iprkeys.Count() != 0)
                {
                    DDLIPRKeys.DataSource = iprkeys;
                    DDLIPRKeys.SelectedIndex = 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        private async Task AddPortalUserRecord(PortalUser portalUser, IDbConnection db)
        {
            var dataProcesser = new DataProcesser(db);
            try
            {
                await dataProcesser.AddPortalUserRecord(portalUser);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        private PortalUser BuildPortalUserRecord()
        {
            var portalUser = new PortalUser()
            {
                PortalUser_Id = int.Parse(DDLPortalUserId.Text),
                Portal_Id = int.Parse(ddlPortalId.Text),
                Username = txtUsername.Text.EncryptStringBase64(),
                Password = txtPassword.Text.EncryptStringBase64(),
                IsExpired = false,
                IsEnabled = true,
                AllowsConcurrent = true
                //Location_Id = int.Parse(txtLocationId.Text)
            };
            return portalUser;
        }

        private async Task GetAgentConfiguration()
        {
            lblStatus.Text = "Getting Agent Configuration";
            try
            {
                var agentConfig = await _agentConfigRepo.FindWithIdAsync(ParentId);
                AgentConfigsToGridView(agentConfig);
                lblStatus.Text = "Getting Agent Configuration done...";
            }
            catch (SqlException)
            {
                throw;
            }
        }
        private async Task GetFacilityMaster(IDbConnection db)
        {
            try
            {
                lblStatus.Text = "Getting Facility Master Records";
                var facilityMaster = await GetFacility(db);
                if (facilityMaster.Count() != 0)
                {
                    FacilityDataToForm(facilityMaster);
                }
                lblStatus.Text = "Done";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Getting facility master records {0}:", ex.ToString());
            }

        }
        private async Task GetClientMaster(IDbConnection db)
        {
            try
            {
                //lblStatus.Text = "Getting Client Records";
                IAsyncRepository<ClientMaster> clientRepo = new ClientMasterRepositoryAsync(db);
                _clientMasterRecords = await clientRepo.GetAllAsync();
                ClientMasterRecords_ToDropDownList(_clientMasterRecords);
                ClientDataToForm(_clientMasterRecords);
                //lblStatus.Text = "Done";
            }
            catch (SqlException)
            {
                throw;
            }

        }
        private async Task GetClientLocations(IDbConnection db)
        {            
            try
            {
                _clientLocationRecords = null;
                lblStatus.Text = "Getting Client Location records";
                _clientLocationRecords = await GetClientLocationRecords(db);
                ClientLocations_To_Drop_Down(_clientLocationRecords);
                ClientLocationToForm(_clientLocationRecords);
                lblStatus.Text = " Getting Client Locations Done";
            }
            catch (Exception)
            {

                throw;
            }

        }

        private async Task GetWebsiteRecords(IDbConnection db)
        {
            lblStatus.Text = "Getting Website Master records";
            IAsyncRepository<WebsiteMaster> websiteRepo = new WebsiteMasterAsyncRepository(db);
            try
            {
                _websiteMasterRecords = await websiteRepo.GetAllAsync();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                WebsiteMasterRecords_ToDropDownList(_websiteMasterRecords);
                lblStatus.Text = "Getting website records done...";
            }
        }

        private async Task GetClientMasterRecordsAsync(IDbConnection db)
        {
            IKernel kernel = new StandardKernel();
            try
            {
                lblStatus.Text = "Getting Client Master records";
                IAsyncRepository<ClientMaster> repo = kernel.Get<ClientMasterRepositoryAsync>(new ConstructorArgument("db", db));
                _clientMasterRecords = await repo.GetAllAsync();
                ClientMasterRecords_ToDropDownList(_clientMasterRecords);
                ClientDataToForm(_clientMasterRecords);

                lblStatus.Text = "Getting Client Master records done...";
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                lblStatus.Text = "Error Getting Client Records!! STB.";
            }

        }
        private async Task GetClientMappingValues(IDbConnection db)
        {
            try
            {
                if (txtClientKey.Text != "")
                {
                    var clientKey = Guid.Parse(txtClientKey.Text);
                    var processer = new DataProcesser(db);
                    //    lblStatus.Text = "Getting Client Mapping Master";
                    var clientMappingValues = await processer.GetClientMappingValues_With_ClientKey(clientKey);
                    ClientMappingValuesToGridView(clientMappingValues);
                    ClientDataToForm(_clientMasterRecords);
                    //    lblStatus.Text = "Getting Client Mapping Master Done...";
                }
            }
            catch (SqlException)
            {
            }
        }
        private async Task GetClientMappingMasterRecordsAsync(IDbConnection db)
        {
            if (txtClientKey.Text != "")
            {
                try
                {
                    IAsyncRepository<ClientMappingMaster> clientMappingMasterRepo = new ClientMappingMasterRepositoryAsync(db);
                    var clientKey = Guid.Parse(txtClientKey.Text);
                    lblStatus.Text = "Getting Client Mapping Master...";
                    var clientMappingMasterRecords = await clientMappingMasterRepo.FindWithGuidAsync(clientKey);
                    ClientMappingMasterRecordsToGridView(clientMappingMasterRecords);
                    lblStatus.Text = "Getting Client Mapping Master Done...";
                }
                catch (SqlException)
                {
                    MessageBox.Show("Getting Client Mapping Master Done...");
                    throw;
                }
            }
        }

        private async Task GetPortalUsers(IDbConnection db)
        {
            try
            {
                IAsyncRepository<PortalUser> portalUserRepo = new PortalUsersAsyncRepository(db);
                lblStatus.Text = "Getting Portal Users";
                var portalUsers = await portalUserRepo.FindWithIdAsync(LocationId);
                var decryptedPortalUsers = DecryptBase64PortalUsers(portalUsers);
                PortalUsersToDataGridView(decryptedPortalUsers);
                lblStatus.Text = "Done";
            }
            catch (SqlException)
            {
                throw;
            }
        }

        private void PortalUsersToDataGridView(IEnumerable<PortalUser> portalUsers)
        {
            if (!portalUsers.Equals(null))
            {
                PortalUsersGridVeiw.DataSource = portalUsers;
            }
        }

        private async Task GetFaciliyDetailValues(IDbConnection db)
        {
            lblStatus.Text = "Getting facility Detials";
            try
            {
                var facilityDetials = await GetFacilityDetails(db);
                FacilityDetialsToGridView(facilityDetials);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                lblStatus.Text = "Done..";
            }
        }
        private async Task GetClientScripts(IDbConnection db)
        {
            try
            {
                await GetClientScriptsRecords(db).
                ContinueWith(t =>
                {
                    ClientScriptsToGridView(t.Result);
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task GetPortals(IDbConnection db)
        {
            try
            {
                lblStatus.Text = "Getting Portals records";
                var portals = await GetPortalRecords(db);
                PortalsToDropDownList(portals);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                lblStatus.Text = "Done";
            }
        }

        private async Task GetPayerWebsiteMappingValues(IDbConnection db)
        {
            try
            {
                var clientKey = Guid.Parse(txtClientKey.Text);
                var clientLocationKey = Guid.Parse(txtClientLocationKey.Text);
                IAsyncRepository<PayerWebsiteMappingValue> payerWebsiteMappingValuesRepo = new PayerWebsiteMappingValuesAsyncRepository(db);
                lblStatus.Text = "Getting payer website mapping value records";
                var payerWebsiteMappingValues = await payerWebsiteMappingValuesRepo.FindWith2GuidsAsync(clientKey, clientLocationKey);
                PayerWebsiteMappingValuesToGridView(payerWebsiteMappingValues);
                lblStatus.Text = "Done";
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task GetUserLogins(IDbConnection db)
        {
            var decryptedUserLogins = new List<UserLogin>();
            lblStatus.Text = "Getting User Logins";
            var userLogins = await GetUserLoginRecords(db);
            foreach (var item in userLogins)
            {
                var login = DecryptUserLoginsBase64(item);
                decryptedUserLogins.Add(login);
            }
            UserLoginsToGridView(decryptedUserLogins);
            lblStatus.Text = "Done";
        }

        private UserLogin DecryptUserLoginsBase64(UserLogin login)
        {
            var userlogin = new UserLogin()
            {
                WebsiteUserName = login.WebsiteUserName.DecryptStringBase64(),
                WebsitePassword = login.WebsitePassword.DecryptStringBase64(),
                WebsiteDescription = login.WebsiteDescription,
                ClientKey = login.ClientKey,
                ClientLocationKey = login.ClientLocationKey,
                WebsiteKey = login.WebsiteKey,
                UserID = login.UserID,
                AgentLogin = login.AgentLogin,
            };
            return userlogin;
        }

        private async Task<UserLogin> GetUserLogin(UserLogin userLogn, IDbConnection db)
        {
            try
            {
                var dataProcesser = new DataProcesser(db);
                return await dataProcesser.FindUserLogin(userLogn);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        private async void GetUserLogin_Click(object sender, EventArgs e)
        {
            btnGetUserLogin.Enabled = false;
            try
            {
                var userLogin = BuildUserLoginRecord();
                var foundLogin = await GetUserLogin(userLogin, _iAgentDevDb);
                if (foundLogin != null)
                {
                    txtUsername.Text = foundLogin.WebsiteUserName.DecryptStringBase64();
                    txtPassword.Text = foundLogin.WebsitePassword.DecryptStringBase64();
                    lblStatus.Text = "Found Login";
                }
                else
                {
                    lblStatus.Text = "No login found";
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            btnGetUserLogin.Enabled = true;
        }

        public async Task UpdatePortalUserRecord(IDbConnection db)
        {
            try
            {
                var portalUser = BuildPortalUserRecord();
                IAsyncRepository<PortalUser> portalUsersRepo = new PortalUsersAsyncRepository(db);
                await portalUsersRepo.UpdateAsync(portalUser);
            }
            catch (SqlException ex)
            {
                lblStatus.Text = "Error updating Portal Users.";
                throw ex;
            }

        }

        private async Task GetTableRecordsAsync()
        {
            try
            {
                IDbConnection prodDb = new SqlConnection(ConfigurationManager.ConnectionStrings["AgentProdDb"].ConnectionString);
                var iprkey = DDLIPRKeys.Text;
                if (radioDevelopment.Checked == true)
                {
                    IDbConnection devDb = new SqlConnection(ConfigurationManager.ConnectionStrings["AgentDevDb"].ConnectionString);
                    await GetClientMappingValues(devDb);
                    await GetClientMappingMasterRecordsAsync(devDb);
                    await GetMultipleRecords(devDb);
                }
                if (radioButtonProduction.Checked.Equals(true))
                {
                    IDbConnection prodSecondConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AgentProdDb"].ConnectionString);
                    if (_clientMasterRecords.Count() < 0)
                    {
                        await GetClientMasterRecordsAsync(prodDb);
                    }
                    await GetClientMappingValues(prodSecondConnection);
                    await GetMultipleRecords(prodDb);
                }
                await GetAutoAgentId(_UpwProdDb);
                await GetPayerReponseRecordsWithIprKeyAsync(prodDb);
                await GetAgentConfiguration();
            }
            catch (SqlException)
            {
            }
        }

        private async Task GetMultipleRecords(IDbConnection db)
        {
            try
            {
                await GetClientMappingMasterRecordsAsync(db);
                await GetClientLocations(db);
                await GetClientScripts(db);
                await GetFacilityMaster(db);
                await GetPayerWebsiteMappingValues(db);
                await GetUserLogins(db);
                await GetPortalUsers(db);
                await GetPortalUserIds(db);
                if (txtFacilityKey.Text != "")
                {
                    await GetFaciliyDetailValues(db);
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        private async Task GetPortalUserIds(IDbConnection db)
        {
            try
            {
                lblStatus.Text = "Getting Portal User Id's";
                IAsyncRepository<PortalUser> portalUsersRepo = new PortalUsersAsyncRepository(db);
                _portalUsers = await portalUsersRepo.GetAllAsync();
                PortalUserIdsToDropDownList(_portalUsers);
                PortalUserDataToForm(_portalUsers);
                lblStatus.Text = "Getting Portal User Id's complete";
            }
            catch (SqlException)
            {
                throw;
            }
        }
    }
}
