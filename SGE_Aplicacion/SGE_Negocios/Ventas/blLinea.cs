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
    public class blLinea : blBase
    {
        public blLinea(Sesion sesion) { base.sesion = sesion; }

        daLinea daLinea;

        public IList<Linea> ObtenerTodos()
        {
            IList<Linea> lineas;
            try
            {
                daLinea = new daLinea();
                daLinea.AbrirSesion();
                lineas = daLinea.ObtenerTodos();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daLinea.CerrarSesion();
            }
            return lineas;
        }

        public IList<Linea> ObtenerActivos()
        {
            IList<Linea> lineas;
            try
            {
                daLinea = new daLinea();
                daLinea.AbrirSesion();
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "activo", true });
                lineas = daLinea.ObtenerLista(filtros);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daLinea.CerrarSesion();
            }
            return lineas;
        }

        public bool Agregar(Linea linea)
        {
            try
            {
                daLinea = new daLinea();
                daLinea.IniciarTransaccion();
                daLinea.Agregar(linea);
                daLinea.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daLinea.AbortarTransaccion();
                throw;
            }
            finally
            {
                daLinea.CerrarSesion();
            }
            return true;
        }

        public bool Actualizar(Linea linea)
        {
            try
            {
                daLinea = new daLinea();
                daLinea.IniciarTransaccion();
                Linea almacen_ = daLinea.ObtenerPorId(linea.idLinea);
                almacen_.descripcion = linea.descripcion;
                almacen_.activo = linea.activo;
                daLinea.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daLinea.AbortarTransaccion();
                throw;
            }
            finally
            {
                daLinea.CerrarSesion();
            }
            return true;
        }

        public bool Eliminar(int idLinea)
        {
            try
            {
                daLinea = new daLinea();
                daLinea.IniciarTransaccion();
                daLinea.EliminarPorId(idLinea, constantes.esquemas.Ventas);
                daLinea.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daLinea.AbortarTransaccion();
                throw;
            }
            finally
            {
                daLinea.CerrarSesion();
            }
            return true;
        }
    }
}
