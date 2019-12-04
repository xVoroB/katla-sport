using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KatlaSport.DataAccess.EmployeeInfo
{
    public class EmployeeConfiguration : EntityTypeConfiguration<Employee>
    {
        public EmployeeConfiguration()
        {
            ToTable("employee");
            HasKey(i => i.Id);
            HasOptional(i => i.ChiefEmployee).WithMany(i => i.Employees).HasForeignKey(i => i.ChiefEmployeeId);
            HasRequired(i => i.EmployeeCVs).WithMany(i => i.Employees).HasForeignKey(i => i.EmployeeCVId);
            HasRequired(i => i.Position).WithMany(i => i.Employees).HasForeignKey(i => i.PositionId);
            Property(i => i.Id).HasColumnName("employee_id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(i => i.Name).HasColumnName("employee_name").HasMaxLength(20).IsRequired();
            Property(i => i.DateBirth).HasColumnName("employee_birth_date").IsRequired();
            Property(i => i.PositionId).HasColumnName("employee_position_id").IsRequired();
            Property(i => i.ChiefEmployeeId).HasColumnName("chief_employee_id").IsOptional();
            Property(i => i.EmployeeCVId).HasColumnName("employee_cv_id").IsRequired();
        }
    }
}