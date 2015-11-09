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
    public partial class invUnidad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        [WebMethod]
        public static object ObtenerTodos(Sesion sesion)
        {
            object resultado = new { };
            try
            {
                blUnidad blUnidad = new blUnidad(sesion);
                IList<Unidad> unidades = blUnidad.ObtenerTodos();
                resultado = new { correcto = true, unidades = unidades };
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
                blUnidad blUnidad = new blUnidad(sesion);
                IList<Unidad> unidades = blUnidad.ObtenerActivos();
                resultado = new { correcto = true, unidades = unidades };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Agregar(Sesion sesion, Unidad unidad)
        {
            object resultado = new { };
            try
            {
                blUnidad blUnidad = new blUnidad(sesion);
                blUnidad.Agregar(unidad);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Actualizar(Sesion sesion, Unidad unidad)
        {
            object resultado = new { };
            try
            {
                blUnidad blUnidad = new blUnidad(sesion);
                blUnidad.Actualizar(unidad);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Eliminar(Sesion sesion, int idUnidad)
        {
            object resultado = new { };
            try
            {
                blUnidad blUnidad = new blUnidad(sesion);
                blUnidad.Eliminar(idUnidad);
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