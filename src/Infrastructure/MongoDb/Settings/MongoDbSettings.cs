namespace MongoDb.Settings
{
    public class MongoDbSettings
    {
        /// <summary>
        /// ConnectionString base for concat.
        /// {0} = username
        /// {1} = password
        /// {2} = cluster
        /// </summary>
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }
}