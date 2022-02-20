using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelaService.Data.Enums
{
    public enum OblikSvojine
    {
        [Description("Privatna svojina")] PrivatnaSvojina,
        [Description("Drzavna svojina RS")] DrzavnaSvojinaRS,
        [Description("Drzavna svojina")] DrzavnaSvojina,
        [Description("Drustvena svojina")] DrustvenaSvojina,
        [Description("Zadruzna svojina")] ZadruznaSvojina,
        [Description("Mesovita svojina")] MesovitaSvojina,
        [Description("Drugi oblici")] DrugiOblici
    }
}
