using CityWatch.Common;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CityWatch.Server.Services.Repositories
{
    public interface ITicketRepository
    {
        Task<IEnumerable<Ticket>> GetAll();
        Task<Ticket> Save(Ticket Ticket);
        Task<bool> Delete(Ticket Ticket);
    }

    public class TicketRepository : ITicketRepository
    {
        internal static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        private IDatabaseService DatabaseService;
        public TicketRepository(IDatabaseService databaseService)
        {
            DatabaseService = databaseService;
        }

        public async Task<bool> Delete(Ticket Ticket)
        {
            try
            {
                using (var ctx = DatabaseService.GetContext())
                {
                    return await ctx.DeleteAsync(Ticket);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Error deleting Ticket");
                return false;
            }
        }

        public async Task<IEnumerable<Ticket>> GetAll()
        {
            try
            {
                using (var ctx = DatabaseService.GetContext())
                {
                    return await ctx.QueryAsync<Ticket>("SELECT * FROM [Ticket]");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Error getting Tickets");
                return null;
            }
        }

        public async Task<Ticket> Save(Ticket Ticket)
        {
            if (Ticket == null) { return null; }

            try
            {
                using (var ctx = DatabaseService.GetContext())
                {
                    if (Ticket.IdTicket == 0)
                    {
                        var result = await ctx.InsertAsync(Ticket);
                        if (result > 0)
                        {
                            Ticket.IdTicket = result;
                            return Ticket;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        if (await ctx.UpdateAsync(Ticket))
                        {
                            return Ticket;
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
                log.Error(ex, "Error saving Ticket");
                return null;
            }
        }
    }
}
