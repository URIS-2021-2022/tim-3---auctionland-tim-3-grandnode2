using AutoMapper;
using JavnoNadmetanje.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Data
{
    public class DokumentPrijavaZaNadmetanjeRepository : IDokumentPrijavaZaNadmetanjeRepository
    {
        private readonly JavnoNadmetanjeContext context;
        private readonly IMapper mapper;

        public DokumentPrijavaZaNadmetanjeRepository(JavnoNadmetanjeContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<DokumentPrijavaZaNadmetanjeEntity> GetDokumentiPrijavaByPrijavaZaNadmetanjeId(Guid prijavaZaNadmetanjeId)
        {
            return context.DokumentiPrijave.Where(dp => (dp.PrijavaZaNadmetanjeId == prijavaZaNadmetanjeId)).ToList();
        }

        public DokumentPrijavaZaNadmetanjeEntity GetDokumentPrijavaById(Guid prijavaZaNadmetanjeId, Guid dokumentId)
        {
            return context.DokumentiPrijave.FirstOrDefault(dp => (dp.PrijavaZaNadmetanjeId == prijavaZaNadmetanjeId && dp.DokumentId == dokumentId));
        }

        public DokumentPrijavaZaNadmetanjeEntity CreateDokumentPrijava(DokumentPrijavaZaNadmetanjeEntity dokumentPrijava)
        {
            dokumentPrijava.DatumDonosenjaDokumenta = DateTime.Now;
            context.DokumentiPrijave.Add(dokumentPrijava);
            DokumentPrijavaZaNadmetanjeEntity dokumentPrijava1 = GetDokumentPrijavaById(dokumentPrijava.PrijavaZaNadmetanjeId,dokumentPrijava.DokumentId);
            return dokumentPrijava1;
        }

        public void UpdateDokumentPrijava(DokumentPrijavaZaNadmetanjeEntity dokumentPrijava)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }

        public void DeleteDokumentPrijavaByPrijavaZaNadmetanjeId(Guid prijavaZaNadmetanjeId)
        {
            context.DokumentiPrijave.Remove(context.DokumentiPrijave.FirstOrDefault(dp => dp.PrijavaZaNadmetanjeId == prijavaZaNadmetanjeId));
        }
    }
}
