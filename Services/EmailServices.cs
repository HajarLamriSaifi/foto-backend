using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Threading.Tasks;

public class EmailService
{
    private readonly string apiKey;
    private const string baseUrl = "https://api.brevo.com/v3/";

    public EmailService(IConfiguration configuration)
    {
        apiKey = configuration["Brevo:ApiKey"]; // API-sleutel uit appsettings.json
    }

    public async Task SendEmailAsync(string to, string subject, string name, string surname, string eventType, string mediaType, string date, string location, string message, string senderEmail)
    {
        var client = new RestClient(baseUrl);
        var request = new RestRequest("smtp/email", Method.Post);

        request.AddHeader("api-key", apiKey);
        request.AddHeader("Content-Type", "application/json");

        // HTML-content met ingevulde formuliergegevens
        string htmlContent = $@"
        <h2>Nieuwe fotoshoot aanvraag</h2>
        <p><strong>Naam:</strong> {name} {surname}</p>
        <p><strong>Evenement:</strong> {eventType}</p>
        <p><strong>Media Type:</strong> {mediaType}</p>
        <p><strong>Datum:</strong> {date}</p>
        <p><strong>Locatie:</strong> {location}</p>
        <p><strong>Bericht:</strong> {message}</p>
        <p><strong>E-mail van klant:</strong> {senderEmail}</p>
        ";

        var body = new
        {
            sender = new { email = "saifihajar@hotmail.com" }, // Vervang door het e-mailadres van de fotograaf
            to = new[] { new { email = to } },  // Mail naar de fotograaf
            subject = subject,
            htmlContent = htmlContent
        };

        request.AddJsonBody(body);

        var response = await client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            Console.WriteLine("E-mail succesvol verzonden!");
        }
        else
        {
            Console.WriteLine($"Fout bij het verzenden van e-mail: {response.ErrorMessage}");
        }
    }
}
