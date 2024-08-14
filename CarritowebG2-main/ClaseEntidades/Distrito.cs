using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaseEntidades
{
    class Distrito
    {
        public int dist_ID { get; set; }
        public string dist_Descripcion { get; set; }
        public Departamento dist_depa_ID { get; set; }
    }
}
