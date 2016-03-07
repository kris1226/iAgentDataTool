using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iAgentDataTool.Helpers.Interfaces;
using Ninject.Modules;
using Ninject.Infrastructure;
using Ninject;
using Ninject.Activation;
using iAgentDataTool.Models;
using iAgentDataTool.Models.Common;
using iAgentDataTool.Repositories.Common;

namespace iAgentDataTool
{
    public static class NinjectInstanceProivder
    {
        public static void RegisterServices(IKernel kerenel)
        {
            kerenel.Bind<IAsyncRepository<ClientMaster>>().To<ClientMasterRepositoryAsync>();
            kerenel.Bind<IEnumerable<ClientMaster>>().To<List<ClientMaster>>();
        }
        public static void Start()
        {
            IKernel kerenel = new StandardKernel();
            RegisterServices(kerenel);
            
        }
    }
}
