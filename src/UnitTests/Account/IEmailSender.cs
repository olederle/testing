namespace UnitTests.Account;

public interface IEmailSender
{
    public void SendEmail(string to, string subject, string body);
}
