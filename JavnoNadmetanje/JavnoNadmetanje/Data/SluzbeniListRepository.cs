using JavnoNadmetanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Data
{
    public class SluzbeniListRepository : ISluzbeniListRepository
    {
        public static List<SluzbeniListModel> SluzbeniListovi { get; set; } = new List<SluzbeniListModel>();

        public SluzbeniListRepository()
        {
            FillData();
        }

        private void FillData()
        {
            SluzbeniListovi.AddRange(new List<SluzbeniListModel>
            {
                new SluzbeniListModel
                {
                    SluzbeniListId = Guid.Parse("1a0d7558-2ebc-45df-83d3-13066c36d42b"),
                    Opstina = "Novi Sad",
                    BrojSluzbenogLista = 5,
                    DatumIzdavanja = DateTime.Parse("11-10-2021")
                },
                new SluzbeniListModel
                {
                    SluzbeniListId = Guid.Parse("76e60dd7-0e18-4c7c-abe0-b59524eca5ff"),
                    Opstina = "Subotica",
                    BrojSluzbenogLista = 8,
                    DatumIzdavanja = DateTime.Parse("11-01-2022")
                }
            });
        }

        public List<SluzbeniListModel> GetSluzbeniListovi()
        {
            return (from s in SluzbeniListovi select s).ToList();
        }

        public SluzbeniListModel GetSluzbeniListById(Guid sluzbeniListId)
        {
            return SluzbeniListovi.FirstOrDefault(s => s.SluzbeniListId == sluzbeniListId);
        }

        public SluzbeniListModel CreateSluzbeniList(SluzbeniListModel sluzbeniList)
        {
            sluzbeniList.SluzbeniListId = Guid.NewGuid();
            SluzbeniListovi.Add(sluzbeniList);
            SluzbeniListModel sList = GetSluzbeniListById(sluzbeniList.SluzbeniListId);
            return sList;
        }

        public SluzbeniListModel UpdateSluzbeniList(SluzbeniListModel sluzbeniList)
        {
            SluzbeniListModel sList = GetSluzbeniListById(sluzbeniList.SluzbeniListId);

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
