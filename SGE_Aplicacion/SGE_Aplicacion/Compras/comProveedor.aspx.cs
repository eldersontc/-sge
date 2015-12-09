using SGE.Entidades.Administracion;
using SGE.Entidades.Compras;
using SGE.Negocios.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGE.Aplicacion.Compras
{
    public partial class comProveedor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        [WebMethod]
        public static object ObtenerTodos(Sesion sesion)
        {
            object resultado = new { };
            try
            {
                blProveedor blProveedor = new blProveedor(sesion);
                IList<Proveedor> proveedores = blProveedor.ObtenerTodos();
                resultado = new { correcto = true, proveedores = proveedores };
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
                blProveedor blProveedor = new blProveedor(sesion);
                IList<Proveedor> proveedores = blProveedor.ObtenerActivos();
                resultado = new { correcto = true, proveedores = proveedores };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Agregar(Sesion sesion, Proveedor proveedor)
        {
            object resultado = new { };
            try
            {
                blProveedor blProveedor = new blProveedor(sesion);
                blProveedor.Agregar(proveedor);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Actualizar(Sesion sesion, Proveedor proveedor)
        {
            object resultado = new { };
            try
            {
                blProveedor blProveedor = new blProveedor(sesion);
                blProveedor.Actualizar(proveedor);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Eliminar(Sesion sesion, int idProveedor)
        {
            object resultado = new { };
            try
            {
                blProveedor blProveedor = new blProveedor(sesion);
                blProveedor.Eliminar(idProveedor);
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