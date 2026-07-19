namespace EmployeeManagement.Infrastructure.Email;

public class SmtpSettings
{
    public const string SectionName = "Smtp";
    public string Host { get; init; } = string.Empty;
    public int Port { get; init; } = 587;
    public string UserName { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string From { get; init; } = string.Empty;
    public string To { get; init; } = string.Empty;
    public bool UseSsl { get; init; } = true;
}
