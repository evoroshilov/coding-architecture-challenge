using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VideoInstagram.DataLayer.Entities;

namespace VideoInstagram.DataLayer.Context
{
    public class VideoInstagramDbContext : DbContext
    {
        private readonly string _connectionString;
        private readonly ILoggerFactory _loggerFactory;

        public VideoInstagramDbContext(string connectionString, ILoggerFactory loggerFactory)
        {
            _connectionString = connectionString;
            _loggerFactory = loggerFactory;
        }

        public VideoInstagramDbContext()
        {
            _connectionString = "Data Source=(local); Integrated Security=SSPI; Initial Catalog=VideoInstagram; MultipleActiveResultSets=true";
        }

        public DbSet<CustomerEntity> Customers { get; set; }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<VideoMetadataEntity> VideoMetadata { get; set; }

        public void OnBeforeSaving()
        {

            var entities = ChangeTracker.Entries().Where(x => x.Entity is EntityBase && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entity in entities)
            {
                ((EntityBase)entity.Entity).LastModifiedBy = "Username or Id"; //CurrentUser.Id;
                ((EntityBase)entity.Entity).LastModifiedDate = DateTimeOffset.Now;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _connectionString;
                
                optionsBuilder.UseSqlServer(connectionString);
            }

            if (_loggerFactory == null)
            {
                return;
            }

            optionsBuilder.UseLoggerFactory(_loggerFactory);

        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VideoInstagramDbContext).Assembly);
        }
    }
}
