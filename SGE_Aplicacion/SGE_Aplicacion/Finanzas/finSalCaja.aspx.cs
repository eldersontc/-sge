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
    public partial class finSalCaja : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        [WebMethod]
        public static object ObtenerTodos(Sesion sesion)
        {
            object resultado = new { };
            try
            {
                blSalCaja blSalCaja = new blSalCaja(sesion);
                IList<SalCaja> salidas = blSalCaja.ObtenerTodos();
                resultado = new { correcto = true, salidas = salidas };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Agregar(Sesion sesion, SalCaja salida)
        {
            object resultado = new { };
            try
            {
                blSalCaja blSalCaja = new blSalCaja(sesion);
                blSalCaja.Agregar(salida);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Eliminar(Sesion sesion, int idSalCaja)
        {
            object resultado = new { };
            try
            {
                blSalCaja blSalCaja = new blSalCaja(sesion);
                blSalCaja.Eliminar(idSalCaja);
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