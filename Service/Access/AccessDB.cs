using Common.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Access
{
    public class AccessDB : DbContext
    {
        public AccessDB() : base("eBeleznikDB")
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }

        public DbSet<Note> Notes { get; set; }

    }
}
