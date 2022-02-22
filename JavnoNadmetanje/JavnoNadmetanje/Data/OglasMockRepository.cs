using JavnoNadmetanje.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Data
{
    public class OglasMockRepository : IOglasRepository
    {
        public static List<OglasEntity> Oglasi { get; set; } = new List<OglasEntity>();

        public OglasMockRepository()
        {
            FillData();
        }

        private static void FillData()
        {
            Oglasi.AddRange(new List<OglasEntity>
            {
                new OglasEntity
                {
                    OglasId = Guid.Parse("382e1636-2705-477e-95c4-8727e819c5e9"),
                    DatumObjavljivanjaOglasa = DateTime.Parse("05-10-2021"),
                    GodinaObjavljivanjaOglasa = 2021,
                    SluzbeniListId = Guid.Parse("76e60dd7-0e18-4c7c-abe0-b59524eca5ff")
                },
                new OglasEntity
                {
                    OglasId = Guid.Parse("abd912e3-5962-463e-a04e-5fdd2b43e30f"),
                    DatumObjavljivanjaOglasa = DateTime.Parse("05-10-2022"),
                    GodinaObjavljivanjaOglasa = 2022,
                    SluzbeniListId = Guid.Parse("1a0d7558-2ebc-45df-83d3-13066c36d42b")
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

        public void UpdateOglas(OglasEntity oglas)
        {
            OglasEntity oglas1 = GetOglasById(oglas.OglasId);

            oglas1.DatumObjavljivanjaOglasa = oglas.DatumObjavljivanjaOglasa;
            oglas1.GodinaObjavljivanjaOglasa = oglas.GodinaObjavljivanjaOglasa;

        }

        public void DeleteOglas(Guid oglasId)
        {
            Oglasi.Remove(Oglasi.FirstOrDefault(o => o.OglasId == oglasId));
        }

        public bool SaveChanges()
        {
            return true;
        }
    }
}
