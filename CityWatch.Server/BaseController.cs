using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;

namespace CityWatch.Server
{
    [Authorize]
    public class BaseController : ControllerBase
    {
        internal static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        JsonSerializerOptions _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        public BaseController()
        {

        }

        internal int GetTenantId()
        {
            if (User == null || User.Identity == null || !User.Identity.IsAuthenticated) return 0;

            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                string idtenatn = claimsIdentity.Claims.FirstOrDefault(x => x.Type == "IdTenant").Value;
                int IdTenant = 0;
                int.TryParse(idtenatn, out IdTenant);

                return IdTenant;
            }
            catch (Exception ex)
            {
                log.Error(ex, "Failed to get IdTenant for user " + User.Identity.Name);
                return 0;
            }
        }
    }
}
