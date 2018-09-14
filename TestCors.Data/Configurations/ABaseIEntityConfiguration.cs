using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestCors.Common.Settings;
using TestCors.Models;

namespace TestCors.Data.Configurations
{
    public abstract class ABaseIEntityConfiguration<TEntity> : ABaseConfiguration<TEntity> where TEntity : class, IEntity
    {
        protected ABaseIEntityConfiguration(DatabaseSettings settings) : base(settings)
        {
        }

        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(e => e.Id);

            ConfigureEntity(builder);
        }

        protected abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);
    }
}
