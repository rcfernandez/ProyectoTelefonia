using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTelefonia.Services
{

    public class InternoService
    {

        ModelDB db = new ModelDB();


        public List<Interno> buscarInterno(long? numeroInterno)
        {
            return (from inte in db.Interno
                    where inte.Numero == numeroInterno
                    select inte).ToList();
        }

    }
}