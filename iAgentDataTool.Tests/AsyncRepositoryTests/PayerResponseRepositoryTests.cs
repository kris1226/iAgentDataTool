using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Ninject;
using iAgentDataTool.Helpers.Interfaces;
using iAgentDataTool.Models.Remix;
using NUnit.Framework;
using Dapper;

namespace iAgentDataTool.Repositories.AsyncRepositoires.RemixRepositoires
{
    [TestFixture]
    public class PayerResponseRepositoryTests
    {
        [Test]
        public async Task Get_By_IprKey_Test()
        {
            var iprkey = "PCMAGNA";
            IPayerResponseRepository payerResponseRepo = new PayerResponseRepositoryAsync();
            var payerReponseRecords = await payerResponseRepo.GetByIPRKeyAsync(iprkey);
            Assert.IsNotEmpty(payerReponseRecords);

            //foreach (var item in payerReponseRecords)
            //{
            //    Console.WriteLine("IprKey: " + item.IPRKey + "\nLook Up Text: " + item.LookupText + " \nHTML response type: " + item.HtmlResponseType + "\n ");
            //}  
        }
        [Test]
        public async Task Get_All_ResponseMapRecords_Test()
        {
            IDapperAsyncRepository<PayerResponseMap> payerResponseRepo = new PayerResponseRepositoryAsync();
            var payerResponseRecords = await payerResponseRepo.GetAllAsync();
            Assert.IsNotEmpty(payerResponseRecords);

            //foreach (var item in payerResponseRecords)
            //{
            //    Console.WriteLine(item.PayerResponseId);
            //}
        }
        [Test]
        public async Task GetById_Test()
        {
            long payerReponseId = 211;
            var kernel = new StandardKernel();
            kernel.Bind<IDapperAsyncRepository<PayerResponseMap>>().To<PayerResponseRepositoryAsync>();
            var payerResponseRepo = kernel.Get<PayerResponseRepositoryAsync>();
            var payerReponseRecord = await payerResponseRepo.GetById(payerReponseId);
            Assert.IsNotNull(payerReponseRecord);
            Console.WriteLine(payerReponseRecord.SingleOrDefault().PayerResponseId);
        }
        [Test]
        public async Task Get_IprKeys_Test()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IPayerResponseRepository>().To<PayerResponseRepositoryAsync>();
            var payerResponseRepo = kernel.Get<PayerResponseRepositoryAsync>();

            var payerReponseRecord = await payerResponseRepo.GetAllResponseMapIPRKeys();
            Assert.IsNotNull(payerReponseRecord);
            foreach (var item in payerReponseRecord)
            {
                Console.WriteLine(item.IPRKey);
            }
        }
    }
}
