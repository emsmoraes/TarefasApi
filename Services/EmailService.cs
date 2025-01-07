using MailKit.Net.Smtp;
using MimeKit;

namespace TarefasApi.Services;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
}
public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var emailSettings = _configuration.GetSection("MailSettings");

        var email = new MimeMessage();
        email.From.Add(new MailboxAddress("Admin", emailSettings["From"]));
        email.To.Add(new MailboxAddress("", to));
        email.Subject = subject;

        var bodyBuilder = new BodyBuilder { HtmlBody = body };
        email.Body = bodyBuilder.ToMessageBody();

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(emailSettings["Host"], int.Parse(emailSettings["Port"]), false);
        await smtp.AuthenticateAsync(emailSettings["UserName"], emailSettings["Password"]);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}