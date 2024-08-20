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

        private CD_Usuarios objClaseDatos = new CD_Usuarios();
        public List<Usuario> Listar()
        {
            return objClaseDatos.Listar();
        }

        public int Registrar(Usuario obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.usua_Nombre) || string.IsNullOrWhiteSpace(obj.usua_Nombre))
            {
                Mensaje = " Ingrese el nombre del usuario";
            }else if(string.IsNullOrEmpty(obj.usua_Apellido) || string.IsNullOrWhiteSpace(obj.usua_Apellido)){
                Mensaje = "Ingrese el apellido del usuario";
            }else if(string.IsNullOrEmpty(obj.usua_Correo) || string.IsNullOrWhiteSpace(obj.usua_Correo)){
                Mensaje = "Ingrese el correo del usuario";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {

                string clave = CN_Recursos.GenerarClave();
                string asunto = "creacion de Cuenta";
                string Mensaje_Correo = "<h3>Su cuenta fue creada correctamente</h3></br><p>Su contraseña para acceder es: !clave!</p>";
                Mensaje_Correo = Mensaje_Correo.Replace("!clave!", clave);

                bool respuesta = CN_Recursos.EnviarCorreo(obj.usua_Correo, asunto, Mensaje_Correo);

                if (respuesta)
                {

                   obj.usua_Clave = CN_Recursos.ConvertirSha256(clave);
                   return objClaseDatos.Registrar(obj, out Mensaje);

                }


                obj.usua_Clave = CN_Recursos.ConvertirSha256(clave);

                return objClaseDatos.Registrar(obj, out Mensaje);
            }
            else
            {
                return 0;
            }
            
        }


        public bool Editar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.usua_Nombre) || string.IsNullOrWhiteSpace(obj.usua_Nombre))
            {
                Mensaje = " Ingrese el nombre del usuario";
            }else if (string.IsNullOrEmpty(obj.usua_Apellido) || string.IsNullOrWhiteSpace(obj.usua_Apellido)){
                Mensaje = "Ingrese el apellido del usuario";
            }else if (string.IsNullOrEmpty(obj.usua_Correo) || string.IsNullOrWhiteSpace(obj.usua_Correo)){
                Mensaje = "Ingrese el correo del usuario";
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
