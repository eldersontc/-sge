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
    public partial class admMoneda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        [WebMethod]
        public static object ObtenerTodos(Sesion sesion)
        {
            object resultado = new { };
            try
            {
                blMoneda blMoneda = new blMoneda(sesion);
                IList<Moneda> monedas = blMoneda.ObtenerTodos();
                resultado = new { correcto = true, monedas = monedas };
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
                blMoneda blMoneda = new blMoneda(sesion);
                IList<Moneda> monedas = blMoneda.ObtenerActivos();
                resultado = new { correcto = true, monedas = monedas };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Agregar(Sesion sesion, Moneda moneda)
        {
            object resultado = new { };
            try
            {
                blMoneda blMoneda = new blMoneda(sesion);
                blMoneda.Agregar(moneda);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Actualizar(Sesion sesion, Moneda moneda)
        {
            object resultado = new { };
            try
            {
                blMoneda blMoneda = new blMoneda(sesion);
                blMoneda.Actualizar(moneda);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Eliminar(Sesion sesion, int idMoneda)
        {
            object resultado = new { };
            try
            {
                blMoneda blMoneda = new blMoneda(sesion);
                blMoneda.Eliminar(idMoneda);
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