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
    public class blLpMaquina : blBase
    {
        public blLpMaquina(Sesion sesion) { base.sesion = sesion; }

        daLpMaquina daLpMaquina;
        daLpMaquinaItem daLpMaquinaItem;
        daLpMaquinaEscala daLpMaquinaEscala;

        public IList<LpMaquina> ObtenerTodos()
        {
            IList<LpMaquina> listas;
            try
            {
                daLpMaquina = new daLpMaquina();
                daLpMaquina.AbrirSesion();
                listas = daLpMaquina.ObtenerTodos();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daLpMaquina.CerrarSesion();
            }
            return listas;
        }

        public IList<LpMaquina> ObtenerActivos()
        {
            IList<LpMaquina> listas;
            try
            {
                daLpMaquina = new daLpMaquina();
                daLpMaquina.AbrirSesion();
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "activo", true });
                listas = daLpMaquina.ObtenerLista(filtros);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daLpMaquina.CerrarSesion();
            }
            return listas;
        }

        public LpMaquina ObtenerPorId(int idLpMaquina)
        {
            LpMaquina lista;
            try
            {
                daLpMaquina = new daLpMaquina();
                lista = daLpMaquina.ObtenerPorId(idLpMaquina);
                daLpMaquinaItem = new daLpMaquinaItem();
                daLpMaquinaItem.AsignarSesion(daLpMaquina);
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "idLpMaquina", idLpMaquina });
                lista.items = daLpMaquinaItem.ObtenerLista(filtros);
                daLpMaquinaEscala = new daLpMaquinaEscala();
                daLpMaquinaEscala.AsignarSesion(daLpMaquina);
                foreach (LpMaquinaItem item in lista.items)
                {
                    filtros = new List<object[]>();
                    filtros.Add(new object[] { "idLpMaquinaItem", item.idLpMaquinaItem });
                    item.escalas = daLpMaquinaEscala.ObtenerLista(filtros);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daLpMaquina.CerrarSesion();
            }
            return lista;
        }

        public bool Agregar(LpMaquina lista)
        {
            try
            {
                daLpMaquina = new daLpMaquina();
                daLpMaquina.IniciarTransaccion();
                daLpMaquina.Agregar(lista);
                daLpMaquinaItem = new daLpMaquinaItem();
                daLpMaquinaItem.AsignarSesion(daLpMaquina);
                daLpMaquinaEscala = new daLpMaquinaEscala();
                daLpMaquinaEscala.AsignarSesion(daLpMaquina);
                foreach (LpMaquinaItem item in lista.items)
                {
                    item.idLpMaquina = lista.idLpMaquina;
                    daLpMaquinaItem.Agregar(item);
                    foreach (LpMaquinaEscala escala in item.escalas)
                    {
                        escala.idLpMaquinaItem = item.idLpMaquinaItem;
                        daLpMaquinaEscala.Agregar(escala);
                    }
                }
                daLpMaquina.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daLpMaquina.AbortarTransaccion();
                throw;
            }
            finally
            {
                daLpMaquina.CerrarSesion();
            }
            return true;
        }

        public bool Actualizar(LpMaquina lista)
        {
            try
            {
                daLpMaquina = new daLpMaquina();
                daLpMaquina.IniciarTransaccion();
                LpMaquina lista_ = daLpMaquina.ObtenerPorId(lista.idLpMaquina);
                lista_.descripcion = lista.descripcion;
                lista_.activo = lista.activo;
                daLpMaquinaItem = new daLpMaquinaItem();
                daLpMaquinaItem.AsignarSesion(daLpMaquina);
                daLpMaquinaEscala = new daLpMaquinaEscala();
                daLpMaquinaEscala.AsignarSesion(daLpMaquina);
                foreach (LpMaquinaItem item in lista.items)
                {
                    if (item.idLpMaquinaItem == 0)
                    {
                        item.idLpMaquina = lista.idLpMaquina;
                        daLpMaquinaItem.Agregar(item);
                        foreach (LpMaquinaEscala escala in item.escalas)
                        {
                            escala.idLpMaquinaItem = item.idLpMaquinaItem;
                            daLpMaquinaEscala.Agregar(escala);
                        }
                    }
                    else {
                        LpMaquinaItem item_ = daLpMaquinaItem.ObtenerPorId(item.idLpMaquinaItem);
                        item_.maquina = item.maquina;
                        item_.factor = item.factor;
                        foreach (LpMaquinaEscala escala in item.escalas)
                        {
                            if (escala.idLpMaquinaEscala == 0)
                            {
                                daLpMaquinaEscala.Agregar(escala);
                            }
                            else { 
                                LpMaquinaEscala escala_ = daLpMaquinaEscala.ObtenerPorId(escala.idLpMaquinaEscala);
                                escala_.desde = escala.desde;
                                escala_.hasta = escala.hasta;
                                escala_.precio = escala.precio;
                            }
                        }
                        foreach (int idEscala in item.idsEscalas)
                        {
                            daLpMaquinaEscala.EliminarPorId(idEscala, constantes.esquemas.Ventas);
                        }
                    }
                }
                foreach (int idItem in lista.idsItems)
                {
                    daLpMaquinaItem.EliminarPorId(idItem, constantes.esquemas.Ventas);
                    daLpMaquinaEscala.EliminarPorIdLpMaquinaItem(idItem);
                }
                daLpMaquina.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daLpMaquina.AbortarTransaccion();
                throw;
            }
            finally
            {
                daLpMaquina.CerrarSesion();
            }
            return true;
        }

        public bool Eliminar(int idLpMaquina)
        {
            try
            {
                daLpMaquina = new daLpMaquina();
                daLpMaquina.IniciarTransaccion();
                daLpMaquina.EliminarPorId(idLpMaquina, constantes.esquemas.Ventas);
                daLpMaquinaItem = new daLpMaquinaItem();
                daLpMaquinaItem.AsignarSesion(daLpMaquina);
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "idLpMaquina", idLpMaquina });
                List<LpMaquinaItem> items = daLpMaquinaItem.ObtenerLista(filtros);
                daLpMaquinaItem.EliminarPorIdLpMaquina(idLpMaquina);
                daLpMaquinaEscala = new daLpMaquinaEscala();
                daLpMaquinaEscala.AsignarSesion(daLpMaquina);
                foreach (LpMaquinaItem item in items)
                {
                    daLpMaquinaEscala.EliminarPorIdLpMaquinaItem(item.idLpMaquinaItem);
                }
                daLpMaquina.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daLpMaquina.AbortarTransaccion();
                throw;
            }
            finally
            {
                daLpMaquina.CerrarSesion();
            }
            return true;
        }
    }
}
