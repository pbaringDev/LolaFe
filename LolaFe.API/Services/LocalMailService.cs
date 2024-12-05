namespace LolaFe.API.Services
{
    public class LocalMailService : IMailService
    {
        private string _mailTo = string.Empty;
        private string _mailFrom = string.Empty;

        public LocalMailService(IConfiguration configuration)
        {
            _mailFrom = configuration["MailSetting:From"];
            _mailTo = configuration["MailSetting:To"];
        }

        public void Send(string subject, string message)
        {
            Console.WriteLine($"Sending mail from {_mailFrom} to {_mailTo} via LocalMailService");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");

        }
    }
}
