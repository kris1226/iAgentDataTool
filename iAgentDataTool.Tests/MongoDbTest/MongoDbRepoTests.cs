using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using iAgentDataTool.Models.Nosql;
using System.Data;
using System.Configuration;
using MongoDB.Driver.Builders;
using NUnit.Framework;

using Ninject.Parameters;
using Ninject;
using iAgentDataTool.Repositories.MongoRepos;


namespace iAgentDataTool.Tests.MongoDbTest
{
    [TestFixture]
    public class MongoDbRepoTests
    {
        [Test]
        public void Get_Auth_Image_ByIdTest()
        {
            var externalMongoId = "5506b8a72466d10f6ce788f3";
            IKernel kernel = new StandardKernel();
            INoSqlRepo mongoRepo = kernel.Get<MongoRepo>();
            var image = mongoRepo.GetAuthImage(externalMongoId);
            System.IO.File.WriteAllBytes(@"C:\Users\kris.lindsey\Pictures\AWESOME_FACE_2.png", image.PNGdata);
        }
    }
}
