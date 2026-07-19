namespace EmployeeManagement.Application.DTOs;

public record EmployeeDto(int Id, string Name, string Email, string Department, bool IsActive, DateTime CreatedAt);
public record CreateEmployeeRequest(string Name, string Email, string Department, bool IsActive);
public record UpdateEmployeeRequest(string Name, string Email, string Department, bool IsActive);
