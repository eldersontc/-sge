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
    public class blSalCaja : blBase
    {
        public blSalCaja(Sesion sesion) { base.sesion = sesion; }

        daSalCaja daSalCaja;
        daSalCajaItem daSalCajaItem;

        public IList<SalCaja> ObtenerTodos()
        {
            IList<SalCaja> salidas;
            try
            {
                daSalCaja = new daSalCaja();
                daSalCaja.AbrirSesion();
                salidas = daSalCaja.ObtenerTodos();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daSalCaja.CerrarSesion();
            }
            return salidas;
        }

        public SalCaja ObtenerPorId(int idSalCaja)
        {
            SalCaja salida;
            try
            {
                daSalCaja = new daSalCaja();
                daSalCaja.AbrirSesion();
                salida = daSalCaja.ObtenerPorId(idSalCaja);
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "idSalCaja", idSalCaja });
                daSalCajaItem = new daSalCajaItem();
                daSalCajaItem.AsignarSesion(daSalCaja);
                salida.items = daSalCajaItem.ObtenerLista(filtros);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daSalCaja.CerrarSesion();
            }
            return salida;
        }

        public bool Agregar(SalCaja salida)
        {
            try
            {
                daSalCaja = new daSalCaja();
                daSalCaja.IniciarTransaccion();
                daSalCaja.Agregar(salida);
                daSalCajaItem = new daSalCajaItem();
                daSalCajaItem.AsignarSesion(daSalCaja);
                foreach (SalCajaItem item in salida.items)
                {
                    item.idSalCaja = salida.idSalCaja;
                    daSalCajaItem.Agregar(item);
                }
                daSalCaja.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daSalCaja.AbortarTransaccion();
                throw;
            }
            finally
            {
                daSalCaja.CerrarSesion();
            }
            return true;
        }

        public bool Eliminar(int idSalCaja)
        {
            try
            {
                daSalCaja = new daSalCaja();
                daSalCaja.IniciarTransaccion();
                daSalCaja.EliminarPorId(idSalCaja, constantes.esquemas.Finanzas);
                daSalCajaItem = new daSalCajaItem();
                daSalCajaItem.AsignarSesion(daSalCaja);
                daSalCajaItem.EliminarPorIdSalCaja(idSalCaja);
                daSalCaja.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daSalCaja.AbortarTransaccion();
                throw;
            }
            finally
            {
                daSalCaja.CerrarSesion();
            }
            return true;
        }
    }
}
