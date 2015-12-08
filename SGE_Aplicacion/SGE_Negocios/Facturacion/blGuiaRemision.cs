using SGE.AccesoDatos.Facturacion;
using SGE.Entidades.Administracion;
using SGE.Entidades.Facturacion;
using SGE.Negocios.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Negocios.GuiaRemisioncion
{
    public class blGuiaRemision : blBase
    {
        public blGuiaRemision(Sesion sesion) { base.sesion = sesion; }

        daGuiaRemision daGuiaRemision;
        daGuiaRemisionItem daGuiaRemisionItem;

        public IList<GuiaRemision> ObtenerTodos()
        {
            IList<GuiaRemision> guias;
            try
            {
                daGuiaRemision = new daGuiaRemision();
                daGuiaRemision.AbrirSesion();
                guias = daGuiaRemision.ObtenerTodos();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daGuiaRemision.CerrarSesion();
            }
            return guias;
        }

        public GuiaRemision ObtenerPorId(int idGuiaRemision)
        {
            GuiaRemision guia;
            try
            {
                daGuiaRemision = new daGuiaRemision();
                daGuiaRemision.AbrirSesion();
                guia = daGuiaRemision.ObtenerPorId(idGuiaRemision);
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "idGuiaRemision", idGuiaRemision });
                daGuiaRemisionItem = new daGuiaRemisionItem();
                daGuiaRemisionItem.AsignarSesion(daGuiaRemision);
                guia.items = daGuiaRemisionItem.ObtenerLista(filtros);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daGuiaRemision.CerrarSesion();
            }
            return guia;
        }

        public bool Agregar(GuiaRemision guia)
        {
            try
            {
                daGuiaRemision = new daGuiaRemision();
                daGuiaRemision.IniciarTransaccion();
                daGuiaRemision.Agregar(guia);
                daGuiaRemisionItem = new daGuiaRemisionItem();
                daGuiaRemisionItem.AsignarSesion(daGuiaRemision);
                foreach (GuiaRemisionItem item in guia.items)
                {
                    item.idGuiaRemision = guia.idGuiaRemision;
                    daGuiaRemisionItem.Agregar(item);
                }
                daGuiaRemision.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daGuiaRemision.AbortarTransaccion();
                throw;
            }
            finally
            {
                daGuiaRemision.CerrarSesion();
            }
            return true;
        }

        public bool Eliminar(int idGuiaRemision)
        {
            try
            {
                daGuiaRemision = new daGuiaRemision();
                daGuiaRemision.IniciarTransaccion();
                daGuiaRemision.EliminarPorId(idGuiaRemision, constantes.esquemas.Facturacion);
                daGuiaRemisionItem = new daGuiaRemisionItem();
                daGuiaRemisionItem.AsignarSesion(daGuiaRemision);
                daGuiaRemisionItem.EliminarPorIdGuiaRemision(idGuiaRemision);
                daGuiaRemision.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daGuiaRemision.AbortarTransaccion();
                throw;
            }
            finally
            {
                daGuiaRemision.CerrarSesion();
            }
            return true;
        }
    }
}
