using JavnoNadmetanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Data
{
    public interface IJavnoNadmetanjeRepository
    {
        List<JavnoNadmetanjeModel> GetJavnaNadmetanja();

        JavnoNadmetanjeModel GetJavnoNadmetanjeById(Guid javnoNadmetanjeId);

        JavnoNadmetanjeModel CreateJavnoNadmetanje(JavnoNadmetanjeModel javnoNadmetanje);

        JavnoNadmetanjeModel UpdateJavnoNadmetanje(JavnoNadmetanjeModel javnoNadmetanje);

        void DeleteJavnoNadmetanje(Guid javnoNadmetanjeId);

    }
}
