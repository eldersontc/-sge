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
    public partial class venCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        [WebMethod]
        public static object ObtenerTodos(Sesion sesion)
        {
            object resultado = new { };
            try
            {
                blCliente blCliente = new blCliente(sesion);
                IList<Cliente> clientes = blCliente.ObtenerTodos();
                resultado = new { correcto = true, clientes = clientes };
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
                blCliente blCliente = new blCliente(sesion);
                IList<Cliente> clientes = blCliente.ObtenerActivos();
                resultado = new { correcto = true, clientes = clientes };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object ObtenerContactos(Sesion sesion, int idCliente)
        {
            object resultado = new { };
            try
            {
                blCliente blCliente = new blCliente(sesion);
                IList<ClienteContacto> contactos = blCliente.ObtenerContactos(idCliente);
                resultado = new { correcto = true, contactos = contactos };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Agregar(Sesion sesion, Cliente cliente)
        {
            object resultado = new { };
            try
            {
                blCliente blCliente = new blCliente(sesion);
                blCliente.Agregar(cliente);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Actualizar(Sesion sesion, Cliente cliente)
        {
            object resultado = new { };
            try
            {
                blCliente blCliente = new blCliente(sesion);
                blCliente.Actualizar(cliente);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Eliminar(Sesion sesion, int idCliente)
        {
            object resultado = new { };
            try
            {
                blCliente blCliente = new blCliente(sesion);
                blCliente.Eliminar(idCliente);
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