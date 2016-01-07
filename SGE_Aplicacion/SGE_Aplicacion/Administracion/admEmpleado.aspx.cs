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
    public partial class admEmpleado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        [WebMethod]
        public static object ObtenerTodos(Sesion sesion)
        {
            object resultado = new { };
            try
            {
                blEmpleado blEmpleado = new blEmpleado(sesion);
                IList<Empleado> empleados = blEmpleado.ObtenerTodos();
                resultado = new { correcto = true, empleados = empleados };
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
                blEmpleado blEmpleado = new blEmpleado(sesion);
                IList<Empleado> empleados = blEmpleado.ObtenerActivos();
                resultado = new { correcto = true, empleados = empleados };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object ObtenerVendedores(Sesion sesion)
        {
            object resultado = new { };
            try
            {
                blEmpleado blEmpleado = new blEmpleado(sesion);
                IList<Empleado> vendedores = blEmpleado.ObtenerActivos();
                resultado = new { correcto = true, vendedores = vendedores };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Agregar(Sesion sesion, Empleado empleado)
        {
            object resultado = new { };
            try
            {
                blEmpleado blEmpleado = new blEmpleado(sesion);
                blEmpleado.Agregar(empleado);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Actualizar(Sesion sesion, Empleado empleado)
        {
            object resultado = new { };
            try
            {
                blEmpleado blEmpleado = new blEmpleado(sesion);
                blEmpleado.Actualizar(empleado);
                resultado = new { correcto = true };
            }
            catch (Exception)
            {
                resultado = new { correcto = false };
            }
            return resultado;
        }

        [WebMethod]
        public static object Eliminar(Sesion sesion, int idEmpleado)
        {
            object resultado = new { };
            try
            {
                blEmpleado blEmpleado = new blEmpleado(sesion);
                blEmpleado.Eliminar(idEmpleado);
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