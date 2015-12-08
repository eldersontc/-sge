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
    public class blOrdenTrabajo : blBase
    {
        public blOrdenTrabajo(Sesion sesion) { base.sesion = sesion; }

        daOrdenTrabajo daOrdenTrabajo;
        daOrdenTrabajoGrupo daOrdenTrabajoGrupo;
        daOrdenTrabajoItem daOrdenTrabajoItem;
        daOrdenTrabajoServicio daOrdenTrabajoServicio; 

        public IList<OrdenTrabajo> ObtenerTodos()
        {
            IList<OrdenTrabajo> ordenes;
            try
            {
                daOrdenTrabajo = new daOrdenTrabajo();
                daOrdenTrabajo.AbrirSesion();
                ordenes = daOrdenTrabajo.ObtenerTodos();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daOrdenTrabajo.CerrarSesion();
            }
            return ordenes;
        }

        public OrdenTrabajo ObtenerPorId(int idOrdenTrabajo)
        {
            OrdenTrabajo orden;
            try
            {
                daOrdenTrabajo = new daOrdenTrabajo();
                orden = daOrdenTrabajo.ObtenerPorId(idOrdenTrabajo);
                daOrdenTrabajoGrupo = new daOrdenTrabajoGrupo();
                daOrdenTrabajoGrupo.AsignarSesion(daOrdenTrabajo);
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "idOrdenTrabajo", idOrdenTrabajo });
                orden.grupos = daOrdenTrabajoGrupo.ObtenerLista(filtros);
                daOrdenTrabajoItem = new daOrdenTrabajoItem();
                daOrdenTrabajoItem.AsignarSesion(daOrdenTrabajo);
                daOrdenTrabajoServicio = new daOrdenTrabajoServicio();
                daOrdenTrabajoServicio.AsignarSesion(daOrdenTrabajo);
                foreach (OrdenTrabajoGrupo grupo in orden.grupos)
                {
                    filtros = new List<object[]>();
                    filtros.Add(new object[] { "idOrdenTrabajoGrupo", grupo.idOrdenTrabajoGrupo });
                    grupo.items = daOrdenTrabajoItem.ObtenerLista(filtros);
                    foreach (OrdenTrabajoItem item in grupo.items)
                    {
                        filtros = new List<object[]>();
                        filtros.Add(new object[] { "idOrdenTrabajoItem", item.idOrdenTrabajoItem });
                        item.servicios = daOrdenTrabajoServicio.ObtenerLista(filtros);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daOrdenTrabajo.CerrarSesion();
            }
            return orden;
        }

        public bool Agregar(OrdenTrabajo orden)
        {
            try
            {
                daOrdenTrabajo = new daOrdenTrabajo();
                daOrdenTrabajo.IniciarTransaccion();
                daOrdenTrabajo.Agregar(orden);
                daOrdenTrabajoGrupo = new daOrdenTrabajoGrupo();
                daOrdenTrabajoGrupo.AsignarSesion(daOrdenTrabajo);
                daOrdenTrabajoItem = new daOrdenTrabajoItem();
                daOrdenTrabajoItem.AsignarSesion(daOrdenTrabajo);
                daOrdenTrabajoServicio = new daOrdenTrabajoServicio();
                daOrdenTrabajoServicio.AsignarSesion(daOrdenTrabajo);
                foreach (OrdenTrabajoGrupo grupo in orden.grupos)
                {
                    grupo.idOrdenTrabajo = orden.idOrdenTrabajo;
                    daOrdenTrabajoGrupo.Agregar(grupo);
                    foreach (OrdenTrabajoItem item in grupo.items)
                    {
                        item.idOrdenTrabajoGrupo = grupo.idOrdenTrabajoGrupo;
                        daOrdenTrabajoItem.Agregar(item);
                        foreach (OrdenTrabajoServicio servicio in item.servicios)
                        {
                            servicio.idOrdenTrabajoItem = item.idOrdenTrabajoItem;
                            daOrdenTrabajoServicio.Agregar(servicio);
                        }
                    }
                }
                daOrdenTrabajo.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daOrdenTrabajo.AbortarTransaccion();
                throw;
            }
            finally
            {
                daOrdenTrabajo.CerrarSesion();
            }
            return true;
        }

        public bool Actualizar(OrdenTrabajo orden)
        {
            try
            {
                daOrdenTrabajo = new daOrdenTrabajo();
                daOrdenTrabajo.IniciarTransaccion();
                OrdenTrabajo orden_ = daOrdenTrabajo.ObtenerPorId(orden.idOrdenTrabajo);
                orden_.descripcion = orden.descripcion;
                orden_.cliente = orden.cliente;
                orden_.cotizador = orden.cotizador;
                orden_.linea = orden.linea;
                orden_.lpMaterial = orden.lpMaterial;
                orden_.lpServicio = orden.lpServicio;
                orden_.lpMaquina = orden.lpMaquina;
                orden_.moneda = orden.moneda;
                orden_.vendedor = orden.vendedor;
                orden_.contacto = orden.contacto;
                orden_.observacion = orden.observacion;
                daOrdenTrabajoGrupo = new daOrdenTrabajoGrupo();
                daOrdenTrabajoGrupo.AsignarSesion(daOrdenTrabajo);
                daOrdenTrabajoItem = new daOrdenTrabajoItem();
                daOrdenTrabajoItem.AsignarSesion(daOrdenTrabajo);
                daOrdenTrabajoServicio = new daOrdenTrabajoServicio();
                daOrdenTrabajoServicio.AsignarSesion(daOrdenTrabajo);
                foreach (OrdenTrabajoGrupo grupo in orden.grupos)
                {
                    if (grupo.idOrdenTrabajoGrupo == 0)
                    {
                        grupo.idOrdenTrabajo = orden.idOrdenTrabajo;
                        daOrdenTrabajoGrupo.Agregar(grupo);
                        foreach (OrdenTrabajoItem item in grupo.items)
                        {
                            item.idOrdenTrabajoGrupo = grupo.idOrdenTrabajoGrupo;
                            daOrdenTrabajoItem.Agregar(item);
                            foreach (OrdenTrabajoServicio servicio in item.servicios)
                            {
                                servicio.idOrdenTrabajoItem = item.idOrdenTrabajoItem;
                                daOrdenTrabajoServicio.Agregar(servicio);
                            }
                        }
                    }
                    else {
                        OrdenTrabajoGrupo grupo_ = daOrdenTrabajoGrupo.ObtenerPorId(grupo.idOrdenTrabajoGrupo);
                        grupo_.titulo = grupo.titulo;
                        grupo_.cantidad = grupo.cantidad;
                        foreach (OrdenTrabajoItem item in grupo.items)
                        {
                            if (item.idOrdenTrabajoItem == 0)
                            {
                                item.idOrdenTrabajoGrupo = grupo.idOrdenTrabajoGrupo;
                                daOrdenTrabajoItem.Agregar(item);
                                foreach (OrdenTrabajoServicio servicio in item.servicios)
                                {
                                    servicio.idOrdenTrabajoItem = item.idOrdenTrabajoItem;
                                    daOrdenTrabajoServicio.Agregar(servicio);
                                }
                            }
                            else { 
                                OrdenTrabajoItem item_ = daOrdenTrabajoItem.ObtenerPorId(item.idOrdenTrabajoItem);
                                item_.titulo = item.titulo;
                                item_.servicio = item.servicio;
                                item_.maquina = item.maquina;
                                item_.material = item.material;
                                item_.conMdA = item.conMdA;
                                item_.conMdC = item.conMdC;
                                item_.conTyr = item.conTyr;
                                item_.conGrf = item.conGrf;
                                item_.conMat = item.conMat;
                                item_.conSrv = item.conSrv;
                                item_.conFnd = item.conFnd;
                                item_.xMa = item.xMa;
                                item_.yMa = item.yMa;
                                item_.xMc = item.xMc;
                                item_.yMc = item.yMc;
                                item_.tC = item.tC;
                                item_.rC = item.rC;
                                item_.fnd = item.fnd;
                                item_.xFI = item.xFI;
                                item_.yFI = item.yFI;
                                item_.sX = item.sX;
                                item_.sY = item.sY;
                                item_.pliegos = item.pliegos;
                                item_.gp180 = item.gp180;
                                item_.gi180 = item.gi180;
                                item_.metodoImpresion = item.metodoImpresion;
                                item_.scntMat = item.scntMat;
                                item_.cntDem = item.cntDem;
                                item_.cntMat = item.cntMat;
                                item_.cntPrd = item.cntPrd;
                                item_.cantidad = item.cantidad;
                                item_.cntPs = item.cntPs;
                                item_.observacion = item.observacion;
                                foreach (OrdenTrabajoServicio servicio in item.servicios)
                                {
                                    if (servicio.idOrdenTrabajoServicio == 0)
                                    {
                                        servicio.idOrdenTrabajoItem = item.idOrdenTrabajoItem;
                                        daOrdenTrabajoServicio.Agregar(servicio);
                                    }
                                    else {
                                        OrdenTrabajoServicio servicio_ = daOrdenTrabajoServicio.ObtenerPorId(servicio.idOrdenTrabajoServicio);
                                        servicio_.cantidad = servicio.cantidad;
                                        servicio_.unidad = servicio.unidad;
                                    }
                                }
                                foreach (int idServicio in item.idsServicios)
                                {
                                    daOrdenTrabajoServicio.EliminarPorId(idServicio, constantes.esquemas.Ventas);
                                }
                            }
                        }
                        foreach (int idItem in grupo.idsItems)
                        {
                            daOrdenTrabajoItem.EliminarPorId(idItem, constantes.esquemas.Ventas);
                        }
                    }
                }
                foreach (int idGrupo in orden.idsGrupos)
                {
                    daOrdenTrabajoGrupo.EliminarPorId(idGrupo, constantes.esquemas.Ventas);
                    daOrdenTrabajoItem.EliminarPorIdOrdenTrabajoGrupo(idGrupo);
                }
                daOrdenTrabajo.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daOrdenTrabajo.AbortarTransaccion();
                throw;
            }
            finally
            {
                daOrdenTrabajo.CerrarSesion();
            }
            return true;
        }

        public bool Eliminar(int idOrdenTrabajo)
        {
            try
            {
                daOrdenTrabajo = new daOrdenTrabajo();
                daOrdenTrabajo.IniciarTransaccion();
                daOrdenTrabajo.EliminarPorId(idOrdenTrabajo, constantes.esquemas.Produccion);
                daOrdenTrabajoGrupo = new daOrdenTrabajoGrupo();
                daOrdenTrabajoGrupo.AsignarSesion(daOrdenTrabajo);
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "idOrdenTrabajo", idOrdenTrabajo });
                List<OrdenTrabajoGrupo> grupos = daOrdenTrabajoGrupo.ObtenerLista(filtros);
                daOrdenTrabajoGrupo.EliminarPorIdOrdenTrabajo(idOrdenTrabajo);
                daOrdenTrabajoItem = new daOrdenTrabajoItem();
                daOrdenTrabajoItem.AsignarSesion(daOrdenTrabajo);
                foreach (OrdenTrabajoGrupo grupo in grupos)
                {
                    filtros = new List<object[]>();
                    filtros.Add(new object[] { "idOrdenTrabajoGrupo", grupo.idOrdenTrabajoGrupo });
                    List<OrdenTrabajoItem> items = daOrdenTrabajoItem.ObtenerLista(filtros);
                    daOrdenTrabajoItem.EliminarPorIdOrdenTrabajoGrupo(grupo.idOrdenTrabajoGrupo);
                    foreach (OrdenTrabajoItem item in items)
                    {
                        daOrdenTrabajoServicio.EliminarPorIdOrdenTrabajoItem(item.idOrdenTrabajoItem);
                    }
                }
                daOrdenTrabajo.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daOrdenTrabajo.AbortarTransaccion();
                throw;
            }
            finally
            {
                daOrdenTrabajo.CerrarSesion();
            }
            return true;
        }
    }
}
