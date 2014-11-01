using DND.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND.Services
{
    public interface ISpellService
    {
        IQueryable<Spell> GetAllSpells();

        string CreateSpell(string spellText, out Spell spell);

        bool SaveSpell(Spell spell);


    }
}
