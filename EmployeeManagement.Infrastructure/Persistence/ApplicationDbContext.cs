using EmployeeManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees => Set<Employee>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var employee = modelBuilder.Entity<Employee>();
        employee.ToTable("Employees"); employee.HasKey(x => x.Id);
        employee.Property(x => x.Name).HasMaxLength(100).IsRequired();
        employee.Property(x => x.Email).HasMaxLength(256).IsRequired(); employee.HasIndex(x => x.Email).IsUnique();
        employee.Property(x => x.Department).HasMaxLength(100).IsRequired();
        employee.Property(x => x.CreatedAt).IsRequired();
        var date = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        employee.HasData(new[] {
            new Employee { Id=1, Name="Ava Rahman", Email="ava.rahman@example.com", Department="Engineering", IsActive=true, CreatedAt=date }, new Employee { Id=2, Name="Noah Islam", Email="noah.islam@example.com", Department="Sales", IsActive=true, CreatedAt=date },
            new Employee { Id=3, Name="Mia Ahmed", Email="mia.ahmed@example.com", Department="Human Resources", IsActive=false, CreatedAt=date }, new Employee { Id=4, Name="Liam Khan", Email="liam.khan@example.com", Department="Finance", IsActive=true, CreatedAt=date },
            new Employee { Id=5, Name="Emma Das", Email="emma.das@example.com", Department="Marketing", IsActive=true, CreatedAt=date }, new Employee { Id=6, Name="Ethan Roy", Email="ethan.roy@example.com", Department="Engineering", IsActive=false, CreatedAt=date },
            new Employee { Id=7, Name="Olivia Sen", Email="olivia.sen@example.com", Department="Operations", IsActive=true, CreatedAt=date }, new Employee { Id=8, Name="James Ali", Email="james.ali@example.com", Department="Sales", IsActive=true, CreatedAt=date },
            new Employee { Id=9, Name="Sophia Paul", Email="sophia.paul@example.com", Department="Finance", IsActive=false, CreatedAt=date }, new Employee { Id=10, Name="Lucas Bose", Email="lucas.bose@example.com", Department="Marketing", IsActive=true, CreatedAt=date }
        });
    }
}
