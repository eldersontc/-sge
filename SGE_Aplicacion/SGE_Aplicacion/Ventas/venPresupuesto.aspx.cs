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
        public static object ObtenerTodos(Sesion sesion, Paginacion paginacion, Orden orden)
        {
            object resultado = new { };
            try
            {
                blPresupuesto blPresupuesto = new blPresupuesto(sesion);
                object[] datos = blPresupuesto.ObtenerTodos(paginacion, orden);
                resultado = new { correcto = true, presupuestos = datos[0], total = datos[1] };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object ObtenerPorId(Sesion sesion, int idPresupuesto)
        {
            object resultado = new { };
            try
            {
                blPresupuesto blPresupuesto = new blPresupuesto(sesion);
                Presupuesto presupuesto = blPresupuesto.ObtenerPorId(idPresupuesto);
                resultado = new { correcto = true, presupuesto = presupuesto };
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
        public static object Eliminar(Sesion sesion, List<int> ids)
        {
            object resultado = new { };
            try
            {
                blPresupuesto blPresupuesto = new blPresupuesto(sesion);
                blPresupuesto.Eliminar(ids);
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