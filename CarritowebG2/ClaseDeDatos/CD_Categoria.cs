using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaseEntidades;
using System.Data.SqlClient;
using System.Data;

namespace ClaseDeDatos
{
    public class CD_Categoria
    {

        public List<Categoria> Listar()
        {
            List<Categoria> lista = new List<Categoria>();
            try
            {
                using (SqlConnection Oconexion = new SqlConnection(Conexion.CN))
                {
                    string query = "select cate_ID, cate_Descripcion, cate_Activo from Categoria";
                    SqlCommand cmd = new SqlCommand(query, Oconexion);
                    cmd.CommandType = CommandType.Text;

                    Oconexion.Open();
                    using (SqlDataReader DR = cmd.ExecuteReader())
                    {
                        while (DR.Read())
                        {
                            lista.Add(new Categoria()
                                {
                                    cate_ID = Convert.ToInt32(DR["IDCategoria"]),
                                    cate_Descripcion = DR["Descripcion"].ToString(),
                                    cate_Activo = Convert.ToBoolean(DR["Activo"])
                                });
                        }
                    }
                }

            }
            catch
            {
                lista = new List<Categoria>();
            }
            return lista;
        }
    

        public int Registrar(Categoria obj, out string Mensaje)
        {
            int idautogenerado = 0;

            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.CN))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarCategoria", oconexion);
                    cmd.Parameters.AddWithValue("Descripcion", obj.cate_Descripcion);
                    cmd.Parameters.AddWithValue("Activo", obj.cate_Activo);
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

        public bool Editar(Categoria obj, out string Mensaje)
        {
            bool Resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.CN))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarCategoria", oconexion);
                    cmd.Parameters.AddWithValue("IDCategoria", obj.cate_ID);
                    cmd.Parameters.AddWithValue("Descripcion", obj.cate_Descripcion);
                    cmd.Parameters.AddWithValue("Activo", obj.cate_Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
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
                Resultado = false;
                Mensaje = ex.Message;
            }
            return Resultado;
        }


        public bool Eliminar(int cate_ID, out string Mensaje)
        {
            bool Resultado = false;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.CN))
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarCategoria", oconexion);
                    cmd.Parameters.AddWithValue("IDCategoria", cate_ID);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
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
                Resultado = false;
                Mensaje = ex.Message;
            }
            return Resultado;
        }



    }
}
