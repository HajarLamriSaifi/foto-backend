using Microsoft.AspNetCore.Mvc;
using foto_backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace foto_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormDataController : ControllerBase
    {
        private static List<FormData> formDataList = new List<FormData>();
        private readonly EmailService _emailService;

        public FormDataController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody] FormData formData)
        {
            Console.WriteLine("Ontvangen formulier data:");
            Console.WriteLine($"Naam: {formData.Name}, Achternaam: {formData.Surname}");
            Console.WriteLine($"Evenement: {formData.EventType}, Media Type: {formData.MediaType}");
            Console.WriteLine($"Datum: {formData.Date}, Locatie: {formData.Location}");
            Console.WriteLine($"Bericht: {formData.Message}");
            Console.WriteLine($"E-mail: {formData.Email}");

            formDataList.Add(formData);

            try
            {
                // ✅ E-mail naar de fotograaf
                await _emailService.SendEmailAsync(
                    "saifihajar@hotmail.com",  // Zet hier het echte e-mailadres van de fotograaf
                    "Nieuwe aanvraag ontvangen",
                    formData.Name,
                    formData.Surname,
                    formData.EventType,
                    formData.MediaType,
                    formData.Date,
                    formData.Location,
                    formData.Message,
                    formData.Email
                );
                Console.WriteLine("📩 E-mail naar fotograaf succesvol verzonden!");

                // ✅ Bevestigingsmail naar de klant
                await _emailService.SendEmailAsync(
                    formData.Email,
                    "Bevestiging van je aanvraag",
                    formData.Name,
                    formData.Surname,
                    formData.EventType,
                    formData.MediaType,
                    formData.Date,
                    formData.Location,
                    "Bedankt voor je aanvraag! We nemen zo snel mogelijk contact met je op.",
                    "fotograaf@email.com" // De afzender is de fotograaf
                );
                Console.WriteLine("📩 Bevestigingsmail naar klant succesvol verzonden!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Fout bij het verzenden van e-mails: {ex.Message}");
                return StatusCode(500, "Fout bij het verzenden van de e-mail.");
            }

            return Ok(new { message = "Formulier verzonden en e-mails verstuurd!" });
        }


        [HttpGet]
        public IActionResult GetFormData()
        {
            return Ok(formDataList);
        }
    }
}
