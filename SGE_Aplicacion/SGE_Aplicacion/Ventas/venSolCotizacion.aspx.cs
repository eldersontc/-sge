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
    public partial class venSolCotizacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        [WebMethod]
        public static object ObtenerTodos(Sesion sesion)
        {
            object resultado = new { };
            try
            {
                blSolCotizacion blSolCotizacion = new blSolCotizacion(sesion);
                IList<SolCotizacion> solicitudes = blSolCotizacion.ObtenerTodos();
                resultado = new { correcto = true, solicitudes = solicitudes };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Agregar(Sesion sesion, SolCotizacion solicitud)
        {
            object resultado = new { };
            try
            {
                blSolCotizacion blSolCotizacion = new blSolCotizacion(sesion);
                blSolCotizacion.Agregar(solicitud);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Actualizar(Sesion sesion, SolCotizacion solicitud)
        {
            object resultado = new { };
            try
            {
                blSolCotizacion blSolCotizacion = new blSolCotizacion(sesion);
                blSolCotizacion.Actualizar(solicitud);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Eliminar(Sesion sesion, int idSolCotizacion)
        {
            object resultado = new { };
            try
            {
                blSolCotizacion blSolCotizacion = new blSolCotizacion(sesion);
                blSolCotizacion.Eliminar(idSolCotizacion);
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