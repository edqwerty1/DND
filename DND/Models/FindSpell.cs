using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DND.Models
{
    public class FindSpell
    {

        [Display(Name = "Select a spell level")]
        public string Level { get; set; }

        public int ClassId { get; set; }

        public string SpellName { get; set; }
    }
}