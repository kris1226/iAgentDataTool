using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using iAgentDataTool.Repositories;
using iAgentDataTool.Models;
using iAgentDataTool.Helpers.Interfaces;
using Ninject.Injection;
using Ninject.Modules;
using System.Reflection;
using iAgentDataTool.Repositories.Common;
using System.Data;

namespace iAgentDataTool
{
    static class Program 
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CompostionRoot.Wire(new ApplcationModule());
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(
                    CompostionRoot.Resolve<ClientInformationForm>()
            );
        }
    }
}
