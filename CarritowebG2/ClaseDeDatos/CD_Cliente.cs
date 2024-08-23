using ClaseEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaseDeDatos
{
    public class CD_Cliente
    {
        public int Registrar(Cliente obj, out string Mensaje)
        {
            int idautogenerado = 0;

            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.CN))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarCliente", oconexion);
                    cmd.Parameters.AddWithValue("Nombre", obj.clie_Nombre);
                    cmd.Parameters.AddWithValue("Apellido", obj.clie_Apellido);
                    cmd.Parameters.AddWithValue("Correo", obj.clie_Correo);
                    cmd.Parameters.AddWithValue("Clave", obj.clie_Clave);
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
        
        public List<Cliente> Listar()
        {
            List<Cliente> lista = new List<Cliente>();
            try
            {
                using (SqlConnection Oconexion = new SqlConnection(Conexion.CN))
                {
                    string query = "select   clie_ID,clie_Apellido,clie_Correo,clie_Clave,clie_Nombre,clie_Reestablecer, from Cliente ";
                    SqlCommand cmd = new SqlCommand(query, Oconexion);
                    cmd.CommandType = CommandType.Text;

                    Oconexion.Open();
                    using (SqlDataReader DR = cmd.ExecuteReader())
                    {
                        while (DR.Read())
                        {
                            lista.Add(new Cliente()
                            {
                                clie_ID = Convert.ToInt32(DR["clie_ID"]),
                                clie_Nombre = DR["clie_Nombre"].ToString(),
                                clie_Apellido = DR["clie_Apellido"].ToString(),
                                clie_Correo = DR["clie_Correo"].ToString(),
                                clie_Clave = DR["clie_Clave"].ToString(),
                                clie_Reestablecer = Convert.ToBoolean(DR["clie_Reestablecer"]),
                            }
                            );
                        }
                    }
                }

            }
            catch
            {
                lista = new List<Cliente>();
            }

            return lista;
        }

        public bool CambiarClave(int clie_Id, string nuevaClave, out string Mensaje)
        {
            bool Resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.CN))
                {
                    SqlCommand cmd = new SqlCommand("update Cliente set clie_Clave = @nuevaclave, clie_Reestablecer = 0 where clie_ID = @id", oconexion);
                    cmd.Parameters.AddWithValue("@id", clie_Id);
                    cmd.Parameters.AddWithValue("@nuevaClave", nuevaClave);
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
        public bool ReestablecerClave(int clie_Id, string Clave, out string Mensaje)
        {
            bool Resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.CN))
                {
                    SqlCommand cmd = new SqlCommand("update Cliente set clie_Clave = @clave, clie_Reestablecer = 1 where clie_ID = @id", oconexion);
                    cmd.Parameters.AddWithValue("@id", clie_Id);
                    cmd.Parameters.AddWithValue("@Clave", Clave);
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
