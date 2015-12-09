using SGE.Entidades.Administracion;
using SGE.Entidades.Inventarios;
using SGE.Negocios.Inventarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGE.Aplicacion.Inventarios
{
    public partial class invIngInventario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        [WebMethod]
        public static object ObtenerTodos(Sesion sesion)
        {
            object resultado = new { };
            try
            {
                blIngInventario blIngInventario = new blIngInventario(sesion);
                IList<IngInventario> ingresos = blIngInventario.ObtenerTodos();
                resultado = new { correcto = true, ingresos = ingresos };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Agregar(Sesion sesion, IngInventario ingreso)
        {
            object resultado = new { };
            try
            {
                blIngInventario blIngInventario = new blIngInventario(sesion);
                blIngInventario.Agregar(ingreso);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Eliminar(Sesion sesion, int idIngInventario)
        {
            object resultado = new { };
            try
            {
                blIngInventario blIngInventario = new blIngInventario(sesion);
                blIngInventario.Eliminar(idIngInventario);
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