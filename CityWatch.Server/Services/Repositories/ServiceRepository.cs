using CityWatch.Common;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CityWatch.Server.Services.Repositories
{
    public interface IServiceRepository
    {
        Task<IEnumerable<Service>> GetAll();
        Task<IEnumerable<Service>> GetAll(int idTenant);
        Task<Service> Save(Service service);
        Task<bool> Delete(Service service);
    }
    public class ServiceRepository : IServiceRepository
    {
        internal static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
        IDatabaseService DatabaseService;

        public ServiceRepository(IDatabaseService databaseService)
        {
            DatabaseService = databaseService;
        }

        public async Task<bool> Delete(Service service)
        {
            try
            {
                using(var ctx = DatabaseService.GetContext())
                {
                    return await ctx.DeleteAsync(service);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Error deleting service");
                return false;
            }
        }

        public async Task<IEnumerable<Service>> GetAll(int idTenant)
        {
            try
            {
                using (var ctx = DatabaseService.GetContext())
                {
                    return await ctx.QueryAsync<Service>("SELECT * FROM [Service] WHERE IdTenant=@idtenant", new { idtenant = idTenant });
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Error getting service");
                return null;
            }
        }

        public async Task<IEnumerable<Service>> GetAll()
        {
            try
            {
                using (var ctx = DatabaseService.GetContext())
                {
                    return await ctx.QueryAsync<Service>("SELECT * FROM [Service]");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Error getting service");
                return null;
            }
        }

        public async Task<Service> Save(Service service)
        {
            if (service == null) return null;

            try
            {
                using (var ctx = DatabaseService.GetContext())
                {
                    if(service.IdTenant == 0)
                    {
                        var result = await ctx.InsertAsync(service);
                        if (result > 0)
                        {
                            service.IdTenant = result;
                            return service;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        if (await ctx.UpdateAsync(service))
                        {
                            return service;
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
                log.Error(ex, "Error saving service");
                return null;
            }
        }
    }
}
