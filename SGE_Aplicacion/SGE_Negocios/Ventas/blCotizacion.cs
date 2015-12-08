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
    public class blCotizacion : blBase
    {
        public blCotizacion(Sesion sesion) { base.sesion = sesion; }

        daCotizacion daCotizacion;
        daCotizacionGrupo daCotizacionGrupo;
        daCotizacionItem daCotizacionItem;
        daCotizacionServicio daCotizacionServicio; 

        public IList<Cotizacion> ObtenerTodos()
        {
            IList<Cotizacion> cotizaciones;
            try
            {
                daCotizacion = new daCotizacion();
                daCotizacion.AbrirSesion();
                cotizaciones = daCotizacion.ObtenerTodos();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daCotizacion.CerrarSesion();
            }
            return cotizaciones;
        }

        public Cotizacion ObtenerPorId(int idCotizacion)
        {
            Cotizacion cotizacion;
            try
            {
                daCotizacion = new daCotizacion();
                cotizacion = daCotizacion.ObtenerPorId(idCotizacion);
                daCotizacionGrupo = new daCotizacionGrupo();
                daCotizacionGrupo.AsignarSesion(daCotizacion);
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "idCotizacion", idCotizacion });
                cotizacion.grupos = daCotizacionGrupo.ObtenerLista(filtros);
                daCotizacionItem = new daCotizacionItem();
                daCotizacionItem.AsignarSesion(daCotizacion);
                daCotizacionServicio = new daCotizacionServicio();
                daCotizacionServicio.AsignarSesion(daCotizacion);
                foreach (CotizacionGrupo grupo in cotizacion.grupos)
                {
                    filtros = new List<object[]>();
                    filtros.Add(new object[] { "idCotizacionGrupo", grupo.idCotizacionGrupo });
                    grupo.items = daCotizacionItem.ObtenerLista(filtros);
                    foreach (CotizacionItem item in grupo.items)
                    {
                        filtros = new List<object[]>();
                        filtros.Add(new object[] { "idCotizacionItem", item.idCotizacionItem });
                        item.servicios = daCotizacionServicio.ObtenerLista(filtros);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daCotizacion.CerrarSesion();
            }
            return cotizacion;
        }

        public bool Agregar(Cotizacion cotizacion)
        {
            try
            {
                daCotizacion = new daCotizacion();
                daCotizacion.IniciarTransaccion();
                daCotizacion.Agregar(cotizacion);
                daCotizacionGrupo = new daCotizacionGrupo();
                daCotizacionGrupo.AsignarSesion(daCotizacion);
                daCotizacionItem = new daCotizacionItem();
                daCotizacionItem.AsignarSesion(daCotizacion);
                daCotizacionServicio = new daCotizacionServicio();
                daCotizacionServicio.AsignarSesion(daCotizacion);
                foreach (CotizacionGrupo grupo in cotizacion.grupos)
                {
                    grupo.idCotizacion = cotizacion.idCotizacion;
                    daCotizacionGrupo.Agregar(grupo);
                    foreach (CotizacionItem item in grupo.items)
                    {
                        item.idCotizacionGrupo = grupo.idCotizacionGrupo;
                        daCotizacionItem.Agregar(item);
                        foreach (CotizacionServicio servicio in item.servicios)
                        {
                            servicio.idCotizacionItem = item.idCotizacionItem;
                            daCotizacionServicio.Agregar(servicio);
                        }
                    }
                }
                daCotizacion.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daCotizacion.AbortarTransaccion();
                throw;
            }
            finally
            {
                daCotizacion.CerrarSesion();
            }
            return true;
        }

        public bool Actualizar(Cotizacion cotizacion)
        {
            try
            {
                daCotizacion = new daCotizacion();
                daCotizacion.IniciarTransaccion();
                Cotizacion cotizacion_ = daCotizacion.ObtenerPorId(cotizacion.idCotizacion);
                cotizacion_.descripcion = cotizacion.descripcion;
                cotizacion_.cliente = cotizacion.cliente;
                cotizacion_.cotizador = cotizacion.cotizador;
                cotizacion_.linea = cotizacion.linea;
                cotizacion_.lpMaterial = cotizacion.lpMaterial;
                cotizacion_.lpServicio = cotizacion.lpServicio;
                cotizacion_.lpMaquina = cotizacion.lpMaquina;
                cotizacion_.moneda = cotizacion.moneda;
                cotizacion_.vendedor = cotizacion.vendedor;
                cotizacion_.formaPago = cotizacion.formaPago;
                cotizacion_.contacto = cotizacion.contacto;
                cotizacion_.observacion = cotizacion.observacion;
                cotizacion_.pcjUtilidad = cotizacion.pcjUtilidad;
                cotizacion_.monUtilidad = cotizacion.monUtilidad;
                cotizacion_.subTotal = cotizacion.subTotal;
                cotizacion_.total = cotizacion.total;
                daCotizacionGrupo = new daCotizacionGrupo();
                daCotizacionGrupo.AsignarSesion(daCotizacion);
                daCotizacionItem = new daCotizacionItem();
                daCotizacionItem.AsignarSesion(daCotizacion);
                daCotizacionServicio = new daCotizacionServicio();
                daCotizacionServicio.AsignarSesion(daCotizacion);
                foreach (CotizacionGrupo grupo in cotizacion.grupos)
                {
                    if (grupo.idCotizacionGrupo == 0)
                    {
                        grupo.idCotizacion = cotizacion.idCotizacion;
                        daCotizacionGrupo.Agregar(grupo);
                        foreach (CotizacionItem item in grupo.items)
                        {
                            item.idCotizacionGrupo = grupo.idCotizacionGrupo;
                            daCotizacionItem.Agregar(item);
                            foreach (CotizacionServicio servicio in item.servicios)
                            {
                                servicio.idCotizacionItem = item.idCotizacionItem;
                                daCotizacionServicio.Agregar(servicio);
                            }
                        }
                    }
                    else {
                        CotizacionGrupo grupo_ = daCotizacionGrupo.ObtenerPorId(grupo.idCotizacionGrupo);
                        grupo_.titulo = grupo.titulo;
                        grupo_.cantidad = grupo.cantidad;
                        foreach (CotizacionItem item in grupo.items)
                        {
                            if (item.idCotizacionItem == 0)
                            {
                                item.idCotizacionGrupo = grupo.idCotizacionGrupo;
                                daCotizacionItem.Agregar(item);
                                foreach (CotizacionServicio servicio in item.servicios)
                                {
                                    servicio.idCotizacionItem = item.idCotizacionItem;
                                    daCotizacionServicio.Agregar(servicio);
                                }
                            }
                            else { 
                                CotizacionItem item_ = daCotizacionItem.ObtenerPorId(item.idCotizacionItem);
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
                                item_.incPresupuesto = item.incPresupuesto;
                                item_.prcPresupuesto = item.prcPresupuesto;
                                item_.ttlMaq = item.ttlMaq;
                                item_.ttlMat = item.ttlMat;
                                item_.ttlSrv = item.ttlSrv;
                                item_.total = item.total;
                                foreach (CotizacionServicio servicio in item.servicios)
                                {
                                    if (servicio.idCotizacionServicio == 0)
                                    {
                                        servicio.idCotizacionItem = item.idCotizacionItem;
                                        daCotizacionServicio.Agregar(servicio);
                                    }
                                    else {
                                        CotizacionServicio servicio_ = daCotizacionServicio.ObtenerPorId(servicio.idCotizacionServicio);
                                        servicio_.cantidad = servicio.cantidad;
                                        servicio_.precio = servicio.precio;
                                        servicio_.precioM = servicio.precioM;
                                        servicio_.unidad = servicio.unidad;
                                        servicio_.total = servicio.total;
                                    }
                                }
                                foreach (int idServicio in item.idsServicios)
                                {
                                    daCotizacionServicio.EliminarPorId(idServicio, constantes.esquemas.Ventas);
                                }
                            }
                        }
                        foreach (int idItem in grupo.idsItems)
                        {
                            daCotizacionItem.EliminarPorId(idItem, constantes.esquemas.Ventas);
                        }
                    }
                }
                foreach (int idGrupo in cotizacion.idsGrupos)
                {
                    daCotizacionGrupo.EliminarPorId(idGrupo, constantes.esquemas.Ventas);
                    daCotizacionItem.EliminarPorIdCotizacionGrupo(idGrupo);
                }
                daCotizacion.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daCotizacion.AbortarTransaccion();
                throw;
            }
            finally
            {
                daCotizacion.CerrarSesion();
            }
            return true;
        }

        public bool Eliminar(int idCotizacion)
        {
            try
            {
                daCotizacion = new daCotizacion();
                daCotizacion.IniciarTransaccion();
                daCotizacion.EliminarPorId(idCotizacion, constantes.esquemas.Ventas);
                daCotizacionGrupo = new daCotizacionGrupo();
                daCotizacionGrupo.AsignarSesion(daCotizacion);
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "idCotizacion", idCotizacion });
                List<CotizacionGrupo> grupos = daCotizacionGrupo.ObtenerLista(filtros);
                daCotizacionGrupo.EliminarPorIdCotizacion(idCotizacion);
                daCotizacionItem = new daCotizacionItem();
                daCotizacionItem.AsignarSesion(daCotizacion);
                foreach (CotizacionGrupo grupo in grupos)
                {
                    filtros = new List<object[]>();
                    filtros.Add(new object[] { "idCotizacionGrupo", grupo.idCotizacionGrupo });
                    List<CotizacionItem> items = daCotizacionItem.ObtenerLista(filtros);
                    daCotizacionItem.EliminarPorIdCotizacionGrupo(grupo.idCotizacionGrupo);
                    foreach (CotizacionItem item in items)
                    {
                        daCotizacionServicio.EliminarPorIdCotizacionItem(item.idCotizacionItem);
                    }
                }
                daCotizacion.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daCotizacion.AbortarTransaccion();
                throw;
            }
            finally
            {
                daCotizacion.CerrarSesion();
            }
            return true;
        }
    }
}
