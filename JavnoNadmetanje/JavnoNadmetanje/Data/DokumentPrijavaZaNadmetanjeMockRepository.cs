using JavnoNadmetanje.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Data
{
    public class DokumentPrijavaZaNadmetanjeMockRepository : IDokumentPrijavaZaNadmetanjeRepository
    {
        public static List<DokumentPrijavaZaNadmetanjeEntity> DokumentiPrijave { get; set; } = new List<DokumentPrijavaZaNadmetanjeEntity>();

        public DokumentPrijavaZaNadmetanjeMockRepository()
        {
            FillData();
        }

        private void FillData()
        {
            DokumentiPrijave.AddRange(new List<DokumentPrijavaZaNadmetanjeEntity>
            {
                new DokumentPrijavaZaNadmetanjeEntity
                {
                    PrijavaZaNadmetanjeId = Guid.Parse("07c0c62b-675e-4714-816c-b492720194d6"),
                    DokumentId = Guid.Parse("b99d4b97-6984-43ef-b0a5-89d04569466e"),
                    DatumDonosenjaDokumenta = DateTime.Parse("09-02-2022")

                },
                new DokumentPrijavaZaNadmetanjeEntity
                {
                    PrijavaZaNadmetanjeId = Guid.Parse("1cd5c783-4bf5-4bbc-b7f0-bd66e2ba0bd7"),
                    DokumentId = Guid.Parse("a99d4b97-6984-43ef-b0a5-89d04569276e"),
                    DatumDonosenjaDokumenta = DateTime.Parse("08-02-2022")
                }
            }); 
        }

        public List<DokumentPrijavaZaNadmetanjeEntity> GetDokumentiPrijavaByPrijavaZaNadmetanjeId(Guid prijavaZaNadmetanjeId)
        {
            return DokumentiPrijave.Where(dp => (dp.PrijavaZaNadmetanjeId == prijavaZaNadmetanjeId)).ToList();
        }

        public DokumentPrijavaZaNadmetanjeEntity GetDokumentPrijavaById(Guid prijavaZaNadmetanjeId, Guid dokumentId)
        {
            return DokumentiPrijave.FirstOrDefault(dp => (dp.PrijavaZaNadmetanjeId == prijavaZaNadmetanjeId && dp.DokumentId == dokumentId));
        }

        public DokumentPrijavaZaNadmetanjeEntity CreateDokumentPrijava(DokumentPrijavaZaNadmetanjeEntity dokumentPrijava)
        {
            dokumentPrijava.DatumDonosenjaDokumenta = DateTime.Now;
            DokumentiPrijave.Add(dokumentPrijava);
            DokumentPrijavaZaNadmetanjeEntity dokumentPrijava1 = GetDokumentPrijavaById(dokumentPrijava.PrijavaZaNadmetanjeId, dokumentPrijava.DokumentId);
            return dokumentPrijava1;
        }

        public void UpdateDokumentPrijava(DokumentPrijavaZaNadmetanjeEntity dokumentPrijava)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }

        public void DeleteDokumentPrijavaByPrijavaZaNadmetanjeId(Guid prijavaZaNadmetanjeId)
        {
           DokumentiPrijave.Remove(DokumentiPrijave.FirstOrDefault(dp => dp.PrijavaZaNadmetanjeId == prijavaZaNadmetanjeId));
        }

        public bool SaveChanges()
        {
            return true;
        }
    }
}
