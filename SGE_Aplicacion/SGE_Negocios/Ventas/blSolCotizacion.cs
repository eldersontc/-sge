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

        public object[] ObtenerTodos(Paginacion paginacion, Orden orden)
        {
            object[] datos;
            try
            {
                daSolCotizacion = new daSolCotizacion();
                daSolCotizacion.AbrirSesion();
                datos = daSolCotizacion.ObtenerPaginacion(new List<object[]>(), paginacion, orden);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daSolCotizacion.CerrarSesion();
            }
            return datos;
        }

        public SolCotizacion ObtenerPorId(int idSolCotizacion)
        {
            SolCotizacion solicitud;
            try
            {
                daSolCotizacion = new daSolCotizacion();
                daSolCotizacion.AbrirSesion();
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
                if (string.IsNullOrEmpty(solicitud.numero)) {
                    solicitud.numero = generarNumeracion(daSolCotizacion, solicitud.numeracion.idNumeracion);    
                }
                solicitud.fechaCreacion = DateTime.Now;
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
                                item.idSolCotizacionGrupo = grupo.idSolCotizacionGrupo;
                                daSolCotizacionItem.Agregar(item);
                            }
                            else { 
                                SolCotizacionItem item_ = daSolCotizacionItem.ObtenerPorId(item.idSolCotizacionItem);
                                item_.titulo = item.titulo;
                                item_.servicio = item.servicio;
                                item_.maquina = item.maquina;
                                item_.material = item.material;
                                item_.flagMA = item.flagMA;
                                item_.flagMC = item.flagMC;
                                item_.flagTYR = item.flagTYR;
                                item_.flagGRF = item.flagGRF;
                                item_.flagMAT = item.flagMAT;
                                item_.flagSRV = item.flagSRV;
                                item_.flagFND = item.flagFND;
                                item_.valXMA = item.valXMA;
                                item_.valYMA = item.valYMA;
                                item_.valXMC = item.valXMC;
                                item_.valYMC = item.valYMC;
                                item_.valTC = item.valTC;
                                item_.valRT = item.valRT;
                                item_.valFND = item.valFND;
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

        public bool Eliminar(List<int> ids)
        {
            try
            {
                daSolCotizacion = new daSolCotizacion();
                daSolCotizacion.IniciarTransaccion();
                foreach (int idSolCotizacion in ids)
                {
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
