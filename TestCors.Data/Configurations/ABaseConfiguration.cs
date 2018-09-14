using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestCors.Common.Settings;

namespace TestCors.Data.Configurations
{
    public abstract class ABaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class
    {
        protected readonly string _schema;

        protected ABaseConfiguration(DatabaseSettings settings)
        {
            _schema = settings.DATABASE_SCHEMA;
        }

        public abstract void Configure(EntityTypeBuilder<TEntity> builder);
    }
}
