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
    public class blMoneda : blBase
    {
        public blMoneda(Sesion sesion) { base.sesion = sesion; }

        daMoneda daMoneda;

        public IList<Moneda> ObtenerTodos()
        {
            IList<Moneda> monedas;
            try
            {
                daMoneda = new daMoneda();
                daMoneda.AbrirSesion();
                monedas = daMoneda.ObtenerTodos();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daMoneda.CerrarSesion();
            }
            return monedas;
        }

        public IList<Moneda> ObtenerActivos()
        {
            IList<Moneda> monedas;
            try
            {
                daMoneda = new daMoneda();
                daMoneda.AbrirSesion();
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "activo", true });
                monedas = daMoneda.ObtenerLista(filtros);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daMoneda.CerrarSesion();
            }
            return monedas;
        }

        public bool Agregar(Moneda moneda)
        {
            try
            {
                daMoneda = new daMoneda();
                daMoneda.IniciarTransaccion();
                daMoneda.Agregar(moneda);
                daMoneda.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daMoneda.AbortarTransaccion();
                throw;
            }
            finally
            {
                daMoneda.CerrarSesion();
            }
            return true;
        }

        public bool Actualizar(Moneda moneda)
        {
            try
            {
                daMoneda = new daMoneda();
                daMoneda.IniciarTransaccion();
                Moneda moneda_ = daMoneda.ObtenerPorId(moneda.idMoneda);
                moneda_.nombre = moneda.nombre;
                moneda_.simbolo = moneda.simbolo;
                moneda_.activo = moneda.activo;
                daMoneda.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daMoneda.AbortarTransaccion();
                throw;
            }
            finally
            {
                daMoneda.CerrarSesion();
            }
            return true;
        }

        public bool Eliminar(int idMoneda)
        {
            try
            {
                daMoneda = new daMoneda();
                daMoneda.IniciarTransaccion();
                daMoneda.EliminarPorId(idMoneda, constantes.esquemas.Administracion);
                daMoneda.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daMoneda.AbortarTransaccion();
                throw;
            }
            finally
            {
                daMoneda.CerrarSesion();
            }
            return true;
        }
    }
}
