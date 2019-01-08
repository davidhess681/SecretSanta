using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SSCF.Models.Entities
{
    public class Person
    {
        [Key]
        public int PID { get; set; }

        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }

        public virtual List<RaffleDetails> Receiving { get; set; }
        public virtual List<RaffleDetails> Giving { get; set; }
        public virtual List<Person> CannotBuyFor { get; set; }
    }
}