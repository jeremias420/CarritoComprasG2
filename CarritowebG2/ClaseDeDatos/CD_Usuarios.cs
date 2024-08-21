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

        #region LISTAR
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();
            try
            {
                using (SqlConnection Oconexion = new SqlConnection(Conexion.CN))
                {
                    string query = "select   usua_ID,usua_Apellido,usua_Correo,usua_Clave,usua_Nombre,usua_Reestablecer, usua_Activo from Usuario ";
                    SqlCommand cmd = new SqlCommand(query, Oconexion);
                    cmd.CommandType = CommandType.Text;

                    Oconexion.Open();
                    using (SqlDataReader DR = cmd.ExecuteReader())
                    {
                        while (DR.Read())
                        {
                            lista.Add( new Usuario()
                                {
                                    usua_ID = Convert.ToInt32(DR["usua_ID"]),
                                    usua_Nombre = DR["usua_Nombre"].ToString(),
                                    usua_Apellido = DR["usua_Apellido"].ToString(),
                                    usua_Correo = DR["usua_Correo"].ToString(),
                                    usua_Clave = DR["usua_Clave"].ToString(),
                                    usua_Reestablecer = Convert.ToBoolean(DR["usua_Reestablecer"]),
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

        #endregion

        public int Registrar(Usuario obj, out string Mensaje)
        {
            int idautogenerado = 0;

            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.CN))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario", oconexion);
                    cmd.Parameters.AddWithValue("Nombre", obj.usua_Nombre);
                    cmd.Parameters.AddWithValue("Apellido", obj.usua_Apellido);
                    cmd.Parameters.AddWithValue("Correo", obj.usua_Correo);
                    cmd.Parameters.AddWithValue("Clave", obj.usua_Clave);
                    cmd.Parameters.AddWithValue("Activo", obj.usua_Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    idautogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idautogenerado = 0;
                Mensaje = ex.Message;
            }
            return idautogenerado;
        }

        public bool Editar(Usuario obj, out string Mensaje)
        {
            bool Resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.CN))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarUsuario", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuario", obj.usua_ID);
                    cmd.Parameters.AddWithValue("Nombre", obj.usua_Nombre);
                    cmd.Parameters.AddWithValue("Apellido", obj.usua_Apellido);
                    cmd.Parameters.AddWithValue("Correo", obj.usua_Correo);
                    cmd.Parameters.AddWithValue("Activo", obj.usua_Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    Resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                Resultado=false;
                Mensaje = ex.Message;
            }
            return Resultado;
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            bool Resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.CN))
                {
                    SqlCommand cmd = new SqlCommand("delete top (1) from Usuario where Usua_ID =@id", oconexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    Resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                Resultado = false;
                Mensaje = ex.Message;
            }
            return Resultado;

        }

    }
}
