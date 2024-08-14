using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaseEntidades
{
    public class DetalleCompra
    {

        public int deta_ID { get; set; }
        public  Compra deta_compra_ID { get; set; }
        public Producto deta_prod_ID { get; set; }
        public int deta_Cantidad { get; set; }
        public decimal deta_Total { get; set; }
        public string comp_IDTransaccion { get; set; }

    }
}
