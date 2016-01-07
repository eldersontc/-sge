using SGE.AccesoDatos.Administracion;
using SGE.Entidades.Administracion;
using SGE.Negocios.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Negocios.Administracion
{
    public class blEmpleado : blBase
    {
        public blEmpleado(Sesion sesion) { base.sesion = sesion; }

        daEmpleado daEmpleado;

        public IList<Empleado> ObtenerTodos()
        {
            IList<Empleado> empleados;
            try
            {
                daEmpleado = new daEmpleado();
                daEmpleado.AbrirSesion();
                empleados = daEmpleado.ObtenerTodos();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daEmpleado.CerrarSesion();
            }
            return empleados;
        }

        public IList<Empleado> ObtenerActivos()
        {
            IList<Empleado> empleados;
            try
            {
                daEmpleado = new daEmpleado();
                daEmpleado.AbrirSesion();
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "activo", true });
                empleados = daEmpleado.ObtenerLista(filtros);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daEmpleado.CerrarSesion();
            }
            return empleados;
        }

        public bool Agregar(Empleado empleado)
        {
            try
            {
                daEmpleado = new daEmpleado();
                daEmpleado.IniciarTransaccion();
                daEmpleado.Agregar(empleado);
                daEmpleado.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daEmpleado.AbortarTransaccion();
                throw;
            }
            finally
            {
                daEmpleado.CerrarSesion();
            }
            return true;
        }

        public bool Actualizar(Empleado empleado)
        {
            try
            {
                daEmpleado = new daEmpleado();
                daEmpleado.IniciarTransaccion();
                Empleado empleado_ = daEmpleado.ObtenerPorId(empleado.idEmpleado);
                empleado_.codigo = empleado.codigo;
                empleado_.nombre = empleado.nombre;
                empleado_.apellidoPaterno = empleado.apellidoPaterno;
                empleado_.apellidoMaterno = empleado.apellidoMaterno;
                empleado_.documentoIdentidad = empleado.documentoIdentidad;
                empleado_.numeroDocumento = empleado.numeroDocumento;
                empleado_.activo = empleado.activo;
                daEmpleado.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daEmpleado.AbortarTransaccion();
                throw;
            }
            finally
            {
                daEmpleado.CerrarSesion();
            }
            return true;
        }

        public bool Eliminar(int idEmpleado)
        {
            try
            {
                daEmpleado = new daEmpleado();
                daEmpleado.IniciarTransaccion();
                daEmpleado.EliminarPorId(idEmpleado, constantes.esquemas.Administracion);
                daEmpleado.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daEmpleado.AbortarTransaccion();
                throw;
            }
            finally
            {
                daEmpleado.CerrarSesion();
            }
            return true;
        }
    }
}
