using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using iAgentDataTool.Models.Common;
using System.Data;
using System.Diagnostics;

namespace iAgentDataTool.DataLayer.DbContexts
{
    public class iAgentRemixDb : DbContext
    {
        public DbSet<dsa_clientMaster> ClientMaster { get; set; }
       // public DbSet<dsa_ClientLocation> ClientLocations { get; set; }

        public iAgentRemixDb(IDbConnection db)
            : base(db.ConnectionString)
        {
            Database.Log = sql => Debug.Write(sql);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("dbo.dsa_clientMaster");
            base.OnModelCreating(modelBuilder);
        }

    }
}
