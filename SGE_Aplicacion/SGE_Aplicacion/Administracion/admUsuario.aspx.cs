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
    public partial class admUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        [WebMethod]
        public static object ObtenerTodos(Sesion sesion)
        {
            object resultado = new { };
            try
            {
                blUsuario blUsuario = new blUsuario(sesion);
                IList<Usuario> usuarios = blUsuario.ObtenerTodos();
                resultado = new { correcto = true, usuarios = usuarios };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Agregar(Sesion sesion, Usuario usuario)
        {
            object resultado = new { };
            try
            {
                blUsuario blUsuario = new blUsuario(sesion);
                blUsuario.Agregar(usuario);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Actualizar(Sesion sesion, Usuario usuario)
        {
            object resultado = new { };
            try
            {
                blUsuario blUsuario = new blUsuario(sesion);
                blUsuario.Actualizar(usuario);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Eliminar(Sesion sesion, int idUsuario)
        {
            object resultado = new { };
            try
            {
                blUsuario blUsuario = new blUsuario(sesion);
                blUsuario.Eliminar(idUsuario);
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