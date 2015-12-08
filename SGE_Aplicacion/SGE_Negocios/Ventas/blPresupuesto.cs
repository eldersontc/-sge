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
    public class blPresupuesto : blBase
    {
        public blPresupuesto(Sesion sesion) { base.sesion = sesion; }

        daPresupuesto daPresupuesto;
        daPresupuestoItem daItemPresupuesto;

        public IList<Presupuesto> ObtenerTodos()
        {
            IList<Presupuesto> presupuestos;
            try
            {
                daPresupuesto = new daPresupuesto();
                daPresupuesto.AbrirSesion();
                presupuestos = daPresupuesto.ObtenerTodos();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daPresupuesto.CerrarSesion();
            }
            return presupuestos;
        }

        public Presupuesto ObtenerPorId(int idPresupuesto)
        {
            Presupuesto presupuesto;
            try
            {
                daPresupuesto = new daPresupuesto();
                presupuesto = daPresupuesto.ObtenerPorId(idPresupuesto);
                daItemPresupuesto = new daPresupuestoItem();
                daItemPresupuesto.AsignarSesion(daPresupuesto);
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "idPresupuesto", idPresupuesto });
                presupuesto.items = daItemPresupuesto.ObtenerLista(filtros);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daPresupuesto.CerrarSesion();
            }
            return presupuesto;
        }

        public bool Agregar(Presupuesto presupuesto)
        {
            try
            {
                daPresupuesto = new daPresupuesto();
                daPresupuesto.IniciarTransaccion();
                daPresupuesto.Agregar(presupuesto);
                daItemPresupuesto = new daPresupuestoItem();
                daItemPresupuesto.AsignarSesion(daPresupuesto);
                foreach (PresupuestoItem  item in presupuesto.items)
                {
                    item.idPresupuesto = presupuesto.idPresupuesto;
                    daItemPresupuesto.Agregar(item);
                }
                daPresupuesto.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daPresupuesto.AbortarTransaccion();
                throw;
            }
            finally
            {
                daPresupuesto.CerrarSesion();
            }
            return true;
        }

        public bool Actualizar(Presupuesto presupuesto)
        {
            try
            {
                daPresupuesto = new daPresupuesto();
                daPresupuesto.IniciarTransaccion();
                Presupuesto presupuesto_ = daPresupuesto.ObtenerPorId(presupuesto.idPresupuesto);
                presupuesto_.cliente = presupuesto.cliente;
                presupuesto_.moneda = presupuesto.moneda;
                presupuesto_.instrucciones = presupuesto.instrucciones;
                presupuesto_.total = presupuesto.total;
                daItemPresupuesto = new daPresupuestoItem();
                daItemPresupuesto.AsignarSesion(daPresupuesto);
                foreach (PresupuestoItem item in presupuesto.items)
                {
                    if (item.idPresupuestoItem == 0)
                    {
                        item.idPresupuesto = presupuesto.idPresupuesto;
                        daItemPresupuesto.Agregar(item);
                    }
                    else 
                    {
                        PresupuestoItem item_ = daItemPresupuesto.ObtenerPorId(item.idPresupuestoItem);
                        item_.cotizacion = item.cotizacion;
                        item_.ttlCot = item.ttlCot;
                        item_.recargo = item.recargo;
                        item_.total = item.total;
                    }
                }
                foreach (int idItem in presupuesto.idsItems)
                {
                    daItemPresupuesto.EliminarPorId(idItem, constantes.esquemas.Administracion);
                }
                daPresupuesto.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daPresupuesto.AbortarTransaccion();
                throw;
            }
            finally
            {
                daPresupuesto.CerrarSesion();
            }
            return true;
        }

        public bool Eliminar(int idPresupuesto)
        {
            try
            {
                daPresupuesto = new daPresupuesto();
                daPresupuesto.IniciarTransaccion();
                daPresupuesto.EliminarPorId(idPresupuesto, constantes.esquemas.Ventas);
                daItemPresupuesto = new daPresupuestoItem();
                daItemPresupuesto.AsignarSesion(daPresupuesto);
                daItemPresupuesto.EliminarPorIdPresupuesto(idPresupuesto);
                daPresupuesto.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daPresupuesto.AbortarTransaccion();
                throw;
            }
            finally
            {
                daPresupuesto.CerrarSesion();
            }
            return true;
        }
    }
}
