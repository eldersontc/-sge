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
    public class blIngInventario : blBase
    {
        public blIngInventario(Sesion sesion) { base.sesion = sesion; }

        daIngInventario daIngInventario;
        daIngInventarioItem daIngInventarioItem;

        public IList<IngInventario> ObtenerTodos()
        {
            IList<IngInventario> ingresos;
            try
            {
                daIngInventario = new daIngInventario();
                daIngInventario.AbrirSesion();
                ingresos = daIngInventario.ObtenerTodos();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daIngInventario.CerrarSesion();
            }
            return ingresos;
        }

        public IngInventario ObtenerPorId(int idIngInventario)
        {
            IngInventario ingreso;
            try
            {
                daIngInventario = new daIngInventario();
                daIngInventario.AbrirSesion();
                ingreso = daIngInventario.ObtenerPorId(idIngInventario);
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "idIngInventario", idIngInventario });
                daIngInventarioItem = new daIngInventarioItem();
                daIngInventarioItem.AsignarSesion(daIngInventario);
                ingreso.items = daIngInventarioItem.ObtenerLista(filtros);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daIngInventario.CerrarSesion();
            }
            return ingreso;
        }

        public bool Agregar(IngInventario ingreso)
        {
            try
            {
                daIngInventario = new daIngInventario();
                daIngInventario.IniciarTransaccion();
                daIngInventario.Agregar(ingreso);
                daIngInventarioItem = new daIngInventarioItem();
                daIngInventarioItem.AsignarSesion(daIngInventario);
                foreach (IngInventarioItem item in ingreso.items)
                {
                    item.idIngInventario = ingreso.idIngInventario;
                    daIngInventarioItem.Agregar(item);
                }
                daIngInventario.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daIngInventario.AbortarTransaccion();
                throw;
            }
            finally
            {
                daIngInventario.CerrarSesion();
            }
            return true;
        }

        public bool Eliminar(int idIngInventario)
        {
            try
            {
                daIngInventario = new daIngInventario();
                daIngInventario.IniciarTransaccion();
                daIngInventario.EliminarPorId(idIngInventario, constantes.esquemas.Inventarios);
                daIngInventarioItem = new daIngInventarioItem();
                daIngInventarioItem.AsignarSesion(daIngInventario);
                daIngInventarioItem.EliminarPorIdIngInventario(idIngInventario);
                daIngInventario.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daIngInventario.AbortarTransaccion();
                throw;
            }
            finally
            {
                daIngInventario.CerrarSesion();
            }
            return true;
        }
    }
}
