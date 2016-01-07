using SGE.AccesoDatos.Administracion;
using SGE.Entidades.Administracion;
using SGE.Negocios.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Negocios.Administracion
{
    public class blNumeracion : blBase
    {
        public blNumeracion(Sesion sesion) { base.sesion = sesion; }

        daNumeracion daNumeracion;

        public IList<Numeracion> ObtenerTodos()
        {
            IList<Numeracion> numeraciones;
            try
            {
                daNumeracion = new daNumeracion();
                daNumeracion.AbrirSesion();
                numeraciones = daNumeracion.ObtenerTodos();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daNumeracion.CerrarSesion();
            }
            return numeraciones;
        }

        public IList<Numeracion> ObtenerActivos()
        {
            IList<Numeracion> numeraciones;
            try
            {
                daNumeracion = new daNumeracion();
                daNumeracion.AbrirSesion();
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "activo", true });
                numeraciones = daNumeracion.ObtenerLista(filtros);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daNumeracion.CerrarSesion();
            }
            return numeraciones;
        }

        public bool Agregar(Numeracion numeracion)
        {
            try
            {
                daNumeracion = new daNumeracion();
                daNumeracion.IniciarTransaccion();
                daNumeracion.Agregar(numeracion);
                daNumeracion.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daNumeracion.AbortarTransaccion();
                throw;
            }
            finally
            {
                daNumeracion.CerrarSesion();
            }
            return true;
        }

        public bool Actualizar(Numeracion numeracion)
        {
            try
            {
                daNumeracion = new daNumeracion();
                daNumeracion.IniciarTransaccion();
                Numeracion numeracion_ = daNumeracion.ObtenerPorId(numeracion.idNumeracion);
                numeracion_.descripcion = numeracion.descripcion;
                numeracion_.documento = numeracion.documento;
                numeracion_.automatico = numeracion.automatico;
                numeracion_.serie = numeracion.serie;
                numeracion_.numeroActual = numeracion.numeroActual;
                numeracion_.longitudNumero = numeracion.longitudNumero;
                numeracion_.impuesto = numeracion.impuesto;
                numeracion_.porcentajeImpuesto = numeracion.porcentajeImpuesto;
                numeracion_.activo = numeracion.activo;
                daNumeracion.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daNumeracion.AbortarTransaccion();
                throw;
            }
            finally
            {
                daNumeracion.CerrarSesion();
            }
            return true;
        }

        public bool Eliminar(int idNumeracion)
        {
            try
            {
                daNumeracion = new daNumeracion();
                daNumeracion.IniciarTransaccion();
                daNumeracion.EliminarPorId(idNumeracion, constantes.esquemas.Administracion);
                daNumeracion.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daNumeracion.AbortarTransaccion();
                throw;
            }
            finally
            {
                daNumeracion.CerrarSesion();
            }
            return true;
        }
    }
}
