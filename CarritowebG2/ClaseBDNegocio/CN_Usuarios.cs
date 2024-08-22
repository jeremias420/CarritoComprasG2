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
                string Mensaje_Correo = "<h3>Su cuenta fue creada correctamente</h3></br><p>Su contraseña pára acceder es: !clave!</p>";
                Mensaje_Correo = Mensaje_Correo.Replace("!clave!", clave);

                bool respuesta = CN_Recursos.EnviarCorreo(obj.usua_Correo, asunto, Mensaje_Correo);

                if (respuesta)
                {

                    obj.usua_Clave = CN_Recursos.ConvertirSha256(clave);
                    return objClaseDatos.Registrar(obj, out Mensaje);

                }
                else
                {
                    Mensaje = "No se puede enviar el correo";
                    return 0;
                }
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
        public bool CambiarClave(int usua_Id, string nuevaClave, out string Mensaje)
        {
            return objClaseDatos.CambiarClave(usua_Id, nuevaClave, out Mensaje);
        }

        public bool ReestablecerClave(int usua_Id, string usua_Correo,out string Mensaje)
        {

            Mensaje = string.Empty;
            string nuevaClave = CN_Recursos.GenerarClave();
            bool resultado = objClaseDatos.ReestablecerClave(usua_Id,CN_Recursos.ConvertirSha256(nuevaClave), out Mensaje);

            if (resultado)
            {
                string asunto = "Contraseña Reestablecida";
                string Mensaje_Correo = "<h3>Su cuenta fue reestablecida</h3></br><p>Su contraseña pára acceder es: !clave!</p>";
                Mensaje_Correo = Mensaje_Correo.Replace("!clave!", nuevaClave);

                bool respuesta = CN_Recursos.EnviarCorreo(usua_Correo, asunto, Mensaje_Correo);

                if (respuesta)
                {
                    return true;
                }
                else
                {
                    Mensaje = "No se pudo enviar el correo";
                    return false;
                }
            }
            else
            {
                Mensaje = "No se pudo reestablecer la contraseña";
                return false;
            }
        }

    }
}
