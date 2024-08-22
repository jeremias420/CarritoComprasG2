using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaseEntidades
{
   public class Producto
    {
        public int prod_ID { get; set; }

        public string prod_Nombre { get; set; }

        public string prod_Descripcion { get; set; }

        public Marca prod_marc_ID { get; set; }

        public Categoria prod_cate_ID { get; set; }

        public decimal Prod_Precio { get; set; }

        public int prod_Stock { get; set; }

        public string prod_RutaImagen { get; set; }

        public string prod_NombreImagen { get; set; }

        public bool prod_Activo { get; set; }

        public bool prod_Base64 { get; set; }

        public bool prod_Extension { get; set; }
     
    }
}
