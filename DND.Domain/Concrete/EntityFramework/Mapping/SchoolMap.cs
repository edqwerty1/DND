using DND.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND.Domain.Concrete.EntityFramework.Mapping
{
    class SchoolMap : BaseEntityMap<School>
    {
        public SchoolMap()
        {
            this.ToTable("Schools");
        }
    }
}
