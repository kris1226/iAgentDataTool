using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using iAgentDataTool.Models.Common;
using iAgentDataTool.Helpers.Interfaces;
using NUnit.Framework;
using Ninject;
using Dapper;
using iAgentDataTool.Repositories;
using Ninject.Parameters;


namespace iAgentDataTool.Tests.AsyncRepositoryTests
{
    [TestFixture]
    public class ScriptMasterRepositoryTest
    {
        IDbConnection _iAgentDevDb = new SqlConnection(ConfigurationManager.ConnectionStrings["AgentDevDb"].ConnectionString);

        [Test]
        public async Task ScriptMaster_Test()
        {
            var websiteKey = Guid.Parse("1c9d5ed2-8114-e211-b35b-000c29729dff");
            IKernel kernel = new StandardKernel();
            IAsyncRepository<ScriptMaster> scriptMasRepo = kernel.Get<ScriptMasterRepository>(new ConstructorArgument("db",_iAgentDevDb));
            var scripts = await scriptMasRepo.FindWithGuidAsync(websiteKey);
            Action<ScriptMaster> print = Console.WriteLine;
            scripts.ToList().ForEach(print);
        }
        [Test]
        public async void GetDataTableTest()
        {
            var websiteKey = Guid.Parse("1c9d5ed2-8114-e211-b35b-000c29729dff");
            IKernel kernel = new StandardKernel();
            IDataBindingRepo scriptMasRepo = kernel.Get<ScriptMasterRepository>(new ConstructorArgument("db", _iAgentDevDb));
            var table = await scriptMasRepo.GetData(websiteKey);
            Assert.IsNotNull(table);
            //Action<DataTable> print = Console.WriteLine;
            //Console.WriteLine(table.ToString());
            //print(table);
         
        }
    }
}
