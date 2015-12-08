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
    public class blMetodoImpresion : blBase
    {
        public blMetodoImpresion(Sesion sesion) { base.sesion = sesion; }

        daMetodoImpresion daMetodoImpresion;

        public IList<MetodoImpresion> ObtenerTodos()
        {
            IList<MetodoImpresion> metodos;
            try
            {
                daMetodoImpresion = new daMetodoImpresion();
                daMetodoImpresion.AbrirSesion();
                metodos = daMetodoImpresion.ObtenerTodos();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daMetodoImpresion.CerrarSesion();
            }
            return metodos;
        }

        public IList<MetodoImpresion> ObtenerActivos()
        {
            IList<MetodoImpresion> metodos;
            try
            {
                daMetodoImpresion = new daMetodoImpresion();
                daMetodoImpresion.AbrirSesion();
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "activo", true });
                metodos = daMetodoImpresion.ObtenerLista(filtros);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daMetodoImpresion.CerrarSesion();
            }
            return metodos;
        }

        public bool Agregar(MetodoImpresion metodo)
        {
            try
            {
                daMetodoImpresion = new daMetodoImpresion();
                daMetodoImpresion.IniciarTransaccion();
                daMetodoImpresion.Agregar(metodo);
                daMetodoImpresion.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daMetodoImpresion.AbortarTransaccion();
                throw;
            }
            finally
            {
                daMetodoImpresion.CerrarSesion();
            }
            return true;
        }

        public bool Actualizar(MetodoImpresion metodo)
        {
            try
            {
                daMetodoImpresion = new daMetodoImpresion();
                daMetodoImpresion.IniciarTransaccion();
                MetodoImpresion almacen_ = daMetodoImpresion.ObtenerPorId(metodo.idMetodoImpresion);
                almacen_.descripcion = metodo.descripcion;
                almacen_.fcPases = metodo.fcPases;
                almacen_.fcCambios = metodo.fcCambios;
                almacen_.fcX = metodo.fcX;
                almacen_.fcY = metodo.fcY;
                almacen_.letras = metodo.letras;
                almacen_.activo = metodo.activo;
                daMetodoImpresion.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daMetodoImpresion.AbortarTransaccion();
                throw;
            }
            finally
            {
                daMetodoImpresion.CerrarSesion();
            }
            return true;
        }

        public bool Eliminar(int idMetodoImpresion)
        {
            try
            {
                daMetodoImpresion = new daMetodoImpresion();
                daMetodoImpresion.IniciarTransaccion();
                daMetodoImpresion.EliminarPorId(idMetodoImpresion, constantes.esquemas.Ventas);
                daMetodoImpresion.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daMetodoImpresion.AbortarTransaccion();
                throw;
            }
            finally
            {
                daMetodoImpresion.CerrarSesion();
            }
            return true;
        }
    }
}
