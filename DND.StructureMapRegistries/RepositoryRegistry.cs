using DND.Domain.Abstract;
using DND.Domain.Concrete.EntityFramework;
using StructureMap.Configuration.DSL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND.StructureMapRegistries
{
    /// <summary>
    /// A StructureMap registry that is used to configure the named and default mappings between the IRepository
    /// interface and the appropriate repository classes.
    /// </summary>
    public class RepositoryRegistry : Registry
    {
        /// <summary>
        /// Constructor to create the registry
        /// </summary>
        public RepositoryRegistry()
        {
            For(typeof(IRepository<>)).Use(typeof(EFRepository<>));

        }
    }
}
