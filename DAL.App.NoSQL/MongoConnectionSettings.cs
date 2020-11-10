namespace DAL.App.NoSQL
{
    public class MongoConnectionSettings : INoSqlConnectionSettings
    {
        public string CollectionName { get; set; } = default!;
        public string ConnectionString { get; set; } = default!;
        public string DatabaseName { get; set; } = default!;
    }
}