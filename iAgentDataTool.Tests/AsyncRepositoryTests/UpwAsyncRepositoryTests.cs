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
    public class UpwAsyncRepositoryTests 
    {
        private readonly IDbConnection _upwDb = new SqlConnection(ConfigurationManager.ConnectionStrings["UPW"].ConnectionString);

        [Test]
        public async Task Find_Upw_Records_Test()
        {
            var sqlDbName = "Clair";
            IAsyncRepository<Upw> upwRepo = new UpwAsyncRepository(_upwDb);
            var upwRecords = await upwRepo.FindByName(sqlDbName);

            foreach (var item in upwRecords)
            {
                Console.WriteLine(item.UserName + " | " + item.SqlDb + " | " + item.SqlServer);
            }
        }

        [Test]
        public async Task GetAutoAgentId_Test()
        {
            var clientKey = "6922413B-7A61-46B1-83E7-8386AFCF6237";
            var clientLocationKey = "2296F05B-A00A-480F-ACCD-2B1CAE2CB171";
            IUpwAsyncRepository upw = new UpwAsyncRepository(_upwDb);
            var upwRecords = await upw.FindAutoAgentId(clientKey, clientLocationKey);

            foreach (var item in upwRecords)
            {
                Console.WriteLine(item.UserName);
            }
        }
    }
}
