using SGE.Entidades.Administracion;
using SGE.Entidades.Facturacion;
using SGE.Negocios.GuiaRemisioncion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGE.Aplicacion.GuiaRemisioncion
{
    public partial class facGuiaRemision : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        [WebMethod]
        public static object ObtenerTodos(Sesion sesion)
        {
            object resultado = new { };
            try
            {
                blGuiaRemision blGuiaRemision = new blGuiaRemision(sesion);
                IList<GuiaRemision> guias = blGuiaRemision.ObtenerTodos();
                resultado = new { correcto = true, guias = guias };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Agregar(Sesion sesion, GuiaRemision guia)
        {
            object resultado = new { };
            try
            {
                blGuiaRemision blGuiaRemision = new blGuiaRemision(sesion);
                blGuiaRemision.Agregar(guia);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Eliminar(Sesion sesion, int idGuiaRemision)
        {
            object resultado = new { };
            try
            {
                blGuiaRemision blGuiaRemision = new blGuiaRemision(sesion);
                blGuiaRemision.Eliminar(idGuiaRemision);
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