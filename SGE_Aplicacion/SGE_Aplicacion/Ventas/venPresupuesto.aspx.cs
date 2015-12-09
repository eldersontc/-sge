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
    public partial class venPresupuesto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        [WebMethod]
        public static object ObtenerTodos(Sesion sesion)
        {
            object resultado = new { };
            try
            {
                blPresupuesto blPresupuesto = new blPresupuesto(sesion);
                IList<Presupuesto> presupuestos = blPresupuesto.ObtenerTodos();
                resultado = new { correcto = true, presupuestos = presupuestos };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Agregar(Sesion sesion, Presupuesto presupuesto)
        {
            object resultado = new { };
            try
            {
                blPresupuesto blPresupuesto = new blPresupuesto(sesion);
                blPresupuesto.Agregar(presupuesto);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Actualizar(Sesion sesion, Presupuesto presupuesto)
        {
            object resultado = new { };
            try
            {
                blPresupuesto blPresupuesto = new blPresupuesto(sesion);
                blPresupuesto.Actualizar(presupuesto);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Eliminar(Sesion sesion, int idPresupuesto)
        {
            object resultado = new { };
            try
            {
                blPresupuesto blPresupuesto = new blPresupuesto(sesion);
                blPresupuesto.Eliminar(idPresupuesto);
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