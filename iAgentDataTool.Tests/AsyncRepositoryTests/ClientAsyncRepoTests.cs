using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using NUnit;
using NUnit.Framework;
using iAgentDataTool.Helpers.Interfaces;
using iAgentDataTool.Models;
using iAgentDataTool.Repositories;
using iAgentDataTool.Repositories.Common;



namespace iAgentDataTool.Tests.AsyncRepositoryTests
{
    [TestFixture]
    public class ClientAsyncRepoTests
    {
        private readonly IDbConnection _iAgentDevDb = new SqlConnection(ConfigurationManager.ConnectionStrings["AgentDevDb"].ConnectionString);
        private readonly IDbConnection _UpwProd = new SqlConnection(ConfigurationManager.ConnectionStrings["UPW"].ConnectionString);

       // [Test]
        //public async void Get_All_Clients_Async()
        //{
        //    var clients = new List<ClientMaster>();
        //    IAsyncRepository<ClientMaster> clientRepo = null;
        //    clientRepo = new ClientRepositoryAsync(_iAgentDevDb);
        //    try
        //    {
        //        var task = await clientRepo.GetAllAsync();

        //              foreach (var item in task)
        //       {
        //           foreach (var location in item.ClientLocations)
        //           {
        //               Console.WriteLine(location.ClientLocationName);
        //           }
        //       }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Write("Error Getting Clients " + ex);
        //    }
        
        //}
    }
    
}
