using DND.Domain.Abstract;
using DND.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DND.Domain.Concrete.EntityFramework
{
    /// <summary>
    /// Base database context from which all other database contexts will be extended
    /// </summary>
    public abstract class BaseContext : DbContext, IDbContext
    {
        /// <summary>
        /// Static constructor to create an empty context
        /// </summary>
        static BaseContext()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<BaseContext>());
        }

        /// <summary>
        /// Constructor creating a database context from a given connection string
        /// </summary>
        /// <param name="nameOrConnectionString">Connection string</param>
        protected BaseContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<BaseContext>());
            this.Configuration.LazyLoadingEnabled = true;
        }

        /// <summary>
        /// Constructor creating a database context from a given database connection
        /// </summary>
        /// <param name="dbConnection">Database connection</param>
        protected BaseContext(DbConnection dbConnection)
            : base(dbConnection, true)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<BaseContext>());
            this.Configuration.LazyLoadingEnabled = true;
        }

        /// <summary>
        /// Name space for context
        /// </summary>
        /// <returns>Name space</returns>
        protected string NamespaceDataContext { get; set; }

        /// <summary>
        /// Schema for the context
        /// </summary>
        /// <returns>Schema</returns>
        protected string Schema { get; set; }

        /// <summary>
        /// Model creating event
        /// </summary>
        /// <param name="modelBuilder">Model builder object being used</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // TODO: Handle management domain model

            //Get subclasses of BaseEntity that are in the same assembly
            var modelClasses = from type in Assembly.GetAssembly(typeof(BaseEntity)).GetTypes()
                               where !String.IsNullOrEmpty(type.Namespace) &&
                               !type.IsAbstract &&
                               !type.ContainsGenericParameters &&
                               type.IsSubclassOf(typeof(BaseEntity))
                               select type;

            // Search for any concrete EntityTypeConfiguration classes and add them to 
            // the model builder configuration.        
            var configurationClasses = from type in Assembly.GetExecutingAssembly().GetTypes()
                                       where !String.IsNullOrEmpty(type.Namespace) &&
                                       !type.ContainsGenericParameters &&
                                       type.IsSubclassOfGeneric(typeof(EntityTypeConfiguration<>))
                                       select type;

            foreach (var type in configurationClasses)
            {
                dynamic instance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(instance);
            }

            // If there are any base entity types without a custom EntityTypeConfiguration class,
            // create an instance of the BaseEntityMap class for that type and add it in to the
            // model builder configuration.
            var standardConfigurations = modelClasses.Where(m =>
               !configurationClasses.Select(c => c.GetBaseGenericArguements(typeof(BaseEntityMap<>)).FirstOrDefault()).Contains(m));

            foreach (var type in standardConfigurations)
            {
                dynamic instance = Activator.CreateInstance(typeof(BaseEntityMap<>).MakeGenericType(type));
                modelBuilder.Configurations.Add(instance);
            }

            //If the schema has been defined, set the default schema
            if (!String.IsNullOrEmpty(Schema))
                modelBuilder.HasDefaultSchema(Schema);
        }

        /// <summary>
        /// This obtains an update lock on a given row by ID, or waits until it can get one (or times out).
        /// </summary>
        /// <param name="id">Row id to lock</param>
        /// <param name="table">Table to update the lock on</param>
        /// <param name="entType">Type of the entity being locked</param>
        public void Lock(int id, string table, Type entType)
        {
            // SQL Server specific

            string query = "SELECT * FROM [" + Database.Connection.Database + "].[dbo].[" + table + "] WITH (ROWLOCK,UPDLOCK) WHERE Id = " + id.ToString();
            int temp = Database.ExecuteSqlCommand(query);
            UnitOfWork.Current.AddToLockTable(entType, id);
        }

        /// <summary>
        /// This refreshes the contents of a given object from the database.
        /// </summary>
        /// <param name="entity">Entity to refresh</param>
        public void Refresh(object entity)
        {
            Entry(entity).Reload();
        }

        /// <summary>
        /// Calls the DbContext SaveChanges. If it fails validation, it will be handled with our own exception
        /// </summary>
        /// <returns>Error code</returns>
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException exception)
            {
                //Handle the exception with our own
                //TODO: Message to be changed and globalised
                throw new Exception("Validation Failed", exception);
            }
        }
    }
}
