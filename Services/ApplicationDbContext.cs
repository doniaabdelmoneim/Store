﻿using Microsoft.EntityFrameworkCore;
using Store.Models;

namespace Store.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options)
        {
        }
         public DbSet<Contact> Contacts { get; set; }
         public DbSet<Product> Products { get; set; }

         public DbSet<Subject> Subjects { get; set; }


    }
}
