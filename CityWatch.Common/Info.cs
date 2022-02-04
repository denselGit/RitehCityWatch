using System;

namespace CityWatch.Common
{
    public class Info
    {
        public static string GetVersion()
        {
            return typeof(CityWatch.Common.Info).Assembly.GetName().Version.ToString();
        }
    }
}
