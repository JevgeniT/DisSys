namespace DAL.App.NoSQL
{
    public class MongoConnectionSettings : INoSqlConnectionSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}