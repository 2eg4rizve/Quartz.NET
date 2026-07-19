using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Infrastructure.Email;
using EmployeeManagement.Infrastructure.Jobs;
using EmployeeManagement.Infrastructure.Persistence;
using EmployeeManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Quartz;

namespace EmployeeManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        
        var smtpSection = configuration.GetSection(SmtpSettings.SectionName);
        services.Configure<SmtpSettings>(options => smtpSection.Bind(options));
        
        services.AddScoped<IEmployeeRepository, EmployeeRepository>(); services.AddScoped<IEmailService, MailKitEmailService>();
        services.AddQuartz(q => { var jobKey = new JobKey(nameof(DailyEmployeeReportJob)); q.AddJob<DailyEmployeeReportJob>(o => o.WithIdentity(jobKey)); q.AddTrigger(t => t.ForJob(jobKey).WithIdentity($"{nameof(DailyEmployeeReportJob)}-trigger").WithCronSchedule("0 0 9 * * ?", x => x.InTimeZone(TimeZoneInfo.Local))); });
        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
        return services;
    }
}
