using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KupacService.Entities
{
    public class Liciter
    {
        [Key]
        [Required]
        public Guid KupacId { get; set; }
        [NotMapped]
        public List<int> Prioritet { get; set; }
        public int OstvarenaPovrsina { get; set; }
        [NotMapped]
        public List<Guid> UplateId { get; set; }
        [NotMapped]
        public List<Guid> OvlascenoLice { get; set; }
        public bool ImaZabranu { get; set; }
        public DateTime? DatumPocetkaZabrane { get; set; }
        public int? DuzinaTrajanjaZabraneUGodinama { get; set; }
        [NotMapped]
        public List<Guid> JavnaNadmetanjaId { get; set; }
        public string BrojLicence { get; set; }
    }
}
