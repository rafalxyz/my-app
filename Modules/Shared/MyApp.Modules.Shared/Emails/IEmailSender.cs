namespace MyApp.Modules.Shared.Emails;

public interface IEmailSender
{
    void Send(Email email);
}
