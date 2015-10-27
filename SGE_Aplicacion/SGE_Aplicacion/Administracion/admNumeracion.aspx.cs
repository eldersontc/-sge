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
    public partial class admNumeracion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        [WebMethod]
        public static object ObtenerTodos()
        {
            object resultado = new { };
            try
            {
                blNumeracion blNumeracion = new blNumeracion();
                IList<Numeracion> numeraciones = blNumeracion.ObtenerTodos();
                resultado = new { correcto = true, numeraciones = numeraciones };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Agregar(Numeracion numeracion)
        {
            object resultado = new { };
            try
            {
                blNumeracion blNumeracion = new blNumeracion();
                blNumeracion.Agregar(numeracion);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Actualizar(Numeracion numeracion)
        {
            object resultado = new { };
            try
            {
                blNumeracion blNumeracion = new blNumeracion();
                blNumeracion.Actualizar(numeracion);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Eliminar(int idNumeracion)
        {
            object resultado = new { };
            try
            {
                blNumeracion blNumeracion = new blNumeracion();
                blNumeracion.Eliminar(idNumeracion);
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