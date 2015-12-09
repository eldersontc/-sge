using SGE.Entidades.Administracion;
using SGE.Entidades.Produccion;
using SGE.Negocios.Produccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGE.Aplicacion.Produccion
{
    public partial class prdOrdenTrabajo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        [WebMethod]
        public static object ObtenerTodos(Sesion sesion)
        {
            object resultado = new { };
            try
            {
                blOrdenTrabajo blOrdenTrabajo = new blOrdenTrabajo(sesion);
                IList<OrdenTrabajo> ordenes = blOrdenTrabajo.ObtenerTodos();
                resultado = new { correcto = true, ordenes = ordenes };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Agregar(Sesion sesion, OrdenTrabajo orden)
        {
            object resultado = new { };
            try
            {
                blOrdenTrabajo blOrdenTrabajo = new blOrdenTrabajo(sesion);
                blOrdenTrabajo.Agregar(orden);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Actualizar(Sesion sesion, OrdenTrabajo orden)
        {
            object resultado = new { };
            try
            {
                blOrdenTrabajo blOrdenTrabajo = new blOrdenTrabajo(sesion);
                blOrdenTrabajo.Actualizar(orden);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Eliminar(Sesion sesion, int idOrdenTrabajo)
        {
            object resultado = new { };
            try
            {
                blOrdenTrabajo blOrdenTrabajo = new blOrdenTrabajo(sesion);
                blOrdenTrabajo.Eliminar(idOrdenTrabajo);
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