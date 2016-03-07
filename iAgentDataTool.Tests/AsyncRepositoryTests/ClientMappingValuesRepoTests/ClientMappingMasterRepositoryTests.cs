using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading.Tasks;
using iAgentDataTool.Models.Common;
using iAgentDataTool.Helpers.Interfaces;
using iAgentDataTool.Repositories.Common;
using NUnit.Framework;

namespace iAgentDataTool.Tests.AsyncRepositoryTests
{
    [TestFixture]
    public class ClientMappingMasterRepositoryTests
    {
        private readonly IDbConnection _iAgentDevDb = new SqlConnection(ConfigurationManager.ConnectionStrings["AgentDevDb"].ConnectionString);

        [Test]
        public async void Get_Client_Mapping_Values_Test_Async()
        {
            var clientKey = new Guid("6b4fcc9e-108c-4908-aead-fb94bcbb1d68");
            IAsyncRepository<ClientMappingMaster> clientMappingMasterRepo = null;
            clientMappingMasterRepo = new ClientMappingMasterRepositoryAsync(_iAgentDevDb);
            var clientMappingMasterRecords = await clientMappingMasterRepo.FindWithGuidAsync (clientKey);

            foreach (var item in clientMappingMasterRecords)
            {
                Console.WriteLine(item.FieldKey);
            }
        }
    }
}
