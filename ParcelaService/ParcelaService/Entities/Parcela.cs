using ParcelaService.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelaService.Entities
{
    public class Parcela
    {
        [Key]
        public Guid ParcelaId { get; set; }
        [Required]
        public string BrojParcele { get; set; }
        [Required]
        public string BrojListaNepokretnosti { get; set; }
        [Required]
        public Guid KatastarskaOpstinaId { get; set; }
        public Kultura Kultura { get; set; }
        public Klasa Klasa { get; set; }
        public Obradivost Obradivost { get; set; }
        [ForeignKey("ZasticenaZona")]
        public Guid ZasticenaZonaId { get; set; }
        public ZasticenaZona ZasticenaZona { get; set; }
        public OblikSvojine OblikSvojine { get; set; }
        [MaxLength(50)]
        public string Odvodnjavanje { get; set; }
        [MaxLength(50)]
        public string KulturaStvarnoStanje { get; set; }
        [MaxLength(50)]
        public string KlasaStvarnoStanje { get; set; }
        [MaxLength(50)]
        public string ObradivostStvarnoStanje { get; set; }
        [MaxLength(50)]
        public string ZasticenaZonaStvarnoStanje { get; set; }
        [MaxLength(50)]
        public string OdvodnjavanjeStvarnoStanje { get; set; }
        public List<DeoParcele> DeloviParcele { get; set; }
    }
}
