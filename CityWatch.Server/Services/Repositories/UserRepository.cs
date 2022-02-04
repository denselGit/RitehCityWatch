using CityWatch.Common;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CityWatch.Server.Services.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll(int idTenant);
        Task<IEnumerable<User>> GetAll();
        Task<User> Save(User user);
        Task<bool> Delete(User user);
    }

    public class UserRepository : IUserRepository
    {
        internal static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
        private IDatabaseService DatabaseService;

        public UserRepository(IDatabaseService databaseService)
        {
            DatabaseService = databaseService;
        }

        public async Task<bool> Delete(User user)
        {
            try
            {
                using(var ctx = DatabaseService.GetContext())
                {
                    return await ctx.DeleteAsync(user);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Error deleting user");
                return false;
            }
        }

        public async Task<IEnumerable<User>> GetAll(int idTenant)
        {
            try
            {
                using (var ctx = DatabaseService.GetContext())
                {
                    return await ctx.QueryAsync<User>("SELECT * FROM [User] WHERE IdTenant=@idtenant", new {  idtenant = idTenant });
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Error getting users");
                return null;
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            try
            {
                using (var ctx = DatabaseService.GetContext())
                {
                    return await ctx.QueryAsync<User>("SELECT * FROM [User]");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Error getting users");
                return null;
            }
        }

        public async Task<User> Save(User user)
        {
            if (user == null) return null;

            try
            {
                using (var ctx = DatabaseService.GetContext())
                {
                    if (user.IdUser > 0)
                    {
                        await ctx.UpdateAsync(user);
                        return user;
                    }
                    else
                    {
                        var result = await ctx.InsertAsync(user);
                        if (result > 0)
                        {
                            user.IdUser = result;
                            return user;
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
                log.Error(ex, "Error saving user");
                return null;
            }
        }
    }
}
