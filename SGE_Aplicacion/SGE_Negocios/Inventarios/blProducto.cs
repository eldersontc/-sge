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
    public class blProducto : blBase
    {
        public blProducto(Sesion sesion) { base.sesion = sesion; }

        daProducto daProducto;
        daProductoUnidad daProductoUnidad;
        daProductoAlmacen daProductoAlmacen;

        public IList<Producto> ObtenerTodos()
        {
            IList<Producto> productos;
            try
            {
                daProducto = new daProducto();
                daProducto.AbrirSesion();
                productos = daProducto.ObtenerTodos();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daProducto.CerrarSesion();
            }
            return productos;
        }

        public Producto ObtenerPorId(int idProducto)
        {
            Producto producto;
            try
            {
                daProducto = new daProducto();
                daProducto.AbrirSesion();
                producto = daProducto.ObtenerPorId(idProducto);
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "idProducto", idProducto });
                daProductoUnidad = new daProductoUnidad();
                daProductoUnidad.AsignarSesion(daProducto);
                producto.unidades = daProductoUnidad.ObtenerLista(filtros);
                daProductoAlmacen = new daProductoAlmacen();
                daProductoAlmacen.AsignarSesion(daProducto);
                producto.almacenes = daProductoAlmacen.ObtenerLista(filtros);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daProducto.CerrarSesion();
            }
            return producto;
        }

        public bool Agregar(Producto producto)
        {
            try
            {
                daProducto = new daProducto();
                daProducto.IniciarTransaccion();
                daProducto.Agregar(producto);
                daProductoUnidad = new daProductoUnidad();
                daProductoUnidad.AsignarSesion(daProducto);
                foreach (ProductoUnidad unidad in producto.unidades)
                {
                    unidad.idProducto = producto.idProducto;
                    daProductoUnidad.Agregar(unidad);
                }
                daProductoAlmacen = new daProductoAlmacen();
                daProductoAlmacen.AsignarSesion(daProducto);
                foreach (ProductoAlmacen almacen in producto.almacenes)
                {
                    almacen.idProducto = producto.idProducto;
                    daProductoAlmacen.Agregar(almacen);
                }
                daProducto.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daProducto.AbortarTransaccion();
                throw;
            }
            finally
            {
                daProducto.CerrarSesion();
            }
            return true;
        }

        public bool Actualizar(Producto producto)
        {
            try
            {
                daProducto = new daProducto();
                daProducto.IniciarTransaccion();
                Producto producto_ = daProducto.ObtenerPorId(producto.idProducto);
                producto_.codigo = producto.codigo;
                producto_.descripcion = producto.descripcion;
                producto_.inventarios = producto.inventarios;
                producto_.compras = producto.compras;
                producto_.ventas = producto.ventas;
                producto_.costoUltimaCompra = producto.costoUltimaCompra;
                producto_.costoPromedio = producto.costoPromedio;
                producto_.costoReferencia = producto.costoReferencia;
                producto_.alto = producto.alto;
                producto_.largo = producto.largo;
                producto_.unidadBase = producto.unidadBase;
                producto_.activo = producto.activo;
                daProductoUnidad = new daProductoUnidad();
                daProductoUnidad.AsignarSesion(daProducto);
                foreach (ProductoUnidad unidad in producto.unidades)
                {
                    if (unidad.idProductoUnidad == 0)
                    {
                        unidad.idProducto = producto.idProducto;
                        daProductoUnidad.Agregar(unidad);
                    }
                    else 
                    {
                        ProductoUnidad productoUnidad_ = daProductoUnidad.ObtenerPorId(unidad.idProductoUnidad);
                        productoUnidad_.unidad = unidad.unidad;
                        productoUnidad_.factor = unidad.factor;
                    }
                }
                foreach (int idUnidad in producto.idsUnidades)
                {
                    daProductoUnidad.EliminarPorId(idUnidad, constantes.esquemas.Inventarios);
                }
                daProductoAlmacen = new daProductoAlmacen();
                daProductoAlmacen.AsignarSesion(daProducto);
                foreach (ProductoAlmacen almacen in producto.almacenes)
                {
                    if (almacen.idProductoAlmacen == 0)
                    {
                        almacen.idProducto = producto.idProducto;
                        daProductoUnidad.Agregar(almacen);
                    }
                }
                foreach (int idAlmacen in producto.idsAlmacenes)
                {
                    daProductoUnidad.EliminarPorId(idAlmacen, constantes.esquemas.Inventarios);
                }
                daProducto.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daProducto.AbortarTransaccion();
                throw;
            }
            finally
            {
                daProducto.CerrarSesion();
            }
            return true;
        }

        public bool Eliminar(int idProducto)
        {
            try
            {
                daProducto = new daProducto();
                daProducto.IniciarTransaccion();
                daProducto.EliminarPorId(idProducto, constantes.esquemas.Administracion);
                daProductoUnidad = new daProductoUnidad();
                daProductoUnidad.AsignarSesion(daProducto);
                daProductoUnidad.EliminarPorIdProducto(idProducto);
                daProductoAlmacen = new daProductoAlmacen();
                daProductoAlmacen.AsignarSesion(daProducto);
                daProductoAlmacen.EliminarPorIdProducto(idProducto);
                daProducto.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daProducto.AbortarTransaccion();
                throw;
            }
            finally
            {
                daProducto.CerrarSesion();
            }
            return true;
        }
    }
}
