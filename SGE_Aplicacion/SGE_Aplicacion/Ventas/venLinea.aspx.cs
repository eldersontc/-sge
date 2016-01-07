using SGE.Entidades.Administracion;
using SGE.Entidades.Ventas;
using SGE.Negocios.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGE.Aplicacion.Ventas
{
    public partial class venLinea : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        [WebMethod]
        public static object ObtenerTodos(Sesion sesion)
        {
            object resultado = new { };
            try
            {
                blLinea blLinea = new blLinea(sesion);
                IList<Linea> lineas = blLinea.ObtenerTodos();
                resultado = new { correcto = true, lineas = lineas };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object ObtenerActivos(Sesion sesion)
        {
            object resultado = new { };
            try
            {
                blLinea blLinea = new blLinea(sesion);
                IList<Linea> lineas = blLinea.ObtenerActivos();
                resultado = new { correcto = true, lineas = lineas };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Agregar(Sesion sesion, Linea linea)
        {
            object resultado = new { };
            try
            {
                blLinea blLinea = new blLinea(sesion);
                blLinea.Agregar(linea);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Actualizar(Sesion sesion, Linea linea)
        {
            object resultado = new { };
            try
            {
                blLinea blLinea = new blLinea(sesion);
                blLinea.Actualizar(linea);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Eliminar(Sesion sesion, int idLinea)
        {
            object resultado = new { };
            try
            {
                blLinea blLinea = new blLinea(sesion);
                blLinea.Eliminar(idLinea);
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