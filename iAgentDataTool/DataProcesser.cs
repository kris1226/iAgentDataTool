using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iAgentDataTool.Repositories.Common;
using iAgentDataTool.Repositories.RemixRepositories;
using iAgentDataTool.Models.Remix;
using iAgentDataTool.Helpers.Interfaces;
using iAgentDataTool.Repositories.AsyncRepositoires;
using iAgentDataTool.Models.Common;
using iAgentDataTool.Models;
using iAgentDataTool.AsyncRepositories.Common;
using iAgentDataTool.Repositories.AsyncRepositoires.RemixRepositoires;

namespace iAgentDataTool
{
    public class DataProcesser
    {
        private readonly IDbConnection _db;

        public DataProcesser(IDbConnection db)
        {
            this._db = db;
        }

        public async Task<IEnumerable<ClientScript>> GetClientScripts_With_ClientKey(Guid clientKey) 
        {
            IAsyncRepository<ClientScript> clientScriptsRepo = new ClientScriptsAsyncRepostiory(_db);
            try
            {
                return await clientScriptsRepo.FindWithGuidAsync(clientKey);
            }
            catch (Exception)
            {            
                throw;
            }          
        }

        public async Task<IEnumerable<ApcoWebDev>> FindApcoRecordsByName(string name)
        {
            IAsyncRepository<ApcoWebDev> apcoRepo = new ApcoWebDevAsyncRepoisitory(_db);
            try
            {
                return await apcoRepo.FindByName(name);
            }
            catch (Exception)
            {         
                throw;
            }      
        }

        public async Task<IEnumerable<Upw>> FindUpwRecords(string name)
        {
            IAsyncRepository<Upw> upwRepo = new UpwAsyncRepository(_db);
            try
            {
                return await upwRepo.FindByName(name);
            }
            catch (Exception)
            {         
                throw;
            }
        }

        public async Task<IEnumerable<ClientLocations>> GetClientLocations_With_ClientKey(Guid clientKey)
        {
            IAsyncRepository<ClientLocations> clientLocationsRepo = null;
            IEnumerable<ClientLocations> clientLocations = null;

            clientLocationsRepo = new ClientLocationsAsyncRepository(_db);
            try
            {
                return clientLocations = await clientLocationsRepo.FindWithGuidAsync(clientKey);
            }
            catch (Exception)
            {
                return clientLocations;
            }
        }

        public async Task<IEnumerable<WebsiteMaster>> GetWebsiteMasterRecords()
        {
            IAsyncRepository<WebsiteMaster> websiteRepo = new WebsiteMasterAsyncRepository(_db);
            try
            {
                return await websiteRepo.GetAllAsync();
            }
            catch (Exception)
            {
                
                throw;
            }         
        }

        public async Task<IEnumerable<Upw>> GetAutoAgentUserName(Upw upwRecord)
        {
            IAsyncRepository<Upw> upwRepo = null;
            try
            {
                upwRepo = new UpwAsyncRepository(_db);
                return await upwRepo.Find(upwRecord);
            }
            catch (Exception)
            {
                
                throw;
            }                 
        }

        public async Task<IEnumerable<FacilityMaster>> GetFacility(Guid clientKey, Guid clientLocationKey)
        {
            IAsyncRepository<FacilityMaster> facilityRepo = new FacilityMasterAsyncRepository(_db);
            try
            {
                return await facilityRepo.FindWith2GuidsAsync(clientKey, clientLocationKey);
            }
            catch (Exception)
            {              
                throw;
            }
        }

        //public async Task<IEnumerable<FacilityDetail>> GetFacilityDetails(Guid facilityKey)
        //{
        //    IAsyncRepository<FacilityDetail> facilityDetialsRepo = new FacilityDetialsAsyncRepository(_db);
        //    try
        //    {
        //        return  await facilityDetialsRepo.FindWithGuidAsync(facilityKey);
        //    }
        //    catch (Exception)
        //    {
                
        //        throw;
        //    }      

        //}

