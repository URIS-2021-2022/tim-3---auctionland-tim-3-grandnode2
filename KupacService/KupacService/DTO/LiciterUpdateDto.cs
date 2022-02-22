using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KupacService.DTO
{
    public class LiciterUpdateDto
    {
        public Guid KupacId { get; set; }
        public List<int> Prioritet { get; set; }
        public int OstvarenaPovrsina { get; set; }
        public List<Guid> UplateId { get; set; }
        public List<Guid> OvlascenoLice { get; set; }
        public bool ImaZabranu { get; set; }
        public DateTime? DatumPocetkaZabrane { get; set; }
        public int? DuzinaTrajanjaZabraneUGodinama { get; set; }
        public List<Guid> JavnaNadmetanjaId { get; set; }
        public string BrojLicence { get; set; }
    }
}
