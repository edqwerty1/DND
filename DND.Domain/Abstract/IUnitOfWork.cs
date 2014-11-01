using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND.Domain.Abstract
{
    /// <summary>
    /// Interface for the unit of work.  A group of database changes to be committed at once.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Commit the unit of work
        /// </summary>
        void Commit();

        /// <summary>
        /// Check of the required entity is locked and has an entry in the lock table
        /// </summary>
        /// <param name="entity">Type of the entity being checked</param>
        /// <param name="i">Id of the entity being checked</param>
        /// <returns>Whether the lock table contains the lock key for the given entity</returns>
        bool ContainsLockKey(Type entity, int i);

        /// <summary>
        /// Add the given entity to the lock table
        /// </summary>
        /// <param name="entity">Entity to add to the lock table</param>
        /// <param name="i">Id of entity to add to the lock table</param>
        void AddToLockTable(Type entity, int i);
    }
}
