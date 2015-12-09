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
    public partial class venLpServicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        [WebMethod]
        public static object ObtenerTodos(Sesion sesion)
        {
            object resultado = new { };
            try
            {
                blLpServicio blLpServicio = new blLpServicio(sesion);
                IList<LpServicio> listas = blLpServicio.ObtenerActivos();
                resultado = new { correcto = true, listas = listas };
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
                blLpServicio blLpServicio = new blLpServicio(sesion);
                IList<LpServicio> listaes = blLpServicio.ObtenerActivos();
                resultado = new { correcto = true, listaes = listaes };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Agregar(Sesion sesion, LpServicio lista)
        {
            object resultado = new { };
            try
            {
                blLpServicio blLpServicio = new blLpServicio(sesion);
                blLpServicio.Agregar(lista);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Actualizar(Sesion sesion, LpServicio lista)
        {
            object resultado = new { };
            try
            {
                blLpServicio blLpServicio = new blLpServicio(sesion);
                blLpServicio.Actualizar(lista);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Eliminar(Sesion sesion, int idLpServicio)
        {
            object resultado = new { };
            try
            {
                blLpServicio blLpServicio = new blLpServicio(sesion);
                blLpServicio.Eliminar(idLpServicio);
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