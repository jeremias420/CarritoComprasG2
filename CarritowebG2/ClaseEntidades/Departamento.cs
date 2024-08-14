using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaseEntidades
{
    public class Departamento
    {

        public int depa_ID { get; set; }
        public string depa_Descripcion { get; set; }
        public Provincia depa_Prov_ID  { get; set; }

    }
}
