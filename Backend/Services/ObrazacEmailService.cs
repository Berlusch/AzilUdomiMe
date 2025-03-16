using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace Backend.Services
{
    /// <summary>
    /// Servis za slanje emailova putem SMTP servera.
    /// </summary>
    public class ObrazacEmailService
    {
        private readonly IConfiguration _config;

        /// <summary>
        /// Konstruktor koji inicijalizira servis za slanje emailova.
        /// </summary>
        /// <param name="config">Konfiguracija koja omogućuje dohvaćanje postavki za SMTP server.</param>
        public ObrazacEmailService(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Metoda koja šalje email na zadanu adresu.
        /// </summary>
        /// <param name="primatelj">Email adresa primatelja na koju će biti poslan email.</param>
        /// <param name="naslov">Naslov emaila koji će biti prikazan primatelju.</param>
        /// <param name="poruka">Tijelo emaila koje sadrži sadržaj koji se šalje primatelju.</param>
        /// <returns>Asinkrona operacija koja šalje email.</returns>
        /// <exception cref="SmtpException">Iznimka koja se baca ako dođe do pogreške pri slanju emaila putem SMTP servera.</exception>
        public async Task PosaljiEmail(string primatelj, string naslov, string poruka)
        {
            // Dohvati postavke iz konfiguracije
            var smtpServer = _config["EmailSettings:SmtpServer"];
            var port = int.Parse(_config["EmailSettings:Port"]!);
            var username = _config["EmailSettings:Username"];
            var password = _config["EmailSettings:Password"];
            var fromName = _config["EmailSettings:FromName"];

            // Postavljanje SMTP klijenta
            var smtpClient = new SmtpClient(smtpServer)
            {
                Port = port,
                Credentials = new NetworkCredential(username, password),
                EnableSsl = true
            };

            // Kreiraj email
            var mailMessage = new MailMessage
            {
                From = new MailAddress(username!, fromName),
                Subject = naslov,
                Body = poruka,
                IsBodyHtml = false
            };
            mailMessage.To.Add(primatelj);

            // Pošaljite email
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
