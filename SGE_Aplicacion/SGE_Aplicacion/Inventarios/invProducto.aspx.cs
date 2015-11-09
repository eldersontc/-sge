using SGE.Entidades.Administracion;
using SGE.Entidades.Inventarios;
using SGE.Negocios.Inventarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGE.Aplicacion.Inventarios
{
    public partial class invProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        [WebMethod]
        public static object ObtenerTodos(Sesion sesion)
        {
            object resultado = new { };
            try
            {
                blProducto blProducto = new blProducto(sesion);
                IList<Producto> productos = blProducto.ObtenerTodos();
                resultado = new { correcto = true, productos = productos };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Agregar(Sesion sesion, Producto producto)
        {
            object resultado = new { };
            try
            {
                blProducto blProducto = new blProducto(sesion);
                blProducto.Agregar(producto);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Actualizar(Sesion sesion, Producto producto)
        {
            object resultado = new { };
            try
            {
                blProducto blProducto = new blProducto(sesion);
                blProducto.Actualizar(producto);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Eliminar(Sesion sesion, int idProducto)
        {
            object resultado = new { };
            try
            {
                blProducto blProducto = new blProducto(sesion);
                blProducto.Eliminar(idProducto);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }
    }
}