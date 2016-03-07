using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using iAgentDataTool.Models.Common;
using iAgentDataTool.Repositories.Common;
using iAgentDataTool.Helpers.Interfaces;
using Dapper;
using NUnit.Framework;

namespace iAgentDataTool.Tests.AsyncRepositoryTests
{
    [TestFixture]
    public class FacilityMasterAsyncRepositoryTest
    {
        private readonly IDbConnection _iAgentDevDb = new SqlConnection(ConfigurationManager.ConnectionStrings["AgentDevDb"].ConnectionString);

        [Test]
        public async void Get_Facility_Master_RecordsAsync_Test()
        {
            var clientKey = new Guid("6b4fcc9e-108c-4908-aead-fb94bcbb1d68");
            var locationKey = new Guid("950a89ed-1e92-4154-8eb9-5e1b804605ea");
            IAsyncRepository<FacilityMaster> facilityMasterRepo = new FacilityMasterAsyncRepository(_iAgentDevDb);
            var facilities = await facilityMasterRepo.FindWith2GuidsAsync(clientKey, locationKey);
            var facility = facilities.SingleOrDefault();
            Console.WriteLine(facility.FacilityName);
        }
    }
}
