using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND.Domain.Concrete.EntityFramework
{
    /// <summary>
    /// Context for the main database
    /// </summary>
    public class DataContext : BaseContext
    {
        /// <summary>
        /// Static constructor to create an empty data context.  If we are building the database instruct 
        /// the system to create a database if it doesn't exst.  This cannot be used for normal usage as
        /// it is incompatible with caching
        /// </summary>
        static DataContext()
        {
            // TODO: Set the initialiser to one that checks the database version once the build 
            // database tool is added
            //   if (BaseContext.CreateDB)
            Database.SetInitializer(new CreateDatabaseIfNotExists<DataContext>());
            //else
            //   Database.SetInitializer(null as IDatabaseInitializer<DataContext>);
        }

        /// <summary>
        /// Constructor to create a data context from the given connection string,
        /// setting whether it is a management database
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <param name="managementModelOnly">Management model only</param>
        public DataContext(string connectionString)
            : base(connectionString)
        {
            base.NamespaceDataContext = "DND.Domain.Concrete.EntityFramework.Mapping";
            base.Schema = "Domain";
        }

        /// <summary>
        /// Constructor to create a data context from the stored connection string,
        /// setting whether it is a management database
        /// </summary>
        /// <param name="managementContext">Management model only</param>
        public DataContext()
            : base(ConfigurationManager.ConnectionStrings["DNDB"].ConnectionString)
        {
            base.NamespaceDataContext = "DND.Domain.Concrete.EntityFramework.Mapping";
            base.Schema = "Domain";
        }
    }
}
