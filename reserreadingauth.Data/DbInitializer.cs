using reserreadingauth.common;
using System;
using System.Linq;


namespace reserreadingauth.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ReserreadingauthContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Accounts.Any())
            {
                return; // DB has been seeded
            }

            var accounts = new Account[]
            {
                new Account {Id = "testID", Username = "testUsername", Email = "testEmail", Password = "testPassword"}
            };
            foreach (Account account in accounts)
            {
                context.Accounts.Add(account);
            }

            context.SaveChanges();
        }
    }
}