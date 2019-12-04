using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KatlaSport.DataAccess.EmployeeInfo
{
    public class EmployeeCVConfiguration : EntityTypeConfiguration<EmployeeCV>
    {
        public EmployeeCVConfiguration()
        {
            ToTable("employee_cv");
            HasKey(i => i.Id);
            Property(i => i.Id).HasColumnName("employee_cv_id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(i => i.File).HasColumnName("file").IsRequired();
            Property(i => i.Name).HasColumnName("file_name").IsRequired();
            Property(i => i.LastUpdate).HasColumnName("updated_utc").IsRequired();
        }
    }
}
