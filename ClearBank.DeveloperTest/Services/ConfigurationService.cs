using System.Configuration;

namespace ClearBank.DeveloperTest.Services
{
    public class ConfigurationService : IConfigurationService
    {
        public string GetConfiguration(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}