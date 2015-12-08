using SGE.AccesoDatos.Produccion;
using SGE.Entidades.Administracion;
using SGE.Entidades.Produccion;
using SGE.Negocios.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Negocios.Produccion
{
    public class blOrdenProduccion : blBase
    {
        public blOrdenProduccion(Sesion sesion) { base.sesion = sesion; }

        daOrdenProduccion daOrdenProduccion;
        daOrdenProduccionItem daItemOrdenProduccion;

        public IList<OrdenProduccion> ObtenerTodos()
        {
            IList<OrdenProduccion> ordenes;
            try
            {
                daOrdenProduccion = new daOrdenProduccion();
                daOrdenProduccion.AbrirSesion();
                ordenes = daOrdenProduccion.ObtenerTodos();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daOrdenProduccion.CerrarSesion();
            }
            return ordenes;
        }

        public OrdenProduccion ObtenerPorId(int idOrdenProduccion)
        {
            OrdenProduccion orden;
            try
            {
                daOrdenProduccion = new daOrdenProduccion();
                orden = daOrdenProduccion.ObtenerPorId(idOrdenProduccion);
                daItemOrdenProduccion = new daOrdenProduccionItem();
                daItemOrdenProduccion.AsignarSesion(daOrdenProduccion);
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "idOrdenProduccion", idOrdenProduccion });
                orden.items = daItemOrdenProduccion.ObtenerLista(filtros);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daOrdenProduccion.CerrarSesion();
            }
            return orden;
        }

        public bool Agregar(OrdenProduccion orden)
        {
            try
            {
                daOrdenProduccion = new daOrdenProduccion();
                daOrdenProduccion.IniciarTransaccion();
                daOrdenProduccion.Agregar(orden);
                daItemOrdenProduccion = new daOrdenProduccionItem();
                daItemOrdenProduccion.AsignarSesion(daOrdenProduccion);
                foreach (OrdenProduccionItem  item in orden.items)
                {
                    item.idOrdenProduccion = orden.idOrdenProduccion;
                    daItemOrdenProduccion.Agregar(item);
                }
                daOrdenProduccion.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daOrdenProduccion.AbortarTransaccion();
                throw;
            }
            finally
            {
                daOrdenProduccion.CerrarSesion();
            }
            return true;
        }

        public bool Actualizar(OrdenProduccion orden)
        {
            try
            {
                daOrdenProduccion = new daOrdenProduccion();
                daOrdenProduccion.IniciarTransaccion();
                OrdenProduccion orden_ = daOrdenProduccion.ObtenerPorId(orden.idOrdenProduccion);
                orden_.cliente = orden.cliente;
                orden_.responsable = orden.responsable;
                daItemOrdenProduccion = new daOrdenProduccionItem();
                daItemOrdenProduccion.AsignarSesion(daOrdenProduccion);
                foreach (OrdenProduccionItem item in orden.items)
                {
                    if (item.idOrdenProduccionItem == 0)
                    {
                        item.idOrdenProduccion = orden.idOrdenProduccion;
                        daItemOrdenProduccion.Agregar(item);
                    }
                }
                foreach (int idItem in orden.idsItems)
                {
                    daItemOrdenProduccion.EliminarPorId(idItem, constantes.esquemas.Administracion);
                }
                daOrdenProduccion.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daOrdenProduccion.AbortarTransaccion();
                throw;
            }
            finally
            {
                daOrdenProduccion.CerrarSesion();
            }
            return true;
        }

        public bool Eliminar(int idOrdenProduccion)
        {
            try
            {
                daOrdenProduccion = new daOrdenProduccion();
                daOrdenProduccion.IniciarTransaccion();
                daOrdenProduccion.EliminarPorId(idOrdenProduccion, constantes.esquemas.Produccion);
                daItemOrdenProduccion = new daOrdenProduccionItem();
                daItemOrdenProduccion.AsignarSesion(daOrdenProduccion);
                daItemOrdenProduccion.EliminarPorIdOrdenProduccion(idOrdenProduccion);
                daOrdenProduccion.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daOrdenProduccion.AbortarTransaccion();
                throw;
            }
            finally
            {
                daOrdenProduccion.CerrarSesion();
            }
            return true;
        }
    }
}
