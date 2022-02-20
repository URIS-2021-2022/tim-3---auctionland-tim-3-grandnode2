using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelaService.Data.Enums
{
    public enum Kultura
    {
        [Description("Njive")] Njive,
        [Description("Vrtovi")] Vrtovi,
        [Description("Vocnjaci")] Vocnjaci,
        [Description("Vinogradi")] Vinogradi,
        [Description("Livade")] Livade,
        [Description("Pasnjaci")] Pasnjaci,
        [Description("Sume")] Sume,
        [Description("Trstici- mocvare")] TrsticiMocvare,
    }
}
