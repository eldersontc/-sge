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
    public partial class invAlmacen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        [WebMethod]
        public static object ObtenerTodos(Sesion sesion)
        {
            object resultado = new { };
            try
            {
                blAlmacen blAlmacen = new blAlmacen(sesion);
                IList<Almacen> almacenes = blAlmacen.ObtenerTodos();
                resultado = new { correcto = true, almacenes = almacenes };
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
                blAlmacen blAlmacen = new blAlmacen(sesion);
                IList<Almacen> almacenes = blAlmacen.ObtenerActivos();
                resultado = new { correcto = true, almacenes = almacenes };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Agregar(Sesion sesion, Almacen almacen)
        {
            object resultado = new { };
            try
            {
                blAlmacen blAlmacen = new blAlmacen(sesion);
                blAlmacen.Agregar(almacen);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Actualizar(Sesion sesion, Almacen almacen)
        {
            object resultado = new { };
            try
            {
                blAlmacen blAlmacen = new blAlmacen(sesion);
                blAlmacen.Actualizar(almacen);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Eliminar(Sesion sesion, int idAlmacen)
        {
            object resultado = new { };
            try
            {
                blAlmacen blAlmacen = new blAlmacen(sesion);
                blAlmacen.Eliminar(idAlmacen);
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