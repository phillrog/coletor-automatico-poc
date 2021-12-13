namespace ColetaAutomatica.API.Configuration
{
    public class AppSettingsBus
    {
        public int MessagePrefetchCount { get; set; }
        public int MessageRetryCount { get; set; }
        public int MessageRetryInterval { get; set; }
        public string Endpoint { get; set; }
        public string Environment { get; set; }
    }
}
