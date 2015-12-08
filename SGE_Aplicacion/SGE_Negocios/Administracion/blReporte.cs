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
    public class blReporte : blBase
    {
        public blReporte(Sesion sesion) { base.sesion = sesion; }

        daReporte daReporte;
        daReporteItem daItemReporte;

        public IList<Reporte> ObtenerTodos()
        {
            IList<Reporte> reportes;
            try
            {
                daReporte = new daReporte();
                daReporte.AbrirSesion();
                reportes = daReporte.ObtenerTodos();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daReporte.CerrarSesion();
            }
            return reportes;
        }

        public Reporte ObtenerPorId(int idReporte)
        {
            Reporte reporte;
            try
            {
                daReporte = new daReporte();
                reporte = daReporte.ObtenerPorId(idReporte);
                daItemReporte = new daReporteItem();
                daItemReporte.AsignarSesion(daReporte);
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "idReporte", idReporte });
                reporte.items = daItemReporte.ObtenerLista(filtros);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daReporte.CerrarSesion();
            }
            return reporte;
        }

        public bool Agregar(Reporte reporte)
        {
            try
            {
                daReporte = new daReporte();
                daReporte.IniciarTransaccion();
                daReporte.Agregar(reporte);
                daItemReporte = new daReporteItem();
                daItemReporte.AsignarSesion(daReporte);
                foreach (ReporteItem  item in reporte.items)
                {
                    item.idReporte = reporte.idReporte;
                    daItemReporte.Agregar(item);
                }
                daReporte.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daReporte.AbortarTransaccion();
                throw;
            }
            finally
            {
                daReporte.CerrarSesion();
            }
            return true;
        }

        public bool Actualizar(Reporte reporte)
        {
            try
            {
                daReporte = new daReporte();
                daReporte.IniciarTransaccion();
                Reporte reporte_ = daReporte.ObtenerPorId(reporte.idReporte);
                reporte_.descripcion = reporte.descripcion;
                reporte_.documento = reporte.documento;
                reporte_.ubicacion = reporte.ubicacion;
                reporte_.activo = reporte.activo;
                daItemReporte = new daReporteItem();
                daItemReporte.AsignarSesion(daReporte);
                foreach (ReporteItem item in reporte.items)
                {
                    if (item.idReporteItem == 0)
                    {
                        item.idReporte = reporte.idReporte;
                        daItemReporte.Agregar(item);
                    }
                    else 
                    {
                        ReporteItem item_ = daItemReporte.ObtenerPorId(item.idReporteItem);
                        item_.nombre = item.nombre;
                        item_.asignarId = item.asignarId;
                        item_.valor = item.valor;
                    }
                }
                foreach (int idItem in reporte.idsItems)
                {
                    daItemReporte.EliminarPorId(idItem, constantes.esquemas.Administracion);
                }
                daReporte.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daReporte.AbortarTransaccion();
                throw;
            }
            finally
            {
                daReporte.CerrarSesion();
            }
            return true;
        }

        public bool Eliminar(int idReporte)
        {
            try
            {
                daReporte = new daReporte();
                daReporte.IniciarTransaccion();
                daReporte.EliminarPorId(idReporte, constantes.esquemas.Administracion);
                daItemReporte = new daReporteItem();
                daItemReporte.AsignarSesion(daReporte);
                daItemReporte.EliminarPorIdReporte(idReporte);
                daReporte.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daReporte.AbortarTransaccion();
                throw;
            }
            finally
            {
                daReporte.CerrarSesion();
            }
            return true;
        }
    }
}
