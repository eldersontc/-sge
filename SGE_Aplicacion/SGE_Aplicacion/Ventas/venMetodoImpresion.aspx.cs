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
    public partial class venMetodoImpresion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        [WebMethod]
        public static object ObtenerTodos(Sesion sesion)
        {
            object resultado = new { };
            try
            {
                blMetodoImpresion blMetodoImpresion = new blMetodoImpresion(sesion);
                IList<MetodoImpresion> metodos = blMetodoImpresion.ObtenerTodos();
                resultado = new { correcto = true, metodos = metodos };
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
                blMetodoImpresion blMetodoImpresion = new blMetodoImpresion(sesion);
                IList<MetodoImpresion> metodos = blMetodoImpresion.ObtenerActivos();
                resultado = new { correcto = true, metodos = metodos };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Agregar(Sesion sesion, MetodoImpresion metodo)
        {
            object resultado = new { };
            try
            {
                blMetodoImpresion blMetodoImpresion = new blMetodoImpresion(sesion);
                blMetodoImpresion.Agregar(metodo);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Actualizar(Sesion sesion, MetodoImpresion metodo)
        {
            object resultado = new { };
            try
            {
                blMetodoImpresion blMetodoImpresion = new blMetodoImpresion(sesion);
                blMetodoImpresion.Actualizar(metodo);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Eliminar(Sesion sesion, int idMetodoImpresion)
        {
            object resultado = new { };
            try
            {
                blMetodoImpresion blMetodoImpresion = new blMetodoImpresion(sesion);
                blMetodoImpresion.Eliminar(idMetodoImpresion);
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