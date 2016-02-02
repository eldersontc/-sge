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
    public class blPlantilla : blBase
    {
        public blPlantilla(Sesion sesion) { base.sesion = sesion; }

        daPlantilla daPlantilla;
        daPlantillaItem daPlantillaItem;

        public object[] ObtenerTodos(Paginacion paginacion, Orden orden)
        {
            object[] datos;
            try
            {
                daPlantilla = new daPlantilla();
                daPlantilla.AbrirSesion();
                datos = daPlantilla.ObtenerPaginacion(new List<object[]>(), paginacion, orden);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daPlantilla.CerrarSesion();
            }
            return datos;
        }

        public IList<Plantilla> ObtenerActivos()
        {
            IList<Plantilla> plantillas;
            try
            {
                daPlantilla = new daPlantilla();
                daPlantilla.AbrirSesion();
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "activo", true });
                plantillas = daPlantilla.ObtenerLista(filtros);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daPlantilla.CerrarSesion();
            }
            return plantillas;
        }

        public Plantilla ObtenerPorId(int idPlantilla)
        {
            Plantilla plantilla;
            try
            {
                daPlantilla = new daPlantilla();
                daPlantilla.AbrirSesion();
                plantilla = daPlantilla.ObtenerPorId(idPlantilla);
                daPlantillaItem = new daPlantillaItem();
                daPlantillaItem.AsignarSesion(daPlantilla);
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "idPlantilla", idPlantilla });
                plantilla.items = daPlantillaItem.ObtenerLista(filtros);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daPlantilla.CerrarSesion();
            }
            return plantilla;
        }

        public bool Agregar(Plantilla plantilla)
        {
            try
            {
                daPlantilla = new daPlantilla();
                daPlantilla.IniciarTransaccion();
                daPlantilla.Agregar(plantilla);
                daPlantillaItem = new daPlantillaItem();
                daPlantillaItem.AsignarSesion(daPlantilla);
                foreach (PlantillaItem item in plantilla.items)
                {
                    item.idPlantilla = plantilla.idPlantilla;
                    daPlantillaItem.Agregar(item);
                }
                daPlantilla.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daPlantilla.AbortarTransaccion();
                throw;
            }
            finally
            {
                daPlantilla.CerrarSesion();
            }
            return true;
        }

        public bool Actualizar(Plantilla plantilla)
        {
            try
            {
                daPlantilla = new daPlantilla();
                daPlantilla.IniciarTransaccion();
                Plantilla plantilla_ = daPlantilla.ObtenerPorId(plantilla.idPlantilla);
                plantilla_.descripcion = plantilla.descripcion;
                plantilla_.linea = plantilla.linea;
                plantilla_.activo = plantilla.activo;
                daPlantillaItem = new daPlantillaItem();
                daPlantillaItem.AsignarSesion(daPlantilla);
                foreach (PlantillaItem item in plantilla.items)
                {
                    if (item.idPlantillaItem == 0)
                    {
                        item.idPlantilla = plantilla.idPlantilla;
                        daPlantillaItem.Agregar(item);
                    }
                    else {
                        PlantillaItem item_ = daPlantillaItem.ObtenerPorId(item.idPlantillaItem);
                        item_.titulo = item.titulo;
                    }
                }
                foreach (int idItem in plantilla.idsItems)
                {
                    daPlantillaItem.EliminarPorId(idItem, constantes.esquemas.Ventas);
                    daPlantillaItem.EliminarPorIdPlantilla(idItem);
                }
                daPlantilla.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daPlantilla.AbortarTransaccion();
                throw;
            }
            finally
            {
                daPlantilla.CerrarSesion();
            }
            return true;
        }

        public bool Eliminar(List<int> ids)
        {
            try
            {
                daPlantilla = new daPlantilla();
                daPlantilla.IniciarTransaccion();
                foreach (int id in ids)
                {
                    daPlantilla.EliminarPorId(id, constantes.esquemas.Ventas);
                    daPlantillaItem = new daPlantillaItem();
                    daPlantillaItem.AsignarSesion(daPlantilla);
                    List<object[]> filtros = new List<object[]>();
                    filtros.Add(new object[] { "idPlantilla", id });
                    List<PlantillaItem> items = daPlantillaItem.ObtenerLista(filtros);
                    daPlantillaItem.EliminarPorIdPlantilla(id);
                }
                daPlantilla.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daPlantilla.AbortarTransaccion();
                throw;
            }
            finally
            {
                daPlantilla.CerrarSesion();
            }
            return true;
        }
    }
}
