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
    public partial class prdOrdenProduccion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        [WebMethod]
        public static object ObtenerTodos(Sesion sesion)
        {
            object resultado = new { };
            try
            {
                blOrdenProduccion blOrdenProduccion = new blOrdenProduccion(sesion);
                IList<OrdenProduccion> ordenes = blOrdenProduccion.ObtenerTodos();
                resultado = new { correcto = true, ordenes = ordenes };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Agregar(Sesion sesion, OrdenProduccion orden)
        {
            object resultado = new { };
            try
            {
                blOrdenProduccion blOrdenProduccion = new blOrdenProduccion(sesion);
                blOrdenProduccion.Agregar(orden);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Actualizar(Sesion sesion, OrdenProduccion orden)
        {
            object resultado = new { };
            try
            {
                blOrdenProduccion blOrdenProduccion = new blOrdenProduccion(sesion);
                blOrdenProduccion.Actualizar(orden);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Eliminar(Sesion sesion, int idOrdenProduccion)
        {
            object resultado = new { };
            try
            {
                blOrdenProduccion blOrdenProduccion = new blOrdenProduccion(sesion);
                blOrdenProduccion.Eliminar(idOrdenProduccion);
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