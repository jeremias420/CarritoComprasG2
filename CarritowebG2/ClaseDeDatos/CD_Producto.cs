using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaseEntidades;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace ClaseDeDatos
{
    public class CD_Producto
    {

        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();
            try
            {
                using (SqlConnection Oconexion = new SqlConnection(Conexion.CN))
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("select prod_ID, prod_Nombre, prod_Descripcion,");
                    sb.AppendLine("marc_ID, marc_Descripcion,");
                    sb.AppendLine("cate_ID, cate_Descripcion,");
                    sb.AppendLine("prod_Precio, prod_Stock, prod_RutaImagen, prod_NombreImagen, prod_Activo");
                    sb.AppendLine("from Producto");
                    sb.AppendLine("join Marca on marc_ID = prod_marc_ID");
                    sb.AppendLine("join Categoria on cate_ID = prod_cate_ID");


                    SqlCommand cmd = new SqlCommand(sb.ToString(), Oconexion);
                    cmd.CommandType = CommandType.Text;

                    Oconexion.Open();

                    using (SqlDataReader DR = cmd.ExecuteReader())
                    {
                        while (DR.Read())
                        {
                            lista.Add(new Producto()
                            {
                                prod_ID = Convert.ToInt32(DR["prod_ID"]),
                                prod_Nombre = DR["prod_Nombre"].ToString(),
                                prod_Descripcion = DR["prod_Descripcion"].ToString(),
                                prod_marc_ID = new Marca() {marc_ID = Convert.ToInt32(DR["marc_ID"]), marc_Descripcion = DR["marc_Descripcion"].ToString()},
                                prod_cate_ID = new Categoria() { cate_ID = Convert.ToInt32(DR["cate_ID"]), cate_Descripcion = DR["cate_Descripcion"].ToString()},
                                prod_Precio = Convert.ToDecimal(DR["prod_Precio"], new CultureInfo("es-AR")),
                                prod_Stock = Convert.ToInt32(DR["prod_Stock"]),
                                prod_RutaImagen = DR["prod_RutaImagen"].ToString(),
                                prod_NombreImagen = DR["prod_NombreImagen"].ToString(),
                                prod_Activo = Convert.ToBoolean(DR["prod_Activo"])
                            });
                        }
                    }
                }

            }
            catch
            {
                lista = new List<Producto>();
            }
            return lista;
        }
        public int Registrar(Producto obj, out string Mensaje)
        {
            int idautogenerado = 0;

            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.CN))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarProducto", oconexion);
                    cmd.Parameters.AddWithValue("Nombre", obj.prod_Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.prod_Descripcion);
                    cmd.Parameters.AddWithValue("marc_ID", obj.prod_marc_ID);
                    cmd.Parameters.AddWithValue("cate_ID", obj.prod_cate_ID);
                    cmd.Parameters.AddWithValue("prod_Precio", obj.prod_Precio);
                    cmd.Parameters.AddWithValue("prod_Stock", obj.prod_Stock);
                    cmd.Parameters.AddWithValue("Activo", obj.prod_Activo);
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

        public bool Editar(Producto obj, out string Mensaje)
        {
            bool Resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.CN))
                {
                    SqlCommand cmd = new SqlCommand("sp_Editarproducto", oconexion);
                    cmd.Parameters.AddWithValue("Idproducto", obj.prod_ID);
                    cmd.Parameters.AddWithValue("Nombre", obj.prod_Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.prod_Descripcion);
                    cmd.Parameters.AddWithValue("marc_ID", obj.prod_marc_ID);
                    cmd.Parameters.AddWithValue("cate_ID", obj.prod_cate_ID);
                    cmd.Parameters.AddWithValue("prod_Precio", obj.prod_Precio);
                    cmd.Parameters.AddWithValue("prod_Stock", obj.prod_Stock);
                    cmd.Parameters.AddWithValue("Activo", obj.prod_Activo);
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

        public bool GuardarDatosImagen(Producto obj, out string mensaje)
        {

            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.CN))
                {

                   // string query = "update producto set prod_RutaImagen = @RutaImagen, prod_NombreImagen = @NombreImagen where prod_ID = @prod_ID";

                    SqlCommand cmd = new SqlCommand("sp_Editarproducto", oconexion);
                    cmd.Parameters.AddWithValue("Idproducto", obj.prod_RutaImagen);
                    cmd.Parameters.AddWithValue("Nombre", obj.prod_NombreImagen);
                    cmd.Parameters.AddWithValue("Descripcion", obj.prod_ID);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();

                   if ( cmd.ExecuteNonQuery() > 0)
                    {
                        resultado = true;
                    }else
                    {
                        mensaje = "No se pudo actualizar Imagen";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }
            return resultado;


        }



        public bool Eliminar(int prod_ID, out string Mensaje)
        {
            bool Resultado = false;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.CN))
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarProducto", oconexion);
                    cmd.Parameters.AddWithValue("IdProducto", prod_ID);
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
