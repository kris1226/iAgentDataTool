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
    [TestFixture]
    public class ApcoWebDevAsyncReposiotryTest
    {
        private readonly IDbConnection _apcoWebDevDb = new SqlConnection(ConfigurationManager.ConnectionStrings["ApcoeWebDev"].ConnectionString);

        [Test]
        public async void GetApcoRecordsTest()
        {
            var kopSiteName = "Clair";
            IAsyncRepository<ApcoWebDev> apcoReo = new ApcoWebDevAsyncRepoisitory(_apcoWebDevDb);
            var apcoRecords = await apcoReo.FindByName(kopSiteName);

            foreach (var item in apcoRecords)
            {
                Console.WriteLine(item.KopSiteName);
            }
        }
    }
}
