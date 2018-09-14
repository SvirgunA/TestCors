using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestCors.Common.Settings;
using TestCors.Models;

namespace TestCors.Data.Configurations
{
    public class LogConfiguration : ABaseIEntityConfiguration<LogEntry>
    {
        private const string _tableName = @"logs";

        public LogConfiguration(DatabaseSettings settings): base(settings)
        {

        }
        protected override void ConfigureEntity(EntityTypeBuilder<LogEntry> builder)
        {
            builder.ToTable(_tableName, _schema);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Severity).HasMaxLength(20);
        }
    }
}
