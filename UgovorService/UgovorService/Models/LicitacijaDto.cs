using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UgovorService.Models
{
    public class LicitacijaDto
    {
        
        public int brojLicitacije { get; set; }

        
        public int goidna { get; set; }

      
        public DateTime datumLicitacije { get; set; }


        public int ogranicenjeLicitacije { get; set; }

       
        public int korakCene { get; set; }

      
        public DateTime rokZaDostavuPrijava { get; set; }
    }
}
