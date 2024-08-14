using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaseEntidades
{
    public class Carrito
    {

        public int carr_ID { get; set; }
        public Cliente carr_clie_ID { get; set; }
        public Producto carr_prod_ID { get; set; }
        public int carr_Cantidad { get; set; }

    }
}
