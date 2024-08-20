using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaseEntidades;
using ClaseDeDatos;

namespace ClaseBDNegocio
{
    public class CN_Categoria
    {
        private CD_Categoria objClaseDatos = new CD_Categoria();
   
        public List<Categoria> Listar()
        {
            return objClaseDatos.Listar();
        }


        public int Registrar(Categoria obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if(String.IsNullOrEmpty(obj.cate_Descripcion) || string.IsNullOrEmpty(obj.cate_Descripcion))
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

        public bool Editar(Categoria obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.cate_Descripcion) || string.IsNullOrWhiteSpace(obj.cate_Descripcion))
            {
                Mensaje = "La descripcion de la categoria no puede ser vacio";
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
