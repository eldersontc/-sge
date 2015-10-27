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
        public static object ObtenerTodos()
        {
            object resultado = new { };
            try
            {
                blUsuario blUsuario = new blUsuario();
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
        public static object Agregar(Usuario usuario)
        {
            object resultado = new { };
            try
            {
                blUsuario blUsuario = new blUsuario();
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
        public static object Actualizar(Usuario usuario)
        {
            object resultado = new { };
            try
            {
                blUsuario blUsuario = new blUsuario();
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
        public static object Eliminar(int idUsuario)
        {
            object resultado = new { };
            try
            {
                blUsuario blUsuario = new blUsuario();
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