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

        public object[] ObtenerTodos(Paginacion paginacion, Orden orden)
        {
            object[] datos;
            try
            {
                daPresupuesto = new daPresupuesto();
                daPresupuesto.AbrirSesion();
                datos = daPresupuesto.ObtenerPaginacion(new List<object[]>(), paginacion, orden);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daPresupuesto.CerrarSesion();
            }
            return datos;
        }

        public Presupuesto ObtenerPorId(int idPresupuesto)
        {
            Presupuesto presupuesto;
            try
            {
                daPresupuesto = new daPresupuesto();
                daPresupuesto.AbrirSesion();
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
                if (string.IsNullOrEmpty(presupuesto.numero))
                {
                    presupuesto.numero = generarNumeracion(daPresupuesto, presupuesto.numeracion.idNumeracion);
                }
                presupuesto.fechaCreacion = DateTime.Now;
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
                presupuesto_.vendedor = presupuesto.vendedor;
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
                        item_.recargo = item.recargo;
                        item_.total = item.total;
                    }
                }
                foreach (int idItem in presupuesto.idsItems)
                {
                    daItemPresupuesto.EliminarPorId(idItem, constantes.esquemas.Ventas);
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

        public bool Eliminar(List<int> ids)
        {
            try
            {
                daPresupuesto = new daPresupuesto();
                daPresupuesto.IniciarTransaccion();
                foreach (int idPresupuesto in ids)
                {
                    daPresupuesto.EliminarPorId(idPresupuesto, constantes.esquemas.Ventas);
                    daItemPresupuesto = new daPresupuestoItem();
                    daItemPresupuesto.AsignarSesion(daPresupuesto);
                    daItemPresupuesto.EliminarPorIdPresupuesto(idPresupuesto);
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
    }
}
