using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using Ninject;
using iAgentDataTool.Helpers.Interfaces;
using iAgentDataTool.Models.Common;
using iAgentDataTool.Models;
using iAgentDataTool.Repositories;
using iAgentDataTool.Models.Remix;
using iAgentDataTool.Repositories.RemixRepositories;
using iAgentDataTool.Repositories.AsyncRepositoires;
using iAgentDataTool.AsyncRepositories.Common;
using iAgentDataTool.Repositories.Common;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using iAgentDataTool.Repositories.AsyncRepositoires.RemixRepositoires;
using iAgentDataTool.Ninject;
using iAgentDataTool.Services;
using Ninject.Parameters;

namespace iAgentDataTool.Ninject
{
    public class TransactionModule : ApplcationModule
    {
        public override void Load() 
        {
            IKernel kernel = new StandardKernel();
            
            IDbConnection _iAgentDevDb = new SqlConnection(ConfigurationManager.ConnectionStrings["AgentDevDb"].ConnectionString);
            IDbConnection _iAgentProdDb = new SqlConnection(ConfigurationManager.ConnectionStrings["AgentProdDb"].ConnectionString);
            var transRepo = kernel.Get<TransactionRepo>(new ConstructorArgument("db", _iAgentProdDb));

            Bind<ITransactionRepo>().To<TransactionRepo>().WithConstructorArgument("db", _iAgentProdDb);
            Bind<ITransactionService>().To<TransactionService>().WithConstructorArgument("transRepo", transRepo);
        }
    }
}
