using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using iAgentDataTool.Helpers.Interfaces;
using iAgentDataTool.Models.Common;
using iAgentDataTool.Models.Remix;
using Dapper;
using Ninject;
using NUnit.Framework;
using iAgentDataTool.Repositories.AsyncRepositoires.RemixRepositoires;

namespace iAgentDataTool.Tests.AsyncRepositoryTests
{
    [TestFixture]
    public class ExtractionMapRepositoryAsyncTest
    {
        [Test]
        public async Task GetReponse_Records_Test() 
        {
            var payerResponseId = 108;
            var kernel = new StandardKernel();
            //kernel.Bind<IDbConnection>().To<SqlConnection>();
            //var sqlConnection = kernel.Get<IDbConnection>();
            //sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AgentDevDb"].ConnectionString);

            kernel.Bind<IDapperAsyncRepository<ExtractionMap>>().To<ExtractionMapRepositoryAsync>()
                   .WithConstructorArgument(
                        "db",
                         new SqlConnection(ConfigurationManager.ConnectionStrings["AgentDevDb"].ConnectionString)
                    );

            var extractionMapRepo = kernel.Get<IDapperAsyncRepository<ExtractionMap>>();
            var extractionMapRecords = await extractionMapRepo.GetById(payerResponseId);
            Assert.IsNotEmpty(extractionMapRecords);

            foreach (var item in extractionMapRecords)
            {
                Console.WriteLine(item.Location);
            }
        }
    }
}
