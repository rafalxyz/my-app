using Microsoft.Extensions.Logging;

namespace MyApp.Modules.Shared.Emails;

internal class EmailSender : IEmailSender
{
    private readonly ILogger<EmailSender> _logger;

    public EmailSender(ILogger<EmailSender> logger)
    {
        _logger = logger;
    }

    public void Send(Email email)
    {
        _logger.LogInformation("Sending email to: {Recipient}.", email.Recipient);
    }
}
