using DND.Domain.Concrete.EntityFramework;
using StructureMap;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace DND.Domain.Abstract
{
    public static class UnitOfWork
    {
        private const string HTTPCONTEXTKEY = "Repository.UnitOfWork.HttpContext.Key";

        private static IUnitOfWorkFactory unitOfWorkFactory;
        private static readonly Hashtable threads = new Hashtable();

        public static void Commit()
        {
            IUnitOfWork unitOfWork = GetUnitOfWork();
            if (unitOfWork != null)
            {
                unitOfWork.Commit();
            }
        }

        public static IUnitOfWork Current
        {
            get
            {
                IUnitOfWork unitOfWork = GetUnitOfWork();
                if (unitOfWork == null)
                {
                    // TODO replace with setter injection
                    //unitOfWorkFactory = ObjectFactory.GetInstance<IUnitOfWorkFactory>();
                    unitOfWorkFactory = new EFUnitOfWorkFactory();
                    unitOfWork = unitOfWorkFactory.Create();
                    SaveUnitOfWork(unitOfWork);
                }
                return unitOfWork;
            }
        }

        /// <summary>
        /// This clears up everything associated with the transaction.
        /// </summary>
        public static void Dispose()
        {
            IUnitOfWork unitOfWork = GetUnitOfWork();

            if (unitOfWork != null)
            {
                unitOfWork.Dispose();
                RemoveCurrentUnitOfWork();
            }
        }

        private static IUnitOfWork GetUnitOfWork()
        {
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Items.Contains(HTTPCONTEXTKEY))
                    return (IUnitOfWork)HttpContext.Current.Items[HTTPCONTEXTKEY];

                return null;
            }
            else
            {
                Thread thread = Thread.CurrentThread;
                if (string.IsNullOrEmpty(thread.Name))
                {
                    thread.Name = Guid.NewGuid().ToString();
                    return null;
                }
                else
                {
                    lock (threads.SyncRoot)
                    {
                        return (IUnitOfWork)threads[Thread.CurrentThread.Name];
                    }
                }
            }
        }

        private static void RemoveCurrentUnitOfWork()
        {
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Items.Contains(HTTPCONTEXTKEY))
                {
                    HttpContext.Current.Items.Remove(HTTPCONTEXTKEY);
                }
            }
            else
            {
                Thread thread = Thread.CurrentThread;
                if (string.IsNullOrEmpty(thread.Name))
                {
                    return;
                }
                else
                {
                    lock (threads.SyncRoot)
                    {
                        threads[Thread.CurrentThread.Name] = null;
                    }
                }
            }
        }

        private static void SaveUnitOfWork(IUnitOfWork unitOfWork)
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Items[HTTPCONTEXTKEY] = unitOfWork;
            }
            else
            {
                lock (threads.SyncRoot)
                {
                    threads[Thread.CurrentThread.Name] = unitOfWork;
                }
            }
        }


    }
}
