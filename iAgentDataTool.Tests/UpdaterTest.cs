using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Data.SqlClient;
using System.Collections;
using phc_DACommon;
using iAgentDataTool.Helpers;
using iAgentDataTool.Repositories;

namespace iAgentDataTool.Tests
{
    [TestFixture]
    public class UpdaterTest
    {
        [Test]
        private void WriteToConsole(IEnumerable items)
        {
            foreach (object o in items)
            {
                Console.WriteLine(o.ToString());
            }
        }
        [Test]
        public void TestEncyption()
        {
            var secret = "My Secret";
            var encoded = secret.Encrypt("mykey");
            Console.WriteLine(encoded);
            var decoded = encoded.Decrypt("myKey");
            Console.WriteLine(decoded);
        }
    }
}
