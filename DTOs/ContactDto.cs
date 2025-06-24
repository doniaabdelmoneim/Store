using System.ComponentModel.DataAnnotations;

namespace Store.DTOs
{
    public class ContactDto
    {
        public string FirstName { get; set; } = "";
        [MaxLength(100)]

        public string LastName { get; set; } = "";
        [EmailAddress]
        public string Email { get; set; } = ""!;
        [MaxLength(15)]
        public string? PhoneNumber { get; set; }
        public int SubjectId { get; set; } 
        [MaxLength(500)]
        public string Message { get; set; } = "";
    }
}
