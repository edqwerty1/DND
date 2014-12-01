using DND.Domain.Abstract;
using DND.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND.Services.Implementation
{
    public class ClassService : IClassService
    {
        private readonly IRepository<Class> classes;
        public ClassService(IRepository<Class> classes)
        {
            this.classes = classes;
        }

        public Class GetClass(string className)
        {
            return classes.First(t => t.Name.Equals(className, StringComparison.OrdinalIgnoreCase));

        }

        public Class GetClass(int id)
        {
            return classes.First(t => t.Id == id);
        }

        public IQueryable<Class> GetAllClasses()
        {
            return classes.GetAll(); ;
        }
    }
}
