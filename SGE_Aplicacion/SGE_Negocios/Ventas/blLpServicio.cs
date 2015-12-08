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
    public class blLpServicio : blBase
    {
        public blLpServicio(Sesion sesion) { base.sesion = sesion; }

        daLpServicio daLpServicio;
        daLpServicioItem daLpServicioItem;
        daLpServicioUnidad daLpServicioUnidad;
        daLpServicioEscala daLpServicioEscala;

        public IList<LpServicio> ObtenerTodos()
        {
            IList<LpServicio> listas;
            try
            {
                daLpServicio = new daLpServicio();
                daLpServicio.AbrirSesion();
                listas = daLpServicio.ObtenerTodos();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daLpServicio.CerrarSesion();
            }
            return listas;
        }

        public LpServicio ObtenerPorId(int idLpServicio)
        {
            LpServicio lista;
            try
            {
                daLpServicio = new daLpServicio();
                lista = daLpServicio.ObtenerPorId(idLpServicio);
                daLpServicioItem = new daLpServicioItem();
                daLpServicioItem.AsignarSesion(daLpServicio);
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "idLpServicio", idLpServicio });
                lista.items = daLpServicioItem.ObtenerLista(filtros);
                daLpServicioUnidad = new daLpServicioUnidad();
                daLpServicioUnidad.AsignarSesion(daLpServicio);
                daLpServicioEscala = new daLpServicioEscala();
                daLpServicioEscala.AsignarSesion(daLpServicio);
                foreach (LpServicioItem item in lista.items)
                {
                    filtros = new List<object[]>();
                    filtros.Add(new object[] { "idLpServicioItem", item.idLpServicioItem });
                    item.unidades = daLpServicioUnidad.ObtenerLista(filtros);
                    foreach (LpServicioUnidad unidad in item.unidades)
                    {
                        filtros = new List<object[]>();
                        filtros.Add(new object[] { "idLpServicioUnidad", unidad.idLpServicioUnidad });
                        unidad.escalas = daLpServicioEscala.ObtenerLista(filtros);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daLpServicio.CerrarSesion();
            }
            return lista;
        }

        public bool Agregar(LpServicio lista)
        {
            try
            {
                daLpServicio = new daLpServicio();
                daLpServicio.IniciarTransaccion();
                daLpServicio.Agregar(lista);
                daLpServicioItem = new daLpServicioItem();
                daLpServicioItem.AsignarSesion(daLpServicio);
                daLpServicioUnidad = new daLpServicioUnidad();
                daLpServicioUnidad.AsignarSesion(daLpServicio);
                daLpServicioEscala = new daLpServicioEscala();
                daLpServicioEscala.AsignarSesion(daLpServicio);
                foreach (LpServicioItem item in lista.items)
                {
                    item.idLpServicio = lista.idLpServicio;
                    daLpServicioItem.Agregar(item);
                    foreach (LpServicioUnidad unidad in item.unidades)
                    {
                        unidad.idLpServicioItem = item.idLpServicioItem;
                        daLpServicioUnidad.Agregar(unidad);
                        foreach (LpServicioEscala escala in unidad.escalas)
                        {
                            escala.idLpServicioUnidad = unidad.idLpServicioUnidad;
                            daLpServicioEscala.Agregar(escala);
                        }
                    }
                }
                daLpServicio.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daLpServicio.AbortarTransaccion();
                throw;
            }
            finally
            {
                daLpServicio.CerrarSesion();
            }
            return true;
        }

        public bool Actualizar(LpServicio lista)
        {
            try
            {
                daLpServicio = new daLpServicio();
                daLpServicio.IniciarTransaccion();
                LpServicio lista_ = daLpServicio.ObtenerPorId(lista.idLpServicio);
                lista_.descripcion = lista.descripcion;
                lista_.activo = lista.activo;
                daLpServicioItem = new daLpServicioItem();
                daLpServicioItem.AsignarSesion(daLpServicio);
                daLpServicioUnidad = new daLpServicioUnidad();
                daLpServicioUnidad.AsignarSesion(daLpServicio);
                daLpServicioEscala = new daLpServicioEscala();
                daLpServicioEscala.AsignarSesion(daLpServicio);
                foreach (LpServicioItem item in lista.items)
                {
                    if (item.idLpServicioItem == 0)
                    {
                        item.idLpServicio = lista.idLpServicio;
                        daLpServicioItem.Agregar(item);
                        foreach (LpServicioUnidad unidad in item.unidades)
                        {
                            unidad.idLpServicioItem = item.idLpServicioItem;
                            daLpServicioUnidad.Agregar(unidad);
                            foreach (LpServicioEscala escala in unidad.escalas)
                            {
                                escala.idLpServicioUnidad = unidad.idLpServicioUnidad;
                                daLpServicioEscala.Agregar(escala);
                            }
                        }
                    }
                    else {
                        foreach (LpServicioUnidad unidad in item.unidades)
                        {
                            if (unidad.idLpServicioUnidad == 0)
                            {
                                unidad.idLpServicioItem = item.idLpServicioItem;
                                daLpServicioUnidad.Agregar(unidad);
                                foreach (LpServicioEscala escala in unidad.escalas)
                                {
                                    escala.idLpServicioUnidad = unidad.idLpServicioUnidad;
                                    daLpServicioEscala.Agregar(escala);
                                }
                            }
                            else {
                                foreach (LpServicioEscala escala in unidad.escalas)
                                {
                                    if (escala.idLpServicioEscala == 0)
                                    {
                                        escala.idLpServicioUnidad = unidad.idLpServicioUnidad;
                                        daLpServicioEscala.Agregar(escala);
                                    }
                                    else {
                                        LpServicioEscala escala_ = daLpServicioEscala.ObtenerPorId(escala.idLpServicioEscala);
                                        escala_.desde = escala.desde;
                                        escala_.hasta = escala.hasta;
                                        escala_.precio = escala.precio;
                                    }
                                }
                                foreach (int idEscala in unidad.idsEscalas)
                                {
                                    daLpServicioEscala.EliminarPorId(idEscala, constantes.esquemas.Ventas);
                                }
                            }
                        }
                        foreach (int idUnidad in item.idsUnidades)
                        {
                            daLpServicioUnidad.EliminarPorId(idUnidad, constantes.esquemas.Ventas);
                        }
                    }
                }
                foreach (int idItem in lista.idsItems)
                {
                    daLpServicioItem.EliminarPorId(idItem, constantes.esquemas.Ventas);
                    List<object[]> filtros = new List<object[]>();
                    filtros.Add(new object[] { "idLpServicioItem", idItem });
                    List<LpServicioUnidad> unidades = daLpServicioUnidad.ObtenerLista(filtros);
                    daLpServicioUnidad.EliminarPorIdLpServicioItem(idItem);
                    foreach (LpServicioUnidad unidad in unidades)
                    {
                        daLpServicioEscala.EliminarPorIdLpServicioUnidad(unidad.idLpServicioUnidad);
                    }
                }
                daLpServicio.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daLpServicio.AbortarTransaccion();
                throw;
            }
            finally
            {
                daLpServicio.CerrarSesion();
            }
            return true;
        }

        public bool Eliminar(int idLpServicio)
        {
            try
            {
                daLpServicio = new daLpServicio();
                daLpServicio.IniciarTransaccion();
                daLpServicio.EliminarPorId(idLpServicio, constantes.esquemas.Ventas);
                daLpServicioItem = new daLpServicioItem();
                daLpServicioItem.AsignarSesion(daLpServicio);
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "idLpServicio", idLpServicio });
                List<LpServicioItem> items = daLpServicioItem.ObtenerLista(filtros);
                daLpServicioItem.EliminarPorIdLpServicio(idLpServicio);
                daLpServicioUnidad = new daLpServicioUnidad();
                daLpServicioUnidad.AsignarSesion(daLpServicio);
                daLpServicioEscala = new daLpServicioEscala();
                daLpServicioEscala.AsignarSesion(daLpServicio);
                foreach (LpServicioItem item in items)
                {
                    filtros = new List<object[]>();
                    filtros.Add(new object[] { "idLpServicioItem", item.idLpServicioItem });
                    List<LpServicioUnidad> unidades = daLpServicioUnidad.ObtenerLista(filtros);
                    daLpServicioUnidad.EliminarPorIdLpServicioItem(item.idLpServicioItem);
                    foreach (LpServicioUnidad unidad in unidades)
                    {
                        daLpServicioEscala.EliminarPorIdLpServicioUnidad(unidad.idLpServicioUnidad);
                    }
                }
                daLpServicio.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daLpServicio.AbortarTransaccion();
                throw;
            }
            finally
            {
                daLpServicio.CerrarSesion();
            }
            return true;
        }
    }
}
