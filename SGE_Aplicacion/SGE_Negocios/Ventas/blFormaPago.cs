using SGE.AccesoDatos.Ventas;
using SGE.Entidades.Administracion;
using SGE.Entidades.Ventas;
using SGE.Negocios.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Negocios.Ventas
{
    public class blFormaPago : blBase
    {
        public blFormaPago(Sesion sesion) { base.sesion = sesion; }

        daFormaPago daFormaPago;

        public IList<FormaPago> ObtenerTodos()
        {
            IList<FormaPago> formas;
            try
            {
                daFormaPago = new daFormaPago();
                daFormaPago.AbrirSesion();
                formas = daFormaPago.ObtenerTodos();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daFormaPago.CerrarSesion();
            }
            return formas;
        }

        public IList<FormaPago> ObtenerActivos()
        {
            IList<FormaPago> formas;
            try
            {
                daFormaPago = new daFormaPago();
                daFormaPago.AbrirSesion();
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "activo", true });
                formas = daFormaPago.ObtenerLista(filtros);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daFormaPago.CerrarSesion();
            }
            return formas;
        }

        public bool Agregar(FormaPago forma)
        {
            try
            {
                daFormaPago = new daFormaPago();
                daFormaPago.IniciarTransaccion();
                daFormaPago.Agregar(forma);
                daFormaPago.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daFormaPago.AbortarTransaccion();
                throw;
            }
            finally
            {
                daFormaPago.CerrarSesion();
            }
            return true;
        }

        public bool Actualizar(FormaPago forma)
        {
            try
            {
                daFormaPago = new daFormaPago();
                daFormaPago.IniciarTransaccion();
                FormaPago almacen_ = daFormaPago.ObtenerPorId(forma.idFormaPago);
                almacen_.descripcion = forma.descripcion;
                almacen_.credito = forma.credito;
                almacen_.dias = forma.dias;
                almacen_.activo = forma.activo;
                daFormaPago.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daFormaPago.AbortarTransaccion();
                throw;
            }
            finally
            {
                daFormaPago.CerrarSesion();
            }
            return true;
        }

        public bool Eliminar(int idFormaPago)
        {
            try
            {
                daFormaPago = new daFormaPago();
                daFormaPago.IniciarTransaccion();
                daFormaPago.EliminarPorId(idFormaPago, constantes.esquemas.Ventas);
                daFormaPago.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daFormaPago.AbortarTransaccion();
                throw;
            }
            finally
            {
                daFormaPago.CerrarSesion();
            }
            return true;
        }
    }
}
