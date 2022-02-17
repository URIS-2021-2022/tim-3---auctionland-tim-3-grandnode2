using JavnoNadmetanje.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Data
{
    public class OglasRepository : IOglasRepository
    {
        public static List<OglasEntity> Oglasi { get; set; } = new List<OglasEntity>();

        public OglasRepository()
        {
            FillData();
        }

        private void FillData()
        {
            Oglasi.AddRange(new List<OglasEntity>
            {
                new OglasEntity
                {
                    OglasId = Guid.Parse("382e1636-2705-477e-95c4-8727e819c5e9"),
                    DatumObjavljivanjaOglasa = DateTime.Parse("05-10-2021"),
                    GodinaObjavljivanjaOglasa = 2021,
                    TipGarantaPlacanja = {}
                },
                new OglasEntity
                {
                    OglasId = Guid.Parse("abd912e3-5962-463e-a04e-5fdd2b43e30f"),
                    DatumObjavljivanjaOglasa = DateTime.Parse("05-10-2020"),
                    GodinaObjavljivanjaOglasa = 2020,
                    TipGarantaPlacanja = {}
                }
            });
        }

        public List<OglasEntity> GetOglasi()
        {
            return (from o in Oglasi select o).ToList();
        }

        public OglasEntity GetOglasById(Guid oglasId)
        {
            return Oglasi.FirstOrDefault(o => o.OglasId == oglasId);
        }

        public OglasEntity CreateOglas(OglasEntity oglas)
        {
            oglas.OglasId = Guid.NewGuid();
            Oglasi.Add(oglas);
            OglasEntity oglas1 = GetOglasById(oglas.OglasId);
            return oglas1;
        }

        public OglasEntity UpdateOglas(OglasEntity oglas)
        {
            OglasEntity oglas1 = GetOglasById(oglas.OglasId);

            oglas1.DatumObjavljivanjaOglasa = oglas.DatumObjavljivanjaOglasa;
            oglas1.GodinaObjavljivanjaOglasa = oglas.GodinaObjavljivanjaOglasa;
            oglas1.TipGarantaPlacanja = oglas.TipGarantaPlacanja;

            return oglas1;
        }

        public void DeleteOglas(Guid oglasId)
        {
            Oglasi.Remove(Oglasi.FirstOrDefault(o => o.OglasId == oglasId));
        }
    }
}
