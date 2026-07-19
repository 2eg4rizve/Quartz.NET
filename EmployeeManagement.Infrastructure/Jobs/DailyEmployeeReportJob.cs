using EmployeeManagement.Application.Interfaces;
using Quartz;

namespace EmployeeManagement.Infrastructure.Jobs;

[DisallowConcurrentExecution]
public class DailyEmployeeReportJob(IEmployeeRepository repository, IEmailService emailService) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var employees = await repository.GetAllAsync(context.CancellationToken);
        var active = employees.Count(x => x.IsActive); var inactive = employees.Count - active;
        var html = $"<html><body><h1>Daily Employee Report</h1><p>Generated: {DateTime.UtcNow:yyyy-MM-dd HH:mm} UTC</p><ul><li>Total employees: {employees.Count}</li><li>Active employees: {active}</li><li>Inactive employees: {inactive}</li></ul></body></html>";
        await emailService.SendAsync("Daily Employee Report", html, context.CancellationToken);
    }
}
