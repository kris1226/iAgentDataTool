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
using iAgentDataTool.AsyncRepositories.Common;



namespace iAgentDataTool.Tests.AsyncRepositoryTests
{
    [TestFixture]
    public class WebsiteMasterAsyncRepositoryTests
    {
        private readonly IDbConnection _iAgentDevDb = new SqlConnection(ConfigurationManager.ConnectionStrings["AgentDevDb"].ConnectionString);
        [Test]
        public async void Get_WebsiteMasterRecords_Test()
        {
            IAsyncRepository<WebsiteMaster> websiteRepo = new WebsiteMasterAsyncRepository(_iAgentDevDb);
            var websiteMasterRecords = await websiteRepo.GetAllAsync();

            foreach (var item in websiteMasterRecords)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
