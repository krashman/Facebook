namespace Facebook.Domain
{
    public class ApplicationSettings
    {
        public string DocumentDatabaseName { get; set; }
        public string DocumentDatabaseAuthorizationKey { get; set; }
        public string DocumentDatabaseEndpoint { get; set; }
        public string Collection { get; set; }
        public string StorageConnectionString { get; set; }
    }
}
