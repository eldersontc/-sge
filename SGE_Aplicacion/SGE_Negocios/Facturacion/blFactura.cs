using SGE.AccesoDatos.Facturacion;
using SGE.Entidades.Administracion;
using SGE.Entidades.Facturacion;
using SGE.Negocios.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Negocios.Facturacion
{
    public class blFactura : blBase
    {
        public blFactura(Sesion sesion) { base.sesion = sesion; }

        daFactura daFactura;
        daFacturaItem daFacturaItem;

        public IList<Factura> ObtenerTodos()
        {
            IList<Factura> facturas;
            try
            {
                daFactura = new daFactura();
                daFactura.AbrirSesion();
                facturas = daFactura.ObtenerTodos();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daFactura.CerrarSesion();
            }
            return facturas;
        }

        public Factura ObtenerPorId(int idFactura)
        {
            Factura factura;
            try
            {
                daFactura = new daFactura();
                daFactura.AbrirSesion();
                factura = daFactura.ObtenerPorId(idFactura);
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "idFactura", idFactura });
                daFacturaItem = new daFacturaItem();
                daFacturaItem.AsignarSesion(daFactura);
                factura.items = daFacturaItem.ObtenerLista(filtros);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daFactura.CerrarSesion();
            }
            return factura;
        }

        public bool Agregar(Factura factura)
        {
            try
            {
                daFactura = new daFactura();
                daFactura.IniciarTransaccion();
                daFactura.Agregar(factura);
                daFacturaItem = new daFacturaItem();
                daFacturaItem.AsignarSesion(daFactura);
                foreach (FacturaItem item in factura.items)
                {
                    item.idFactura = factura.idFactura;
                    daFacturaItem.Agregar(item);
                }
                daFactura.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daFactura.AbortarTransaccion();
                throw;
            }
            finally
            {
                daFactura.CerrarSesion();
            }
            return true;
        }

        public bool Eliminar(int idFactura)
        {
            try
            {
                daFactura = new daFactura();
                daFactura.IniciarTransaccion();
                daFactura.EliminarPorId(idFactura, constantes.esquemas.Facturacion);
                daFacturaItem = new daFacturaItem();
                daFacturaItem.AsignarSesion(daFactura);
                daFacturaItem.EliminarPorIdFactura(idFactura);
                daFactura.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daFactura.AbortarTransaccion();
                throw;
            }
            finally
            {
                daFactura.CerrarSesion();
            }
            return true;
        }
    }
}
