using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using iAgentDataTool.Models.Remix;
using iAgentDataTool.Helpers.Interfaces;
using iAgentDataTool.Helpers;
using iAgentDataTool.Repositories.RemixRepositories;
using NUnit.Framework;

namespace iAgentDataTool.Tests.AsyncRepositoryTests
{
    [TestFixture]
    public class PortalUserRepositoryAsyncTest
    {
        private readonly IDbConnection _iAgentDevDb = new SqlConnection(ConfigurationManager.ConnectionStrings["AgentDevDb"].ConnectionString);

        public IDbConnection Database { get { return _iAgentDevDb; } }

        [Test]
        public async void Get_Portal_Users_ByLocationId_Test()
        {
            int locationId = 153;
            IAsyncRepository<PortalUser> portalRepo = new PortalUsersAsyncRepository(Database);
            var portalUsers = await portalRepo.FindWithIdAsync(locationId);

            foreach (var item in portalUsers)
            {
                Console.WriteLine(item.Username.DecryptStringBase64() + " Password: " + item.Password.DecryptStringBase64() + " Portal Id: " + item.Portal_Id);

            }
        }
        [Test]
        public async Task Update_Portal_User_Test()
        {
            var portalUser = new PortalUser()
            {
                PortalUser_Id = 881,
                Portal_Id = 63,
                Username = "2WJROE/PV0ymHnizaof9nx9t5vKyGwWS4gftIXY2DsA=",
                Password = "+R7M+F4vowC3AvzbfOnIwiSqsUZ8Gr3VoGMQvCLfiGw="
            };
            IAsyncRepository<PortalUser> portalUserRepo = new PortalUsersAsyncRepository(Database);
            await portalUserRepo.UpdateAsync(portalUser);
            var updatedPortalUser = await portalUserRepo.Find(portalUser);

            foreach (var item in updatedPortalUser)
            {
                Console.WriteLine(item.PortalUser_Id + " " + item.Password.DecryptStringBase64());
            }
        }
        [Test]
        public async Task Find_PortalUser_Test()
        {
            var portalUser = new PortalUser()
            {
                PortalUser_Id = 881,
                Portal_Id = 63,
                Username = "2WJROE/PV0ymHnizaof9nx9t5vKyGwWS4gftIXY2DsA=",
                Password = "+R7M+F4vowC3AvzbfOnIwiSqsUZ8Gr3VoGMQvCLfiGw="
            };
            IAsyncRepository<PortalUser> portalUserRepo = new PortalUsersAsyncRepository(Database);
            var updatedPortalUser = await portalUserRepo.Find(portalUser);

            foreach (var item in updatedPortalUser)
            {
                Console.WriteLine(item.PortalUser_Id + " " + item.Username.DecryptStringBase64() + " " + item.Password.DecryptStringBase64());
            }
        }
        [Test]
        public async Task GetAllPortalUsers_Test()
        {
            IAsyncRepository<PortalUser> portalsRepo = new PortalUsersAsyncRepository(_iAgentDevDb);
            var portalUsers = await portalsRepo.GetAllAsync();
            foreach (var portalUser in portalUsers)
            {
                Console.WriteLine(portalUser.PortalUser_Id);
            }
        }

    }
}
