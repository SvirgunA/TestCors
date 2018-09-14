using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestCors.Common.Settings;
using TestCors.Models;

namespace TestCors.Data.Configurations
{
    public class PhoneConfiguration : ABaseIEntityConfiguration<Phone>
    {
        private const string _tableName = @"camps";
        public PhoneConfiguration(DatabaseSettings settings) : base(settings)
        {
        }

        protected override void ConfigureEntity(EntityTypeBuilder<Phone> builder)
        {
            builder.ToTable(_tableName, _schema);
            builder.Property(c => c.Name).HasMaxLength(CommonConfigurationValues.PhoneNameMaxLength);
        }
    }
}
