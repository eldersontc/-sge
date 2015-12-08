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
    public class blIngCaja : blBase
    {
        public blIngCaja(Sesion sesion) { base.sesion = sesion; }

        daIngCaja daIngCaja;
        daIngCajaItem daIngCajaItem;

        public IList<IngCaja> ObtenerTodos()
        {
            IList<IngCaja> ingresos;
            try
            {
                daIngCaja = new daIngCaja();
                daIngCaja.AbrirSesion();
                ingresos = daIngCaja.ObtenerTodos();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daIngCaja.CerrarSesion();
            }
            return ingresos;
        }

        public IngCaja ObtenerPorId(int idIngCaja)
        {
            IngCaja ingreso;
            try
            {
                daIngCaja = new daIngCaja();
                daIngCaja.AbrirSesion();
                ingreso = daIngCaja.ObtenerPorId(idIngCaja);
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "idIngCaja", idIngCaja });
                daIngCajaItem = new daIngCajaItem();
                daIngCajaItem.AsignarSesion(daIngCaja);
                ingreso.items = daIngCajaItem.ObtenerLista(filtros);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daIngCaja.CerrarSesion();
            }
            return ingreso;
        }

        public bool Agregar(IngCaja ingreso)
        {
            try
            {
                daIngCaja = new daIngCaja();
                daIngCaja.IniciarTransaccion();
                daIngCaja.Agregar(ingreso);
                daIngCajaItem = new daIngCajaItem();
                daIngCajaItem.AsignarSesion(daIngCaja);
                foreach (IngCajaItem item in ingreso.items)
                {
                    item.idIngCaja = ingreso.idIngCaja;
                    daIngCajaItem.Agregar(item);
                }
                daIngCaja.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daIngCaja.AbortarTransaccion();
                throw;
            }
            finally
            {
                daIngCaja.CerrarSesion();
            }
            return true;
        }

        public bool Eliminar(int idIngCaja)
        {
            try
            {
                daIngCaja = new daIngCaja();
                daIngCaja.IniciarTransaccion();
                daIngCaja.EliminarPorId(idIngCaja, constantes.esquemas.Finanzas);
                daIngCajaItem = new daIngCajaItem();
                daIngCajaItem.AsignarSesion(daIngCaja);
                daIngCajaItem.EliminarPorIdIngCaja(idIngCaja);
                daIngCaja.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daIngCaja.AbortarTransaccion();
                throw;
            }
            finally
            {
                daIngCaja.CerrarSesion();
            }
            return true;
        }
    }
}
