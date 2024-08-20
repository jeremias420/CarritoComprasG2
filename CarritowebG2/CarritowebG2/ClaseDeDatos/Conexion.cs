using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace ClaseDeDatos
{
    public class Conexion
    {
        public static string CN = ConfigurationManager.ConnectionStrings["cadena"].ToString();
    }
}
