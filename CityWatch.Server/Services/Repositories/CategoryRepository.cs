using CityWatch.Common;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CityWatch.Server.Services.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll(int idTenant);
        Task<Category> Save(Category Category);
        Task<bool> Delete(Category Category);
    }
    public class CategoryRepository
    {
        internal static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        private IDatabaseService DatabaseService;
        public CategoryRepository(IDatabaseService databaseService)
        {
            DatabaseService = databaseService;
        }

        public async Task<bool> Delete(Category Category)
        {
            try
            {
                using (var ctx = DatabaseService.GetContext())
                {
                    return await ctx.DeleteAsync(Category);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Error deleting Category");
                return false;
            }
        }

        public async Task<IEnumerable<Category>> GetAll(int idTenant)
        {
            try
            {
                using (var ctx = DatabaseService.GetContext())
                {
                    return await ctx.QueryAsync<Category>("SELECT * FROM [Category] where IdTenant=@idtenant", new { idtenant = idTenant });
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Error getting Categories");
                return null;
            }
        }

        public async Task<Category> Save(Category Category)
        {
            if (Category == null) { return null; }

            try
            {
                using (var ctx = DatabaseService.GetContext())
                {
                    if (Category.IdCategory == 0)
                    {
                        var result = await ctx.InsertAsync(Category);
                        if (result > 0)
                        {
                            Category.IdCategory = result;
                            return Category;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        if (await ctx.UpdateAsync(Category))
                        {
                            return Category;
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
                log.Error(ex, "Error saving Category");
                return null;
            }
        }
    }
}
