using CityWatch.Common;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CityWatch.Server.Services.Repositories
{
    public interface ISubCategoryRepository
    {
        Task<IEnumerable<SubCategory>> GetAll(int idCategory);
        Task<SubCategory> Save(SubCategory SubCategory);
        Task<bool> Delete(SubCategory SubCategory);
    }
    public class SubCategoryRepository
    {
        internal static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        private IDatabaseService DatabaseService;
        public SubCategoryRepository(IDatabaseService databaseService)
        {
            DatabaseService = databaseService;
        }

        public async Task<bool> Delete(SubCategory SubCategory)
        {
            try
            {
                using (var ctx = DatabaseService.GetContext())
                {
                    return await ctx.DeleteAsync(SubCategory);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Error deleting SubCategory");
                return false;
            }
        }

        public async Task<IEnumerable<SubCategory>> GetAll(int idCategory)
        {
            try
            {
                using (var ctx = DatabaseService.GetContext())
                {
                    return await ctx.QueryAsync<SubCategory>("SELECT * FROM [SubCategory] where IdCategory=@idcategory", new { idcategory = idCategory });
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Error getting SubCategories");
                return null;
            }
        }

        public async Task<SubCategory> Save(SubCategory SubCategory)
        {
            if (SubCategory == null) { return null; }

            try
            {
                using (var ctx = DatabaseService.GetContext())
                {
                    if (SubCategory.IdSubCategory == 0)
                    {
                        var result = await ctx.InsertAsync(SubCategory);
                        if (result > 0)
                        {
                            SubCategory.IdSubCategory = result;
                            return SubCategory;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        if (await ctx.UpdateAsync(SubCategory))
                        {
                            return SubCategory;
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
                log.Error(ex, "Error saving SubCategory");
                return null;
            }
        }
    }
}
