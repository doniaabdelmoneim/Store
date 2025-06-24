using Microsoft.AspNetCore.Mvc;
using Store.DTOs;
using Store.Models;
using Store.Services;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        // subject options for dropdown list 
        private readonly List<string> ListSubjects = new List<string>()
        {
            "Order Status",
            "Refund Request",
            "Feedback",
            "Technical Support",
            "Sales Inquiry",
            "Other"
        };

        public ContactController(ApplicationDbContext context)
        {
            this.context = context;

        }
        // GET: api/contact/subjects
        [HttpGet("subjects")]
        public IActionResult GetSubjects()
        {
            return Ok(ListSubjects);
        }

        [HttpGet]
        public IActionResult GetContacts()
        {
            var contacts = context.Contacts.ToList();
            return Ok(contacts);
        }
        [HttpGet("{id}")]
        public IActionResult GetContact(int id)
        {
            var contact = context.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }
        [HttpPost]
        public IActionResult CreateContact (ContactDto contactDto)
        {
            if (!ListSubjects.Contains(contactDto.Subject))
            {
                ModelState.AddModelError("Subject", "Invalid subject selected.");
                return BadRequest(ModelState);
            }
       
            var contact = new Contact
            {
                FirstName = contactDto.FirstName,
                LastName = contactDto.LastName,
                Email = contactDto.Email,
                PhoneNumber = contactDto.PhoneNumber ?? "",
                Subject = contactDto.Subject,
                Message = contactDto.Message,
                CreatedAt = DateTime.Now
            };
            context.Contacts.Add(contact);
            context.SaveChanges();
            return Ok(contact);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateContact(int id, ContactDto contactDto)
        {
            if (!ListSubjects.Contains(contactDto.Subject))
            {
                ModelState.AddModelError("Subject", "Invalid subject selected.");
                return BadRequest(ModelState);
            }
            var contact = context.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound();
            }
            contact.FirstName = contactDto.FirstName;
            contact.LastName = contactDto.LastName;
            contact.Email = contactDto.Email;
            contact.PhoneNumber = contactDto.PhoneNumber ?? "";
            contact.Subject = contactDto.Subject;
            contact.Message = contactDto.Message;
            context.SaveChanges();
            return Ok(contact);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteContact(int id)
        {
            try
            {
                var contact = new Contact() { Id = id };
                context.Contacts.Remove(contact);
                context.SaveChanges();

            }
            catch (Exception )
            {
                NotFound();
            }
        
            return NoContent();
        }


    }
}
