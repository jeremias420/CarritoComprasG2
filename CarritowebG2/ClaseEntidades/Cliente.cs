using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaseEntidades
{
   public class Cliente
    {
        public int clie_ID { get; set; }
        public string clie_Nombre { get; set; }
        public string clie_Apellido { get; set; }
        public string clie_Correo { get; set; }
        public string clie_Clave { get; set; }
        public bool clie_Restablecer { get; set; }
    }
}
