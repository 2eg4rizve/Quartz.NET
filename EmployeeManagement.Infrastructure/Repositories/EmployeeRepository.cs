using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Repositories;

public class EmployeeRepository(ApplicationDbContext context) : IEmployeeRepository
{
    public async Task<IReadOnlyList<Employee>> GetAllAsync(CancellationToken cancellationToken = default) => await context.Employees.AsNoTracking().OrderBy(x => x.Id).ToListAsync(cancellationToken);
    public Task<Employee?> GetByIdAsync(int id, CancellationToken cancellationToken = default) => context.Employees.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    public Task AddAsync(Employee employee, CancellationToken cancellationToken = default) => context.Employees.AddAsync(employee, cancellationToken).AsTask();
    public void Update(Employee employee) => context.Employees.Update(employee);
    public void Remove(Employee employee) => context.Employees.Remove(employee);
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => context.SaveChangesAsync(cancellationToken);
}
