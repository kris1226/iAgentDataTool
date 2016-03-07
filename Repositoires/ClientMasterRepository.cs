using iAgentDataTool.DataLayer.DbContexts;
using iAgentDataTool.Models.Common;
using iAgentTool.DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data;


namespace Repositoires
{
    public class ClientMasterRepository : IRepository<dsa_clientMaster>
    {
        private iAgentRemixDb _dbctx;

        public ClientMasterRepository(iAgentRemixDb dbctx)
        {
            this._dbctx = dbctx;
        }
        public async Task<int> AddAsync(dsa_clientMaster client)
        {
            using (_dbctx)
            {
                _dbctx.ClientMaster.Add(client);
                return await _dbctx.SaveChangesAsync();
            }
        }

        public Task<int> RemoveAsync(dsa_clientMaster t)
        {
            throw new NotImplementedException();
        }

        public Task<List<dsa_clientMaster>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(dsa_clientMaster t)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<dsa_clientMaster> FindAsync(System.Linq.Expressions.Expression<Func<dsa_clientMaster, bool>> match)
        {
            throw new NotImplementedException();
        }

        public Task<List<dsa_clientMaster>> FindAllAsync(System.Linq.Expressions.Expression<Func<dsa_clientMaster, bool>> match)
        {
            throw new NotImplementedException();
        }
    }
}
