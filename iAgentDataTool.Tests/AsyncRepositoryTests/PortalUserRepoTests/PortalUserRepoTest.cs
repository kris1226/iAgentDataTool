using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using iAgentDataTool.Helpers.Interfaces;
using Ninject;
using Ninject.Parameters;
using NUnit.Framework;
using iAgentDataTool.Helpers;
using iAgentDataTool.Repositories.AsyncRepositoires.RemixRepositoires;



namespace iAgentDataTool.Tests.AsyncRepositoryTests
{
    [TestFixture]
    class PortalUserRepoTest
    {
        private readonly IDbConnection _iAgentDevDb = new SqlConnection(ConfigurationManager.ConnectionStrings["AgentDevDb"].ConnectionString);
        [Test]
        public async void UpdateUsername_Test()
        {
            int Portal_Id = 13;
            int Id = 915; 
            //var username = "Test";

            IKernel kernel = new StandardKernel();
            IPortalUserRepo portalUserRepo = kernel.Get<PortalUserRepo>(new ConstructorArgument("db", _iAgentDevDb));
            try
            {
                await portalUserRepo.UpdatePortalId(Portal_Id, Id);
            }
            catch (SqlException)
            {               
                throw;
            }

        }
        [Test]
        public void Test()
        {
            string value = "oYgaSpH7HxyuCxuXZSSn+NriD+vjOVJe0FJkTB2XWPs=";
            var decode = value.DecryptStringBase64();
            Console.WriteLine(decode);
        }
    }
}
