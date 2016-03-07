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
    public class PayerWebsiteMappingValuesAsyncTest
    {
        private readonly IDbConnection _iAgentProdDb = new SqlConnection(ConfigurationManager.ConnectionStrings["AgentProdDb"].ConnectionString);

        [Test]
        public async void Get_PayerWebsiteMappingValues_Test()
        {
            var clientKey = new Guid("e955eb96-166a-4654-b474-9367d7a01fe2");
            var clientLocationKey = new Guid("421580b9-a564-e211-97df-000c29729dff");
            IAsyncRepository<PayerWebsiteMappingValue> payerWebsiteRepo = new PayerWebsiteMappingValuesAsyncRepository(_iAgentProdDb);
            var payerWebsiteMappingValues = await payerWebsiteRepo.FindWith2GuidsAsync(clientKey, clientLocationKey);

            foreach (var item in payerWebsiteMappingValues)
	        {
                 Console.WriteLine("Defualt Value: " + item.DefaultValue + " Detial Label: " + item.DetailLabel + " Primary Payer Key: " + item.Primary_PayerKey);
	        }
            Assert.IsNotNull(payerWebsiteMappingValues);
        }
    }
}
