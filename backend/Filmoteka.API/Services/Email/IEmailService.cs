namespace Filmoteka.API.Services.Email
{
    public interface IEmailService
    {
        public Task SendAsync(string to, string subject, string body);
    }
}
