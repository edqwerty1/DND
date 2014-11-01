using DND.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND.Domain.Concrete.EntityFramework.Mapping
{
    class ClassMap : BaseEntityMap<Class>
    {
        public ClassMap()
        {
            this.ToTable("Classes");
        }
    }
}
