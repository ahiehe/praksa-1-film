using Microsoft.EntityFrameworkCore;
using praktika1.Data;

namespace Filmoteka.API.Services.Email
{

    public class RezervacijaReminderService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<RezervacijaReminderService> _logger;

        public RezervacijaReminderService(IServiceProvider serviceProvider, ILogger<RezervacijaReminderService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await SendReminders(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Greška u SendReminders.");
                }

                var nextRun = DateTime.Today.AddDays(1).AddHours(8);
                var delay = nextRun - DateTime.Now;
                try
                {
                    await Task.Delay(delay, stoppingToken);
                }
                catch (OperationCanceledException) { }
            }
        }

        private async Task SendReminders(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<MyAppContext>();
            var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

            var sutra = DateTime.Today.AddDays(1);
            var prekosutra = DateTime.Today.AddDays(2);

            var rezervacije = await context.Rezervacije
                .Include(r => r.User)
                .Include(r => r.Termin).ThenInclude(t => t.Film)
                .Include(r => r.Termin).ThenInclude(t => t.Sala)
                .Where(r => r.Termin.PocetakProjekcije >= sutra && r.Termin.PocetakProjekcije < prekosutra)
                .ToListAsync(stoppingToken);

            foreach (var rezervacija in rezervacije)
            {
                try
                {
                    var subject = $"Podsetnik: {rezervacija.Termin.Film.Naziv} sutra!";
                    var body = $@"
                    <h2>Podsetnik za projekciju</h2>
                    <p>Poštovani {rezervacija.User.Username},</p>
                    <p>Podsećamo vas da ste rezervisali mesto za sledeću projekciju:</p>
                    <ul>
                        <li><strong>Film:</strong> {rezervacija.Termin.Film.Naziv}</li>
                        <li><strong>Datum i vreme:</strong> {rezervacija.Termin.PocetakProjekcije:dd.MM.yyyy HH:mm}</li>
                        <li><strong>Sala:</strong> {rezervacija.Termin.Sala.Naziv}</li>
                    </ul>
                    <p>Vidimo se!</p>
                ";

                    await emailService.SendAsync(rezervacija.User.Email, subject, body);
                    _logger.LogInformation($"Email poslat: {rezervacija.User.Email} za termin {rezervacija.Termin.Id}");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Greška pri slanju emaila: {ex.Message}");
                }
                
            }
        }
    }
}
