using AuthServer.Host.DTOs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;

namespace AuthServer.Host.BackgroundJobs
{

    // usage : https://docs.abp.io/en/abp/latest/Background-Jobs
    public class EmailSendingBackgroundJob : AsyncBackgroundJob<EmailSendingArgs>, ITransientDependency
    {
        private readonly IEmailSender _emailSender;

        public EmailSendingBackgroundJob(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public override async Task ExecuteAsync(EmailSendingArgs args)
        {
            await _emailSender.SendAsync(
                args.EmailAddress,
                args.Subject,
                args.Body
            );
        }
    }
}