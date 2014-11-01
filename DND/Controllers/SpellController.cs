using DND.Domain.Concrete.EntityFramework;
using DND.Domain.Entities;
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
        private InitialiseDatabase test;
        public SpellController(ISpellService spellService, InitialiseDatabase test)
        {
            this.spellService = spellService;
            this.test = test;   
        }

        public SpellController()
        {}

        public ActionResult Index()
        {
            //test.Initialise();
            var spells = spellService.GetAllSpells();
            return View(spells);
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