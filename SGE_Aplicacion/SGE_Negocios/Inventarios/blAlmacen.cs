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
    public class blAlmacen : blBase
    {
        public blAlmacen(Sesion sesion) { base.sesion = sesion; }

        daAlmacen daAlmacen;

        public IList<Almacen> ObtenerTodos()
        {
            IList<Almacen> almacenes;
            try
            {
                daAlmacen = new daAlmacen();
                daAlmacen.AbrirSesion();
                almacenes = daAlmacen.ObtenerTodos();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daAlmacen.CerrarSesion();
            }
            return almacenes;
        }

        public IList<Almacen> ObtenerActivos()
        {
            IList<Almacen> almacenes;
            try
            {
                daAlmacen = new daAlmacen();
                daAlmacen.AbrirSesion();
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "activo", true });
                almacenes = daAlmacen.ObtenerLista(filtros);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daAlmacen.CerrarSesion();
            }
            return almacenes;
        }

        public bool Agregar(Almacen almacen)
        {
            try
            {
                daAlmacen = new daAlmacen();
                daAlmacen.IniciarTransaccion();
                daAlmacen.Agregar(almacen);
                daAlmacen.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daAlmacen.AbortarTransaccion();
                throw;
            }
            finally
            {
                daAlmacen.CerrarSesion();
            }
            return true;
        }

        public bool Actualizar(Almacen almacen)
        {
            try
            {
                daAlmacen = new daAlmacen();
                daAlmacen.IniciarTransaccion();
                Almacen almacen_ = daAlmacen.ObtenerPorId(almacen.idAlmacen);
                almacen_.codigo = almacen.codigo;
                almacen_.descripcion = almacen.descripcion;
                almacen_.activo = almacen.activo;
                daAlmacen.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daAlmacen.AbortarTransaccion();
                throw;
            }
            finally
            {
                daAlmacen.CerrarSesion();
            }
            return true;
        }

        public bool Eliminar(int idAlmacen)
        {
            try
            {
                daAlmacen = new daAlmacen();
                daAlmacen.IniciarTransaccion();
                daAlmacen.EliminarPorId(idAlmacen, constantes.esquemas.Inventarios);
                daAlmacen.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daAlmacen.AbortarTransaccion();
                throw;
            }
            finally
            {
                daAlmacen.CerrarSesion();
            }
            return true;
        }
    }
}
