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
    public partial class invSalInventario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        [WebMethod]
        public static object ObtenerTodos(Sesion sesion)
        {
            object resultado = new { };
            try
            {
                blSalInventario blSalInventario = new blSalInventario(sesion);
                IList<SalInventario> salidas = blSalInventario.ObtenerTodos();
                resultado = new { correcto = true, salidas = salidas };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Agregar(Sesion sesion, SalInventario salida)
        {
            object resultado = new { };
            try
            {
                blSalInventario blSalInventario = new blSalInventario(sesion);
                blSalInventario.Agregar(salida);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Eliminar(Sesion sesion, int idSalInventario)
        {
            object resultado = new { };
            try
            {
                blSalInventario blSalInventario = new blSalInventario(sesion);
                blSalInventario.Eliminar(idSalInventario);
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