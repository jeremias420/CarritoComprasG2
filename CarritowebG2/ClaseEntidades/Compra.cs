using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaseEntidades
{
    public class Compra
    {

        public int comp_ID { get; set; }
        public Cliente comp_clie_ID { get; set; }
        public int comp_TotalProductos { get; set; }
        public decimal comp_MontoTotal { get; set; }
        public string comp_Contacto { get; set; }
        public string comp_dist_ID { get; set; }
        public string comp_Telefono { get; set; }
        public string comp_Direccion { get; set; }
        public string comp_IDTransaccion { get; set; }
        public List<DetalleCompra> oDetalleCompras { get; set; }

    }
}
