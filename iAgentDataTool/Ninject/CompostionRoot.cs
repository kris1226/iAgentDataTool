using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Modules;
using iAgentDataTool.Helpers.Interfaces;
using iAgentDataTool.Models;
using iAgentDataTool.Repositories;
using iAgentDataTool.Models.Common;
using iAgentDataTool.Repositories.Common;

namespace iAgentDataTool
{
    public class CompostionRoot
    {
        private static IKernel _ninjectKerenl;
 
        public static void Wire(INinjectModule module) 
        {
            _ninjectKerenl = new StandardKernel(module);
        }

        public static T Resolve<T>()
        {
            try
            {
                return _ninjectKerenl.Get<T>();
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("And error occured while attempting to instantiae an ovject of type: " + e + " ", typeof(T)));
            }
     
        }
        public static object Resole<T1>()
        {
            return _ninjectKerenl.Get<T1>();
        }
    }
}
