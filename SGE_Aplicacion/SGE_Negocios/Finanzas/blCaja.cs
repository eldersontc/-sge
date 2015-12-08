using SGE.AccesoDatos.Finanzas;
using SGE.Entidades.Administracion;
using SGE.Entidades.Finanzas;
using SGE.Negocios.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Negocios.Finanzas
{
    public class blCaja : blBase
    {
        public blCaja(Sesion sesion) { base.sesion = sesion; }

        daCaja daCaja;

        public IList<Caja> ObtenerTodos()
        {
            IList<Caja> cajas;
            try
            {
                daCaja = new daCaja();
                daCaja.AbrirSesion();
                cajas = daCaja.ObtenerTodos();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daCaja.CerrarSesion();
            }
            return cajas;
        }

        public bool Agregar(Caja caja)
        {
            try
            {
                daCaja = new daCaja();
                daCaja.IniciarTransaccion();
                daCaja.Agregar(caja);
                daCaja.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daCaja.AbortarTransaccion();
                throw;
            }
            finally
            {
                daCaja.CerrarSesion();
            }
            return true;
        }

        public bool Actualizar(Caja caja)
        {
            try
            {
                daCaja = new daCaja();
                daCaja.IniciarTransaccion();
                Caja caja_ = daCaja.ObtenerPorId(caja.idCaja);
                caja_.descripcion = caja.descripcion;
                caja_.moneda = caja.moneda;
                caja_.activo = caja.activo;
                daCaja.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daCaja.AbortarTransaccion();
                throw;
            }
            finally
            {
                daCaja.CerrarSesion();
            }
            return true;
        }

        public bool Eliminar(int idCaja)
        {
            try
            {
                daCaja = new daCaja();
                daCaja.IniciarTransaccion();
                daCaja.EliminarPorId(idCaja, constantes.esquemas.Finanzas);
                daCaja.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daCaja.AbortarTransaccion();
                throw;
            }
            finally
            {
                daCaja.CerrarSesion();
            }
            return true;
        }
    }
}
