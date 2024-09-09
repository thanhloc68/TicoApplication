﻿using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
        public DbSet<Accounts> Account { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<StruckInfo> StruckInfo { get; set; }
        public DbSet<StruckScales> StruckScale { get; set; }
        public DbSet<TankStrucks> TankStruck { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Product> Product { get; set; }
    }
}
