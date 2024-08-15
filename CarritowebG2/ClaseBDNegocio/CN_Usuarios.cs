using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaseEntidades;
using ClaseDeDatos;

namespace ClaseBDNegocio
{
    public class CN_Usuarios
    {

        private CD_Usuarios objClaseEntidades = new CD_Usuarios();
        public List<Usuario> Listar()
        {
            return objClaseEntidades.Listar();
        }



    }
}
