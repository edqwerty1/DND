using DND.Domain.Abstract;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND.Domain.Concrete.EntityFramework
{
    public class EFUnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork Create()
        {
            Func<DataContext> dataContextFunc = () => new DataContext();


            return new EFUnitOfWork(dataContextFunc);
        }
    }
}
