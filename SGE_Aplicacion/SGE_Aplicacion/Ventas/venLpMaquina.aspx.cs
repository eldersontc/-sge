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
    public partial class venLpMaquina : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        [WebMethod]
        public static object ObtenerTodos(Sesion sesion)
        {
            object resultado = new { };
            try
            {
                blLpMaquina blLpMaquina = new blLpMaquina(sesion);
                IList<LpMaquina> listas = blLpMaquina.ObtenerActivos();
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
                blLpMaquina blLpMaquina = new blLpMaquina(sesion);
                IList<LpMaquina> listaes = blLpMaquina.ObtenerActivos();
                resultado = new { correcto = true, listaes = listaes };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Agregar(Sesion sesion, LpMaquina lista)
        {
            object resultado = new { };
            try
            {
                blLpMaquina blLpMaquina = new blLpMaquina(sesion);
                blLpMaquina.Agregar(lista);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Actualizar(Sesion sesion, LpMaquina lista)
        {
            object resultado = new { };
            try
            {
                blLpMaquina blLpMaquina = new blLpMaquina(sesion);
                blLpMaquina.Actualizar(lista);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Eliminar(Sesion sesion, int idLpMaquina)
        {
            object resultado = new { };
            try
            {
                blLpMaquina blLpMaquina = new blLpMaquina(sesion);
                blLpMaquina.Eliminar(idLpMaquina);
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