using DND.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND.Services
{
    public interface ISchoolService
    {
        School GetSchool(string schoolName);

        IQueryable<School> GetAllSchools();
    }
}
