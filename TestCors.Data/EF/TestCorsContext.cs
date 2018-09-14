using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TestCors.Common.Settings;
using TestCors.Data.Configurations;
using TestCors.Models;

namespace TestCors.Data.EF
{
    public class TestCorsContext : DbContext
    {
        private readonly DatabaseSettings _settings;
        public DbSet<Phone> Phones { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }

        public TestCorsContext(IOptions<DatabaseSettings> settings) : base(
            new DbContextOptionsBuilder<TestCorsContext>().UseSqlServer(settings.Value.CONNECTION_STRING).Options)
        {
            _settings = settings.Value;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .ApplyConfiguration(new PhoneConfiguration(_settings))
                .ApplyConfiguration(new LogConfiguration(_settings));
        }

    }
}
