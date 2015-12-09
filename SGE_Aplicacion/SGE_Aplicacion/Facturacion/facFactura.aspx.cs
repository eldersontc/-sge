using SGE.Entidades.Administracion;
using SGE.Entidades.Facturacion;
using SGE.Negocios.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGE.Aplicacion.Facturacion
{
    public partial class facFactura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        [WebMethod]
        public static object ObtenerTodos(Sesion sesion)
        {
            object resultado = new { };
            try
            {
                blFactura blFactura = new blFactura(sesion);
                IList<Factura> facturas = blFactura.ObtenerTodos();
                resultado = new { correcto = true, facturas = facturas };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Agregar(Sesion sesion, Factura factura)
        {
            object resultado = new { };
            try
            {
                blFactura blFactura = new blFactura(sesion);
                blFactura.Agregar(factura);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Eliminar(Sesion sesion, int idFactura)
        {
            object resultado = new { };
            try
            {
                blFactura blFactura = new blFactura(sesion);
                blFactura.Eliminar(idFactura);
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