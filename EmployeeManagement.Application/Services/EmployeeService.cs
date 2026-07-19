using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Services;

public class EmployeeService(IEmployeeRepository repository) : IEmployeeService
{
    public async Task<IReadOnlyList<EmployeeDto>> GetAllAsync(CancellationToken cancellationToken = default) =>
        (await repository.GetAllAsync(cancellationToken)).Select(Map).ToList();
    public async Task<EmployeeDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        (await repository.GetByIdAsync(id, cancellationToken)) is { } employee ? Map(employee) : null;
    public async Task<EmployeeDto> CreateAsync(CreateEmployeeRequest request, CancellationToken cancellationToken = default)
    {
        var employee = new Employee { Name = request.Name, Email = request.Email, Department = request.Department, IsActive = request.IsActive, CreatedAt = DateTime.UtcNow };
        await repository.AddAsync(employee, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        return Map(employee);
    }
    public async Task<bool> UpdateAsync(int id, UpdateEmployeeRequest request, CancellationToken cancellationToken = default)
    {
        var employee = await repository.GetByIdAsync(id, cancellationToken);
        if (employee is null) return false;
        employee.Name = request.Name; employee.Email = request.Email; employee.Department = request.Department; employee.IsActive = request.IsActive;
        repository.Update(employee); await repository.SaveChangesAsync(cancellationToken); return true;
    }
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var employee = await repository.GetByIdAsync(id, cancellationToken);
        if (employee is null) return false;
        repository.Remove(employee); await repository.SaveChangesAsync(cancellationToken); return true;
    }
    private static EmployeeDto Map(Employee e) => new(e.Id, e.Name, e.Email, e.Department, e.IsActive, e.CreatedAt);
}
