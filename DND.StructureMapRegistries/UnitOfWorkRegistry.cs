using DND.Domain.Abstract;
using DND.Domain.Concrete.EntityFramework;
using StructureMap.Configuration.DSL;

namespace DND.StructureMapRegistries
{
    /// <summary>
    /// A StructureMap registry that is used to configure the named and default mappings between the IBusinessTransaction
    /// interface and the appropriate business transaction classes.
    /// </summary>
    public class UnitOfWorkRegistry : Registry
    {
        /// <summary>
        /// Constructor to create the registry
        /// </summary>
        public UnitOfWorkRegistry()
        {
            For<IUnitOfWorkFactory>().Use<EFUnitOfWorkFactory>();
            For<IUnitOfWork>().Use<EFUnitOfWork>();
        }
    }
}
