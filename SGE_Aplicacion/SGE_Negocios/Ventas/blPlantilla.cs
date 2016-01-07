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
        daPlantillaGrupo daPlantillaGrupo;
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
                daPlantillaGrupo = new daPlantillaGrupo();
                daPlantillaGrupo.AsignarSesion(daPlantilla);
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "idPlantilla", idPlantilla });
                plantilla.grupos = daPlantillaGrupo.ObtenerLista(filtros);
                daPlantillaItem = new daPlantillaItem();
                daPlantillaItem.AsignarSesion(daPlantilla);
                foreach (PlantillaGrupo grupo in plantilla.grupos)
                {
                    filtros = new List<object[]>();
                    filtros.Add(new object[] { "idPlantillaGrupo", grupo.idPlantillaGrupo });
                    grupo.items = daPlantillaItem.ObtenerLista(filtros);
                }
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
                daPlantillaGrupo = new daPlantillaGrupo();
                daPlantillaGrupo.AsignarSesion(daPlantilla);
                daPlantillaItem = new daPlantillaItem();
                daPlantillaItem.AsignarSesion(daPlantilla);
                foreach (PlantillaGrupo grupo in plantilla.grupos)
                {
                    grupo.idPlantilla = plantilla.idPlantilla;
                    daPlantillaGrupo.Agregar(grupo);
                    foreach (PlantillaItem item in grupo.items)
                    {
                        item.idPlantillaGrupo = grupo.idPlantillaGrupo;
                        daPlantillaItem.Agregar(item);
                    }
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
                daPlantillaGrupo = new daPlantillaGrupo();
                daPlantillaGrupo.AsignarSesion(daPlantilla);
                daPlantillaItem = new daPlantillaItem();
                daPlantillaItem.AsignarSesion(daPlantilla);
                foreach (PlantillaGrupo grupo in plantilla.grupos)
                {
                    if (grupo.idPlantillaGrupo == 0)
                    {
                        grupo.idPlantilla = plantilla.idPlantilla;
                        daPlantillaGrupo.Agregar(grupo);
                        foreach (PlantillaItem item in grupo.items)
                        {
                            item.idPlantillaGrupo = grupo.idPlantillaGrupo;
                            daPlantillaItem.Agregar(item);
                        }
                    }
                    else {
                        PlantillaGrupo grupo_ = daPlantillaGrupo.ObtenerPorId(grupo.idPlantillaGrupo);
                        grupo_.titulo = grupo.titulo;
                        foreach (PlantillaItem item in grupo.items)
                        {
                            if (item.idPlantillaItem == 0)
                            {
                                item.idPlantillaGrupo = grupo.idPlantillaGrupo;
                                daPlantillaItem.Agregar(item);
                            }
                            else { 
                                PlantillaItem item_ = daPlantillaItem.ObtenerPorId(item.idPlantillaItem);
                                item_.titulo = item.titulo;
                                item_.servicio = item.servicio;
                                item_.material = item.material;
                                item_.flagMA = item.flagMA;
                                item_.flagMC = item.flagMC;
                                item_.flagTYR = item.flagTYR;
                                item_.flagGRF = item.flagGRF;
                                item_.flagMAT = item.flagMAT;
                                item_.flagSRV = item.flagSRV;
                                item_.flagFND = item.flagFND;
                            }
                        }
                        foreach (int idItem in grupo.idsItems)
                        {
                            daPlantillaItem.EliminarPorId(idItem, constantes.esquemas.Ventas);
                        }
                    }
                }
                foreach (int idGrupo in plantilla.idsGrupos)
                {
                    daPlantillaGrupo.EliminarPorId(idGrupo, constantes.esquemas.Ventas);
                    daPlantillaItem.EliminarPorIdPlantillaGrupo(idGrupo);
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
                    daPlantillaGrupo = new daPlantillaGrupo();
                    daPlantillaGrupo.AsignarSesion(daPlantilla);
                    List<object[]> filtros = new List<object[]>();
                    filtros.Add(new object[] { "idPlantilla", id });
                    List<PlantillaGrupo> grupos = daPlantillaGrupo.ObtenerLista(filtros);
                    daPlantillaGrupo.EliminarPorIdPlantilla(id);
                    daPlantillaItem = new daPlantillaItem();
                    daPlantillaItem.AsignarSesion(daPlantilla);
                    foreach (PlantillaGrupo grupo in grupos)
                    {
                        daPlantillaItem.EliminarPorIdPlantillaGrupo(grupo.idPlantillaGrupo);
                    }
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
