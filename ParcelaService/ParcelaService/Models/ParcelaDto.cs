using ParcelaService.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelaService.Models
{
    public class ParcelaDto
    {
        public Guid ParcelaId { get; set; }
        public string BrojParcele { get; set; }
        public string BrojListaNepokretnosti { get; set; }
        public Guid KatastarskaOpstinaId { get; set; }
        public Kultura Kultura { get; set; }
        public Klasa Klasa { get; set; }
        public Obradivost Obradivost { get; set; }
        public Guid ZasticenaZonaId { get; set; }
        public OblikSvojine OblikSvojine { get; set; }
        public string Odvodnjavanje { get; set; }
        public string KulturaStvarnoStanje { get; set; }
        public string KlasaStvarnoStanje { get; set; }
        public string ObradivostStvarnoStanje { get; set; }
        public string ZasticenaZonaStvarnoStanje { get; set; }
        public string OdvodnjavanjeStvarnoStanje { get; set; }
    }
}
