using KatastarskaOpstina.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KatastarskaOpstina.Data
{
    public interface IKatastarskaOpstinaRepository
    {
        List<KatastarskaOpstinaE> GetKatastarskaOpstinas();

        KatastarskaOpstinaE GetKatastarskaOpstinaById(Guid katastarskaOpstinaId);

        KatastarskaOpstinaConfirmation CreateKatastarskaOpstina(KatastarskaOpstinaE katastarskaOpstina);

        void UpdateKatastarskaOpstina(KatastarskaOpstinaE katastarskaOpstina);

        void DeleteKatastarskaOpstina(Guid katastarskaOpstinaId);

        bool SaveChanges();
    }
}
