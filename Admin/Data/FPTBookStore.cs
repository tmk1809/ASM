using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Admin.Models;

namespace Admin.Data
{
    public class FPTBookStore : DbContext
    {
        public FPTBookStore(DbContextOptions<FPTBookStore> options) : base(options) { }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<CategoryReq> CategoryReqs { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}

