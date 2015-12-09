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
    public partial class venMaquina : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        [WebMethod]
        public static object ObtenerTodos(Sesion sesion)
        {
            object resultado = new { };
            try
            {
                blMaquina blMaquina = new blMaquina(sesion);
                IList<Maquina> maquinas = blMaquina.ObtenerTodos();
                resultado = new { correcto = true, maquinas = maquinas };
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
                blMaquina blMaquina = new blMaquina(sesion);
                IList<Maquina> maquinaes = blMaquina.ObtenerActivos();
                resultado = new { correcto = true, maquinaes = maquinaes };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Agregar(Sesion sesion, Maquina maquina)
        {
            object resultado = new { };
            try
            {
                blMaquina blMaquina = new blMaquina(sesion);
                blMaquina.Agregar(maquina);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Actualizar(Sesion sesion, Maquina maquina)
        {
            object resultado = new { };
            try
            {
                blMaquina blMaquina = new blMaquina(sesion);
                blMaquina.Actualizar(maquina);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Eliminar(Sesion sesion, int idMaquina)
        {
            object resultado = new { };
            try
            {
                blMaquina blMaquina = new blMaquina(sesion);
                blMaquina.Eliminar(idMaquina);
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