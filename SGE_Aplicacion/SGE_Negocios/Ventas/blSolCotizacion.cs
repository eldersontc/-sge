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
    public class blSolCotizacion : blBase
    {
        public blSolCotizacion(Sesion sesion) { base.sesion = sesion; }

        daSolCotizacion daSolCotizacion;
        daSolCotizacionGrupo daSolCotizacionGrupo;
        daSolCotizacionItem daSolCotizacionItem;

        public IList<SolCotizacion> ObtenerTodos()
        {
            IList<SolCotizacion> solicitudes;
            try
            {
                daSolCotizacion = new daSolCotizacion();
                daSolCotizacion.AbrirSesion();
                solicitudes = daSolCotizacion.ObtenerTodos();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daSolCotizacion.CerrarSesion();
            }
            return solicitudes;
        }

        public SolCotizacion ObtenerPorId(int idSolCotizacion)
        {
            SolCotizacion solicitud;
            try
            {
                daSolCotizacion = new daSolCotizacion();
                solicitud = daSolCotizacion.ObtenerPorId(idSolCotizacion);
                daSolCotizacionGrupo = new daSolCotizacionGrupo();
                daSolCotizacionGrupo.AsignarSesion(daSolCotizacion);
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "idSolCotizacion", idSolCotizacion });
                solicitud.grupos = daSolCotizacionGrupo.ObtenerLista(filtros);
                daSolCotizacionItem = new daSolCotizacionItem();
                daSolCotizacionItem.AsignarSesion(daSolCotizacion);
                foreach (SolCotizacionGrupo grupo in solicitud.grupos)
                {
                    filtros = new List<object[]>();
                    filtros.Add(new object[] { "idSolCotizacionGrupo", grupo.idSolCotizacionGrupo });
                    grupo.items = daSolCotizacionItem.ObtenerLista(filtros);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daSolCotizacion.CerrarSesion();
            }
            return solicitud;
        }

        public bool Agregar(SolCotizacion solicitud)
        {
            try
            {
                daSolCotizacion = new daSolCotizacion();
                daSolCotizacion.IniciarTransaccion();
                daSolCotizacion.Agregar(solicitud);
                daSolCotizacionGrupo = new daSolCotizacionGrupo();
                daSolCotizacionGrupo.AsignarSesion(daSolCotizacion);
                daSolCotizacionItem = new daSolCotizacionItem();
                daSolCotizacionItem.AsignarSesion(daSolCotizacion);
                foreach (SolCotizacionGrupo grupo in solicitud.grupos)
                {
                    grupo.idSolCotizacion = solicitud.idSolCotizacion;
                    daSolCotizacionGrupo.Agregar(grupo);
                    foreach (SolCotizacionItem item in grupo.items)
                    {
                        item.idSolCotizacionGrupo = grupo.idSolCotizacionGrupo;
                        daSolCotizacionItem.Agregar(item);
                    }
                }
                daSolCotizacion.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daSolCotizacion.AbortarTransaccion();
                throw;
            }
            finally
            {
                daSolCotizacion.CerrarSesion();
            }
            return true;
        }

        public bool Actualizar(SolCotizacion solicitud)
        {
            try
            {
                daSolCotizacion = new daSolCotizacion();
                daSolCotizacion.IniciarTransaccion();
                SolCotizacion solicitud_ = daSolCotizacion.ObtenerPorId(solicitud.idSolCotizacion);
                solicitud_.descripcion = solicitud.descripcion;
                solicitud_.cliente = solicitud.cliente;
                solicitud_.linea = solicitud.linea;
                solicitud_.lpMaterial = solicitud.lpMaterial;
                solicitud_.lpServicio = solicitud.lpServicio;
                solicitud_.lpMaquina = solicitud.lpMaquina;
                solicitud_.moneda = solicitud.moneda;
                solicitud_.vendedor = solicitud.vendedor;
                solicitud_.formaPago = solicitud.formaPago;
                solicitud_.contacto = solicitud.contacto;
                solicitud_.observacion = solicitud.observacion;
                daSolCotizacionGrupo = new daSolCotizacionGrupo();
                daSolCotizacionGrupo.AsignarSesion(daSolCotizacion);
                daSolCotizacionItem = new daSolCotizacionItem();
                daSolCotizacionItem.AsignarSesion(daSolCotizacion);
                foreach (SolCotizacionGrupo grupo in solicitud.grupos)
                {
                    if (grupo.idSolCotizacionGrupo == 0)
                    {
                        grupo.idSolCotizacion = solicitud.idSolCotizacion;
                        daSolCotizacionGrupo.Agregar(grupo);
                        foreach (SolCotizacionItem item in grupo.items)
                        {
                            item.idSolCotizacionGrupo = grupo.idSolCotizacionGrupo;
                            daSolCotizacionItem.Agregar(item);
                        }
                    }
                    else {
                        SolCotizacionGrupo grupo_ = daSolCotizacionGrupo.ObtenerPorId(grupo.idSolCotizacionGrupo);
                        grupo_.titulo = grupo.titulo;
                        grupo_.cantidad = grupo.cantidad;
                        foreach (SolCotizacionItem item in grupo.items)
                        {
                            if (item.idSolCotizacionItem == 0)
                            {
                                daSolCotizacionItem.Agregar(item);
                            }
                            else { 
                                SolCotizacionItem item_ = daSolCotizacionItem.ObtenerPorId(item.idSolCotizacionItem);
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
                                item_.acabados = item.acabados;
                            }
                        }
                        foreach (int idItem in grupo.idsItems)
                        {
                            daSolCotizacionItem.EliminarPorId(idItem, constantes.esquemas.Ventas);
                        }
                    }
                }
                foreach (int idGrupo in solicitud.idsGrupos)
                {
                    daSolCotizacionGrupo.EliminarPorId(idGrupo, constantes.esquemas.Ventas);
                    daSolCotizacionItem.EliminarPorIdSolCotizacionGrupo(idGrupo);
                }
                daSolCotizacion.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daSolCotizacion.AbortarTransaccion();
                throw;
            }
            finally
            {
                daSolCotizacion.CerrarSesion();
            }
            return true;
        }

        public bool Eliminar(int idSolCotizacion)
        {
            try
            {
                daSolCotizacion = new daSolCotizacion();
                daSolCotizacion.IniciarTransaccion();
                daSolCotizacion.EliminarPorId(idSolCotizacion, constantes.esquemas.Ventas);
                daSolCotizacionGrupo = new daSolCotizacionGrupo();
                daSolCotizacionGrupo.AsignarSesion(daSolCotizacion);
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "idSolCotizacion", idSolCotizacion });
                List<SolCotizacionGrupo> grupos = daSolCotizacionGrupo.ObtenerLista(filtros);
                daSolCotizacionGrupo.EliminarPorIdSolCotizacion(idSolCotizacion);
                daSolCotizacionItem = new daSolCotizacionItem();
                daSolCotizacionItem.AsignarSesion(daSolCotizacion);
                foreach (SolCotizacionGrupo grupo in grupos)
                {
                    daSolCotizacionItem.EliminarPorIdSolCotizacionGrupo(grupo.idSolCotizacionGrupo);
                }
                daSolCotizacion.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daSolCotizacion.AbortarTransaccion();
                throw;
            }
            finally
            {
                daSolCotizacion.CerrarSesion();
            }
            return true;
        }
    }
}
