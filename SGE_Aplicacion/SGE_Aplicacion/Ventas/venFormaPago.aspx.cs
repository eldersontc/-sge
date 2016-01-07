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
    public partial class venFormaPago : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        [WebMethod]
        public static object ObtenerTodos(Sesion sesion)
        {
            object resultado = new { };
            try
            {
                blFormaPago blFormaPago = new blFormaPago(sesion);
                IList<FormaPago> formasPago = blFormaPago.ObtenerTodos();
                resultado = new { correcto = true, formasPago = formasPago };
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
                blFormaPago blFormaPago = new blFormaPago(sesion);
                IList<FormaPago> formasPago = blFormaPago.ObtenerActivos();
                resultado = new { correcto = true, formasPago = formasPago };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Agregar(Sesion sesion, FormaPago forma)
        {
            object resultado = new { };
            try
            {
                blFormaPago blFormaPago = new blFormaPago(sesion);
                blFormaPago.Agregar(forma);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Actualizar(Sesion sesion, FormaPago forma)
        {
            object resultado = new { };
            try
            {
                blFormaPago blFormaPago = new blFormaPago(sesion);
                blFormaPago.Actualizar(forma);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Eliminar(Sesion sesion, int idFormaPago)
        {
            object resultado = new { };
            try
            {
                blFormaPago blFormaPago = new blFormaPago(sesion);
                blFormaPago.Eliminar(idFormaPago);
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