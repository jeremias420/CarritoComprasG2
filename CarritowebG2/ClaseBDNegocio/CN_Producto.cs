using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaseEntidades;
using ClaseDeDatos;

namespace ClaseBDNegocio
{
    public class CN_Producto
    {

        private CD_Producto objClaseDatos = new CD_Producto();

        public List<Producto> Listar()
        {
            return objClaseDatos.Listar();
        }


        public int Registrar(Producto obj, out string Mensaje)
        {

            Mensaje = string.Empty;
            //Comprobar nombre prod
            if (string.IsNullOrEmpty(obj.prod_Nombre) || string.IsNullOrWhiteSpace(obj.prod_Descripcion))
            {
                Mensaje = "El nombre del producto no puede estar vacio";
            }
            //comprobar marca del prod
            else if (obj.prod_marc_ID.marc_ID == 0)
            {
                Mensaje = "Debe seleccionar una Marca";
            }
            //Comprobar descripcion del prod
            else if (string.IsNullOrEmpty(obj.prod_Descripcion) || string.IsNullOrWhiteSpace(obj.prod_Descripcion))
            {
                Mensaje = "La descripcion del producto no puede estar vacio";
            }
            // Comprobar categoria prod
            else if (obj.prod_cate_ID.cate_ID == 0)
            {
                Mensaje = "Debe seleccionar una Categoria";
            }
            //Comprobar precio prod
            else if (obj.Prod_Precio == 0)
            {
                Mensaje = "Debe Ingresar el precio del producto";
            }
            //Comprobar Stock prod
            else if (obj.prod_Stock == 0)
            {
                Mensaje = "Debe Ingresar el Stock del producto";
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

        public bool Editar(Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            //Comprobar nombre prod
            if (string.IsNullOrEmpty(obj.prod_Nombre) || string.IsNullOrWhiteSpace(obj.prod_Descripcion))
            {
                Mensaje = "El nombre del producto no puede estar vacio";
            }
            //comprobar marca del prod
            else if (obj.prod_marc_ID.marc_ID == 0)
            {
                Mensaje = "Debe seleccionar una Marca";
            }
            //Comprobar descripcion del prod
            else if (string.IsNullOrEmpty(obj.prod_Descripcion) || string.IsNullOrWhiteSpace(obj.prod_Descripcion))
            {
                Mensaje = "La descripcion del producto no puede estar vacio";
            }
            // Comprobar categoria prod
            else if (obj.prod_cate_ID.cate_ID == 0)
            {
                Mensaje = "Debe seleccionar una Categoria";
            }
            //Comprobar precio prod
            else if (obj.Prod_Precio == 0)
            {
                Mensaje = "Debe Ingresar el precio del producto";
            }
            //Comprobar Stock prod
            else if (obj.prod_Stock == 0)
            {
                Mensaje = "Debe Ingresar el Stock del producto";
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

        public bool GuardarDatosImagen(Producto obj, out string mensaje)
        {

            return objClaseDatos.GuardarDatosImagen(obj, out mensaje);

        }

    }

}
