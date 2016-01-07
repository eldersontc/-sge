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
    public class blMaterial : blBase
    {
        public blMaterial(Sesion sesion) { base.sesion = sesion; }

        daMaterial daMaterial;
        daMaterialUnidad daMaterialUnidad;
        daMaterialAlmacen daMaterialAlmacen;

        public IList<Material> ObtenerTodos()
        {
            IList<Material> materiales;
            try
            {
                daMaterial = new daMaterial();
                daMaterial.AbrirSesion();
                materiales = daMaterial.ObtenerTodos();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daMaterial.CerrarSesion();
            }
            return materiales;
        }

        public IList<Material> ObtenerActivos()
        {
            IList<Material> materiales;
            try
            {
                daMaterial = new daMaterial();
                daMaterial.AbrirSesion();
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "activo", true });
                materiales = daMaterial.ObtenerLista(filtros);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daMaterial.CerrarSesion();
            }
            return materiales;
        }

        public Material ObtenerPorId(int idMaterial)
        {
            Material material;
            try
            {
                daMaterial = new daMaterial();
                daMaterial.AbrirSesion();
                material = daMaterial.ObtenerPorId(idMaterial);
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "idMaterial", idMaterial });
                daMaterialUnidad = new daMaterialUnidad();
                daMaterialUnidad.AsignarSesion(daMaterial);
                material.unidades = daMaterialUnidad.ObtenerLista(filtros);
                daMaterialAlmacen = new daMaterialAlmacen();
                daMaterialAlmacen.AsignarSesion(daMaterial);
                material.almacenes = daMaterialAlmacen.ObtenerLista(filtros);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daMaterial.CerrarSesion();
            }
            return material;
        }

        public bool Agregar(Material material)
        {
            try
            {
                daMaterial = new daMaterial();
                daMaterial.IniciarTransaccion();
                daMaterial.Agregar(material);
                daMaterialUnidad = new daMaterialUnidad();
                daMaterialUnidad.AsignarSesion(daMaterial);
                foreach (MaterialUnidad unidad in material.unidades)
                {
                    unidad.idMaterial = material.idMaterial;
                    daMaterialUnidad.Agregar(unidad);
                }
                daMaterialAlmacen = new daMaterialAlmacen();
                daMaterialAlmacen.AsignarSesion(daMaterial);
                foreach (MaterialAlmacen almacen in material.almacenes)
                {
                    almacen.idMaterial = material.idMaterial;
                    daMaterialAlmacen.Agregar(almacen);
                }
                daMaterial.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daMaterial.AbortarTransaccion();
                throw;
            }
            finally
            {
                daMaterial.CerrarSesion();
            }
            return true;
        }

        public bool Actualizar(Material material)
        {
            try
            {
                daMaterial = new daMaterial();
                daMaterial.IniciarTransaccion();
                Material material_ = daMaterial.ObtenerPorId(material.idMaterial);
                material_.codigo = material.codigo;
                material_.descripcion = material.descripcion;
                material_.inventarios = material.inventarios;
                material_.compras = material.compras;
                material_.ventas = material.ventas;
                material_.costoUltimaCompra = material.costoUltimaCompra;
                material_.costoPromedio = material.costoPromedio;
                material_.costoReferencia = material.costoReferencia;
                material_.alto = material.alto;
                material_.largo = material.largo;
                material_.unidadBase = material.unidadBase;
                material_.activo = material.activo;
                daMaterialUnidad = new daMaterialUnidad();
                daMaterialUnidad.AsignarSesion(daMaterial);
                foreach (MaterialUnidad unidad in material.unidades)
                {
                    if (unidad.idMaterialUnidad == 0)
                    {
                        unidad.idMaterial = material.idMaterial;
                        daMaterialUnidad.Agregar(unidad);
                    }
                    else 
                    {
                        MaterialUnidad unidad_ = daMaterialUnidad.ObtenerPorId(unidad.idMaterialUnidad);
                        unidad_.unidad = unidad.unidad;
                        unidad_.factor = unidad.factor;
                    }
                }
                foreach (int idUnidad in material.idsUnidades)
                {
                    daMaterialUnidad.EliminarPorId(idUnidad, constantes.esquemas.Inventarios);
                }
                daMaterialAlmacen = new daMaterialAlmacen();
                daMaterialAlmacen.AsignarSesion(daMaterial);
                foreach (MaterialAlmacen almacen in material.almacenes)
                {
                    if (almacen.idMaterialAlmacen == 0)
                    {
                        almacen.idMaterial = material.idMaterial;
                        daMaterialUnidad.Agregar(almacen);
                    }
                }
                foreach (int idAlmacen in material.idsAlmacenes)
                {
                    daMaterialUnidad.EliminarPorId(idAlmacen, constantes.esquemas.Inventarios);
                }
                daMaterial.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daMaterial.AbortarTransaccion();
                throw;
            }
            finally
            {
                daMaterial.CerrarSesion();
            }
            return true;
        }

        public bool Eliminar(int idMaterial)
        {
            try
            {
                daMaterial = new daMaterial();
                daMaterial.IniciarTransaccion();
                daMaterial.EliminarPorId(idMaterial, constantes.esquemas.Administracion);
                daMaterialUnidad = new daMaterialUnidad();
                daMaterialUnidad.AsignarSesion(daMaterial);
                daMaterialUnidad.EliminarPorIdMaterial(idMaterial);
                daMaterialAlmacen = new daMaterialAlmacen();
                daMaterialAlmacen.AsignarSesion(daMaterial);
                daMaterialAlmacen.EliminarPorIdMaterial(idMaterial);
                daMaterial.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daMaterial.AbortarTransaccion();
                throw;
            }
            finally
            {
                daMaterial.CerrarSesion();
            }
            return true;
        }
    }
}
