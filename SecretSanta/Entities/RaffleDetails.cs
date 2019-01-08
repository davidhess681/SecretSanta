using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SSCF.Models.Entities
{
    public class RaffleDetails
    {
        [Key][ForeignKey("Family")]
        public int FamID { get; set; }
        public virtual Family Family { get; set; }

        [Key][ForeignKey("Person")]
        public int PID { get; set; }
        public virtual Person Receiver { get; set; }

        [ForeignKey("Person")]
        public int? HolderID { get; set; }
        public virtual Person Giver { get; set; }

        public int? Year { get; set; }


    }
}