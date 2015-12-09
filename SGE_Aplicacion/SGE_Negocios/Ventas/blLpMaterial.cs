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
    public class blLpMaterial : blBase
    {
        public blLpMaterial(Sesion sesion) { base.sesion = sesion; }

        daLpMaterial daLpMaterial;
        daLpMaterialItem daLpMaterialItem;
        daLpMaterialUnidad daLpMaterialUnidad;
        daLpMaterialEscala daLpMaterialEscala;

        public IList<LpMaterial> ObtenerTodos()
        {
            IList<LpMaterial> listas;
            try
            {
                daLpMaterial = new daLpMaterial();
                daLpMaterial.AbrirSesion();
                listas = daLpMaterial.ObtenerTodos();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daLpMaterial.CerrarSesion();
            }
            return listas;
        }

        public IList<LpMaterial> ObtenerActivos()
        {
            IList<LpMaterial> listas;
            try
            {
                daLpMaterial = new daLpMaterial();
                daLpMaterial.AbrirSesion();
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "activo", true });
                listas = daLpMaterial.ObtenerLista(filtros);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daLpMaterial.CerrarSesion();
            }
            return listas;
        }

        public LpMaterial ObtenerPorId(int idLpMaterial)
        {
            LpMaterial lista;
            try
            {
                daLpMaterial = new daLpMaterial();
                lista = daLpMaterial.ObtenerPorId(idLpMaterial);
                daLpMaterialItem = new daLpMaterialItem();
                daLpMaterialItem.AsignarSesion(daLpMaterial);
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "idLpMaterial", idLpMaterial });
                lista.items = daLpMaterialItem.ObtenerLista(filtros);
                daLpMaterialUnidad = new daLpMaterialUnidad();
                daLpMaterialUnidad.AsignarSesion(daLpMaterial);
                daLpMaterialEscala = new daLpMaterialEscala();
                daLpMaterialEscala.AsignarSesion(daLpMaterial);
                foreach (LpMaterialItem item in lista.items)
                {
                    filtros = new List<object[]>();
                    filtros.Add(new object[] { "idLpMaterialItem", item.idLpMaterialItem });
                    item.unidades = daLpMaterialUnidad.ObtenerLista(filtros);
                    foreach (LpMaterialUnidad unidad in item.unidades)
                    {
                        filtros = new List<object[]>();
                        filtros.Add(new object[] { "idLpMaterialUnidad", unidad.idLpMaterialUnidad });
                        unidad.escalas = daLpMaterialEscala.ObtenerLista(filtros);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daLpMaterial.CerrarSesion();
            }
            return lista;
        }

        public bool Agregar(LpMaterial lista)
        {
            try
            {
                daLpMaterial = new daLpMaterial();
                daLpMaterial.IniciarTransaccion();
                daLpMaterial.Agregar(lista);
                daLpMaterialItem = new daLpMaterialItem();
                daLpMaterialItem.AsignarSesion(daLpMaterial);
                daLpMaterialUnidad = new daLpMaterialUnidad();
                daLpMaterialUnidad.AsignarSesion(daLpMaterial);
                daLpMaterialEscala = new daLpMaterialEscala();
                daLpMaterialEscala.AsignarSesion(daLpMaterial);
                foreach (LpMaterialItem item in lista.items)
                {
                    item.idLpMaterial = lista.idLpMaterial;
                    daLpMaterialItem.Agregar(item);
                    foreach (LpMaterialUnidad unidad in item.unidades)
                    {
                        unidad.idLpMaterialItem = item.idLpMaterialItem;
                        daLpMaterialUnidad.Agregar(unidad);
                        foreach (LpMaterialEscala escala in unidad.escalas)
                        {
                            escala.idLpMaterialUnidad = unidad.idLpMaterialUnidad;
                            daLpMaterialEscala.Agregar(escala);
                        }
                    }
                }
                daLpMaterial.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daLpMaterial.AbortarTransaccion();
                throw;
            }
            finally
            {
                daLpMaterial.CerrarSesion();
            }
            return true;
        }

        public bool Actualizar(LpMaterial lista)
        {
            try
            {
                daLpMaterial = new daLpMaterial();
                daLpMaterial.IniciarTransaccion();
                LpMaterial lista_ = daLpMaterial.ObtenerPorId(lista.idLpMaterial);
                lista_.descripcion = lista.descripcion;
                lista_.activo = lista.activo;
                daLpMaterialItem = new daLpMaterialItem();
                daLpMaterialItem.AsignarSesion(daLpMaterial);
                daLpMaterialUnidad = new daLpMaterialUnidad();
                daLpMaterialUnidad.AsignarSesion(daLpMaterial);
                daLpMaterialEscala = new daLpMaterialEscala();
                daLpMaterialEscala.AsignarSesion(daLpMaterial);
                foreach (LpMaterialItem item in lista.items)
                {
                    if (item.idLpMaterialItem == 0)
                    {
                        item.idLpMaterial = lista.idLpMaterial;
                        daLpMaterialItem.Agregar(item);
                        foreach (LpMaterialUnidad unidad in item.unidades)
                        {
                            unidad.idLpMaterialItem = item.idLpMaterialItem;
                            daLpMaterialUnidad.Agregar(unidad);
                            foreach (LpMaterialEscala escala in unidad.escalas)
                            {
                                escala.idLpMaterialUnidad = unidad.idLpMaterialUnidad;
                                daLpMaterialEscala.Agregar(escala);
                            }
                        }
                    }
                    else {
                        foreach (LpMaterialUnidad unidad in item.unidades)
                        {
                            if (unidad.idLpMaterialUnidad == 0)
                            {
                                unidad.idLpMaterialItem = item.idLpMaterialItem;
                                daLpMaterialUnidad.Agregar(unidad);
                                foreach (LpMaterialEscala escala in unidad.escalas)
                                {
                                    escala.idLpMaterialUnidad = unidad.idLpMaterialUnidad;
                                    daLpMaterialEscala.Agregar(escala);
                                }
                            }
                            else {
                                foreach (LpMaterialEscala escala in unidad.escalas)
                                {
                                    if (escala.idLpMaterialEscala == 0)
                                    {
                                        escala.idLpMaterialUnidad = unidad.idLpMaterialUnidad;
                                        daLpMaterialEscala.Agregar(escala);
                                    }
                                    else {
                                        LpMaterialEscala escala_ = daLpMaterialEscala.ObtenerPorId(escala.idLpMaterialEscala);
                                        escala_.desde = escala.desde;
                                        escala_.hasta = escala.hasta;
                                        escala_.precio = escala.precio;
                                    }
                                }
                                foreach (int idEscala in unidad.idsEscalas)
                                {
                                    daLpMaterialEscala.EliminarPorId(idEscala, constantes.esquemas.Ventas);
                                }
                            }
                        }
                        foreach (int idUnidad in item.idsUnidades)
                        {
                            daLpMaterialUnidad.EliminarPorId(idUnidad, constantes.esquemas.Ventas);
                        }
                    }
                }
                foreach (int idItem in lista.idsItems)
                {
                    daLpMaterialItem.EliminarPorId(idItem, constantes.esquemas.Ventas);
                    List<object[]> filtros = new List<object[]>();
                    filtros.Add(new object[] { "idLpMaterialItem", idItem });
                    List<LpMaterialUnidad> unidades = daLpMaterialUnidad.ObtenerLista(filtros);
                    daLpMaterialUnidad.EliminarPorIdLpMaterialItem(idItem);
                    foreach (LpMaterialUnidad unidad in unidades)
                    {
                        daLpMaterialEscala.EliminarPorIdLpMaterialUnidad(unidad.idLpMaterialUnidad);
                    }
                }
                daLpMaterial.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daLpMaterial.AbortarTransaccion();
                throw;
            }
            finally
            {
                daLpMaterial.CerrarSesion();
            }
            return true;
        }

        public bool Eliminar(int idLpMaterial)
        {
            try
            {
                daLpMaterial = new daLpMaterial();
                daLpMaterial.IniciarTransaccion();
                daLpMaterial.EliminarPorId(idLpMaterial, constantes.esquemas.Ventas);
                daLpMaterialItem = new daLpMaterialItem();
                daLpMaterialItem.AsignarSesion(daLpMaterial);
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "idLpMaterial", idLpMaterial });
                List<LpMaterialItem> items = daLpMaterialItem.ObtenerLista(filtros);
                daLpMaterialItem.EliminarPorIdLpMaterial(idLpMaterial);
                daLpMaterialUnidad = new daLpMaterialUnidad();
                daLpMaterialUnidad.AsignarSesion(daLpMaterial);
                daLpMaterialEscala = new daLpMaterialEscala();
                daLpMaterialEscala.AsignarSesion(daLpMaterial);
                foreach (LpMaterialItem item in items)
                {
                    filtros = new List<object[]>();
                    filtros.Add(new object[] { "idLpMaterialItem", item.idLpMaterialItem });
                    List<LpMaterialUnidad> unidades = daLpMaterialUnidad.ObtenerLista(filtros);
                    daLpMaterialUnidad.EliminarPorIdLpMaterialItem(item.idLpMaterialItem);
                    foreach (LpMaterialUnidad unidad in unidades)
                    {
                        daLpMaterialEscala.EliminarPorIdLpMaterialUnidad(unidad.idLpMaterialUnidad);
                    }
                }
                daLpMaterial.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daLpMaterial.AbortarTransaccion();
                throw;
            }
            finally
            {
                daLpMaterial.CerrarSesion();
            }
            return true;
        }
    }
}
