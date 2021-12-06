using System;
using Microsoft.EntityFrameworkCore;
using reserreadingauth.common;

namespace reserreadingauth.Data
{
    public class ReserreadingauthContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public ReserreadingauthContext(DbContextOptions<ReserreadingauthContext> options) : base(options)
        {
        }
    }
}