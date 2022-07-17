using Microsoft.Extensions.Logging;

namespace VideoInstagram.DataLayer.Context
{
    public class DataContextFactory: IDataContextFactory
    {
        private readonly ILoggerFactory _loggerFactory;

        private string connectionString =
            "Data Source=(local); Integrated Security=SSPI; Initial Catalog=VideoInstagram; MultipleActiveResultSets=true";

        public DataContextFactory(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            
        }

        public VideoInstagramDbContext GetDataContext()
        {
            return new VideoInstagramDbContext(connectionString, _loggerFactory);
        }
    }
}
