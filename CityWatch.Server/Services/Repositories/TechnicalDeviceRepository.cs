using CityWatch.Common;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CityWatch.Server.Services.Repositories
{
    public interface ITechnicalDeviceRepository
    {
        Task<IEnumerable<TechnicalDevice>> GetAll(int idTenant);
        Task<TechnicalDevice> Save(TechnicalDevice TechnicalDevice);
        Task<bool> Delete(TechnicalDevice TechnicalDevice);
    }

    public class TechnicalDeviceRepository : ITechnicalDeviceRepository
    {
        internal static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        private IDatabaseService DatabaseService;
        public TechnicalDeviceRepository(IDatabaseService databaseService)
        {
            DatabaseService = databaseService;
        }

        public async Task<bool> Delete(TechnicalDevice TechnicalDevice)
        {
            try
            {
                using (var ctx = DatabaseService.GetContext())
                {
                    return await ctx.DeleteAsync(TechnicalDevice);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Error deleting TechnicalDevice");
                return false;
            }
        }

        public async Task<IEnumerable<TechnicalDevice>> GetAll(int idTenant)
        {
            try
            {
                using (var ctx = DatabaseService.GetContext())
                {
                    return await ctx.QueryAsync<TechnicalDevice>("SELECT * FROM [TechnicalDevice] where IdTenant=@idtenant", new { idtenant = idTenant });
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Error getting Categories");
                return null;
            }
        }

        public async Task<TechnicalDevice> Save(TechnicalDevice TechnicalDevice)
        {
            if (TechnicalDevice == null) { return null; }

            try
            {
                using (var ctx = DatabaseService.GetContext())
                {
                    if (TechnicalDevice.IdTechnicalDevice == 0)
                    {
                        var result = await ctx.InsertAsync(TechnicalDevice);
                        if (result > 0)
                        {
                            TechnicalDevice.IdTechnicalDevice = result;
                            return TechnicalDevice;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        if (await ctx.UpdateAsync(TechnicalDevice))
                        {
                            return TechnicalDevice;
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
                log.Error(ex, "Error saving TechnicalDevice");
                return null;
            }
        }
    }
}
