namespace VideoInstagram.DataLayer.Context
{
    public interface IDataContextFactory
    {
        VideoInstagramDbContext GetDataContext();
    }
}
