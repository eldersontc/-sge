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
    public partial class venServicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        [WebMethod]
        public static object ObtenerTodos(Sesion sesion)
        {
            object resultado = new { };
            try
            {
                blServicio blServicio = new blServicio(sesion);
                IList<Servicio> servicios = blServicio.ObtenerTodos();
                resultado = new { correcto = true, servicios = servicios };
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
                blServicio blServicio = new blServicio(sesion);
                IList<Servicio> servicioes = blServicio.ObtenerActivos();
                resultado = new { correcto = true, servicioes = servicioes };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Agregar(Sesion sesion, Servicio servicio)
        {
            object resultado = new { };
            try
            {
                blServicio blServicio = new blServicio(sesion);
                blServicio.Agregar(servicio);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Actualizar(Sesion sesion, Servicio servicio)
        {
            object resultado = new { };
            try
            {
                blServicio blServicio = new blServicio(sesion);
                blServicio.Actualizar(servicio);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Eliminar(Sesion sesion, int idServicio)
        {
            object resultado = new { };
            try
            {
                blServicio blServicio = new blServicio(sesion);
                blServicio.Eliminar(idServicio);
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