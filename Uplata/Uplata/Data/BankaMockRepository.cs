using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uplata.Entities;

namespace Uplata.Data
{
    public class BankaMockRepository : IBankaRepository
    {
        public static List<BankaEntity> Banke { get; set; } = new List<BankaEntity>();

        public BankaMockRepository()
        {
            FillData();
        }

        private void FillData()
        {
            Banke.AddRange(new List<BankaEntity>
            {
                new BankaEntity
                {
                    BankaId = Guid.Parse("9aef1da1-d5af-4073-9d40-8794f9d33564"),
                    NazivBanke = "OTP banka",
                    Adresa = "Bulevar Oslobodjenja 80",
                    Grad = "Novi Sad"
                },
                new BankaEntity
                {
                    BankaId = Guid.Parse("ceed4ee2-ea12-499b-a0c9-be41d4ac0748"),
                    NazivBanke = "UniCredit banka",
                    Adresa = "Resavska 28",
                    Grad = "Beograd"
                }
            });
        }

        public List<BankaEntity> GetBanke()
        {
            return (from b in Banke select b).ToList();
        }

        public BankaEntity GetBankaById(Guid bankaId)
        {
            return Banke.FirstOrDefault(b => b.BankaId == bankaId);
        }

        public BankaEntity CreateBanka(BankaEntity banka)
        {
            banka.BankaId = Guid.NewGuid();
            Banke.Add(banka);
            BankaEntity banka1 = GetBankaById(banka.BankaId);
            return banka1;
        }

        public void UpdateBanka(BankaEntity banka)
        {
            BankaEntity banka1 = GetBankaById(banka.BankaId);

            banka1.NazivBanke = banka.NazivBanke;
            banka1.Adresa = banka.Adresa;
            banka1.Grad = banka.Grad;

        }
        public void DeleteBanka(Guid bankaId)
        {
            Banke.Remove(Banke.FirstOrDefault(b => b.BankaId == bankaId));
        }

        public bool SaveChanges()
        {
            return true;
        }
    }
}
