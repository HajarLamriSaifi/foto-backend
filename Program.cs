var builder = WebApplication.CreateBuilder(args);

// Voeg CORS-beleid toe
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin() // Sta alle origins toe (voor development doeleinden)
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddSingleton<EmailService>();

var app = builder.Build();

// Gebruik CORS-beleid
app.UseCors("AllowAllOrigins");

app.MapControllers();

// Zorg ervoor dat de juiste poort wordt gebruikt
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080"; // Render gebruikt vaak poort 8080
app.Run($"http://0.0.0.0:{port}");


app.MapGet("/", () => "Backend is live 🚀");
