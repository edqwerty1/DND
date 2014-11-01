using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND.Domain.Entities
{
    public class Spell : BaseEntity
    {
        public string Name { get; set; }

        public int Level { get; set; }

        public virtual ICollection<Class> Classes { get; set; }

        public virtual School School { get; set; }

        public string CastingTime { get; set; }

        public string Range { get; set; }

        public string Components { get; set; }

        public string Duration { get; set; }

        public string Description { get; set; }

        public string Source { get; set; }
    }

    //
}
