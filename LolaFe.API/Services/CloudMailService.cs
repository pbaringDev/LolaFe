namespace LolaFe.API.Services
{
    public class CloudMailService : IMailService
    {
        private string _mailTo = "sampleTo@lolafe.com";
        private string _mailFrom = "sampleFrom@lolafe.com";
        public void Send(string subject, string message)
        {
            Console.WriteLine($"Sending mail from {_mailFrom} to {_mailTo} via CloudMailService");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
        }
    }
}
