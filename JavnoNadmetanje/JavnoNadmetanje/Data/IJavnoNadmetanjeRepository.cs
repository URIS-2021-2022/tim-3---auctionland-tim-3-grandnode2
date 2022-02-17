using JavnoNadmetanje.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Data
{
    public interface IJavnoNadmetanjeRepository
    {
        List<JavnoNadmetanjeEntity> GetJavnaNadmetanja();

        JavnoNadmetanjeEntity GetJavnoNadmetanjeById(Guid javnoNadmetanjeId);

        JavnoNadmetanjeEntity CreateJavnoNadmetanje(JavnoNadmetanjeEntity javnoNadmetanje);

        JavnoNadmetanjeEntity UpdateJavnoNadmetanje(JavnoNadmetanjeEntity javnoNadmetanje);

        void DeleteJavnoNadmetanje(Guid javnoNadmetanjeId);

    }
}
