namespace Monopoly.Shared.Configuration
{
    public class BaseConfiguration
    {
        public BaseConfiguration()
        {
            BaseUrl = string.Empty;
            Port = 0;
            Monopoly = new MonopolyConfiguration();
        }

        public int Port { get; set; }
        public string BaseUrl { get; set; }

        public MonopolyConfiguration Monopoly { get; set; }
    }
}