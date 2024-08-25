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
    public class CD_Marca
    {
       #region LISTAR

        public List<Marca> Listar()
        {
            List<Marca> lista = new List<Marca>();
            try
            {
                using (SqlConnection Oconexion = new SqlConnection(Conexion.CN))
                {
                    string query = "select marc_ID, marc_Descripcion, marc_Activo from Marca";
                    SqlCommand cmd = new SqlCommand(query, Oconexion);
                    cmd.CommandType = CommandType.Text;

                    Oconexion.Open();
                    using (SqlDataReader DR = cmd.ExecuteReader())
                    {
                        while (DR.Read())
                        {
                            lista.Add(new Marca()
                            {
                                marc_ID = Convert.ToInt32(DR["marc_ID"]),
                                marc_Descripcion = DR["marc_Descripcion"].ToString(),
                                marc_Activo = Convert.ToBoolean(DR["marc_Activo"])
                            });
                        }
                    }
                }

            }
            catch
            {
                lista = new List<Marca>();
            }
            return lista;
        }

        #endregion 

        #region REGISTRAR

        public int Registrar(Marca obj, out string Mensaje)
        {
            int idautogenerado = 0;

            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.CN))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarMarca", oconexion);
                    cmd.Parameters.AddWithValue("Descripcion", obj.marc_Descripcion);
                    cmd.Parameters.AddWithValue("Activo", obj.marc_Activo);
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

        #endregion

        #region EDITAR

        public bool Editar(Marca obj, out string Mensaje)
        {
            bool Resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.CN))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarMarca", oconexion);
                    cmd.Parameters.AddWithValue("IdMarca", obj.marc_ID);
                    cmd.Parameters.AddWithValue("Descripcion", obj.marc_Descripcion);
                    cmd.Parameters.AddWithValue("Activo", obj.marc_Activo);
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
                Resultado = false;
                Mensaje = ex.Message;
            }
            return Resultado;
        }

        #endregion

        #region ELIMINAR

        public bool Eliminar(int marc_ID, out string Mensaje)
        {
            bool Resultado = false;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.CN))
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarMarca", oconexion);
                    cmd.Parameters.AddWithValue("IdMarca", marc_ID);
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

        #endregion
        #region LISTARporCategorai

        public List<Marca> ListarMarcaPorCategoria(int cate_ID)
        {
            List<Marca> lista = new List<Marca>();
            try
            {
                using (SqlConnection Oconexion = new SqlConnection(Conexion.CN))
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("select distinct m.marc_ID,m.marc_Descripcion from Producto p");
                    sb.AppendLine("join Categoria c on c.cate_ID = p.cate_ID");
                    sb.AppendLine("join Marca m on m.marc_ID = p.marc_ID and m.marc_Activo");
                    sb.AppendLine("where c.cate_ID = iff(@cate_ID = 0,c.cate_ID, @cate_ID)");


                    SqlCommand cmd = new SqlCommand(sb.ToString(), Oconexion);

                    cmd.Parameters.AddWithValue("@cate_ID", cate_ID);
                    cmd.CommandType = CommandType.Text;

                    Oconexion.Open();
                    using (SqlDataReader DR = cmd.ExecuteReader())
                    {
                        while (DR.Read())
                        {
                            lista.Add(new Marca()
                            {
                                marc_ID = Convert.ToInt32(DR["marc_ID"]),
                                marc_Descripcion = DR["marc_Descripcion"].ToString(),
                            });
                        }
                    }
                }

            }
            catch
            {
                lista = new List<Marca>();
            }
            return lista;
        }

        #endregion 
    }

}
