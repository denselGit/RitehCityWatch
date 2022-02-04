using CityWatch.Common;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CityWatch.Server.Services.Repositories
{
    public interface ITenantRepository
    {
        Task<IEnumerable<Tenant>> GetAll();
        Task<Tenant> Save(Tenant tenant);
        Task<bool> Delete(Tenant tenant);
    }

    public class TenantRepository : ITenantRepository
    {
        internal static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        private IDatabaseService DatabaseService;
        public TenantRepository(IDatabaseService databaseService)
        {
            DatabaseService = databaseService;
        }

        public async Task<bool> Delete(Tenant tenant)
        {
            try
            {
                // brisanje tenanta je komplicirano... moramo sve linkane podatke brisati redom
                using (var ctx = DatabaseService.GetContext())
                {
                    return await ctx.DeleteAsync(tenant);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Error deleting tenant");
                return false;
            }
        }

        public async Task<IEnumerable<Tenant>> GetAll()
        {
            try
            {
                using (var ctx = DatabaseService.GetContext())
                {
                    return await ctx.QueryAsync<Tenant>("SELECT * FROM [Tenant]");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Error getting tenants");
                return null;
            }
        }

        public async Task<Tenant> Save(Tenant tenant)
        {
            if (tenant == null) {  return null; }

            try
            {
                using (var ctx = DatabaseService.GetContext())
                {
                    if (tenant.IdTenant == 0)
                    {
                        var result = await ctx.InsertAsync(tenant);
                        if (result > 0)
                        {
                            tenant.IdTenant = result;
                            return tenant;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        if (await ctx.UpdateAsync(tenant))
                        {
                            return tenant;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Error saving tenant");
                return null;
            }
        }
    }
}
