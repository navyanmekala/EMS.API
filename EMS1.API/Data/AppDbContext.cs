using System;
using EMS1.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EMS1.API.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<User> Users { get; set; }

    }
}
