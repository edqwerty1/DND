using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DND.Models
{
    public class CreateSpellForm
    {
        public string Name { get; set; }

        public int Level { get; set; }

        public int Class1 { get; set; }
        public int Class2 { get; set; }
        public int Class3 { get; set; }

        public int School { get; set; }

        public string CastingTime { get; set; }

        public string Range { get; set; }

        public bool Ritual { get; set; }

        public bool Somatic { get; set; }

        public bool Verbal { get; set; }

        public bool Material { get; set; }

        public string MaterialDescription { get; set; }

        public string Duration { get; set; }

        public string Description { get; set; }

        public string Source { get; set; }
    }
}