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
    public partial class admPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        [WebMethod]
        public static object ObtenerTodos(Sesion sesion)
        {
            object resultado = new { };
            try
            {
                blPerfil blPerfil = new blPerfil(sesion);
                IList<Perfil> perfiles = blPerfil.ObtenerTodos();
                resultado = new { correcto = true, perfiles = perfiles };
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
                blPerfil blPerfil = new blPerfil(sesion);
                IList<Perfil> perfiles = blPerfil.ObtenerActivos();
                resultado = new { correcto = true, perfiles = perfiles };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Agregar(Sesion sesion, Perfil perfil)
        {
            object resultado = new { };
            try
            {
                blPerfil blPerfil = new blPerfil(sesion);
                blPerfil.Agregar(perfil);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Actualizar(Sesion sesion, Perfil perfil)
        {
            object resultado = new { };
            try
            {
                blPerfil blPerfil = new blPerfil(sesion);
                blPerfil.Actualizar(perfil);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Eliminar(Sesion sesion, int idPerfil)
        {
            object resultado = new { };
            try
            {
                blPerfil blPerfil = new blPerfil(sesion);
                blPerfil.Eliminar(idPerfil);
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