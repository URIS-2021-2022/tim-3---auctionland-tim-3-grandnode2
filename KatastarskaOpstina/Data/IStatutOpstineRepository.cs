using KatastarskaOpstina.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KatastarskaOpstina.Data
{
    public interface IStatutOpstineRepository
    {
        List<StatutOpstineE> GetStatutOpstines();

        StatutOpstineE GetStatutOpstine(Guid statutOpstineId);

        StatutOpstineConfirmation CreateStatutOpstine(StatutOpstineE statutOpstine);

        void UpdateStatutOpstine(StatutOpstineE statutOpstine);

        void DeleteStatutOpstine(Guid statutOpstineId);

        bool SaveChanges();
    }
}
