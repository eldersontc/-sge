using SGE.AccesoDatos.Inventarios;
using SGE.Entidades.Administracion;
using SGE.Entidades.Inventarios;
using SGE.Negocios.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Negocios.Inventarios
{
    public class blUnidad : blBase
    {
        public blUnidad(Sesion sesion) { base.sesion = sesion; }

        daUnidad daUnidad;

        public IList<Unidad> ObtenerTodos()
        {
            IList<Unidad> unidades;
            try
            {
                daUnidad = new daUnidad();
                daUnidad.AbrirSesion();
                unidades = daUnidad.ObtenerTodos();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daUnidad.CerrarSesion();
            }
            return unidades;
        }

        public IList<Unidad> ObtenerActivos()
        {
            IList<Unidad> unidades;
            try
            {
                daUnidad = new daUnidad();
                daUnidad.AbrirSesion();
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "activo", true });
                unidades = daUnidad.ObtenerLista(filtros);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daUnidad.CerrarSesion();
            }
            return unidades;
        }

        public bool Agregar(Unidad unidad)
        {
            try
            {
                daUnidad = new daUnidad();
                daUnidad.IniciarTransaccion();
                daUnidad.Agregar(unidad);
                daUnidad.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daUnidad.AbortarTransaccion();
                throw;
            }
            finally
            {
                daUnidad.CerrarSesion();
            }
            return true;
        }

        public bool Actualizar(Unidad unidad)
        {
            try
            {
                daUnidad = new daUnidad();
                daUnidad.IniciarTransaccion();
                Unidad unidad_ = daUnidad.ObtenerPorId(unidad.idUnidad);
                unidad_.descripcion = unidad.descripcion;
                unidad_.abreviacion = unidad.abreviacion;
                unidad_.activo = unidad.activo;
                daUnidad.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daUnidad.AbortarTransaccion();
                throw;
            }
            finally
            {
                daUnidad.CerrarSesion();
            }
            return true;
        }

        public bool Eliminar(int idUnidad)
        {
            try
            {
                daUnidad = new daUnidad();
                daUnidad.IniciarTransaccion();
                daUnidad.EliminarPorId(idUnidad, constantes.esquemas.Inventarios);
                daUnidad.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daUnidad.AbortarTransaccion();
                throw;
            }
            finally
            {
                daUnidad.CerrarSesion();
            }
            return true;
        }
    }
}
