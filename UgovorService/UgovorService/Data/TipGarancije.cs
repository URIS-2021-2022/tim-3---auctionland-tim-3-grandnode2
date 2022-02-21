using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace UgovorService.Data
{
    public enum TipGarancije
    {
        [Description("Jemstvo")] Jemstvo,
        [Description("Bankarska garancija")] BankarskaGarancija,
        [Description("Garancija nekretninom")] GarancijaNekretninom,
        [Description("Zirantska")] Zirantska,
        [Description("Uplata gotovinom")] UplataGotovinom
    }
}
