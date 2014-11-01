using DND.Domain.Concrete.EntityFramework;
using StructureMap.Configuration.DSL;
using StructureMap.Web;
namespace DND.StructureMapRegistries
{
    /// <summary>
    /// A StructureMap registry that is used to configure the named and default mappings between the IRepository
    /// interface and the appropriate repository classes.
    /// </summary>
    public class DbContextRegistry : Registry
    {
        /// <summary>
        /// Constructor to create the database context from the connection string
        /// </summary>
        /// <param name="managementOnly">Is this a management syatem</param>
        /// <param name="connectionString">Connection string</param>
        /// <param name="createDB">
        /// Are we creating a database?  This dictates if we allow the context to create a database if
        /// it is not found
        /// </param>
        public DbContextRegistry()
        {
            For<DataContext>().HybridHttpOrThreadLocalScoped().Use(o => new DataContext());

        }
    }
}
