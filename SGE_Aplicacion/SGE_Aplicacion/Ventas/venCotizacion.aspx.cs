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
    public partial class venCotizacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        [WebMethod]
        public static object ObtenerTodos(Sesion sesion)
        {
            object resultado = new { };
            try
            {
                blCotizacion blCotizacion = new blCotizacion(sesion);
                IList<Cotizacion> cotizaciones = blCotizacion.ObtenerTodos();
                resultado = new { correcto = true, cotizaciones = cotizaciones };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Agregar(Sesion sesion, Cotizacion cotizacion)
        {
            object resultado = new { };
            try
            {
                blCotizacion blCotizacion = new blCotizacion(sesion);
                blCotizacion.Agregar(cotizacion);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Actualizar(Sesion sesion, Cotizacion cotizacion)
        {
            object resultado = new { };
            try
            {
                blCotizacion blCotizacion = new blCotizacion(sesion);
                blCotizacion.Actualizar(cotizacion);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Eliminar(Sesion sesion, int idCotizacion)
        {
            object resultado = new { };
            try
            {
                blCotizacion blCotizacion = new blCotizacion(sesion);
                blCotizacion.Eliminar(idCotizacion);
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