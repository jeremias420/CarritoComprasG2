using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaseEntidades;
using ClaseDeDatos;

namespace ClaseBDNegocio
{
    class CN_Marca
    {

        private CN_Marca objClaseDatos = new CN_Marca();

        public List<Marca> Listar()
        {
            return objClaseDatos.Listar();
        }


        public int Registrar(Marca obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (String.IsNullOrEmpty(obj.marc_Descripcion) || string.IsNullOrEmpty(obj.marc_Descripcion))
            {
                Mensaje = "La descripcion no puede estar vacia";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objClaseDatos.Registrar(obj, out Mensaje);
            }
            else
            {
                return 0;
            }

        }

        public bool Editar(Marca obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.marc_Descripcion) || string.IsNullOrWhiteSpace(obj.marc_Descripcion))
            {
                Mensaje = "La descripcion de la Marca no puede ser vacio";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objClaseDatos.Editar(obj, out Mensaje);
            }
            else
            {
                return false;
            }

        }
        public bool Eliminar(int id, out string Mensaje)
        {
            return objClaseDatos.Eliminar(id, out Mensaje);
        }

    }
}
