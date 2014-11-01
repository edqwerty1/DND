using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND.Domain.Concrete.EntityFramework
{
    /// <summary>
    /// Interface for database contexts
    /// </summary>
    public interface IDbContext : IDisposable
    {
     /// <summary>
      /// Save the changes to all entities within the current context
      /// </summary>
      /// <returns>Error code</returns>
      int SaveChanges();

      /// <summary>
      /// Returns a DbSet for the current entity type
      /// </summary>
      /// <typeparam name="TEntity">The entity type</typeparam>
      /// <returns>DbSet</returns>
      DbSet<TEntity> Set<TEntity>() where TEntity : class;

      /// <summary>
      /// This obtains an update lock on a given row by ID, or waits until it can get one (or times out).
      /// </summary>
      /// <param name="id">Id of entity to lock</param>
      /// <param name="table">Table to get the lock on</param>
      /// <param name="entType">Type of the entity being locked</param>
      /// <returns></returns>
      void Lock(int id, string table, Type entType);

      /// <summary>
      /// This refreshes the contents of a given object from the database.
      /// </summary>
      /// <param name="entity">Object to be refreshed</param>
      void Refresh(object entity);

      /// <summary>
      /// Gets a System.Data.Entity.Infrastructure.DbEntityEntry object for the given entity
      /// </summary>
      /// <param name="entity">Entity to use</param>
      /// <returns>DbEntityEntry</returns>
      DbEntityEntry Entry(object entity);
   }
}
