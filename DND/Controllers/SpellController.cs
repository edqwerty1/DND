using DND.Domain.Concrete.EntityFramework;
using DND.Domain.Entities;
using DND.Models;
using DND.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DND.Controllers
{
    public class SpellController : Controller
    {
        private readonly ISpellService spellService;
        private readonly IClassService classService;
        private readonly ISchoolService schoolService;
        private InitialiseDatabase test;
        public SpellController(ISpellService spellService, InitialiseDatabase test, IClassService classService, 
                                    ISchoolService schoolService)
        {
            this.spellService = spellService;
            this.classService = classService;
            this.schoolService = schoolService;
            this.test = test;   
        }

        public SpellController()
        {}

        public static IEnumerable<Class> classes;

        public ActionResult Index()
        {
            var tempClasses = classService.GetAllClasses().ToList();
            tempClasses.Add(new Class { Id = 0, Name = "All" });
            classes = tempClasses.OrderBy(t => t.Name).AsEnumerable();

            ViewBag.classes = new SelectList(classes, "Id", "Name");

          //  var spells = spellService.GetAllSpells().ToList();
            return View(new FindSpell());
        }

        public ActionResult Initialise()
        {
            test.Initialise();
            return RedirectToAction("Index");
        }

        public ActionResult CreateSpell()
        {
            return View();
        }

        [HttpPost]
        public PartialViewResult CreateSpell(string spellText)
        {
            Spell spell; 
            string response = spellService.CreateSpell(spellText, out spell);
            return PartialView("CreateSpellPartial", spell);
        }

                public static IEnumerable<School> schools;
                public static IEnumerable<Class> classes2;

        public ActionResult CreateSpellForm()
        {
            var tempClasses = classService.GetAllClasses().ToList();
            tempClasses.Add(new Class { Id = 0, Name = "All" });
            classes = tempClasses.OrderBy(t => t.Name).AsEnumerable();
            ViewBag.classes = new SelectList(classes, "Id", "Name");

            // 1337 hackers name things 2
            classes2 = classService.GetAllClasses().OrderBy(t => t.Name);

            ViewBag.classes2 = new SelectList(classes2, "Id", "Name");

            schools = schoolService.GetAllSchools().OrderBy(t => t.Name);

            ViewBag.schools = new SelectList(schools, "Id", "Name");

            return View();
        }
        [HttpPost]
        public ActionResult CreateSpellForm(CreateSpellForm spell)
        {
            Spell newSpell = new Spell
                {
                    Name = spell.Name,
                    CastingTime = spell.CastingTime,
                    Ritual = spell.Ritual,
                    Verbal = spell.Verbal,
                    Material = spell.Material,
                    MaterialDescription = spell.MaterialDescription,
                    Somatic = spell.Somatic,
                    Description = spell.Description,
                    Duration = spell.Duration,
                    Level = spell.Level,
                    Range = spell.Range,
                    Source = spell.Source

                };
            newSpell.School = schoolService.GetSchool(spell.School);

            newSpell.Classes = new HashSet<Class>();

            if (spell.Class1 != 0)
            {
                newSpell.Classes.Add(classService.GetClass(spell.Class1));
            }

            if (spell.Class2 != 0)
            {
                newSpell.Classes.Add(classService.GetClass(spell.Class2));
            }

            if (spell.Class3 != 0)
            {
                newSpell.Classes.Add(classService.GetClass(spell.Class3));
            }


            spellService.AddSpell(newSpell);
            return RedirectToAction("CreateSpellForm");

        }
        [HttpPost]
        public JsonResult SaveSpell(Spell spell)
        {
            spellService.SaveSpell(spell);
            return Json(new { message = "Spell Added!" }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult DisplayCreateSpell(Spell spell)
        {

            return PartialView(spell);
        }

        [HttpPost]
        public PartialViewResult FindSpells(FindSpell viewModel)
        {
            return PartialView("SpellListPartial",spellService.FindSpells(viewModel.Level, viewModel.ClassId, viewModel.SpellName)
                .ToList());

        }

        // GET: Spell
        //public ActionResult Index(string sortOrder)
        //{
        //    ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        //    ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
        //    var students = spellService.GetAllSpells();
        //    switch (sortOrder)
        //    {
        //        //case "name_desc":
        //        //    students = students.OrderByDescending(s => s.LastName);
        //        //    break;
        //        //case "Date":
        //        //    students = students.OrderBy(s => s.EnrollmentDate);
        //        //    break;
        //        //case "date_desc":
        //        //    students = students.OrderByDescending(s => s.EnrollmentDate);
        //        //    break;
        //        //default:
        //        //    students = students.OrderBy(s => s.LastName);
        //        //    break;
        //    }
        //    return View(students.ToList());
        //}
    }
}