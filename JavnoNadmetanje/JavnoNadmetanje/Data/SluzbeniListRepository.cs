using JavnoNadmetanje.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Data
{
    public class SluzbeniListRepository : ISluzbeniListRepository
    {
        public static List<SluzbeniListEntity> SluzbeniListovi { get; set; } = new List<SluzbeniListEntity>();

        public SluzbeniListRepository()
        {
            FillData();
        }

        private void FillData()
        {
            SluzbeniListovi.AddRange(new List<SluzbeniListEntity>
            {
                new SluzbeniListEntity
                {
                    SluzbeniListId = Guid.Parse("1a0d7558-2ebc-45df-83d3-13066c36d42b"),
                    Opstina = "Novi Sad",
                    BrojSluzbenogLista = 5,
                    DatumIzdavanja = DateTime.Parse("11-10-2021")
                },
                new SluzbeniListEntity
                {
                    SluzbeniListId = Guid.Parse("76e60dd7-0e18-4c7c-abe0-b59524eca5ff"),
                    Opstina = "Subotica",
                    BrojSluzbenogLista = 8,
                    DatumIzdavanja = DateTime.Parse("11-01-2022")
                }
            });
        }

        public List<SluzbeniListEntity> GetSluzbeniListovi()
        {
            return (from s in SluzbeniListovi select s).ToList();
        }

        public SluzbeniListEntity GetSluzbeniListById(Guid sluzbeniListId)
        {
            return SluzbeniListovi.FirstOrDefault(s => s.SluzbeniListId == sluzbeniListId);
        }

        public SluzbeniListEntity CreateSluzbeniList(SluzbeniListEntity sluzbeniList)
        {
            sluzbeniList.SluzbeniListId = Guid.NewGuid();
            SluzbeniListovi.Add(sluzbeniList);
            SluzbeniListEntity sList = GetSluzbeniListById(sluzbeniList.SluzbeniListId);
            return sList;
        }

        public SluzbeniListEntity UpdateSluzbeniList(SluzbeniListEntity sluzbeniList)
        {
            SluzbeniListEntity sList = GetSluzbeniListById(sluzbeniList.SluzbeniListId);

            sList.Opstina = sluzbeniList.Opstina;
            sList.BrojSluzbenogLista = sluzbeniList.BrojSluzbenogLista;
            sList.DatumIzdavanja = sluzbeniList.DatumIzdavanja;

            return sList;
        }

        public void DeleteSluzbeniList(Guid sluzbeniListId)
        {
            SluzbeniListovi.Remove(SluzbeniListovi.FirstOrDefault(s => s.SluzbeniListId == sluzbeniListId));
        }

    }
}
