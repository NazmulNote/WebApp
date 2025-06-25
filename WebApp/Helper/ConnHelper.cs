namespace WebApp.Helper
{
    public class ConnHelper
    {
        private readonly IConfiguration _configuration;
        public ConnHelper()
        {
            // Build configuration
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }
        public string GetConnString(string name)
        {
            return _configuration.GetConnectionString(name);
        }
    }
}
