using ClaseDeDatos;
using ClaseEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaseBDNegocio
{
    public class CN_Cliente
    {
        private CD_Cliente objClaseDatos = new CD_Cliente();
      
        public List<Cliente> Listar()
        {
            return objClaseDatos.Listar();
        }




        public int Registrar(Cliente obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.clie_Nombre) || string.IsNullOrWhiteSpace(obj.clie_Nombre))
            {
                Mensaje = " Ingrese el nombre del Cliente";
            }
            else if (string.IsNullOrEmpty(obj.clie_Apellido) || string.IsNullOrWhiteSpace(obj.clie_Apellido))
            {
                Mensaje = "Ingrese el apellido del Cliente";
            }
            else if (string.IsNullOrEmpty(obj.clie_Correo) || string.IsNullOrWhiteSpace(obj.clie_Correo))
            {
                Mensaje = "Ingrese el correo del Cliente";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {

                obj.clie_Clave = CN_Recursos.ConvertirSha256(obj.clie_Clave);
                return objClaseDatos.Registrar(obj, out Mensaje);
                
            }
            else
            {
                return 0;
            }

        }

        

        public bool CambiarClave(int clie_Id, string nuevaClave, out string Mensaje)
        {
            return objClaseDatos.CambiarClave(clie_Id, nuevaClave, out Mensaje);
        }



        public bool ReestablecerClave(int clie_Id, string clie_Correo, out string Mensaje)
        {

            Mensaje = string.Empty;
            string nuevaClave = CN_Recursos.GenerarClave();
            bool resultado = objClaseDatos.ReestablecerClave(clie_Id, CN_Recursos.ConvertirSha256(nuevaClave), out Mensaje);

            if (resultado)
            {
                string asunto = "Contraseña Reestablecida";
                string Mensaje_Correo = "<h3>Su cuenta fue reestablecida</h3></br><p>Su contraseña pára acceder es: !clave!</p>";
                Mensaje_Correo = Mensaje_Correo.Replace("!clave!", nuevaClave);

                bool respuesta = CN_Recursos.EnviarCorreo(clie_Correo, asunto, Mensaje_Correo);

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
