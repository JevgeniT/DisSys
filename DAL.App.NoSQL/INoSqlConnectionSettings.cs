using System;

namespace DAL.App.NoSQL
{
    public interface INoSqlConnectionSettings
    {
        string CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}