        public async Task<IEnumerable<PortalUser>> GetPortalUsersWithLocationd(int id)
        {
            IAsyncRepository<PortalUser> portalUsersRepo = new PortalUsersAsyncRepository(_db);
            try
            {
                return await portalUsersRepo.FindWithIdAsync(id);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public async Task<IEnumerable<AgentConfiguration>> GetAgentConfigurations(int id)
        {
            IAsyncRepository<AgentConfiguration> agentConfigRepo = new AgentConfigurationAsyncRepository();
            try
            {
                return await agentConfigRepo.FindWithIdAsync(id);
            }
            catch (Exception)
            {              
                throw;
            }
        }

        public async Task<IEnumerable<PayerWebsiteMappingValue>> GetPayerWebsiteMappingValues_With_client_and_LocationKeys(Guid clientKey, Guid clientLocationKey)
        {
            IAsyncRepository<PayerWebsiteMappingValue> payerWebsiteMappingValuesRepo = new PayerWebsiteMappingValuesAsyncRepository(_db);
            try
            {
                return await payerWebsiteMappingValuesRepo.FindWith2GuidsAsync(clientKey, clientLocationKey);
            }
            catch (Exception)
            {              
                throw;
            }
        }

        //public async Task<IEnumerable<ClientMappingMaster>> GetClientMappingMasterRecords_With_ClientKey(Guid clientKey)
        //{
        //    IAsyncRepository<ClientMappingMaster> clientMappingMasterRepo = new ClientMappingMasterRepositoryAsync(_db);
        //    try
        //    {
        //        return await clientMappingMasterRepo.FindWithGuidAsync(clientKey);
        //    }
        //    catch (Exception)
        //    {
                
        //        throw;
        //    }
        //}

        public async Task<IEnumerable<ClientMappingValue>> GetClientMappingValues_With_ClientKey(Guid clientKey)
        {
            IAsyncRepository<ClientMappingValue> clientMappingValuesRepo = new ClientMappingValuesRepositoryAsync(_db);
            try
            {
                return await clientMappingValuesRepo.FindWithGuidAsync(clientKey);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public async Task<IEnumerable<ClientMaster>> GetAllClients()
        {
            IAsyncRepository<ClientMaster> clientRepo = new ClientMasterRepositoryAsync(_db);
            try
            {
                return await clientRepo.GetAllAsync();
            }
            catch (Exception)
            {
                
                throw;
            }
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Portal>> GetAllPortalRecords()
        {
            try
            {
                IAsyncRepository<Portal> portalRepo = new PortalsAsyncRepository(_db);
                return await portalRepo.GetAllAsync();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<UserLogin>> GetUserLoginRecords(Guid clientKey, Guid clientLocationKey)
        {
            try
            {
                IAsyncRepository<UserLogin> userLoginsRepo = new UserLoginsRepositoryAsync(_db);
                return await userLoginsRepo.FindWith2GuidsAsync(clientKey, clientLocationKey);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async Task AddUserLogin(UserLogin userLogin)
        {
            try
            {
                 IAsyncRepository<UserLogin> userLoginsRepo = new UserLoginsRepositoryAsync(_db);
                 await userLoginsRepo.AddAsync(userLogin);
            }
            catch (SqlException ex)
            {            
                throw ex;
            }
        }

        public async Task<IEnumerable<UserLogin>> GetAllUserLogins()
        {
            IAsyncRepository<UserLogin> userLoginsRepo = new UserLoginsRepositoryAsync(_db);
            try
            {
                return await userLoginsRepo.GetAllAsync();
            }
            catch (SqlException ex)
            {               
                throw ex;
            }
        }

        public async Task<IEnumerable<UserLogin>> FindUserLogins_With_ClientKey_ClientLocationKey(Guid clienKey, Guid clientLocationKey)
        {
            IAsyncRepository<UserLogin> userLoginsRepo = new UserLoginsRepositoryAsync(_db);
            try
            {
                return await userLoginsRepo.FindWith2GuidsAsync(clienKey, clientLocationKey);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async Task AddListOfUserLogins(IEnumerable<UserLogin> userlogins)
        {
            try
            {
                IAddMultipleEntitesToDbAsync<UserLogin> userLoginsRepo = new UserLoginsRepositoryAsync(_db);
                await userLoginsRepo.AddAll(userlogins);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public async Task<IEnumerable<WebsiteMaster>> GetWebsiteRecords()
        {
            try
            {
                IAsyncRepository<WebsiteMaster> websiteRepo = new WebsiteMasterAsyncRepository(_db);
                return await websiteRepo.GetAllAsync();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
       
        public async Task<IEnumerable<ClientMaster>> GetClientRecords(IDbConnection db)
        {
            var processer = new DataProcesser(db);
            try
            {
                IAsyncRepository<ClientMaster> clientRepo = new ClientMasterRepositoryAsync(db);
                return await clientRepo.GetAllAsync();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async Task<UserLogin> FindUserLogin(UserLogin userlogin)
        {
            try
            {
                IAsyncRepository<UserLogin> userloginRepo = new UserLoginsRepositoryAsync(_db);
                var task = await userloginRepo.Find(userlogin);
                return task.SingleOrDefault();

            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async Task AddPortalUserRecord(PortalUser portalUser)
        {
            IAsyncRepository<PortalUser> portalUserRepo = new PortalUsersAsyncRepository(_db);
            try
            {
                await portalUserRepo.AddAsync(portalUser);
            }
            catch (SqlException ex)
            {                
                throw ex;
            }
        }
    }
}
