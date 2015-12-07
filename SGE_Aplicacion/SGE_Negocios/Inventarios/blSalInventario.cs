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
    public class blSalInventario : blBase
    {
        public blSalInventario(Sesion sesion) { base.sesion = sesion; }

        daSalInventario daSalInventario;
        daSalInventarioItem daSalInventarioItem;

        public IList<SalInventario> ObtenerTodos()
        {
            IList<SalInventario> salidas;
            try
            {
                daSalInventario = new daSalInventario();
                daSalInventario.AbrirSesion();
                salidas = daSalInventario.ObtenerTodos();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daSalInventario.CerrarSesion();
            }
            return salidas;
        }

        public SalInventario ObtenerPorId(int idSalInventario)
        {
            SalInventario salida;
            try
            {
                daSalInventario = new daSalInventario();
                daSalInventario.AbrirSesion();
                salida = daSalInventario.ObtenerPorId(idSalInventario);
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "idSalInventario", idSalInventario });
                daSalInventarioItem = new daSalInventarioItem();
                daSalInventarioItem.AsignarSesion(daSalInventario);
                salida.items = daSalInventarioItem.ObtenerLista(filtros);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daSalInventario.CerrarSesion();
            }
            return salida;
        }

        public bool Agregar(SalInventario salida)
        {
            try
            {
                daSalInventario = new daSalInventario();
                daSalInventario.IniciarTransaccion();
                daSalInventario.Agregar(salida);
                daSalInventarioItem = new daSalInventarioItem();
                daSalInventarioItem.AsignarSesion(daSalInventario);
                foreach (SalInventarioItem item in salida.items)
                {
                    item.idSalInventario = salida.idSalInventario;
                    daSalInventarioItem.Agregar(item);
                }
                daSalInventario.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daSalInventario.AbortarTransaccion();
                throw;
            }
            finally
            {
                daSalInventario.CerrarSesion();
            }
            return true;
        }

        public bool Eliminar(int idSalInventario)
        {
            try
            {
                daSalInventario = new daSalInventario();
                daSalInventario.IniciarTransaccion();
                daSalInventario.EliminarPorId(idSalInventario, constantes.esquemas.Inventarios);
                daSalInventarioItem = new daSalInventarioItem();
                daSalInventarioItem.AsignarSesion(daSalInventario);
                daSalInventarioItem.EliminarPorIdSalInventario(idSalInventario);
                daSalInventario.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daSalInventario.AbortarTransaccion();
                throw;
            }
            finally
            {
                daSalInventario.CerrarSesion();
            }
            return true;
        }
    }
}
