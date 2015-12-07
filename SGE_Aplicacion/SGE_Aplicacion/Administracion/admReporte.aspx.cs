using SGE.Entidades.Administracion;
using SGE.Negocios.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGE.Aplicacion.Administracion
{
    public partial class admReporte : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        [WebMethod]
        public static object ObtenerTodos(Sesion sesion)
        {
            object resultado = new { };
            try
            {
                blReporte blReporte = new blReporte(sesion);
                IList<Reporte> reportes = blReporte.ObtenerTodos();
                resultado = new { correcto = true, reportes = reportes };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object ObtenerPorId(Sesion sesion, int idReporte)
        {
            object resultado = new { };
            try
            {
                blReporte blReporte = new blReporte(sesion);
                Reporte reporte = blReporte.ObtenerPorId(idReporte);
                resultado = new { correcto = true, reporte = reporte };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Agregar(Sesion sesion, Reporte reporte)
        {
            object resultado = new { };
            try
            {
                blReporte blReporte = new blReporte(sesion);
                blReporte.Agregar(reporte);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Actualizar(Sesion sesion, Reporte reporte)
        {
            object resultado = new { };
            try
            {
                blReporte blReporte = new blReporte(sesion);
                blReporte.Actualizar(reporte);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Eliminar(Sesion sesion, int idReporte)
        {
            object resultado = new { };
            try
            {
                blReporte blReporte = new blReporte(sesion);
                blReporte.Eliminar(idReporte);
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