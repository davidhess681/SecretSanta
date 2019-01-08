using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SSCF.Models.Entities
{
    public class Family
    {
        [Key]
        public int FamID { get; set; }

        public string FamName { get; set; }

        public virtual List<RaffleDetails> RaffleDetails { get; set; }
    }
}