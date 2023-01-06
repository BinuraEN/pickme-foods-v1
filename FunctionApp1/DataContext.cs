using Microsoft.EntityFrameworkCore;
using System;


namespace FunctionApp1
{
    public class DataContext : DbContext
    {
        public DbSet<Session> Sessions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("SqlConnection"));
        }
    }
}
