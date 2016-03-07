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
using Ninject;
using Ninject.Parameters;


namespace iAgentDataTool.Tests.AsyncRepositoryTests
{
    [TestFixture]
    public class ClientScriptsAsyncRepositoryTest
    {
        private readonly IDbConnection _iAgentDevDb = new SqlConnection(ConfigurationManager.ConnectionStrings["AgentDevDb"].ConnectionString);
        private readonly IDbConnection _iAgentProdDb = new SqlConnection(ConfigurationManager.ConnectionStrings["AgentProdDb"].ConnectionString);

        [Test]
        public async void Get_Client_Scripts_Async_Test()
        {
            var clientKey = new Guid("C55C99A5-E0CF-4023-AD29-BE262C9B2939");
            IAsyncRepository<ClientScript> clientScriptsRepo = new ClientScriptsAsyncRepostiory(_iAgentDevDb);
            var clientScriptRecords = await clientScriptsRepo.FindWithGuidAsync(clientKey);

            foreach (var item in clientScriptRecords)
            {
                Console.WriteLine(item.ToString());
            }
        }
        [Test]
        public async Task Get_Client_Scripts_Then_Insert_Into_Server()
        {
            var clientKey = new Guid("9bef1707-6e7f-4549-b682-215f9b9be4a1");
            IKernel kernel = new StandardKernel();
            IAsyncRepository<ClientScript> repo = kernel.Get<ClientScriptsAsyncRepostiory>(new ConstructorArgument("db", _iAgentDevDb));
            try
            {
                var clientScripts = await repo.FindWithGuidAsync(clientKey);
                if (!clientScripts.Equals(null))
                {
                    Assert.DoesNotThrow(async () =>  {
                        await repo.AddMultipleToProd(clientScripts);
                    });
                }
            }
            catch (Exception)
            {              
                throw;
            }
        }
    }
}
