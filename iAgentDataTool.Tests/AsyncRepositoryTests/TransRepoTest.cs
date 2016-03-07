using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using NUnit.Framework;
using Ninject;
using Ninject.Parameters;
using iAgentDataTool.Services;
using iAgentDataTool.Models.Remix;
using iAgentDataTool.Helpers.Interfaces;
using iAgentDataTool.Repositories.AsyncRepositoires.RemixRepositoires;
using iAgentDataTool.Repositories.RemixRepositories;


namespace iAgentDataTool.Tests.AsyncRepositoryTests
{
    [TestFixture]
    class TransRepoTest
    {
        private readonly IDbConnection _iAgentDevDb = new SqlConnection(ConfigurationManager.ConnectionStrings["AgentDevDb"].ConnectionString);
        private readonly IDbConnection  _iAgentProd = new SqlConnection(ConfigurationManager.ConnectionStrings["AgentProdDb"].ConnectionString);

        [Test]
        public async Task Test_Transaction_Rerun()
        {

            var transactionId = new Guid("bb9a9a5b-c8de-11e4-9511-005056ab171b");
            IKernel kernel = new StandardKernel();
            ITransactionRepo transRepo = kernel.Get<TransactionRepo>(new ConstructorArgument("db", _iAgentProd));
            ITransactionService transService = kernel.Get<TransactionService>(new ConstructorArgument("transRepo",transRepo));
            try
            {
                var rerun = await transService.RerunTransactionAsync(transactionId);
            }
            catch (Exception)
            {               
                throw;
            }
        }
        [Test]
        public void ReparseTransactionTest()
        {
            var transId = new Guid("2ee526e9-c99d-11e4-9511-005056ab171b");
            IKernel kernel = new StandardKernel();
            ITransactionRepo transRepo = kernel.Get<TransactionRepo>(new ConstructorArgument("db", _iAgentProd));
            ITransactionService transService = kernel.Get<TransactionService>(new ConstructorArgument("transRepo", transRepo));
            try
            {
                 transService.ReparseTransactionAsync(transId);
            }
            catch (Exception)
            {            
                throw;
            }
        }

        [Test]
        public async Task GetAgentScheduleTest()
        {
            var transId = new Guid("bb9a9a5b-c8de-11e4-9511-005056ab171b");
            IKernel kernel = new StandardKernel();
            var transRepo = kernel.Get<TransactionRepo>(new ConstructorArgument("db", _iAgentDevDb));
            try
            {
                var agentSchdule = await transRepo.FindInAgentScheduleAsync(transId);
                Action<AgentSchedule> print = Console.WriteLine;
                agentSchdule.ToList().ForEach(print);
            }
            catch (Exception)
            {              
                throw;
            }
        }
        [Test]
        public void MultipleAgentReparseTest()
        {
            List<Guid> transIds = new List<Guid>()
            {
                Guid.Parse("50b12285-c9b1-11e4-9511-005056ab171b"),
                Guid.Parse("0cc1396b-c9ad-11e4-9511-005056ab171b"),
                Guid.Parse("f1095767-c977-11e4-9511-005056ab171b")
            };
            IKernel kernel = new StandardKernel();
            ITransactionRepo transRepo = kernel.Get<TransactionRepo>(new ConstructorArgument("db", _iAgentProd));
            ITransactionService transService = kernel.Get<TransactionService>(new ConstructorArgument("transRepo", transRepo));
            try
            {
                 transService.ReparseMultipleTransactionsAsync(transIds);
            }
            catch (Exception)
            {                
                throw;
            }
        }
    }
}
