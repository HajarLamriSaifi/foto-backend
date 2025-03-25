namespace foto_backend.Models
{
    public class FormData
    {
        public string? Name { get; set; }  // Voornaam in de frontend
        public string? Surname { get; set; }  // Achternaam in de frontend
        public string? EventType { get; set; }
        public string? MediaType { get; set; }
        public string? Date { get; set; }


        public string? Location { get; set; }
        public string? Message { get; set; }
        public string? Email { get; set; }
    }
}
