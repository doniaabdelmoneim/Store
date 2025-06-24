using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class Contact
    {

        public int Id { get; set; }
        [MaxLength(100)]
        public string FirstName { get; set; }="";
        [MaxLength(100)]

        public string LastName { get; set; } ="";
        [EmailAddress]
        public string Email { get; set; } = ""!;
        [MaxLength(15)]
        public string PhoneNumber { get; set; } = "";
        [MaxLength(200)]
        public string Subject { get; set; } = "";
        [MaxLength(500)]
        public string Message { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.Now;




    }
}
