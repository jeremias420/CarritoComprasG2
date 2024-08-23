using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaseEntidades
{
    public class Reporte
    {
        public string FechaVenta { get; set; }

        public string Cliente { get; set; }

        public decimal Producto { get; set; }

        public int Precio { get; set; }

        public decimal Total { get; set; }

        public string IDTransaccion { get; set; }
    }
}
