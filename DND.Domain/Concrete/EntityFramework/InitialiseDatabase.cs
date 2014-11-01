using DND.Domain.Abstract;
using DND.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND.Domain.Concrete.EntityFramework
{
    public class InitialiseDatabase
    {
        private IRepository<Spell> spells;
        private IRepository<Class> classes;

           private  IRepository<School> schools;
        public InitialiseDatabase(IRepository<Spell> spells, IRepository<Class> classes,
            IRepository<School> schools)
        {
            this.spells = spells;
            this.schools = schools;
            this.classes = classes;
        }

        public void Initialise()
        {
            schools.Add(new School { Name = "Conjuration" });
            schools.Add(new School { Name = "Abjuration" });
            schools.Add(new School { Name = "Transmutation" });
            schools.Add(new School { Name = "Enchantment" });
            schools.Add(new School { Name = "Divination," });
            schools.Add(new School { Name = "Necromancy" });
            schools.Add(new School { Name = "Evocation,," });
            schools.Add(new School { Name = "Illusion," });

            UnitOfWork.Commit();
            classes.Add(new Class { Name = "Bard" });
            classes.Add(new Class { Name = "Cleric" });
            classes.Add(new Class { Name = "Druid" });
            classes.Add(new Class { Name = "Paladin" });
            classes.Add(new Class { Name = "Sorcerer" });
            classes.Add(new Class { Name = "Warlock" });
            classes.Add(new Class { Name = "Wizard" });

            UnitOfWork.Commit();
            Spell spell = new Spell
            {
                Name = "Animal Friendship",
                CastingTime = "1 Action",
                Range = "30 feet",
                Components = "v, s, m, (a morsel of food)",
                Duration = "24 Hours",
                Source = "Player's Handbook - p. 212",
                Classes = new HashSet<Class>()

            };

            spell.Classes.Add(classes.First(t => t.Name.Equals("Bard", StringComparison.OrdinalIgnoreCase)));
            spell.Classes.Add(classes.First(t => t.Name.Equals("Druid", StringComparison.OrdinalIgnoreCase)));
            spell.Classes.Add(classes.First(t => t.Name.Equals("Ranger", StringComparison.OrdinalIgnoreCase)));
            spell.School = schools.First(t => t.Name.Equals("Enchantment", StringComparison.OrdinalIgnoreCase));

            spells.Add(spell);

            UnitOfWork.Commit();
        }

    }
}
