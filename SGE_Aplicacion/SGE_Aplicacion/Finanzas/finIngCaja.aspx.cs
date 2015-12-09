using SGE.Entidades.Administracion;
using SGE.Entidades.Finanzas;
using SGE.Negocios.Finanzas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGE.Aplicacion.Finanzas
{
    public partial class finIngCaja : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        [WebMethod]
        public static object ObtenerTodos(Sesion sesion)
        {
            object resultado = new { };
            try
            {
                blIngCaja blIngCaja = new blIngCaja(sesion);
                IList<IngCaja> ingresos = blIngCaja.ObtenerTodos();
                resultado = new { correcto = true, ingresos = ingresos };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Agregar(Sesion sesion, IngCaja ingreso)
        {
            object resultado = new { };
            try
            {
                blIngCaja blIngCaja = new blIngCaja(sesion);
                blIngCaja.Agregar(ingreso);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Eliminar(Sesion sesion, int idIngCaja)
        {
            object resultado = new { };
            try
            {
                blIngCaja blIngCaja = new blIngCaja(sesion);
                blIngCaja.Eliminar(idIngCaja);
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