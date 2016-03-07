using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using iAgentDataTool.Models.Common;
using iAgentDataTool.Helpers.Interfaces;
using iAgentDataTool.Repositories.Common;
using NUnit.Framework;

namespace iAgentDataTool.Tests.AsyncRepositoryTests
{
    public class ClientMappingValuesAsyncTests
    {
        private readonly IDbConnection _iAgentDevDb = new SqlConnection(ConfigurationManager.ConnectionStrings["AgentDevDb"].ConnectionString);

        public async void Get_Client_Mapping_Values_Test()
        {
            var clientKey = new Guid("6b4fcc9e-108c-4908-aead-fb94bcbb1d68");
            IAsyncRepository<ClientMappingValue> clientMappingValusRepo = null;
            clientMappingValusRepo = new ClientMappingValuesRepositoryAsync(_iAgentDevDb);
            var mappingValues = await clientMappingValusRepo.FindWithGuidAsync(clientKey);

            foreach (var item in mappingValues)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
