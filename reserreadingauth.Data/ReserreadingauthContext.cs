using System;
using Microsoft.EntityFrameworkCore;
using reserreadingauth.common;

namespace reserreadingauth.Data
{
    public class ReserreadingauthContext : DbContext
    {
        public ReserreadingauthContext(DbContextOptions<ReserreadingauthContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        
    }
}