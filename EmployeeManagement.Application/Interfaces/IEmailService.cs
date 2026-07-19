namespace EmployeeManagement.Application.Interfaces;

public interface IEmailService
{
    Task SendAsync(string subject, string htmlBody, CancellationToken cancellationToken = default);
}
