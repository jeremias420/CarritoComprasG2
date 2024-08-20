using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaseEntidades
{
    public class Usuario
    {
        public int usua_ID { get; set; }
        public string usua_Nombre { get; set; }
        public string usua_Apellido { get; set; }
        public string usua_Correo { get; set; }
        public string usua_Clave { get; set; }
        public bool usua_Reestablecer { get; set; }
        public bool usua_Activo { get; set; }
    }
}
