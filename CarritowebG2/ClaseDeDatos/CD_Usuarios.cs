using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClaseEntidades;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ClaseDeDatos
{
    public class CD_Usuarios
    {

        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();
            try
            {
                using (SqlConnection Oconexion = new SqlConnection(Conexion.CN))
                {
                    string query = "select   usua_ID,usua_Apellido,usua_Correo,usua_Clave,usua_Nombre,usua_Restablecer, usua_Activo from Usuario ";
                    SqlCommand cmd = new SqlCommand(query, Oconexion);
                    cmd.CommandType = CommandType.Text;

                    Oconexion.Open();
                    using (SqlDataReader DR = cmd.ExecuteReader())
                    {
                        while (DR.Read())
                        {
                            lista.Add(
                                new Usuario()
                                {
                                    usua_ID = Convert.ToInt32(DR["usua_ID"]),
                                    usua_Nombre = DR["usua_Nombre"].ToString(),
                                    usua_Apellido = DR["usua_Apellido"].ToString(),
                                    usua_Correo = DR["usua_Correo"].ToString(),
                                    usua_Clave = DR["usua_Clave"].ToString(),
                                    usua_Restablecer = Convert.ToBoolean(DR["usua_Restablecer"]),
                                    usua_Activo = Convert.ToBoolean(DR["usua_Activo"])
                                }
                                );
                        }
                    }
                }

            }
            catch
            {
                lista = new List<Usuario>();
            }

            return lista;
        }

    }
}
