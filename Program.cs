var builder = WebApplication.CreateBuilder(args);

// Voeg CORS-beleid toe
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin() // Sta alle origin toe (voor development doeleinden)
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

app.Run();
