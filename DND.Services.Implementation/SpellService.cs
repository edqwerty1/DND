using DND.Domain.Abstract;
using DND.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace DND.Services.Implementation
{
    public class SpellService : ISpellService
    {
        private readonly IRepository<Spell> spells;
        private readonly IClassService classService;
        private readonly ISchoolService schoolService;
        
        public SpellService(IRepository<Spell> spells, IClassService classService, ISchoolService schoolService)
        {
            this.spells = spells;
            this.classService = classService;
            this.schoolService = schoolService;
        }
        public IQueryable<Spell> GetAllSpells()
        {
            return spells.GetAll().Include(t => t.School);
        }

        public void InitialiseData()
        {

        }

        //Spell Name: Acid Splash
        //Spell Level: 0 (Cantrip)
        //Spell School: Conjuration
        //Classes: Sorcerer, Wizard
        //Casting Time: 1 Action
        //Range: 60 feet
        //Components: Verbal, Somatic
        //Duration: Instantaneous
        //Description: You hurl a bubble of acid. Choose one creature within range, or choose two creatures within range that are within 5 feet o f each other. A target must succeed on a Dexterity saving throw or take 1d6 acid damage.
        //This spell’s damage increases by 1d6 when you reach 5th level (2d6), 11th level (3d6), and 17th level (4d6).

        public string CreateSpell(string spellText, out Spell spell)
        {
            spell = null;
            string response;
            string spellName = null;

            var lines = spellText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var myString in lines)
            {
                if (myString.Trim().StartsWith("Spell Name:", StringComparison.OrdinalIgnoreCase))
                {
                    spellName = myString.Substring(12);
                }
            }

            if (String.IsNullOrEmpty(spellName))
            {
                response = "Spell Name not provided";
                return response;
            }
            spell = spells.First(t => t.Name.Equals(spellName, StringComparison.OrdinalIgnoreCase));

            response = spell == null ? "Spell Created" : "Spell Updated";

            if  (spell == null)
            {
                spell = new Spell { Name = spellName, Classes = new HashSet<Class>() };
            }

            string x;
            foreach (var myString in lines)
            {
                myString.Trim();
                if (myString.StartsWith("Spell Level:", StringComparison.OrdinalIgnoreCase))
                {
                    x = myString.Substring(13, 1);
                    int y;
                    int.TryParse(x, out y);

                    spell.Level = y;
                }

                if (myString.StartsWith("Spell School:", StringComparison.OrdinalIgnoreCase))
                {
                    x = myString.Substring(14);
                    spell.School = schoolService.GetSchool(x);
                }

                if (myString.StartsWith("Classes:", StringComparison.OrdinalIgnoreCase))
                {
                    x = myString.Substring(9);
                    foreach(var className in x.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        spell.Classes.Add(classService.GetClass(className.Trim()));
                    }
                    
                    spell.School = schoolService.GetSchool(x);
                }

                if (myString.StartsWith("Casting Time:", StringComparison.OrdinalIgnoreCase))
                {
                    x = myString.Substring(14);
                    spell.CastingTime = x.Trim();
                }

                if (myString.StartsWith("Range:", StringComparison.OrdinalIgnoreCase))
                {
                    x = myString.Substring(7);
                    spell.Range = x.Trim();
                }

                if (myString.StartsWith("Components:", StringComparison.OrdinalIgnoreCase))
                {
                    x = myString.Substring(12);
                    spell.Components = x.Trim();
                }


                if (myString.StartsWith("Duration:", StringComparison.OrdinalIgnoreCase))
                {
                    x = myString.Substring(10);
                    spell.Duration = x.Trim();
                }

                if (myString.StartsWith("Description:", StringComparison.OrdinalIgnoreCase))
                {
                    x = myString.Substring(13);
                    spell.Description = x.Trim();
                }
                x = string.Empty;
            }
            spells.Add(spell);
            UnitOfWork.Commit();

            return response;

        }

        public bool SaveSpell(Spell spell)
        {
            try
            {
                
                UnitOfWork.Commit();

            }
            catch
            {

                return false;
            }
            return true;
        }


        public IQueryable<Spell> FindSpells(string level, int classId, string spellName)
        {
            int levelInt;
            if (level == "Cantrip")
                levelInt = 0;
            else
            {
            int.TryParse(level, out levelInt);
            }
            


            return spells.Search(t => (level == "All" || t.Level == levelInt) 
                && (classId == 0 || t.Classes.Any(c => c.Id == classId) )
                && (String.IsNullOrEmpty(spellName) || t.Name.Contains(spellName)));
        }

        public bool AddSpell(Spell spell)
        {
            spells.Add(spell);

            UnitOfWork.Commit();

            return true; // so useful
        }

    }
}
