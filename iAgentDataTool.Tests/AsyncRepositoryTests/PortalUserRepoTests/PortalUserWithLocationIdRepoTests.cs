using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Dapper;
using NUnit.Framework;
using iAgentDataTool.Helpers;
using iAgentDataTool.Helpers.Interfaces;
using iAgentDataTool.Models.Remix.ViewModels;
using iAgentDataTool.Repositories.AsyncRepositoires.RemixRepositoires.ViewModelRepos;



namespace iAgentDataTool.Tests.AsyncRepositoryTests
{
    [TestFixture]
    public class PortalUserWithLocationIdRepoTests
    {
        private readonly IDbConnection _iAgentDevDb = new SqlConnection(ConfigurationManager.ConnectionStrings["AgentDevDb"].ConnectionString);

        [Test]
        public async Task Get_Portal_Users_By_LocationId_Test()
        {
            var locationId = 118;
            IAsyncRepository<PortalUserAndLocationId> portalUserRepo = new PortalUserDataRepo(_iAgentDevDb);
            try
            {
                var portalUsers = await portalUserRepo.FindWithIdAsync(locationId);
                Assert.IsNotNull(portalUsers);

                var decryptPortalUsers = portalUsers.Select( p => new 
                { 
                    locationId = p.Location_Id,
                    portalId = p.Portal_Id,
                    portalUserId = p.PortalUser_Id,
                    password = p.Password.DecryptStringBase64(),
                    username = p.Username.DecryptStringBase64(),
                    isEnabled = p.IsEnabled,
                    isExpired = p.IsExpired,
                    allowsConcurrent = p.AllowsConcurrent
                });

                foreach (var portalUser in decryptPortalUsers)
                {
                    Console.WriteLine(portalUser);
                }
            }
            catch (SqlException)
            {              
                throw;
            }
        }
       
        
    }
}
