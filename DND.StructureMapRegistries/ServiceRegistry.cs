using DND.Domain.Concrete.EntityFramework;
using DND.Services;
using DND.Services.Implementation;
using StructureMap.Configuration.DSL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND.StructureMapRegistries
{
    /// <summary>
    /// A StructureMap registry that is used to configure the named and default mappings between the IBusinessTransaction
    /// interface and the appropriate business transaction classes.
    /// </summary>
    public class ServiceRegistry : Registry
    {
        /// <summary>
        /// Constructor to create the registry
        /// </summary>
        public ServiceRegistry()
        {
            For<ISpellService>().Use<SpellService>();
            For<IClassService>().Use<ClassService>();
            For<ISchoolService>().Use<SchoolService>();
            For<InitialiseDatabase>().Use<InitialiseDatabase>();
        }
    }
}
