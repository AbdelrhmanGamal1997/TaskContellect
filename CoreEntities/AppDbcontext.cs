using System;
using CoreEntities.Enities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CoreEntities
{
    public class AppDbcontext : DbContext
    {
        public AppDbcontext(DbContextOptions<AppDbcontext> options) : base(options)
        {
           
        }

        public DbSet<User> Users { get; set; }
    }
}
