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
    public class blServicio : blBase
    {
        public blServicio(Sesion sesion) { base.sesion = sesion; }

        daServicio daServicio;
        daServicioUnidad daServicioUnidad;

        public IList<Servicio> ObtenerTodos()
        {
            IList<Servicio> servicios;
            try
            {
                daServicio = new daServicio();
                daServicio.AbrirSesion();
                servicios = daServicio.ObtenerTodos();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daServicio.CerrarSesion();
            }
            return servicios;
        }

        public Servicio ObtenerPorId(int idServicio)
        {
            Servicio servicio;
            try
            {
                daServicio = new daServicio();
                daServicio.AbrirSesion();
                servicio = daServicio.ObtenerPorId(idServicio);
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "idServicio", idServicio });
                daServicioUnidad = new daServicioUnidad();
                daServicioUnidad.AsignarSesion(daServicio);
                servicio.unidades = daServicioUnidad.ObtenerLista(filtros);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daServicio.CerrarSesion();
            }
            return servicio;
        }

        public bool Agregar(Servicio servicio)
        {
            try
            {
                daServicio = new daServicio();
                daServicio.IniciarTransaccion();
                daServicio.Agregar(servicio);
                daServicioUnidad = new daServicioUnidad();
                daServicioUnidad.AsignarSesion(daServicio);
                foreach (ServicioUnidad unidad in servicio.unidades)
                {
                    unidad.idServicio = servicio.idServicio;
                    daServicioUnidad.Agregar(unidad);
                }
                daServicio.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daServicio.AbortarTransaccion();
                throw;
            }
            finally
            {
                daServicio.CerrarSesion();
            }
            return true;
        }

        public bool Actualizar(Servicio servicio)
        {
            try
            {
                daServicio = new daServicio();
                daServicio.IniciarTransaccion();
                Servicio servicio_ = daServicio.ObtenerPorId(servicio.idServicio);
                servicio_.codigo = servicio.codigo;
                servicio_.descripcion = servicio.descripcion;
                servicio_.activo = servicio.activo;
                daServicioUnidad = new daServicioUnidad();
                daServicioUnidad.AsignarSesion(daServicio);
                foreach (ServicioUnidad unidad in servicio.unidades)
                {
                    if (unidad.idServicioUnidad == 0)
                    {
                        unidad.idServicio = servicio.idServicio;
                        daServicioUnidad.Agregar(unidad);
                    }
                    else 
                    {
                        ServicioUnidad unidad_ = daServicioUnidad.ObtenerPorId(unidad.idServicioUnidad);
                        unidad_.unidad = unidad.unidad;
                        unidad_.factor = unidad.factor;
                    }
                }
                foreach (int idUnidad in servicio.idsUnidades)
                {
                    daServicioUnidad.EliminarPorId(idUnidad, constantes.esquemas.Inventarios);
                }
                daServicio.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daServicio.AbortarTransaccion();
                throw;
            }
            finally
            {
                daServicio.CerrarSesion();
            }
            return true;
        }

        public bool Eliminar(int idServicio)
        {
            try
            {
                daServicio = new daServicio();
                daServicio.IniciarTransaccion();
                daServicio.EliminarPorId(idServicio, constantes.esquemas.Administracion);
                daServicioUnidad = new daServicioUnidad();
                daServicioUnidad.AsignarSesion(daServicio);
                daServicioUnidad.EliminarPorIdServicio(idServicio);
                daServicio.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daServicio.AbortarTransaccion();
                throw;
            }
            finally
            {
                daServicio.CerrarSesion();
            }
            return true;
        }
    }
}
