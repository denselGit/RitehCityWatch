using DbUp;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace CityWatch.DatabaseUpgrade
{
    internal class Program
    {
        internal static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        static string CountryCode = "";
        static bool UpgradeActive = false;
        static int UntilVersion = 0;

        static string Codes = "HR,BIH,SRB,SLO,ENG,MK,CG,BG,AL,HU";

        static bool auto = false;

        public static void Main(string[] args)
        {
            try
            {
                IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();

                string connectionString = "";
                string lang = "";
                auto = false;

                if (args.Length > 0)
                {
                    auto = args[0].Contains("q");
                    connectionString = args[2];
                    lang = args[4];
                }
                else
                {
                    connectionString = config.GetConnectionString("ConnectionString0");
                }

                var scriptsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Scripts");

                var upgrader = DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsFromFileSystem(scriptsPath, IsIncluded)
                    .LogToConsole()
                    .Build();

                if (connectionString.Trim().Length == 0)
                {
                    log.Info("ConnectionString is empty, please check arguments or appsettings.json!");
                    ApplicationEnd();
                    return;
                }

                log.Info("\r\nDatabase:\r\n" + connectionString);

                do
                {
                    if (lang != "")
                    {
                        CountryCode = lang;
                        break;
                    }

                    Console.Write("\r\nEnter country code (" + Codes + ", default HR):");
                    CountryCode = Console.ReadLine().ToUpper().Trim();

                    if (CountryCode == "") CountryCode = "HR";
                }
                while ((Codes + ",").IndexOf(CountryCode + ",") < 0);

                log.Info("Selected CountryCode: " + CountryCode);

                var listToExecute = upgrader.GetScriptsToExecute();

                if (listToExecute.Count > 0)
                {
                    if (auto)
                    {
                        log.Info("Update required using following scripts:");
                    }
                    else
                    {
                        Console.WriteLine("Update required using following scripts:");
                    }

                    foreach (var script in listToExecute)
                    {
                        log.Info(script.Name);
                    }
                }
                else
                {
                    log.Info("Upgrade not required");
                    ApplicationEnd();
                    return;
                }

                if (auto)
                {
                    upgrader.PerformUpgrade();
                }
                else
                {
                    Console.WriteLine("\r\n\r\nMENU:");
                    Console.WriteLine("---------");
                    Console.WriteLine("UP    - upgrade to latest version");
                    Console.WriteLine("UPnn  - upgrade to version nn (including)");
                    Console.WriteLine("FIX   - set upgrade status to last version");
                    Console.WriteLine("FIXnn - set upgrade status to version nn");
                    Console.Write("Your selection:");
                    var command = Console.ReadLine().Replace(" ", "").ToUpper().Trim();

                    var versionText = Regex.Replace(command, @"[^0-9]", "");
                    int.TryParse(versionText, out UntilVersion);

                    UpgradeActive = true;
                    if (command.StartsWith("UP"))
                    {
                        upgrader.PerformUpgrade();
                    }
                    else if (command.StartsWith("FIX"))
                    {
                        upgrader.MarkAsExecuted();
                    }
                    Console.WriteLine("Process done!");
                }


            }
            catch (Exception ex)
            {
                log.Error(ex.Message, "An error occured:");
            }

            ApplicationEnd();
        }

        #region IsIncluded
        private static bool IsIncluded(string filename)
        {
            var file = Path.GetFileName(filename);

            //prvo dali ima countrycode u nazivu, ako nema, onda ide uvijek
            var hasCC = false;
            foreach (var cc in Codes.Split(','))
            {
                if (file.IndexOf("." + cc + ".") > 0)
                {
                    hasCC = true;
                    break;
                }
            }

            var result = true;
            if (hasCC)
            {
                result = file.IndexOf("." + CountryCode + ".") > 0;
            }
            if (UpgradeActive && (UntilVersion > 0))
            {
                var versionText = System.Text.RegularExpressions.Regex.Replace(file.Substring(0, 10), @"[^0-9]", "");
                var version = 0;
                int.TryParse(versionText, out version);
                result = result && (version <= UntilVersion);
            }
            //if (result && !UpgradeActive) log.Info(file);
            return result;
        }
        #endregion

        #region ApplicationEnd
        private static void ApplicationEnd()
        {
            if (!auto)
            {
                Console.WriteLine("Press ENTER to quit");
                Console.ReadLine();
            }
        }
        #endregion
    }
}
