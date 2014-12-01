using DND.Domain.Abstract;
using DND.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND.Services.Implementation
{
    public class SchoolService : ISchoolService
    {
        private readonly IRepository<School> schools;
        public SchoolService(IRepository<School> schools)
        {
            this.schools = schools;
        }

        public School GetSchool(string className)
        {
            return schools.First(t => t.Name.Equals(className, StringComparison.OrdinalIgnoreCase));

        }

        public School GetSchool(int id)
        {
            return schools.First(t => t.Id == id);
        }

        public IQueryable<School> GetAllSchools()
        {
            return schools.GetAll(); ;
        }
    }
}
