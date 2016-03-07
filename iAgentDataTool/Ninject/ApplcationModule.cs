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

namespace iAgentDataTool
{
    public class ApplcationModule : NinjectModule
    {

        public override void Load()
        {
            IDbConnection _iAgentDevDb = new SqlConnection(ConfigurationManager.ConnectionStrings["AgentDevDb"].ConnectionString);
            IDbConnection _iAgentProdDb = new SqlConnection(ConfigurationManager.ConnectionStrings["AgentProdDb"].ConnectionString);
   
            //Bind<IAsyncRepository<UserLogin>>().To<UserLoginsRepositoryAsync>();
            //Bind<IAsyncRepository<PortalUser>>().To<PortalUsersAsyncRepository>();
            //Bind<IAsyncRepository<ClientLocation>>().To<ClientLocationsAsyncRepository>();
            //Bind<IAsyncRepository<WebsiteMaster>>().To<WebsiteMasterAsyncRepository>();
            //Bind<IAsyncRepository<ClientMaster>>().To<ClientMasterRepositoryAsync>();
            Bind<IDbConnection>().To<SqlConnection>();
            Bind<IAsyncRepository<AgentConfiguration>>().To<AgentConfigurationAsyncRepository>();

            Bind<IDapperAsyncRepository<PayerResponseMap>>().To<PayerResponseRepositoryAsync>();
            Bind<IPayerResponseRepository>().To<PayerResponseRepositoryAsync>();
        

            Bind<IDapperAsyncRepository<ExtractionMap>>()
                .To<ExtractionMapRepositoryAsync>()
                .WithConstructorArgument("db", _iAgentDevDb);

            Bind<IAsyncRepository<ScriptMaster>>().To<ScriptMasterRepository>().WithConstructorArgument("db", _iAgentDevDb);

            Bind<IAsyncRepository<WebsiteMaster>>().To<WebsiteMasterAsyncRepository>().WithConstructorArgument("db", _iAgentDevDb);
            Bind<IDataBindingRepo>().To<ScriptMasterRepository>().WithConstructorArgument("db", _iAgentDevDb);

            var transRepo = Bind<ITransactionRepo>().To<TransactionRepo>().WithConstructorArgument("db", _iAgentProdDb);
            Bind<ITransactionService>().To<TransactionService>().WithConstructorArgument("transRepo", transRepo);
            Bind<IAsyncRepository<ClientScript>>().To<ClientScriptsAsyncRepostiory>().WithConstructorArgument("db", _iAgentDevDb);

        }
    }
}
