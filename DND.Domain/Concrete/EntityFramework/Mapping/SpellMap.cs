using DND.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND.Domain.Concrete.EntityFramework.Mapping
{
    class SpellMap : BaseEntityMap<Spell>
    {
        public SpellMap()
        {
            this.ToTable("Spells");
            this.HasMany(t => t.Classes).WithMany();
            this.HasRequired(t => t.School);
        }
    }
}
