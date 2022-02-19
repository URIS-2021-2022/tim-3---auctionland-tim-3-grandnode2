using komisijaService.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace komisijaService.Data
{
     public interface IKomisijaRepository { 
        List<Komisija> GetKomsijas(string naziv = null, string oznakaKomisije = null );
        Komisija GetKomisijaById(Guid id);
        Komisija GetKomisjaByOznaka(String oznakaKomisije);
        void CreateKomsija(Komisija komisija);
        void UpdateKomisija(Komisija oldKomisija, Komisija newKomisija);
        void DeleteKomsija(Guid id);
        bool SaveChanges();
    }
}
