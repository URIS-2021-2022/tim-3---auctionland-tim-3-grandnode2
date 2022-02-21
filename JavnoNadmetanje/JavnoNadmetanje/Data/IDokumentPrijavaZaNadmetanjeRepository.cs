using JavnoNadmetanje.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Data
{
    public interface IDokumentPrijavaZaNadmetanjeRepository
    { 

        List<DokumentPrijavaZaNadmetanjeEntity> GetDokumentiPrijavaByPrijavaZaNadmetanjeId(Guid prijavaZaNadmetanjeId);

        DokumentPrijavaZaNadmetanjeEntity GetDokumentPrijavaById(Guid prijavaZaNadmetanjeId, Guid dokumentId);

        DokumentPrijavaZaNadmetanjeEntity CreateDokumentPrijava(DokumentPrijavaZaNadmetanjeEntity dokumentPrijava);

        void UpdateDokumentPrijava(DokumentPrijavaZaNadmetanjeEntity dokumentPrijava);

        void DeleteDokumentPrijavaByPrijavaZaNadmetanjeId(Guid prijavaZaNadmetanjeId);

        bool SaveChanges();
    }
}
