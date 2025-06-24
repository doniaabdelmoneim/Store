using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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



        public ContactController(ApplicationDbContext context)
        {
            this.context = context;

        }
        // GET: api/contact/subjects
        [HttpGet("subjects")]
        public IActionResult GetSubjects()
        {
            var ListSubjects = context.Subjects.ToList();
            return Ok(ListSubjects);
        }

        [HttpGet]
        public IActionResult GetContacts(int? page)
        {
            //add pagination 
            if (page == null || page < 1)
            {
                page = 1;
            }
            int pageSize = 10;
            int totalPages = 0;
            decimal totalContacts = context.Contacts.Count();
            if (totalContacts > 0)
            {
                totalPages = (int) Math.Ceiling(totalContacts / pageSize);
            }

            var contacts = context.Contacts.Include(c => c.Subject)
                .OrderByDescending(c=>c.Id)
                .Skip((int)(page!-1)*pageSize)
                .Take(pageSize) 
                .ToList();

            var response = new
            {
                Contacts = contacts,
                Page = page,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalContacts = totalContacts
            };

            return Ok(response);
        }
        [HttpGet("{id}")]
        public IActionResult GetContact(int id)
        {
            var contact = context.Contacts.Include(c=>c.Subject).FirstOrDefault(c =>c.Id== id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }
        [HttpPost]
        public IActionResult CreateContact (ContactDto contactDto)
        {
            var subject = context.Subjects.Find(contactDto.SubjectId);
            if (subject==null)
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
                Subject = subject,
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
            var subject = context.Subjects.Find(contactDto.SubjectId);

            if (subject==null)
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
            contact.Subject = subject;
            contact.Message = contactDto.Message;
            context.SaveChanges();
            return Ok(contact);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteContact(int id)
        {
            try
            {
                var contact = new Contact() { Id = id , Subject = new Subject() };
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
