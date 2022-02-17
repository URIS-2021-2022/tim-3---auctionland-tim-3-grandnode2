using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Models
{
    public class SluzbeniListModel
    {
        public Guid SluzbeniListId { get; set; }

        public String Opstina { get; set; }

        public int BrojSluzbenogLista { get; set; }

        public DateTime DatumIzdavanja { get; set; }

    }
}
