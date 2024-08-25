using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaseEntidades;
using ClaseDeDatos;

namespace ClaseBDNegocio
{
    public class CN_Marca
    {
        private CD_Marca objClaseDatos = new CD_Marca();

        public List<Marca> Listar()
        {
            return objClaseDatos.Listar();
        }


        public int Registrar(Marca obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.marc_Descripcion) || string.IsNullOrWhiteSpace(obj.marc_Descripcion))
            {
                Mensaje = "Ingrese la descripción de la marca";
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
                Mensaje = "Ingrese la descripción de la marca";
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
 
        public List<Marca> ListarPorCategoria(int cate_ID)
        {
            return objClaseDatos.ListarMarcaPorCategoria(cate_ID);

        }

    }
}
