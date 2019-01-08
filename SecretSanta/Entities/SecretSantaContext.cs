using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SSCF.Models.Entities
{
    public class SecretSantaContext : DbContext
    {
        public virtual DbSet<Family> Families { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<RaffleDetails> RaffleDetails { get; set; }

    }
}