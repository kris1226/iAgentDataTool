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
using iAgentDataTool.Repositories.RemixRepositories;
using NUnit.Framework;


namespace iAgentDataTool.Tests.AsyncRepositoryTests
{
    [TestFixture]
    public class AgentConfigurationAsyncRepositoryTest
    {
        private readonly IDbConnection _iAgentProdDb = new SqlConnection(ConfigurationManager.ConnectionStrings["AgentProdDb"].ConnectionString);

        [Test]
        public async void Get_Agnet_Configuration_By_ParentId_Test()
        {
            int parentId = 20;
            IAsyncRepository<AgentConfiguration> agentConfigRepo = new AgentConfigurationAsyncRepository();
            var agentConfigruation = await agentConfigRepo.FindWithIdAsync(parentId);

            foreach (var item in agentConfigruation)
            {
                Console.WriteLine(item.AgentMachineName);
            }
        }
    }
}
