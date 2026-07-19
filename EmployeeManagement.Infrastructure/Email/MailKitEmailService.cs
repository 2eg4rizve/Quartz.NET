using EmployeeManagement.Application.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace EmployeeManagement.Infrastructure.Email;

public class MailKitEmailService(IOptions<SmtpSettings> options) : IEmailService
{
    public async Task SendAsync(string subject, string htmlBody, CancellationToken cancellationToken = default)
    {
        var settings = options.Value;
        var message = new MimeMessage(); message.From.Add(MailboxAddress.Parse(settings.From)); message.To.Add(MailboxAddress.Parse(settings.To)); message.Subject = subject;
        message.Body = new BodyBuilder { HtmlBody = htmlBody }.ToMessageBody();
        using var client = new SmtpClient();
        await client.ConnectAsync(settings.Host, settings.Port, settings.UseSsl, cancellationToken);
        await client.AuthenticateAsync(settings.UserName, settings.Password, cancellationToken);
        await client.SendAsync(message, cancellationToken); await client.DisconnectAsync(true, cancellationToken);
    }
}
