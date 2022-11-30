﻿using Microsoft.EntityFrameworkCore;
using Phoenix.Models;

namespace Phoenix.Data
{
    public class PhoenixContext : DbContext
    {
        public PhoenixContext (DbContextOptions<PhoenixContext> options)
            : base(options)
        {
        }

        public DbSet<Phoenix.Domains.Status> Status { get; set; }

        public DbSet<Phoenix.Models.Country> Country { get; set; }

        public DbSet<Phoenix.Models.State> State { get; set; }

        public DbSet<Phoenix.Models.City> City { get; set; }

        public DbSet<Phoenix.Models.User> User { get; set; }

        public DbSet<Phoenix.Models.Person> Person { get; set; }

        public DbSet<Phoenix.Models.Bank> Bank { get; set; }

    }

}
