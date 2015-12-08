using SGE.AccesoDatos.Compras;
using SGE.Entidades.Administracion;
using SGE.Entidades.Compras;
using SGE.Negocios.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Negocios.Compras
{
    public class blProveedor : blBase
    {
        public blProveedor(Sesion sesion) { base.sesion = sesion; }

        daProveedor daProveedor;

        public IList<Proveedor> ObtenerTodos()
        {
            IList<Proveedor> proveedores;
            try
            {
                daProveedor = new daProveedor();
                daProveedor.AbrirSesion();
                proveedores = daProveedor.ObtenerTodos();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daProveedor.CerrarSesion();
            }
            return proveedores;
        }

        public Proveedor ObtenerPorId(int idProveedor)
        {
            Proveedor proveedor;
            try
            {
                daProveedor = new daProveedor();
                proveedor = daProveedor.ObtenerPorId(idProveedor);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daProveedor.CerrarSesion();
            }
            return proveedor;
        }

        public bool Agregar(Proveedor proveedor)
        {
            try
            {
                daProveedor = new daProveedor();
                daProveedor.IniciarTransaccion();
                daProveedor.Agregar(proveedor);
                daProveedor.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daProveedor.AbortarTransaccion();
                throw;
            }
            finally
            {
                daProveedor.CerrarSesion();
            }
            return true;
        }

        public bool Actualizar(Proveedor proveedor)
        {
            try
            {
                daProveedor = new daProveedor();
                daProveedor.IniciarTransaccion();
                Proveedor proveedor_ = daProveedor.ObtenerPorId(proveedor.idProveedor);
                proveedor_.razonSocial = proveedor.razonSocial;
                proveedor_.documentoIdentidad = proveedor.documentoIdentidad;
                proveedor_.nombreComercial = proveedor.nombreComercial;
                proveedor_.telefono = proveedor.telefono;
                proveedor_.correo = proveedor.correo;
                proveedor_.activo = proveedor.activo;
                daProveedor.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daProveedor.AbortarTransaccion();
                throw;
            }
            finally
            {
                daProveedor.CerrarSesion();
            }
            return true;
        }

        public bool Eliminar(int idProveedor)
        {
            try
            {
                daProveedor = new daProveedor();
                daProveedor.IniciarTransaccion();
                daProveedor.EliminarPorId(idProveedor, constantes.esquemas.Compras);
                daProveedor.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daProveedor.AbortarTransaccion();
                throw;
            }
            finally
            {
                daProveedor.CerrarSesion();
            }
            return true;
        }
    }
}
