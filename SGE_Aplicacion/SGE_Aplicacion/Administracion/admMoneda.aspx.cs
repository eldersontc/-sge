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
        public static object ObtenerTodos()
        {
            object resultado = new { };
            try
            {
                blMoneda blMoneda = new blMoneda();
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
        public static object Agregar(Moneda moneda)
        {
            object resultado = new { };
            try
            {
                blMoneda blMoneda = new blMoneda();
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
        public static object Actualizar(Moneda moneda)
        {
            object resultado = new { };
            try
            {
                blMoneda blMoneda = new blMoneda();
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
        public static object Eliminar(int idMoneda)
        {
            object resultado = new { };
            try
            {
                blMoneda blMoneda = new blMoneda();
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