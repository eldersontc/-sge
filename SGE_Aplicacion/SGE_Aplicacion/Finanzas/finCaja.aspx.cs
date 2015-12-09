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
    public partial class finCaja : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        [WebMethod]
        public static object ObtenerTodos(Sesion sesion)
        {
            object resultado = new { };
            try
            {
                blCaja blCaja = new blCaja(sesion);
                IList<Caja> cajas = blCaja.ObtenerTodos();
                resultado = new { correcto = true, cajas = cajas };
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
                blCaja blCaja = new blCaja(sesion);
                IList<Caja> cajaes = blCaja.ObtenerActivos();
                resultado = new { correcto = true, cajaes = cajaes };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Agregar(Sesion sesion, Caja caja)
        {
            object resultado = new { };
            try
            {
                blCaja blCaja = new blCaja(sesion);
                blCaja.Agregar(caja);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Actualizar(Sesion sesion, Caja caja)
        {
            object resultado = new { };
            try
            {
                blCaja blCaja = new blCaja(sesion);
                blCaja.Actualizar(caja);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Eliminar(Sesion sesion, int idCaja)
        {
            object resultado = new { };
            try
            {
                blCaja blCaja = new blCaja(sesion);
                blCaja.Eliminar(idCaja);
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